using System;
using System.Collections.Generic;

namespace Library.WebApi.Models;

public partial class BorrowInfo
{
    public int BorrowId { get; set; }

    public int StudentId { get; set; }

    public int BookId { get; set; }

    public DateTime BorrowDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
