namespace MyWallet.WebApi.Controllers.Base;

public class BaseApiController : ControllerBase
{
    protected UserManager<User> UserManager { get; }

    public BaseApiController(UserManager<User> userManager)
    {
        UserManager = userManager;
    }

    protected async Task<User?> GetUserAsync()
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        var user = await UserManager.FindByEmailAsync(email);
        return user;
    }
}
