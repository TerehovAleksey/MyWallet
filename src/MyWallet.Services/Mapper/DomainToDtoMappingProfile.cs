using AutoMapper;
using MyWallet.Core;
using MyWallet.Services.Dto;

namespace MyWallet.Services.Mapper;

public class DomainToDtoMappingProfile : Profile
{
	public DomainToDtoMappingProfile()
	{
		CreateMap<SubCategory, CategoryDto>();
		CreateMap<Category, CategoryResponseDto>()
			.ForMember(dto => dto.Subcategories, opt => opt.MapFrom(p => p.SubCategories.ToList()));
	}
}
