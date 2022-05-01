using Contracts.Models;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ITranscationRepository Transcation { get; }
    }
}
