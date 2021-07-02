using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Haxpe.Coupons;
using Haxpe.Customers;
using Haxpe.Infrastructure;
using Haxpe.Orders;
using Haxpe.Partners;
using Haxpe.Roles;
using Haxpe.Taxes;
using Haxpe.Users;
using Haxpe.V1.Events;
using Haxpe.Workers;

namespace Haxpe.V1.Orders
{
    public class OrderV1Service: ApplicationService, IOrderV1Service
    {
        private readonly IRepository<Order, Guid> orderRepository;
        private readonly IRepository<Worker, Guid> workerRepository;
        private readonly IRepository<Partner, Guid> partnerRepository;
        private readonly IRepository<Customer, Guid> customerRepository;
        private readonly IRepository<Haxpe.ServiceTypes.ServiceType, int> serviceTypeRepository;
        private readonly IRepository<Coupon, Guid> couponRepository;
        private readonly ITaxProvider taxProvider;
        private readonly ICurrentUserService currentUserService;
        private readonly IEventEmitter eventEmitter;

        public OrderV1Service(
            IRepository<Order, Guid> orderRepository,
            IRepository<Worker, Guid> workerRepository,
            IRepository<Partner, Guid> partnerRepository,
            IRepository<Customer, Guid> customerRepository,
            IRepository<Haxpe.ServiceTypes.ServiceType, int> serviceTypeRepository,
            IRepository<Coupon, Guid> couponRepository,
            ITaxProvider taxProvider,
            ICurrentUserService currentUserService,
            IEventEmitter eventEmitter,
            IMapper mapper) : base(mapper)
        {
            this.orderRepository = orderRepository;
            this.workerRepository = workerRepository;
            this.partnerRepository = partnerRepository;
            this.customerRepository = customerRepository;
            this.serviceTypeRepository = serviceTypeRepository;
            this.taxProvider = taxProvider;
            this.currentUserService = currentUserService;
            this.couponRepository = couponRepository;
            this.eventEmitter = eventEmitter;
        }

        public async Task<OrderV1Dto> ApplyCoupon(Guid id, ApplyCouponDto model)
        {
            var order = await orderRepository.FindAsync(id);
            if (order == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.NotFound);
            }

            var coupon = await this.couponRepository.FindAsync(x => x.Code == model.Code);
            if (coupon == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.CouponNotFound);
            }
            order.ApplyCoupon(coupon);

            var res = this.mapper.Map<OrderV1Dto>(order);
            await this.OrderChangedNotify(res);
            return res;
        }

        public async Task<OrderV1Dto> AssignWorker(Guid id, Guid workerId)
        {
            var order = await orderRepository.FindAsync(id);
            if (order == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderNotFound);
            }

            var userId = await this.currentUserService.GetCurrentUserIdAsync();
            var userRoles = await this.currentUserService.GetCurrentUserRolesAsync();
            var worker = await workerRepository.FindAsync(workerId);

            if (userRoles.Contains(RoleConstants.Partner))
            {
                var partner = await partnerRepository.FindAsync(x => x.OwnerUserId == userId);
                if (worker.PartnerId != partner.Id)
                {
                    throw new BusinessException(HaxpeDomainErrorCodes.OrderAssignWorkerFromOtherPartner);
                }
            }
            order.AssignWorker(worker);

            var res = this.mapper.Map<OrderV1Dto>(order);
            await this.OrderChangedNotify(res);
            return res;
        }

        public async Task<OrderV1Dto> CancelOrder(Guid id)
        {
            var order = await orderRepository.FindAsync(id);
            if (order == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderNotFound);
            }

            var userId = await this.currentUserService.GetCurrentUserIdAsync();
            var customer = await this.customerRepository.FindAsync(x => x.UserId == userId);
            if (order.CustomerId != customer.Id)
            {
                throw new UnauthorizedAccessException();
            }

            order.Cancel();

            var res = this.mapper.Map<OrderV1Dto>(order);
            await this.OrderChangedNotify(res);
            return res;
        }

        public async Task<OrderV1Dto> CompleteJob(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderV1Dto> ConfirmOrder(Guid id)
        {
            var order = await orderRepository.FindAsync(id);
            if (order == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderNotFound);
            }

            var userId = await this.currentUserService.GetCurrentUserIdAsync();
            var customer = await this.customerRepository.FindAsync(x => x.UserId == userId);
            if (order.CustomerId != customer.Id)
            {
                throw new UnauthorizedAccessException();
            }

            order.Confirm();
            var res = this.mapper.Map<OrderV1Dto>(order);
            await this.OrderChangedNotify(res);
            return res;
        }

        public async Task<OrderV1Dto> CreateOrder(CreateOrderV1Dto orderRequest)
        {
            var serviceType = await serviceTypeRepository.FindAsync(orderRequest.ServiceTypeId);
            var order = new Order(Guid.NewGuid(),
                orderRequest.CustomerId,
                orderRequest.AddressId,
                DateTimeOffset.Now,
                serviceType.IndustryId,
                serviceType.Id,
                orderRequest.PaymentMethod,
                taxProvider.GetTax(),
                OrderStatus.Draft);

            await orderRepository.CreateAsync(order);
            var res = this.mapper.Map<OrderV1Dto>(order);
            await this.OrderChangedNotify(res);
            return res;
        }

        public async Task<IReadOnlyCollection<OrderV1Dto>> GetListAsync(OrderListRequestV1Dto request)
        {
            var query = PredicateBuilder.True<Order>();
            if(request.CustomerId.HasValue)
            {
                query = query.And(order => order.CustomerId == request.CustomerId.Value);
            }
            if (request.WorkerId.HasValue)
            {
                query = query.And(order => order.WorkerId == request.WorkerId.Value);
            }
            if (request.Status.HasValue)
            {
                query = query.And(order => order.OrderStatus == request.Status.Value);
            }
            if (request.IsActive.HasValue)
            {
                query = query.And(this.IsActive());
            }

            var res = await this.orderRepository.GetListAsync(query);
            return this.mapper.Map<IReadOnlyCollection<OrderV1Dto>>(res);
        }

        private Expression<Func<Order, bool>> IsActive()
        {
            var activeOrderStatus = Order.GetActiveOrderStatus();
            return PredicateBuilder.Create<Order>(order => activeOrderStatus.Contains(order.OrderStatus));
        }

        public async Task<OrderV1Dto> GetOrder(Guid id)
        {
            var order = await orderRepository.FindAsync(id);
            if(order == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderNotFound);
            }

            var userId = await this.currentUserService.GetCurrentUserIdAsync();
            var userRoles = await this.currentUserService.GetCurrentUserRolesAsync();

            if (userRoles.Contains(RoleConstants.Admin))
            {
                return this.mapper.Map<Order, OrderV1Dto>(order);
            }
            else if (userRoles.Contains(RoleConstants.Customer))
            {
                var customer = await this.customerRepository.FindAsync(x => x.UserId == userId);
                if (order.CustomerId == customer.Id)
                {
                    return this.mapper.Map<Order, OrderV1Dto>(order);
                }
            }
            else if (userRoles.Contains(RoleConstants.Worker))
            {
                if(order.OrderStatus == OrderStatus.Created)
                {
                    return base.mapper.Map<Order, OrderV1Dto>(order);
                }

                var worker = await workerRepository.FindAsync(x => x.UserId == userId);
                if (order.WorkerId == worker.Id)
                {
                    return this.mapper.Map<Order, OrderV1Dto>(order);
                }
            }
            else if (userRoles.Contains(RoleConstants.Partner))
            {
                var partner = await partnerRepository.FindAsync(x => x.OwnerUserId == userId);
                if (order.PartnerId == partner.Id)
                {
                    return this.mapper.Map<Order, OrderV1Dto>(order);
                }
            }
            throw new UnauthorizedAccessException();
        }

        public Task<OrderV1Dto> StartJob(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderV1Dto> UpdateOrder(CreateOrderV1Dto order)
        {
            throw new NotImplementedException();
        }

        private async Task OrderChangedNotify(OrderV1Dto dto)
        {
            var customer = await this.customerRepository.FindAsync(dto.CustomerId);
            await this.eventEmitter.SendEvent(customer.UserId, new OrderChangedEvent() { Payload = dto });

            if (dto.WorkerId.HasValue)
            {
                var worker = await this.workerRepository.FindAsync(dto.WorkerId.Value);
                await this.eventEmitter.SendEvent(worker.UserId, new OrderChangedEvent() { Payload = dto });
            }
        }
    }
}