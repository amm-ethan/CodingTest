using Contracts.Models;
using Entities.Models;

namespace Repository.Models
{
    public class TranscationRepository : RepositoryBase<Transaction>, ITranscationRepository
    {
        public TranscationRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }

}
