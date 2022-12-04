namespace MyWallet.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/info")]
    [Produces(MediaTypeNames.Application.Json)]
    public class InfoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public InfoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //[HttpGet]
        //public IActionResult GetInfo() 
        //{
        //    Dictionary<string, string?> dic = new()
        //    {
        //        { "KeyVaultName", _configuration["KeyVaultName"] },
        //        { "SecretKey", _configuration["JwtTokenConfig:SecretKey"] },
        //        { "Issuer", _configuration["JwtTokenConfig:Issuer"] },
        //        { "Audience", _configuration["JwtTokenConfig:Audience"] }
        //    };
        //    return Ok(dic);
        //}
    }
}
