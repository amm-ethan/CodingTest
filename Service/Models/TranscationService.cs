using AutoMapper;
using Contracts;
using Contracts.Services;
using Service.Contracts.Models;
using Shared.DataTransferObjects;

namespace Service.Models
{
    internal sealed class TranscationService : ITranscationService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public TranscationService(IRepositoryManager repository, ILoggerManager
        logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionAsync(bool trackChanges)
        {
            var transcations = await _repository.Transcation.GetAllTransactionAsync(trackChanges);
            return _mapper.Map<IEnumerable<TransactionDto>>(transcations);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionAsyncByCurrency(string currency, bool trackChanges)
        {
            var transcations = await _repository.Transcation.GetAllTransactionAsyncByCurrency(currency, trackChanges);
            return _mapper.Map<IEnumerable<TransactionDto>>(transcations);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionAsyncByDateRange(DateTimeDto dateTimeDto, bool trackChanges)
        {
            var transcations = await _repository.Transcation.GetAllTransactionAsyncByDateRange(dateTimeDto, trackChanges);
            return _mapper.Map<IEnumerable<TransactionDto>>(transcations);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionAsyncByStatus(string status, bool trackChanges)
        {
            var transcations = await _repository.Transcation.GetAllTransactionAsyncByStatus(status, trackChanges);
            return _mapper.Map<IEnumerable<TransactionDto>>(transcations);
        }
    }
}
