using AutoMapper;
using PcBuilder.Models;
using PcBuilder.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ConfigurationProductViewModel, Product>().ReverseMap();
        }
    }
}
