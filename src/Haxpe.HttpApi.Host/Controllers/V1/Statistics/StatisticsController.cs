using Haxpe.Customers;
using Haxpe.Helpers;
using Haxpe.Models;
using Haxpe.Orders;
using Haxpe.Partners;
using Haxpe.Statistics;
using Haxpe.V1.Statistics;
using Haxpe.Workers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Haxpe.Controllers.V1.Statistics
{
    [ApiController]
    public class StatisticsController : ControllerBase
    {

        private readonly IStatisticsService _service;

        public StatisticsController(IStatisticsService service)
        {
            _service = service;
        }

        [Route("api/v1/statistics/order")]
        [HttpGet]
        public async Task<Response<IReadOnlyCollection<CountByDateV1Dto>>> GetOrdersBetweenDatesAsync([FromQuery] CountByDatesQuery input)
        {
            var order = await _service.GetOrderCountByDatesAsync(input);
            return Response<IReadOnlyCollection<CountByDateV1Dto>>.Ok(order);
        }

        [Route("api/v1/statistics/partner")]
        [HttpGet]
        public async Task<Response<IReadOnlyCollection<CountByDateV1Dto>>> GetPartnersBetweenDatesAsync([FromQuery] CountByDatesQuery input)
        {
            var order = await _service.GetPartnerCountByDatesAsync(input);
            return Response<IReadOnlyCollection<CountByDateV1Dto>>.Ok(order);
        }

        [Route("api/v1/statistics/customer")]
        [HttpGet]
        public async Task<Response<IReadOnlyCollection<CountByDateV1Dto>>> GetCustomersBetweenDatesAsync([FromQuery] CountByDatesQuery input)
        {
            var order = await _service.GetCustomerCountByDatesAsync(input);
            return Response<IReadOnlyCollection<CountByDateV1Dto>>.Ok(order);
        }

        [Route("api/v1/statistics/worker")]
        [HttpGet]
        public async Task<Response<IReadOnlyCollection<CountByDateV1Dto>>> GetWorkersBetweenDatesAsync([FromQuery] CountByDatesQuery input)
        {
            var order = await _service.GetWorkerCountByDatesAsync(input);
            return Response<IReadOnlyCollection<CountByDateV1Dto>>.Ok(order);
        }
    }
}
