
using System.ComponentModel.DataAnnotations;

namespace SalesSystem.Core.Entities;

public class User
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(16)]
    public string Phone { get; set; } = string.Empty;

    public ICollection<Order> Orders { get; set; } = [];
}
