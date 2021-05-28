using System;
using System.Threading.Tasks;
using Haxpe.V1.Partners;
using Haxpe.Customers;
using Haxpe.Roles;
using Haxpe.Infrastructure;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Haxpe.V1.Customers
{
    public class CustomerV1Service:
        CrudAppService<Customer, CustomerV1Dto, Guid, UpdateCustomerV1Dto>,
        ICustomerV1Service
    {
        public CustomerV1Service(IRepository<Customer, Guid> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<CustomerV1Dto> UpdateAsync(Guid id, UpdateCustomerV1Dto input)
        {
            var customer = await Repository.FindAsync(id);
            return await base.UpdateAsync(id, input);
        }

        public override async Task DeleteAsync(Guid id)
        {
            var customer = await Repository.FindAsync(id);
            await base.DeleteAsync(id);
        }

        public async Task<CustomerV1Dto> GetByUserId(Guid userId)
        {
            var customer = await Repository.FindAsync(x => x.UserId == userId);

            if(customer == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.CustomerNotFound);
            }

            return base.MapToGetOutputDto(customer);
        }

        public async Task<IReadOnlyCollection<CustomerV1Dto>> GetListAsync(CustomerListQuery query)
        {
            if (query.CustomerIds?.Any() == true)
            {
                var customers = await Repository.GetListAsync(x => query.CustomerIds.Contains(x.Id));
                return customers.Select(base.MapToGetOutputDto).ToArray();
            }

            if (query.UserIds?.Any() == true)
            {
                var customers = await Repository.GetListAsync(x => query.UserIds.Contains(x.UserId));
                return customers.Select(base.MapToGetOutputDto).ToArray();
            }

            return null;
        }
    }
}