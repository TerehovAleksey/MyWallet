namespace MyWallet.Services.Mapper;

public class DtoToDomainMappingProfile : Profile
{
    public DtoToDomainMappingProfile()
    {
        CreateMap<AccountCreateDto, Account>()
            .ForMember(p => p.CurrencySymbol, opt => opt.MapFrom(dto => dto.CurrencySymbol.ToUpperInvariant()));

        CreateMap<AccountTypeCreateDto, AccountType>()
            .ForMember(p => p.Id, opt => Guid.NewGuid());

        CreateMap<BaseCategoryDto, SubCategory>()
            .ForMember(p => p.Id, opt => Guid.NewGuid());

        CreateMap<CategoryCreateDto, Category>()
            .ForMember(p => p.Id, pot => Guid.NewGuid())
            .ForMember(p => p.SubCategories, opt => opt.MapFrom(dto => dto.Subcategories));

        CreateMap<RecordCreateDto, Journal>()
            .ForMember(p => p.Id, pot => Guid.NewGuid())
            .ForMember(p => p.DateOfCreation, opt => opt.MapFrom(dto => dto.DateTime))
            .ForMember(p => p.SubCategoryId, opt => opt.MapFrom(dto => dto.SubcategoryId));
    }
}