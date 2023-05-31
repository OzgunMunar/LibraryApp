using System;
using System.Collections.Generic;

namespace Library.WebApi.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string BookAuthor { get; set; } = null!;

    public string BookTitle { get; set; } = null!;

    public string BookPublisher { get; set; } = null!;

    public virtual ICollection<BorrowInfo> BorrowInfos { get; set; } = new List<BorrowInfo>();
}
