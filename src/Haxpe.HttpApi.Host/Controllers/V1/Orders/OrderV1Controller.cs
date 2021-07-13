using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haxpe.Infrastructure;
using Haxpe.Models;
using Haxpe.Orders;
using Haxpe.Partners;
using Haxpe.Roles;
using Haxpe.Taxes;
using Haxpe.Workers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Haxpe.V1.Orders
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [ControllerName("Order")]
    public class OrderV1Controller: ControllerBase
    {
        private readonly IOrderV1Service service;

        public OrderV1Controller(IOrderV1Service service)
        {
            this.service = service;
        }
        
        [HttpPost]
        [Route("api/v1/order/create")]
        public async Task<Response<OrderV1Dto>> CreateOrder([FromBody] CreateOrderV1Dto orderRequest)
        {
            var res = await service.CreateOrder(orderRequest);
            return Response<OrderV1Dto>.Ok(res);
        }

        [HttpGet]
        [Route("api/v1/order/{id}")]
        public async Task<Response<OrderV1Dto>> GetOrder(Guid id)
        {
            var res = await service.GetOrder(id);
            return Response<OrderV1Dto>.Ok(res);
        }

        [HttpGet]
        [Route("api/v1/order")]
        public async Task<Response<IReadOnlyCollection<OrderV1Dto>>> Get([FromQuery] OrderListRequestV1Dto request)
        {
            var res = await service.GetListAsync(request);
            return Response<OrderV1Dto>.Ok(res);
        }

        [HttpPost]
        [Route("api/v1/order/{id}/assign-worker/{workerId}")]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Partner + "," + RoleConstants.Worker)]
        public async Task<Response<OrderV1Dto>> AssignWorker(Guid id, Guid workerId)
        {
            var res = await service.AssignWorker(id, workerId);
            return Response<OrderV1Dto>.Ok(res);
        }

        [HttpPost]
        [Route("api/v1/order/{id}/start")]
        [Authorize(Roles = RoleConstants.Worker)]
        public async Task<Response<OrderV1Dto>> StartJob(Guid id)
        {
            var res = await service.StartJob(id);
            return Response<OrderV1Dto>.Ok(res);
        }

        [HttpPost]
        [Route("api/v1/order/{id}/complete")]
        public async Task<Response<OrderV1Dto>> CompleteJob(Guid id)
        {
            var res = await service.CompleteJob(id);
            return Response<OrderV1Dto>.Ok(res);
        }

        [HttpPost]
        [Route("api/v1/order/{id}/confirm")]
        [Authorize(Roles = RoleConstants.Customer)]
        public async Task<Response<OrderV1Dto>> ConfirmOrder(Guid id)
        {
            var res = await service.ConfirmOrder(id);
            return Response<OrderV1Dto>.Ok(res);
        }

        [HttpPost]
        [Route("api/v1/order/{id}/cancel")]
        [Authorize(Roles = RoleConstants.Customer)]
        public async Task<Response<OrderV1Dto>> CancelOrder(Guid id, [FromBody] OrderCancelReasonDto reasonDto)
        {
            var res = await service.CancelOrder(id, reasonDto);
            return Response<OrderV1Dto>.Ok(res);
        }

        [HttpPost]
        [Route("api/v1/order/{id}/apply-coupon")]
        [Authorize(Roles = RoleConstants.Customer)]
        public async Task<Response<OrderV1Dto>> ApplyCoupon(Guid id, [FromBody] ApplyCouponDto code)
        {
            var res = await service.ApplyCoupon(id, code);
            return Response<OrderV1Dto>.Ok(res);
        }
    }
}