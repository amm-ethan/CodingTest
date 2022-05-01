using Entities.Models;

namespace Contracts.Models
{
    public interface ITranscationRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionAsync(bool trackChanges);

    }
}
