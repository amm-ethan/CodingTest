using Contracts.Models;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ITransactionRepository Transaction { get; }
        IImportDetailRepository ImportDetail { get; }
        Task SaveAsync();
    }
}
