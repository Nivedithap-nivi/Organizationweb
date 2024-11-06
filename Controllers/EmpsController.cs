using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organizationweb.DTO;
using Organizationweb.Models;

namespace Organizationweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpsController : ControllerBase
    {
        private readonly OrganizationContext _context;

        public EmpsController(OrganizationContext context)
        {
            _context = context;
        }

        // GET: api/Emps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emp>>> GetEmps()
        {
            return await _context.Emps.ToListAsync();
        }

        // GET: api/Emps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emp>> GetEmp(int id)
        {
            var emp = await _context.Emps.FindAsync(id);

            if (emp == null)
            {
                return NotFound();
            }

            return emp;
        }

        // PUT: api/Emps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmp(int id, EmployeeDTO empDTO)
        {
            Emp emp = new Emp();
            emp.EmpId=empDTO.EmpId;
            emp.EmpName=empDTO.EmpName;
            emp.DeptId=empDTO.DeptId;

            if (id != emp.EmpId)
            {
                return BadRequest();
            }

            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Emps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Emp>> PostEmp(EmployeeDTO empDTO)
        {
            Emp emp = new Emp();
            emp.EmpId = empDTO.EmpId;
            emp.EmpName = empDTO.EmpName;
            emp.DeptId = empDTO.DeptId;

            _context.Emps.Add(emp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmpExists(emp.EmpId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmp", new { id = emp.EmpId }, emp);
        }

        // DELETE: api/Emps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmp(int id)
        {
            var emp = await _context.Emps.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }

            _context.Emps.Remove(emp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpExists(int id)
        {
            return _context.Emps.Any(e => e.EmpId == id);
        }
    }
}
