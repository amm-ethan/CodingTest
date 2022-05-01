using Api.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Api.Presentation.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IServiceManager _service;
        public TransactionController(IServiceManager service) => _service = service;

        [HttpGet(Name = "GetAllTransaction")]
        public async Task<IActionResult> GetAllTransaction()
        {
            var transactions = await _service.TranscationService.GetAllTransactionAsync(trackChanges: false);
            return Ok(transactions);
        }

        [HttpGet("Currency/{currency}", Name = "GetAllTransactionByCurrency")]
        [ServiceFilter(typeof(ObjectNullAttribute))]
        public async Task<IActionResult> GetAllTransactionByCurrency(string currency)
        {
            var transactions = await _service.TranscationService.GetAllTransactionAsyncByCurrency(currency, trackChanges: false);
            return Ok(transactions);
        }

        [HttpGet("DateRange", Name = "GetAllTransaction")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        //Todo : Implement as Query?
        public async Task<IActionResult> GetAllTransactionByDate([FromBody] DateTimeDto dateTimeDto)
        {
            var transactions = await _service.TranscationService.GetAllTransactionAsyncByDateRange(dateTimeDto, trackChanges: false);
            return Ok(transactions);
        }

        [HttpGet("{status}", Name = "GetAllTransactionByStatus")]
        [ServiceFilter(typeof(ObjectNullAttribute))]
        public async Task<IActionResult> GetAllTransactionByStatus(string status)
        {
            var transactions = await _service.TranscationService.GetAllTransactionAsyncByStatus(status, trackChanges: false);
            return Ok(transactions);
        }
    }

}
