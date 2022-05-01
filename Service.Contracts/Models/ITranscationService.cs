using Shared.DataTransferObjects;

namespace Service.Contracts.Models
{
    public interface ITranscationService
    {
        Task<IEnumerable<TransactionDto>> GetAllTransactionAsync(bool trackChanges);
        Task<IEnumerable<TransactionDto>> GetAllTransactionAsyncByCurrency(string currency, bool trackChanges);
        Task<IEnumerable<TransactionDto>> GetAllTransactionAsyncByDateRange(DateTimeDto dateTimeDto, bool trackChanges);
        Task<IEnumerable<TransactionDto>> GetAllTransactionAsyncByStatus(string status, bool trackChanges);
    }
}
