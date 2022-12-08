namespace MyWallet.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/journal")]
    [Produces(MediaTypeNames.Application.Json)]
    public class JournalController : BaseApiController
    {
        private readonly IRecordService _recordService;

        public JournalController(UserManager<User> userManager, IRecordService recordService) : base(userManager)
        {
            _recordService = recordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecords([FromQuery] DateTime startDate, [FromQuery] DateTime finishDate)
        {
            var user = await GetUserAsync();
            if (user is null)
            {
                return Unauthorized();
            }

            var result = await _recordService.GetRecordsAsync(user.Id, startDate, finishDate);
            return Ok(result);
        }
            

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRecordByIdAsync([FromRoute] Guid id)
        {
            var result = await _recordService.GetRecordByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecordAsync([FromBody] RecordCreateDto record)
        {
            var result = await _recordService.CreateRecordAsync(record);
            return result is null ? BadRequest() : Created($"api/journal/{result.Id}", result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRecordAsync([FromRoute] Guid id, [FromBody] RecordUpdateDto record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }
            
            var result = await _recordService.UpdateRecordAsync(record);
            return result ? NoContent() : NotFound();
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRecordAsync([FromRoute] Guid id)
        {
            var result = await _recordService.DeleteRecordAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}