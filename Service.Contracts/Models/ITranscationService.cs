using Shared.DataTransferObjects;

namespace Service.Contracts.Models
{
    public interface ITranscationService
    {
        Task<IEnumerable<TransactionDto>> GetAllTransactionAsync(bool trackChanges);
    }
}
