using System;
using System.Collections.Generic;

namespace libretapp.Data.Entities;

public partial class Debt
{
    public int DebtId { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal RemainingAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateOnly? DueDate { get; set; }

    public bool? IsPaid { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Product Product { get; set; } = null!;
}
