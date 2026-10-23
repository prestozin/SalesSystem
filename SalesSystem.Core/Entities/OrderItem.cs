using System.ComponentModel.DataAnnotations.Schema;

namespace SalesSystem.Core.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }

    public int ProductId { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(11,2)")]
    public decimal UnitPrice { get; set; }

    [NotMapped]  //nao armazena no banco
    public decimal Subtotal => Quantity * UnitPrice;


    public Product Product { get; set; } = null!;
    public Order Order { get; set; } = null!;
}