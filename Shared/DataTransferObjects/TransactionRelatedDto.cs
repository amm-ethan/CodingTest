using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObjects
{
    public record TransactionDto
    {
        public string? Id { get; init; }
        public string? Payment { get; init; }
        public string? Status { get; init; }
    }

    public class TransactionCreationDto
    {
        public string? TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string? CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Status { get; set; }
    }

    public class TransactionErrorDto
    {
        public string? Titile { get; set; } = "One or more validation errors occured";
        public List<TransactionSubError>? Details { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }

    public class TransactionSubError
    {
        [JsonPropertyName("Position(row,column)")]
        public string? Position { get; set; }
        public string? Header { get; set; }
        public string? Value { get; set; }
        public string? Error { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}