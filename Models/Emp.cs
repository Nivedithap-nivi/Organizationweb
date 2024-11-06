using System;
using System.Collections.Generic;

namespace Organizationweb.Models;

public partial class Emp
{
    public int EmpId { get; set; }

    public string EmpName { get; set; } = null!;

    public int DeptId { get; set; }

    public virtual DeptTbl Dept { get; set; } = null!;
}
