namespace Shared.DataTransferObjects
{
    public record TransactionDto
    {
        public string? Id { get; init; }
        public string? Payment { get; init; }
        public string? Status { get; init; }
    }

    public record TransactionCreationDto
    {
        public string? TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string? CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Status { get; set; }
    }

}
