using Entities.Exceptions.BadRequest;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.RequestFeatures.Models;
using System.Text.Json;

namespace Api.Presentation.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IServiceManager _service;
        public TransactionController(IServiceManager service) => _service = service;

        [HttpGet(Name = "GetAllTransactions")]
        public async Task<IActionResult> GetAllTransaction([FromQuery] TransactionParameters transactionParameters)
        {
            if (!transactionParameters.ValidDateRange)
                throw new IncorrectDateRangeBadRequestException();

            var result = await _service.TranscationService.GetAllTransactionsAsync(transactionParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return Ok(result.transcations);
        }

        [HttpGet("by-currency/{currency}", Name = "GetAllTransactionsByCurrency")]
        public async Task<IActionResult> GetAllTransactionByCurrency(string currency)
        {
            var transactions = await _service.TranscationService.GetAllTransactionsAsyncByCurrency(currency, trackChanges: false);
            return Ok(transactions);
        }

        [HttpGet("by-date-range/{fromDate}-{toDate}", Name = "GetAllTransactionsByDateRange")]
        public async Task<IActionResult> GetAllTransactionByDateRange(DateTime fromDate, DateTime toDate)
        {
            if (fromDate.Date > toDate.Date && fromDate.Date > DateTime.Today.Date && toDate.Date > DateTime.Today.Date)
                throw new IncorrectDateRangeBadRequestException();
            var transactions = await _service.TranscationService.GetAllTransactionsAsyncByDateRange(fromDate, toDate, trackChanges: false);
            return Ok(transactions);
        }

        [HttpGet("by-status/{status}", Name = "GetAllTransactionsByStatus")]
        public async Task<IActionResult> GetAllTransactionByStatus(string status)
        {
            var transactions = await _service.TranscationService.GetAllTransactionsAsyncByStatus(status, trackChanges: false);
            return Ok(transactions);
        }
    }

}
