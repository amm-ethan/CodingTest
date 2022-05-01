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
        public IActionResult GetAllTransaction()
        {
            var transactions =
            _service.TranscationService.GetAllTransactionAsync(trackChanges: false);
            return Ok(transactions);
        }

        [HttpGet("Currency/{currency}", Name = "GetAllTransactionByCurrency")]
        public IActionResult GetAllTransactionByCurrency(string currency)
        {
            var transactions =
            _service.TranscationService.GetAllTransactionAsyncByCurrency(currency, trackChanges: false);
            return Ok(transactions);
        }

        [HttpGet("DateRange", Name = "GetAllTransaction")]
        public IActionResult GetAllTransactionByDate([FromBody] DateTimeDto dateTimeDto)
        {
            var transactions =
            _service.TranscationService.GetAllTransactionAsyncByDateRange(dateTimeDto, trackChanges: false);
            return Ok(transactions);
        }

        [HttpGet("{status}", Name = "GetAllTransactionByStatus")]
        public IActionResult GetAllTransactionByStatus(string status)
        {
            var transactions =
            _service.TranscationService.GetAllTransactionAsyncByStatus(status, trackChanges: false);
            return Ok(transactions);
        }
    }

}
