using Service.Contracts.Models;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        ITranscationService TranscationService { get; }
    }
}