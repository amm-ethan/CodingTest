using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record DateTimeDto
    {
        [Required]
        public DateTime FromDate { get; init; }

        [Required]
        public DateTime ToDate { get; init; }
    }
}
