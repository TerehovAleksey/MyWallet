using Microsoft.AspNetCore.Mvc;
using MyWallet.Services;
using MyWallet.Services.Dto;

namespace MyWallet.WebApi.Controllers
{
    [Route("api/currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public IEnumerable<CurrencyDto> GetCurrencySymols()
        {
            return _currencyService.GetAllSimbolsWithDescription()
                .Select(x => new CurrencyDto(x.Key, x.Value));
        }
    }
}
