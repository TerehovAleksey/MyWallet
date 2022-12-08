namespace MyWallet.Client.Models;

public record UserAuthData(string Email, string Password);
public record UserRegisterData(string Email, string Password, string PasswordConfirm);
public record PasswordChangeData(string OldPassword, string NewPassword, string NewPasswordConfirm);
public record AuthData(string Token, string RefreshToken);
public record UserData(string Email, string? FirstName, string? LastName, DateTime? BirthdayDate, Gender Gender);
public record UserUpdateData(string? FirstName, string? LastName, DateTime? BirthdayDate, Gender Gender);

public enum Gender
{
    Empty,
    Male,
    Female
}
