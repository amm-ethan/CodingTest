using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using Shared.RequestFeatures.Models;

namespace Service.Contracts.Models
{
    public interface ITranscationService
    {
        Task<(IEnumerable<TransactionDto> transcations, MetaData metaData)> GetAllTransactionsAsync(TransactionParameters transactionParameters,bool trackChanges);
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsyncByCurrency(string currency, bool trackChanges);
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsyncByDateRange(DateTime fromDate, DateTime toDate, bool trackChanges);
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsyncByStatus(string status, bool trackChanges);
    }
}
