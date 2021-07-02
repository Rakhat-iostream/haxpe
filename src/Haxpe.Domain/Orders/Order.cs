using System;
using Haxpe.Workers;
using Haxpe.Infrastructure;
using Haxpe.Coupons;

namespace Haxpe.Orders
{
    public class Order: AggregateRoot<Guid>
    {
        public Order(Guid id,
            Guid customerId,
            Guid addressId,
            DateTimeOffset orderDate,
            int industryId,
            int serviceTypeId,
            PaymentMethod paymentMethod,
            decimal tax,
            OrderStatus orderStatus) : base(id)
        {
            CustomerId = customerId;
            AddressId = addressId;
            OrderDate = orderDate;
            IndustryId = industryId;
            ServiceTypeId = serviceTypeId;
            PaymentMethod = paymentMethod;
            Tax = tax;
            OrderStatus = orderStatus;
            CreationDate = DateTime.UtcNow;
        }
        
        private Order() { }

        public Guid? PartnerId { get; private set; }
        
        public Guid? WorkerId { get; private set; }
        
        public Guid CustomerId { get; private set; }
        
        public Guid AddressId { get; private set; }
        
        public DateTimeOffset OrderDate { get; private set; }
        
        public DateTimeOffset StartDate { get; private set; }
        
        public DateTimeOffset CompletedDate { get; private set; }
        
        public int IndustryId { get; private set; }
        
        public int ServiceTypeId { get; private set; }
        
        public PaymentMethod PaymentMethod { get; private set; }

        public decimal? NetAmount { get; private set; }

        public decimal Tax { get; private set; }
        
        public decimal? TotalAmount { get; private set; }
        
        public float? Rating { get; private set; }
        
        public string? Comment { get; private set; }
        
        public OrderStatus OrderStatus { get; private set; }

        public Guid? CouponId { get; set; }

        public string? CouponCode { get; set; }

        public void AssignWorker(Worker worker)
        {
            if (OrderStatus != OrderStatus.Created)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
            
            WorkerId = worker.Id;
            PartnerId = worker.PartnerId;
            OrderStatus = OrderStatus.WorkerFound;
        }
        
        public void StartJob()
        {
            if (OrderStatus != OrderStatus.WorkerFound)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
            StartDate = DateTimeOffset.Now;
            OrderStatus = OrderStatus.InProgress;
        }

        public void Confirm()
        {
            if (OrderStatus != OrderStatus.Draft)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
            OrderStatus = OrderStatus.Created;
        }

        public void Cancel()
        {
            if (OrderStatus != OrderStatus.Draft)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
            if (OrderStatus == OrderStatus.Draft || OrderStatus == OrderStatus.Created)
            {
                OrderStatus = OrderStatus.Canceled;
            }
            OrderStatus = OrderStatus.Canceled;
        }

        public void CompleteJob()
        {
            if (OrderStatus != OrderStatus.InProgress)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
            CompletedDate = DateTimeOffset.Now;
            OrderStatus = OrderStatus.Completed;
        }

        public void ApplyCoupon(Coupon coupon)
        {
            CouponId = coupon.Id;
            CouponCode = coupon.Code;
        }

        public static OrderStatus[] GetActiveOrderStatus()
        {
            return new OrderStatus[]
            {
                OrderStatus.Draft,
                OrderStatus.Created,
                OrderStatus.WorkerFound,
                OrderStatus.InProgress
            };
        }
    }
}