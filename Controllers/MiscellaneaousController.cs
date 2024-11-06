using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizationweb.Models;

namespace Organizationweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiscellaneaousController : ControllerBase
    {
        OrganizationContext em = new OrganizationContext();
        [HttpGet]

        public object GetAll()
        {
            var res = from emp in em.Emps
                      join dept in em.DeptTbls on emp.DeptId equals dept.DeptId
                      join lib in em.Libraries on dept.LibId equals lib.LibId
                      select new
                      {
                          Name = emp.EmpName,
                          Dept = dept.DeptName,
                          Liba = lib.LibName
                      };
            return res;

        }
    }
}
