using System;
using System.Collections.Generic;

namespace Organizationweb.Models;

public partial class Library
{
    public int LibId { get; set; }

    public string LibName { get; set; } = null!;

    public short? Ui { get; set; }

    public virtual ICollection<DeptTbl> DeptTbls { get; set; } = new List<DeptTbl>();
}
