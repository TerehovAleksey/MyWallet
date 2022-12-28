namespace MyWallet.Client.Models;

public record AuthResponse(string Token, string RefreshToken, DateTime TokenExpiresDate);
public record RefreshTokenRequest(string Token, string RefreshToken);
