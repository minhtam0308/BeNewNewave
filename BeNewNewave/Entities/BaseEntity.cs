using System.ComponentModel.DataAnnotations;

namespace BeNewNewave.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }


        public bool IsDeleted { get; set; } = false;

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
