using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RadioTaxisAPI.Data.Entities;
using RadioTaxisAPI.Models;

namespace RadioTaxisAPI.Data.Repository
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            this.CreateMap<BusinessEntity, BusinessModel>()
                .ReverseMap();

            this.CreateMap<DriverModel, DriverEntity>()
                .ForMember(driver => driver.Business, opt => opt.MapFrom(scr => new BusinessEntity { Id = scr.BusinessId }))
                .ReverseMap()
                .ForMember(dest => dest.BusinessId, opt => opt.MapFrom(scr => scr.Business.Id));
        }
    }
}
