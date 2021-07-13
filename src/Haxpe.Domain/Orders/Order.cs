using System;
using Haxpe.Workers;
using Haxpe.Infrastructure;
using Haxpe.Coupons;
using System.Collections.Generic;
using System.Linq;

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
            TimeTrackers = new List<OrderTimeTracker>();
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

        public string? CancelReason { get; set; }

        public ICollection<OrderTimeTracker> TimeTrackers { get; set; }

        public void AssignWorker(Worker worker)
        {
            if (OrderStatus != OrderStatus.Created)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
            
            WorkerId = worker.Id;
            PartnerId = worker.PartnerId;
            OrderStatus = OrderStatus.Reserved;
        }
        
        public void StartJob()
        {
            if (OrderStatus != OrderStatus.Reserved)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
            StartDate = DateTimeOffset.Now;
            OrderStatus = OrderStatus.InProgress;
            TimeTrackers.Add(
                new OrderTimeTracker() { OrderId = Id, StartDate = DateTime.UtcNow }
            );
        }

        public void Paused()
        {
            if (OrderStatus != OrderStatus.InProgress)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
            OrderStatus = OrderStatus.Paused;

            var lastTimeTracker = TimeTrackers.FirstOrDefault(x => x.EndDate == null);
            if(lastTimeTracker != null)
            {
                lastTimeTracker.EndDate = DateTime.UtcNow;
            }
        }

        public void Resume()
        {
            if (OrderStatus != OrderStatus.Paused)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
            OrderStatus = OrderStatus.InProgress;
            TimeTrackers.Add(
                new OrderTimeTracker() { OrderId = Id, StartDate = DateTime.UtcNow }
            );
        }

        public void Confirm()
        {
            if (OrderStatus != OrderStatus.Draft)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
            OrderStatus = OrderStatus.Created;
        }

        public void Cancel(string? cancelReason = null)
        {
            if (OrderStatus == OrderStatus.Draft || OrderStatus == OrderStatus.Created)
            {
                OrderStatus = OrderStatus.Canceled;
                CancelReason = cancelReason;
            }
            else
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
        }

        public void CompleteJob()
        {
            if (OrderStatus != OrderStatus.InProgress)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.OrderWorkflowViolation);
            }
            CompletedDate = DateTimeOffset.Now;
            OrderStatus = OrderStatus.Completed;

            var lastTimeTracker = TimeTrackers.FirstOrDefault(x => x.EndDate == null);
            if (lastTimeTracker != null)
            {
                lastTimeTracker.EndDate = DateTime.UtcNow;
            }
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
                OrderStatus.Reserved,
                OrderStatus.InProgress,
                OrderStatus.Paused
            };
        }
    }
}