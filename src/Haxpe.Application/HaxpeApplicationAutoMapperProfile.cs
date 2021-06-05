using AutoMapper;
using Haxpe.Addresses;
using Haxpe.Coupons;
using Haxpe.Customers;
using Haxpe.Industries;
using Haxpe.Orders;
using Haxpe.Partners;
using Haxpe.ServiceTypes;
using Haxpe.Users;
using Haxpe.V1.Account;
using Haxpe.V1.Addresses;
using Haxpe.V1.Coupons;
using Haxpe.V1.Industry;
using Haxpe.V1.Orders;
using Haxpe.V1.Partners;
using Haxpe.V1.ServiceType;
using Haxpe.V1.Users;
using Haxpe.V1.Workers;
using Haxpe.Workers;
using Microsoft.AspNetCore.Identity;

namespace Haxpe
{
    public class HaxpeApplicationAutoMapperProfile : Profile
    {
        public HaxpeApplicationAutoMapperProfile()
        {
            CreateMap<Partner, PartnerV1Dto>();
            CreateMap<UpdatePartnerV1Dto, Partner>();
            
            CreateMap<Worker, WorkerV1Dto>();
            CreateMap<UpdateWorkerV1Dto, Worker>();
            
            CreateMap<Industry, IndustryV1Dto>();
            CreateMap<CreateUpdateIndustryV1Dto, Industry>();
            
            CreateMap<ServiceType, ServiceTypeV1Dto>();
            CreateMap<CreateUpdateServiceTypeV1Dto, ServiceType>();
            
            CreateMap<WorkerServiceType, WorkerServiceTypeV1Dto>();
            CreateMap<WorkerServiceTypeV1Dto, WorkerServiceType>();
            
            CreateMap<Address, AddressV1Dto>();
            CreateMap<UpdateAddressV1Dto, Address>();
            
            CreateMap<Customer, CustomerV1Dto>();
            CreateMap<UpdateCustomerV1Dto, Customer>();

            CreateMap<Order, OrderV1Dto>();

            CreateMap<User, UserProfileDto>();
            CreateMap<User, UserV1Dto>();

            CreateMap<User, IdentityUserDto>();

            CreateMap<Coupon, CouponV1Dto>();

        }
    }
}
