using Contracts.Models;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models
{
    public class TranscationRepository : RepositoryBase<Transaction>, ITranscationRepository
    {
        public TranscationRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();
    }

}
