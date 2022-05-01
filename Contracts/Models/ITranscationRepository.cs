using Entities.Models;
using Shared.DataTransferObjects;

namespace Contracts.Models
{
    public interface ITranscationRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionAsync(bool trackChanges);
        Task<IEnumerable<Transaction>> GetAllTransactionAsyncByCurrency(string currency, bool trackChanges);
        Task<IEnumerable<Transaction>> GetAllTransactionAsyncByDateRange(DateTimeDto dateTimeDto, bool trackChanges);
        Task<IEnumerable<Transaction>> GetAllTransactionAsyncByStatus(string status,bool trackChanges);


    }
}
