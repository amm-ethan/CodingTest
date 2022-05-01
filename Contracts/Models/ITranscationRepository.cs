using Entities.Models;
using Shared.RequestFeatures;
using Shared.RequestFeatures.Models;

namespace Contracts.Models
{
    public interface ITranscationRepository
    {
        Task<PagedList<Transaction>> GetAllTransactionsAsync(TransactionParameters transactionParameters,bool trackChanges);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsyncByCurrency(string currency, bool trackChanges);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsyncByDateRange(DateTime fromDate, DateTime toDate, bool trackChanges);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsyncByStatus(string status,bool trackChanges);


    }
}
