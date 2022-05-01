using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class ImportDetail
    {
        [Key]
        public Guid Guid { get; set; }
        public string? Filename { get; set; }
        public DateTime ImportedDate { get; set; } = DateTime.Now;
        public bool IsSuccess { get; set; } = false;

        [ForeignKey(nameof(Transaction))]
        public Guid TransactionGuid { get; set; }
        public Transaction? Transaction { get; set; }
    }
}
