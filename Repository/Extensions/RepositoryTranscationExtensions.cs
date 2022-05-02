using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class RepositoryTranscationExtensions
    {
        public static IQueryable<Transaction> FilterTransactionsByDateRange(this IQueryable<Transaction> transactions, DateTime fromDate, DateTime toDate)
        {
            if (fromDate == default && toDate == default)
                return transactions;
            else if (fromDate != default && toDate != default && (fromDate == toDate))
                return transactions.Where(doc => doc.TransactionDate.Date == fromDate.Date);
            else if (fromDate != default && toDate != default && fromDate != toDate)
                return transactions.Where(doc => doc.TransactionDate.Date >= fromDate.Date && doc.TransactionDate.Date < toDate.Date);
            else
                return transactions.Where(doc => doc.TransactionDate.Date >= fromDate.Date && doc.TransactionDate.Date <= toDate.Date);
        }

        public static IQueryable<Transaction> FilterTransactionsByStatus(this IQueryable<Transaction> transactions, string? status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return transactions;

            if (status.ToLower() == "a")
                return transactions.Where(c => c.Status.Equals(Status.Approved));
            else if (status.ToLower() == "r")
                return transactions.Where(c => c.Status.Equals(Status.Failed) || c.Status!.Equals(Status.Rejected));
            else if (status.ToLower() == "d")
                return transactions.Where(c => c.Status.Equals(Status.Finished) || c.Status!.Equals(Status.Done));
            else if (status.ToLower() == "failed")
                return transactions.Where(c => c.Status.Equals(Status.Failed));
            else if (status.ToLower() == "rejected")
                return transactions.Where(c => c.Status.Equals(Status.Rejected));
            else if (status.ToLower() == "finished")
                return transactions.Where(c => c.Status.Equals(Status.Finished));
            else if (status.ToLower() == "done")
                return transactions.Where(c => c.Status.Equals(Status.Done));
            else if (status.ToLower() == "approved")
                return transactions.Where(c => c.Status.Equals(Status.Approved));
            else
                return transactions.Where(c => c.Status.Equals(status));
        }

        public static IQueryable<Transaction> Search(this IQueryable<Transaction> transactions, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return transactions;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return transactions.Where(e => e.CurrencyCode!.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Transaction> Sort(this IQueryable<Transaction> transactions, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return transactions.OrderBy(e => e.TransactionId);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Transaction>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return transactions.OrderBy(e => e.TransactionId);
            return transactions.OrderBy(orderQuery);
        }
    }
}