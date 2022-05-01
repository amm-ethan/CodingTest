using Contracts.Models;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObjects;

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

        public async Task<IEnumerable<Transaction>> GetAllTransactionAsyncByCurrency(string currency, bool trackChanges) =>
            await FindByCondition(c => c.CurrencyCode!.ToLower().Equals(currency.ToLower()), trackChanges).ToListAsync();


        public async Task<IEnumerable<Transaction>> GetAllTransactionAsyncByDateRange(DateTimeDto dateTimeDto, bool trackChanges) =>
            await FindByCondition(c => c.TransactionDate.Date >= dateTimeDto.ToDate.Date && c.TransactionDate.Date <= dateTimeDto.FromDate.Date, trackChanges).ToListAsync();

        public async Task<IEnumerable<Transaction>> GetAllTransactionAsyncByStatus(string status, bool trackChanges)
        {
            if (status.ToLower() == "a")
                return await FindByCondition(c => c.Status!.Equals(Status.Approved), trackChanges).ToListAsync();
            else if (status.ToLower() == "r")
                return await FindByCondition(c => c.Status!.Equals(Status.Failed) || c.Status!.Equals(Status.Rejected), trackChanges).ToListAsync();
            else if (status.ToLower() == "d")
                return await FindByCondition(c => c.Status!.Equals(Status.Finished) || c.Status!.Equals(Status.Done), trackChanges).ToListAsync();
            else if (status.ToLower() == "failed")
                return await FindByCondition(c => c.Status!.Equals(Status.Failed), trackChanges).ToListAsync();
            else if (status.ToLower() == "rejected")
                return await FindByCondition(c => c.Status!.Equals(Status.Rejected), trackChanges).ToListAsync();
            else if (status.ToLower() == "finished")
                return await FindByCondition(c => c.Status!.Equals(Status.Finished), trackChanges).ToListAsync();
            else if (status.ToLower() == "done")
                return await FindByCondition(c => c.Status!.Equals(Status.Done), trackChanges).ToListAsync();
            else if (status.ToLower() == "approved")
                return await FindByCondition(c => c.Status!.Equals(Status.Approved), trackChanges).ToListAsync();
            else
                return await FindByCondition(c => c.Status!.Equals(status), trackChanges).ToListAsync();
        }

    }

}
