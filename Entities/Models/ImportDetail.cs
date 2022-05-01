using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [Serializable]
    public class ImportDetail
    {
        [Key]
        public Guid Guid { get; set; }
        public string? Filename { get; set; }
        public byte[]? Detail { get; set; }
        public DateTime ImportedDate { get; set; } = DateTime.Now;
        public bool IsSuccess { get; set; } = false;

        public ICollection<Transaction>? Transactions { get; set; }
    }
}
