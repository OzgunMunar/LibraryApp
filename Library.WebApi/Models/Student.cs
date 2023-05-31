using System;
using System.Collections.Generic;

namespace Library.WebApi.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Grade { get; set; }

    public virtual ICollection<BorrowInfo> BorrowInfos { get; set; } = new List<BorrowInfo>();
}
