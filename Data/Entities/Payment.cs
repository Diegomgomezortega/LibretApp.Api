using System;
using System.Collections.Generic;

namespace libretapp.Data.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int DebtId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Note { get; set; }

    public virtual Debt Debt { get; set; } = null!;
}
