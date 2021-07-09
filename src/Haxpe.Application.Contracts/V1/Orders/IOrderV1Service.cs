using Haxpe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haxpe.V1.Orders
{
    public interface IOrderV1Service: IApplicationService
    {
        Task<OrderV1Dto> CreateOrder(CreateOrderV1Dto order);

        Task<OrderV1Dto> UpdateOrder(CreateOrderV1Dto order);

        Task<OrderV1Dto> ConfirmOrder(Guid orderId);

        Task<OrderV1Dto> CancelOrder(Guid orderId, OrderCancelReasonDto reasonDto);

        Task<OrderV1Dto> GetOrder(Guid id);

        Task<IReadOnlyCollection<OrderV1Dto>> GetListAsync(OrderListRequestV1Dto query);

        Task<OrderV1Dto> AssignWorker(Guid id, Guid workerId);
        
        Task<OrderV1Dto> StartJob(Guid id);
        
        Task<OrderV1Dto> CompleteJob(Guid id);

        Task<OrderV1Dto> ApplyCoupon(Guid id, ApplyCouponDto model);

    }
}