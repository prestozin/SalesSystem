using System.ComponentModel.DataAnnotations.Schema;

namespace SalesSystem.Core.Entities;

public class Order
{
    public int Id { get; set; }
    public int StatusId { get; set; }

    [Column(TypeName = "decimal(11,2)")]
    public decimal Total { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public OrderStatus Status { get; set; } = null!;

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}