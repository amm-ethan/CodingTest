using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Api.Presentation.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IServiceManager _service;
        public TransactionController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult Get()
        {
                var transactions =
                _service.TranscationService.GetAllTransactionAsync(trackChanges: false);
                return Ok(transactions);
        }
    }

}
