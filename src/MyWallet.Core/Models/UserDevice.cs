namespace MyWallet.Core.Models;

public class UserDevice : BaseEntity
{
    public string DeviceName { get; set; } = default!;
    public string? LastIpAddress { get; set; }
    public string RefreshToken { get; set; } = default!;
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTime LastLoginDate { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
