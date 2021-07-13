using AutoMapper;
using Haxpe.Infrastructure;
using Haxpe.Orders;
using Haxpe.V1.Events;
using Haxpe.V1.Orders;
using Haxpe.Workers;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Workers
{
    public class WorkerNotifierService : IWorkerNotifierService
    {
        IRepository<Worker, Guid> workerRepository;

        IRepository<Order, Guid> orderRepository;

        private readonly IEventEmitter eventEmitter;
        private readonly IMapper mapper;

        public WorkerNotifierService(IRepository<Worker, Guid> workerRepository, 
            IRepository<Order, Guid> orderRepository, 
            IEventEmitter eventEmitter,
            IMapper mapper)
        {
            this.workerRepository = workerRepository ?? throw new ArgumentNullException(nameof(workerRepository));
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.eventEmitter = eventEmitter ?? throw new ArgumentNullException(nameof(eventEmitter));
            this.mapper = mapper;
        }

        public async Task OrderOfferNotify(Guid orderId)
        {
            var order = await this.orderRepository.FindAsync(orderId);
            if(order?.OrderStatus != OrderStatus.Created)
            {
                Log.Logger.Information($"WorkerNotifierService. Order status is not suitable, orderstatus = {order?.OrderStatus}");
                return;
            }

            var workers = await this.workerRepository.GetListAsync(x => x.ServiceTypes.Any(s => s.ServiceTypeId == order.ServiceTypeId));

            var creationDate = DateTime.UtcNow;

            var tasks = workers.Select(w => this.eventEmitter.SendEvent(w.UserId, new OrderOfferEvent
            {
                Payload = new OrderOffer
                {
                    CreationDate = creationDate,
                    Order = this.mapper.Map<Order, OrderV1Dto>(order)
                }
            })).ToArray();

            await Task.WhenAll(tasks);
        }
    }
}
