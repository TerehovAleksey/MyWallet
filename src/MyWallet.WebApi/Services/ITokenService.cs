namespace MyWallet.WebApi.Services;

public interface ITokenService
{
    int RefreshTokenExpiration { get; }
    Task<List<Claim>> GetClaims(User user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    Task<string> GenerateToken(User user);
    string GenerateRefreshToken();
}