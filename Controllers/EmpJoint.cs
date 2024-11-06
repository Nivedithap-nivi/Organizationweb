using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizationweb.Models;

namespace Organizationweb.Controllers
{

    public class EmpJointResult
    {
        public int DeptId { get; set; }

        public int EmployeeId { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class EmpJoint : ControllerBase
    {
        private readonly OrganizationContext _context;

        public EmpJoint(OrganizationContext context)
        {
            _context=context;
        }

        [HttpGet]

        public List<EmpJointResult> GetEmpJointResults()
        {
            var result = (from emp in _context.Emps
                          join dept in _context.DeptTbls on emp.DeptId equals dept.DeptId 
                          select new EmpJointResult { EmployeeId = emp.EmpId, DeptId = dept.DeptId }).ToList();
            return result;
        }
    }
}
