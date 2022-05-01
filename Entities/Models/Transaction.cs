using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Transaction
    {
        [Key]
        public Guid Guid { get; set; }

        [JsonPropertyName("Transaction Id")]
        [Required(ErrorMessage = "Transaction Id is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Transaction Id is 50 characters.")]
        public string? TransactionId { get; set; }

        [Required(ErrorMessage = "Amount is a required field.")]
        [Column(TypeName = "decimal(14, 2)")]
        public decimal Amount { get; set; }

        [JsonPropertyName("Currency Code")]
        [Required(ErrorMessage = "Currency Code is a required field.")]
        public string? CurrencyCode { get; set; }

        [JsonPropertyName("Transaction Date")]
        [Required(ErrorMessage = "Transaction Date is a required field.")]
        public DateTime TransactionDate { get; set; }

        [Required(ErrorMessage = "Status is a required field.")]
        public Status Status { get; set; }

        [ForeignKey(nameof(ImportDetail))]
        public Guid ImportDetailGuid { get; set; }
        public ImportDetail? ImportDetail { get; set; }
    }

    public enum Status
    {
        Approved,
        Failed,
        Finished,
        Rejected,
        Done
    }
}
