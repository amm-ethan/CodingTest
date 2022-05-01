using Contracts;
using Contracts.Models;
using Repository.Models;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ITranscationRepository> _transcationRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _transcationRepository = new Lazy<ITranscationRepository>(() => new
            TranscationRepository(repositoryContext));
        }
        public ITranscationRepository Transcation => _transcationRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
