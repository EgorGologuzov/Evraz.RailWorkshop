﻿using AutoMapper;
using RailWorkshop.API.Dto;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeCreateDto, Employee>()
                .ReverseMap();
            CreateMap<EmployeeUpdateDto, Employee>()
                .ReverseMap();
            CreateMap<EmployeeReturnDto, Employee>()
                .ReverseMap();
            CreateMap<ProductReturnDto, Product>()
                .ReverseMap();
            CreateMap<ProductCreateDto, Product>()
                .ReverseMap();
            CreateMap<ProductUpdateDto, Product>()
                .ReverseMap();
            CreateMap<StatementReturnDto, Statement>()
                .ReverseMap();
            CreateMap<ConsignmentReturnDto, Consignment>()
                .ReverseMap();
            CreateMap<ConsignmentDefectReturnDto, ConsignmentDefect>()
                .ReverseMap();
            CreateMap<StatementCreateDto, Statement>()
                .ReverseMap();
            CreateMap<ConsignmentCreateDto, Consignment>()
                .ReverseMap();
            CreateMap<ConsignmentDefectCreateDto, ConsignmentDefect>()
                .ReverseMap();
        }
    }
}