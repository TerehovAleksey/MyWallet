namespace MyWallet.Common.Dto;

public record UserForAuthDto(string Email, string Password);
public record AuthResponseDto(string Token, string RefreshToken, DateTime TokenExpiresDate);
public record UserForRegistrationDto(string Email, string Password, string ConfirmPassword);
public record PasswordChangeDto(string OldPassword, string NewPassword, string NewPasswordConfirm);
public record RefreshTokenDto(string Token, string RefreshToken);
public record UserDto(string? FirstName, string? LastName, string Email, DateTime? BirthdayDate, Gender Gender, string? LogoUri);
public record UserUpdateDto(string? FirstName, string? LastName, DateTime? BirthdayDate, Gender Gender);
public record DeviceLocationDto(string Hostname, string City, string Region, string Country, string Loc, string Org);
public record UserDeviceDto(string Name, string? Ip, DateTime LastLoginDate)
{
    public DeviceLocationDto? DeviceLocation { get; set; }
}