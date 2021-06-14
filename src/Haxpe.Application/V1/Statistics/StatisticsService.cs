using AutoMapper;
using Haxpe.Customers;
using Haxpe.Infrastructure;
using Haxpe.Infrastructure.Statistics;
using Haxpe.Orders;
using Haxpe.Partners;
using Haxpe.Statistics;
using Haxpe.Workers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haxpe.V1.Statistics
{
    public class StatisticsService : ApplicationService, IStatisticsService
    {
        private IStatisticsRepository<Order, Guid> _orderRepository;
        private IStatisticsRepository<Partner, Guid> _partnerRepository;
        private IStatisticsRepository<Customer, Guid> _customerRepository;
        private IStatisticsRepository<Worker, Guid> _workerRepository;

        public StatisticsService(
            IStatisticsRepository<Order, Guid> orderRepository,
            IStatisticsRepository<Partner, Guid> partnerRepository,
            IStatisticsRepository<Customer, Guid> customerRepository,
            IStatisticsRepository<Worker, Guid> workerRepository,
            IMapper mapper) : base(mapper)
        {
            _orderRepository = orderRepository;
            _partnerRepository = partnerRepository;
            _customerRepository = customerRepository;
            _workerRepository = workerRepository;
        }

        public async Task<IReadOnlyCollection<CountByDateV1Dto>> GetOrderCountByDatesAsync(CountByDatesQuery query)
        {
            var statistic = await _orderRepository.GetCountByDatesAsync(query.StartDate, query.EndDate ?? DateTime.UtcNow);
            return mapper.Map<IReadOnlyCollection<CountByDateV1Dto>>(statistic);
        }

        public async Task<IReadOnlyCollection<CountByDateV1Dto>> GetPartnerCountByDatesAsync(CountByDatesQuery query)
        {
            var statistic = await _partnerRepository.GetCountByDatesAsync(query.StartDate, query.EndDate ?? DateTime.UtcNow);
            return mapper.Map<IReadOnlyCollection<CountByDateV1Dto>>(statistic);
        }

        public async Task<IReadOnlyCollection<CountByDateV1Dto>> GetCustomerCountByDatesAsync(CountByDatesQuery query)
        {
            var statistic = await _customerRepository.GetCountByDatesAsync(query.StartDate, query.EndDate ?? DateTime.UtcNow);
            return mapper.Map<IReadOnlyCollection<CountByDateV1Dto>>(statistic);
        }

        public async Task<IReadOnlyCollection<CountByDateV1Dto>> GetWorkerCountByDatesAsync(CountByDatesQuery query)
        {
            var statistic = await _workerRepository.GetCountByDatesAsync(query.StartDate, query.EndDate ?? DateTime.UtcNow);
            return mapper.Map<IReadOnlyCollection<CountByDateV1Dto>>(statistic);
        }
    }
}
