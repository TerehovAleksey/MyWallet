namespace MyWallet.Services.Dto;

public record AccountTypeDto(Guid Id, string Name);
public record AccountTypeCreateDto(string Name, int Order);
public record AccountTypeUpdateDto(Guid Id, string Name, int Order);
