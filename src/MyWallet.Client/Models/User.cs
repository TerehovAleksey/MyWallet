namespace MyWallet.Client.Models;

public record UserAuthData(string Email, string Password);
public record UserRegisterData(string Email, string Password, string PasswordConfirm);
public record PasswordChangeData(string OldPassword, string NewPassword, string NewPasswordConfirm);
public record AuthData(string Token, string RefreshToken);

public record UserData
{
    public string Email { get; set; } = default!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthdayDate { get; set; }
    public Gender Gender { get; set; }
}

public record UserUpdateData(string? FirstName, string? LastName, DateTime? BirthdayDate, Gender Gender);

public enum Gender
{
    Empty,
    Male,
    Female
}

public record DeviceLocationDto
{
    public string Hostname { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Region { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string Loc { get; set; } = default!;
    public string Org { get; set; } = default!;
}
public record UserDeviceDto
{
    public string Name { get; set; } = default!;
    public string? Ip { get; set; }
    public DateTime LastLoginDate { get; set; }
    public DeviceLocationDto? DeviceLocation { get; set; }

    public override string ToString() => $"{Ip}: {DeviceLocation?.Country}, {DeviceLocation?.City}";
}
