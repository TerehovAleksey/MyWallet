namespace MyWallet.Common.Dto;

public record UserForAuthDto(string Email, string Password);
public record AuthResponseDto(string Token, string RefreshToken, DateTime TokenExpiresDate);
public record UserForRegistrationDto(string Email, string Password, string ConfirmPassword);
public record PasswordChangeDto(string OldPassword, string NewPassword, string NewPasswordConfirm);
public record RefreshTokenDto(string Token, string RefreshToken);
public record UserDto(string? FirstName, string? LastName, string Email, DateOnly? BirthdayDate, Gender Gender, string? LogoUri);
public record UserUpdateDto(string? FirstName, string? LastName, DateOnly? BirthdayDate, Gender Gender);