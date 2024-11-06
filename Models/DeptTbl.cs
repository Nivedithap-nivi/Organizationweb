using System;
using System.Collections.Generic;

namespace Organizationweb.Models;

public partial class DeptTbl
{
    public int DeptId { get; set; }

    public string DeptName { get; set; } = null!;

    public int LibId { get; set; }

    public virtual ICollection<Emp> Emps { get; set; } = new List<Emp>();

    public virtual Library Lib { get; set; } = null!;
}
