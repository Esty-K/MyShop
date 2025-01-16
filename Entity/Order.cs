using System;
using System.Collections.Generic;

namespace Entity;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime? Date { get; set; }

    public double? Sum { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
