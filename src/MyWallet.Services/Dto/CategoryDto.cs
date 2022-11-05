namespace MyWallet.Services.Dto;

public record CategoryDto(Guid Id, string Name, bool IsVisible, string? ImageName);
public record CategoryCreateDto(string Name, string? ImageName);
