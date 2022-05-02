namespace Shared.RequestFeatures.Models
{
    public class TransactionParameters : RequestParameters
    {
        public TransactionParameters() => OrderBy = "TransactionId";

        public string? Currency { get; set; }
        public string? Status { get; set; }
        public string? OrderBy { get; set; }
        public DateTime FromDate { get; init; }
        public DateTime ToDate { get; init; }

    }
}
