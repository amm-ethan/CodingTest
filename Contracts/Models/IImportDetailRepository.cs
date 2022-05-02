using Entities.Models;

namespace Contracts.Models
{
    public interface IImportDetailRepository
    {
        void CreateImportDetail(ImportDetail importDetail);
    }
}
