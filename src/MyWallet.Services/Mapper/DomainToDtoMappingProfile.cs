﻿using AutoMapper;
using MyWallet.Core;
using MyWallet.Services.Dto;

namespace MyWallet.Services.Mapper;

public class DomainToDtoMappingProfile : Profile
{
	public DomainToDtoMappingProfile()
	{
		CreateMap<Account, AccountDto>();
		CreateMap<SubCategory, CategoryDto>();
		CreateMap<Category, CategoryResponseDto>()
			.ForMember(dto => dto.Subcategories, opt => opt.MapFrom(p => p.SubCategories.ToList()));

		CreateMap<Journal, RecordDto>()
			.ForMember(dto => dto.Subcategory, opt => opt.MapFrom(p => p.SubCategory.Name))
			.ForMember(dto => dto.Category, opt => opt.MapFrom(p => p.SubCategory.Category.Name))
			.ForMember(dto => dto.IsIncome, opt => opt.MapFrom(p => p.SubCategory.Category.IsIncome))
			.ForMember(dto => dto.Account, opt => opt.MapFrom(p => p.Account.Name))
			.ForMember(dto => dto.CurrencySymbol, opt => opt.MapFrom(p => p.Account.CurrencySymbol));
	}
}
