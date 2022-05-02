using Contracts;
using Contracts.Models;
using Repository.Models;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ITransactionRepository> _transactionRepository;
        private readonly Lazy<IImportDetailRepository> _importDetailRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _transactionRepository = new Lazy<ITransactionRepository>(() => new
            TransactionRepository(repositoryContext));
            _importDetailRepository = new Lazy<IImportDetailRepository>(() => new
           ImportDetailRepository(repositoryContext));
        }

        public ITransactionRepository Transaction => _transactionRepository.Value;

        public IImportDetailRepository ImportDetail => _importDetailRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}