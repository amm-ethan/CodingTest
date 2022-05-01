using AutoMapper;
using Contracts;
using Contracts.Services;
using Service.Contracts.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using Shared.RequestFeatures.Models;

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

        public async Task<(IEnumerable<TransactionDto> transcations, MetaData metaData)> GetAllTransactionsAsync(TransactionParameters transactionParameters, bool trackChanges)
        {
            var transcations = await _repository.Transcation.GetAllTransactionsAsync(transactionParameters, trackChanges);
            var transcationDto = _mapper.Map<IEnumerable<TransactionDto>>(transcations);
            return (transcations: transcationDto, transcations.MetaData);


        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsyncByCurrency(string currency, bool trackChanges)
        {
            var transcations = await _repository.Transcation.GetAllTransactionsAsyncByCurrency(currency, trackChanges);
            return _mapper.Map<IEnumerable<TransactionDto>>(transcations);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsyncByDateRange(DateTime fromDate, DateTime toDate, bool trackChanges)
        {
            var transcations = await _repository.Transcation.GetAllTransactionsAsyncByDateRange(fromDate, toDate, trackChanges);
            return _mapper.Map<IEnumerable<TransactionDto>>(transcations);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsyncByStatus(string status, bool trackChanges)
        {
            var transcations = await _repository.Transcation.GetAllTransactionsAsyncByStatus(status, trackChanges);
            return _mapper.Map<IEnumerable<TransactionDto>>(transcations);
        }
    }
}
