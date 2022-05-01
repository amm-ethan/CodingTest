using Contracts;
using Contracts.Services;
using Service.Contracts.Models;

namespace Service.Models
{
    internal sealed class TranscationService : ITranscationService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public TranscationService(IRepositoryManager repository, ILoggerManager
        logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}
