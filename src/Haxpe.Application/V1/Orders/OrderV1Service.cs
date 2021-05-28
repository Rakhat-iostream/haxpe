using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Haxpe.Customers;
using Haxpe.Infrastructure;
using Haxpe.Orders;
using Haxpe.Partners;
using Haxpe.Roles;
using Haxpe.Taxes;
using Haxpe.Users;
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
        private readonly ITaxProvider taxProvider;
        private readonly ICurrentUserService currentUserService;

        public OrderV1Service(
            IRepository<Order, Guid> orderRepository,
            IRepository<Worker, Guid> workerRepository,
            IRepository<Partner, Guid> partnerRepository,
            IRepository<Customer, Guid> customerRepository,
            IRepository<Haxpe.ServiceTypes.ServiceType, int> serviceTypeRepository,
            ITaxProvider taxProvider,
            ICurrentUserService currentUserService,
            IMapper mapper
        ) : base(mapper)
        {
            this.orderRepository = orderRepository;
            this.workerRepository = workerRepository;
            this.partnerRepository = partnerRepository;
            this.customerRepository = customerRepository;
            this.serviceTypeRepository = serviceTypeRepository;
            this.taxProvider = taxProvider;
            this.currentUserService = currentUserService;
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
            return this.mapper.Map<Order, OrderV1Dto>(order);
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
            if (order.CustomerId == customer.Id)
            {
                throw new UnauthorizedAccessException();
            }

            order.Cancel();
            return this.mapper.Map<Order, OrderV1Dto>(order);
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
            if (order.CustomerId == customer.Id)
            {
                throw new UnauthorizedAccessException();
            }

            order.Confirm();
            return this.mapper.Map<Order, OrderV1Dto>(order);
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
            return this.mapper.Map<Order, OrderV1Dto>(order);
        }

        public async Task<PagedResultDto<OrderV1Dto>> Get(OrderListRequestV1Dto request)
        {
            throw new NotImplementedException();
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
        /*
public async Task<OrderV1Dto> CreateOrder(CreateOrderV1Dto orderRequest)
{
var serviceType = await _serviceTypeRepository.FindAsync(orderRequest.ServiceTypeId);
var order = new Order(Guid.NewGuid(),
orderRequest.CustomerId,
orderRequest.AddressId,
DateTimeOffset.Now,
serviceType.IndustryId,
serviceType.Id,
Enum.Parse<PaymentMethod>(orderRequest.PaymentMethod),
_taxProvider.GetTax(),
OrderStatus.Created);

await _orderRepository.CreateAsync(order);
return this.mapper.Map<Order, OrderV1Dto>(order);
}

public async Task<OrderV1Dto> GetOrder(Guid id)
{
var order = await _orderRepository.FindAsync(id);

if (order.OrderStatus == OrderStatus.Created)
{
return this.mapper.Map<Order, OrderV1Dto>(order);
}

if (CurrentUser.IsInRole(RoleConstants.Admin))
{
return this.mapper.Map<Order, OrderV1Dto>(order);
}
else if (CurrentUser.IsInRole(RoleConstants.Customer))
{
if (order.CustomerId == CurrentUser.Id)
{
  return this.mapper.Map<Order, OrderV1Dto>(order);
}
}
else if (CurrentUser.IsInRole(RoleConstants.Worker))
{
var worker = await _workerRepository.FindAsync(x => x.UserId == CurrentUser.Id);
if (order.WorkerId == worker.Id)
{
  return this.mapper.Map<Order, OrderV1Dto>(order);
}
}
else if (CurrentUser.IsInRole(RoleConstants.Partner))
{
var partner = await _partnerRepository.FindAsync(x => x.OwnerUserId == CurrentUser.Id);
if (order.PartnerId == partner.Id)
{
  return this.mapper.Map<Order, OrderV1Dto>(order);
}
}
throw new BusinessException("Access denied");
}

public Task<PagedResultDto<OrderV1Dto>> Get(OrderListRequestV1Dto request)
{
throw new NotImplementedException();
}

public async Task<OrderV1Dto> AssignWorker(Guid id, Guid workerId)
{
CheckPermission(RoleConstants.Admin, RoleConstants.Partner, RoleConstants.Worker);
var order = await _orderRepository.FindAsync(id);
var worker = await _workerRepository.FindAsync(workerId);

if (CurrentUser.IsInRole(RoleConstants.Partner))
{
var partner = await _partnerRepository.FindAsync(x => x.OwnerUserId == CurrentUser.Id);
if (worker.PartnerId != partner.Id)
{
  throw new BusinessException("Can't assign a worker from other partner");
}
}
order.AssignWorker(worker);
await CurrentUnitOfWork.SaveChangesAsync();

return this.mapper.Map<Order, OrderV1Dto>(order);
}

public async Task<OrderV1Dto> StartJob(Guid id)
{
CheckPermission(RoleConstants.Worker);
var order = await _orderRepository.FindAsync(id);
var worker = await _workerRepository.FindAsync(x => x.UserId == CurrentUser.Id);
if (order.WorkerId != worker.Id)
{
throw new BusinessException("Access denied");
}
order.StartJob();
await CurrentUnitOfWork.SaveChangesAsync();

return this.mapper.Map<Order, OrderV1Dto>(order);
}

public async Task<OrderV1Dto> CompleteJob(Guid id)
{
CheckPermission(RoleConstants.Worker);
var order = await _orderRepository.FindAsync(id);
var worker = await _workerRepository.FindAsync(x => x.UserId == CurrentUser.Id);
if (order.WorkerId != worker.Id)
{
throw new BusinessException("Access denied");
}
order.CompleteJob();
await CurrentUnitOfWork.SaveChangesAsync();

return this.mapper.Map<Order, OrderV1Dto>(order);
}

private void CheckPermission(params string[] roles)
{
if (!roles.Any(r => CurrentUser.IsInRole(r)))
{
throw new BusinessException("Access denied");
}
}
*/
    }
}