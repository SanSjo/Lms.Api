using AutoMapper;
using Lms.Core.Dto;
using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Data.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Module, ModuleDto>().ReverseMap();
        }
    }
}
