﻿using AutoMapper;
using MyWallet.Core;
using MyWallet.Services.Dto;

namespace MyWallet.Services.Mapper;

public class DtoToDomainMappingProfile : Profile
{
	public DtoToDomainMappingProfile()
	{
		CreateMap<BaseCategoryDto, SubCategory>()
			.ForMember(p => p.Id, opt => Guid.NewGuid());

		CreateMap<CategoryCreateDto, Category>()
			.ForMember(p => p.Id, pot => Guid.NewGuid())
			.ForMember(p => p.SubCategories, opt => opt.MapFrom(dto => dto.Subcategories));
	}
}
