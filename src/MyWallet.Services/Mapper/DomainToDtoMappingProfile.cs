namespace MyWallet.Services.Mapper;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<UserCurrency, UserCurrencyDto>()
            .ForMember(dto => dto.Symbol, opt => opt.MapFrom(p => p.CurrencySymbol));

        CreateMap<Account, AccountDto>()
            .ForMember(dto => dto.AccountType, opt => opt.MapFrom(p => p.AccountType.Name))
            .ForMember(dto => dto.TypeImage, opt => opt.MapFrom(p => p.AccountType.ImageName));

        CreateMap<AccountType, AccountTypeDto>();
        
        CreateMap<CategoryType, CategoryTypeDto>();
        CreateMap<SubCategory, CategoryDto>()
            .ForMember(dto => dto.Type, opt => opt.MapFrom(p => p.CategoryType));
        CreateMap<Category, CategoryResponseDto>()
            .ForMember(dto => dto.Subcategories, opt => opt.MapFrom(p => p.SubCategories.ToList()))
            .ForMember(dto => dto.Type, opt => opt.MapFrom(p => p.CategoryType));
        
        CreateMap<Journal, RecordDto>()
            .ForMember(dto => dto.Subcategory, opt => opt.MapFrom(p => p.SubCategory.Name))
            .ForMember(dto => dto.Category, opt => opt.MapFrom(p => p.SubCategory.Category.Name))
            .ForMember(dto => dto.IsIncome, opt => opt.MapFrom(p => p.SubCategory.Category.IsIncome))
            .ForMember(dto => dto.Account, opt => opt.MapFrom(p => p.Account.Name))
            .ForMember(dto => dto.CurrencySymbol, opt => opt.MapFrom(p => p.Account.CurrencySymbol))
            .ForMember(dto => dto.Color, opt => opt.MapFrom(p => p.SubCategory.Color));
    }
}
