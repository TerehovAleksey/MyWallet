namespace MyWallet.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/currency")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CurrencyController : BaseApiController
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService, UserManager<User> userManager) : base(userManager)
        {
            _currencyService = currencyService;
        }

        /// <summary>
        /// Список всех валют
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CurrencyDto>), StatusCodes.Status200OK)]
        public IEnumerable<CurrencyDto> GetCurrencySymbols()
        {
            return _currencyService.GetAllSymbolsWithDescription()
                .Select(x => new CurrencyDto(x.Key, x.Value));
        }

        /// <summary>
        /// Список всех валют пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("user")]
        [ProducesResponseType(typeof(IEnumerable<UserCurrencyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserCurrenciesAsync()
        {
            var user = await GetUserAsync();

            if (user is null)
            {
                return Unauthorized();
            }

            var result = await _currencyService.GetUserCurrnciesAsync(user.Id);
            return Ok(result);
        }

        /// <summary>
        /// Валюта пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("user/{symbol}")]
        [ProducesResponseType(typeof(IEnumerable<UserCurrencyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserCurrencyAsync([FromRoute] string symbol)
        {
            var user = await GetUserAsync();

            if (user is null)
            {
                return Unauthorized();
            }

            var currencies = await _currencyService.GetUserCurrnciesAsync(user.Id);
            var result = currencies.FirstOrDefault(x => x.Symbol.ToLower() == symbol.ToLower());
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Добавление валюты пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("user")]
        [ProducesResponseType(typeof(UserCurrencyDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateUserCurrencyAsync([FromBody] CurrencyDto currency)
        {
            currency = currency with { Symbol = currency.Symbol.ToUpperInvariant() };

            var allSymbols = GetCurrencySymbols().Select(x => x.Symbol).ToArray();
            if (!allSymbols.Contains(currency.Symbol))
            {
                return BadRequest(ErrorMessage.Create(Strings.CurrencyDoesNotExist));
            }

            var user = await GetUserAsync();

            if (user is null)
            {
                return Unauthorized();
            }

            var result = await _currencyService.CreateUserCurrencyAsync(user.Id, currency.Symbol);
            return Created($"api/currency/{result.Symbol}", result);
        }
    }
}
