using Contracts.Models;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using Shared.RequestFeatures.Models;
using System.Diagnostics;

namespace Repository.Models
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<bool> IsTranscationIdExists(string transactionId) => await FindByCondition(c => c.TransactionId.ToLower().Equals(transactionId.ToLower()), false).SingleOrDefaultAsync() != null;

        public async Task<PagedList<Transaction>> GetAllTransactionsAsync(TransactionParameters transactionParameters, bool trackChanges)
        {
            var transcations = await FindAll(trackChanges)
            .FilterTransactionsByDateRange(transactionParameters.FromDate, transactionParameters.ToDate)
            .FilterTransactionsByStatus(transactionParameters.Status)
            .Search(transactionParameters.Currency)
            .Sort(transactionParameters.OrderBy!)
            .ToListAsync();

            return PagedList<Transaction>.ToPagedList(transcations, transactionParameters.PageNumber, transactionParameters.PageSize);
        }


        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsyncByCurrency(string currency, bool trackChanges) =>
            await FindByCondition(c => c.CurrencyCode!.ToLower().Equals(currency.ToLower()), trackChanges).OrderBy(c => c.TransactionId).ToListAsync();


        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsyncByDateRange(DateTime fromDate, DateTime toDate, bool trackChanges)
        {
            if (fromDate == default && toDate == default)
                return await FindAll(trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
            else if (fromDate != default && toDate != default && (fromDate == toDate))
                return await FindByCondition(doc => doc.TransactionDate.Date == fromDate.Date, trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
            else if (fromDate != default && toDate != default && fromDate != toDate)
                return await FindByCondition(doc => doc.TransactionDate.Date >= fromDate.Date && doc.TransactionDate.Date < toDate.Date, trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
            else
                return await FindByCondition(doc => doc.TransactionDate.Date >= fromDate.Date && doc.TransactionDate.Date <= toDate.Date, trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
        }


        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsyncByStatus(string status, bool trackChanges)
        {
            if (status.ToLower() == "a")
                return await FindByCondition(c => c.Status!.Equals(Status.Approved), trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
            else if (status.ToLower() == "r")
                return await FindByCondition(c => c.Status!.Equals(Status.Failed) || c.Status!.Equals(Status.Rejected), trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
            else if (status.ToLower() == "d")
                return await FindByCondition(c => c.Status!.Equals(Status.Finished) || c.Status!.Equals(Status.Done), trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
            else if (status.ToLower() == "failed")
                return await FindByCondition(c => c.Status!.Equals(Status.Failed), trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
            else if (status.ToLower() == "rejected")
                return await FindByCondition(c => c.Status!.Equals(Status.Rejected), trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
            else if (status.ToLower() == "finished")
                return await FindByCondition(c => c.Status!.Equals(Status.Finished), trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
            else if (status.ToLower() == "done")
                return await FindByCondition(c => c.Status!.Equals(Status.Done), trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
            else if (status.ToLower() == "approved")
                return await FindByCondition(c => c.Status!.Equals(Status.Approved), trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
            else
                return await FindByCondition(c => c.Status!.Equals(status), trackChanges).OrderBy(c => c.TransactionId).ToListAsync();
        }
    }
}
