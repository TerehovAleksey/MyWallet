namespace MyWallet.WebApi.Controllers
{
    [ApiController]
    [Route("api/currency")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CurrencyDto>), StatusCodes.Status200OK)]
        public IEnumerable<CurrencyDto> GetCurrencySymbols()
        {
            return _currencyService.GetAllSymbolsWithDescription()
                .Select(x => new CurrencyDto(x.Key, x.Value));
        }
    }
}
