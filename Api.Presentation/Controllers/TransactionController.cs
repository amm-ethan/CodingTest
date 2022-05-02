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

        [HttpPost("import", Name = "ImportTranscations")]
        public async Task<IActionResult> ImportTranscations()
        {
            var filesFromWeb = Request.Form.Files;
            await _service.TranscationService.ImportTransactions(filesFromWeb[0]);

            return Ok();
        }

        #region Get

        [HttpGet(Name = "GetAllTransactions")]
        public async Task<IActionResult> GetAllTransaction([FromQuery] TransactionParameters transactionParameters)
        {
            var (transcations, metaData) = await _service.TranscationService.GetAllTransactionsAsync(transactionParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));
            return Ok(transcations);
        }

        [HttpGet("by-currency/{currency}", Name = "GetAllTransactionsByCurrency")]
        public async Task<IActionResult> GetAllTransactionByCurrency(string currency)
        {
            var transactions = await _service.TranscationService.GetAllTransactionsAsyncByCurrency(currency, trackChanges: false);
            return Ok(transactions);
        }

        [HttpGet("by-date-range/{fromDate}/{toDate}", Name = "GetAllTransactionsByDateRange")]
        public async Task<IActionResult> GetAllTransactionByDateRange(string fromDate, string toDate)
        {
            var transactions = await _service.TranscationService.GetAllTransactionsAsyncByDateRange(DateTime.Parse(fromDate), DateTime.Parse(toDate), trackChanges: false);
            return Ok(transactions);
        }

        [HttpGet("by-status/{status}", Name = "GetAllTransactionsByStatus")]
        public async Task<IActionResult> GetAllTransactionByStatus(string status)
        {
            var transactions = await _service.TranscationService.GetAllTransactionsAsyncByStatus(status, trackChanges: false);
            return Ok(transactions);
        }

        #endregion Get
    }
}