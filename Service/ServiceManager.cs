using Contracts;
using Contracts.Services;
using Service.Contracts;
using Service.Contracts.Models;
using Service.Models;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITranscationService> _transcationService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
        logger)
        {
            _transcationService = new Lazy<ITranscationService>(() => new
            TranscationService(repositoryManager, logger));
        }
        public ITranscationService TranscationService => _transcationService.Value;

    }
}