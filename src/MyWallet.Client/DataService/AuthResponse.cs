namespace MyWallet.Client.DataService;

internal record AuthResponse(string Token, string RefreshToken, DateTime TokenExpiresDate);
internal record RefreshTokenRequest(string Token, string RefreshToken);
