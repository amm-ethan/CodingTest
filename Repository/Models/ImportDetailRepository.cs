using Contracts.Models;
using Entities.Models;

namespace Repository.Models
{
    public class ImportDetailRepository : RepositoryBase<ImportDetail>, IImportDetailRepository
    {
        public ImportDetailRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void CreateImportDetail(ImportDetail importDetail) =>Create(importDetail);
    }
}
