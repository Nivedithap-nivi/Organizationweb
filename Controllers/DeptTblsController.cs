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
    public class DeptTblsController : ControllerBase
    {
        private readonly OrganizationContext _context;

        public DeptTblsController(OrganizationContext context)
        {
            _context = context;
        }

        // GET: api/DeptTbls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeptTbl>>> GetDeptTbls()
        {
            return await _context.DeptTbls.ToListAsync();
        }

        // GET: api/DeptTbls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeptTbl>> GetDeptTbl(int id)
        {
            var deptTbl = await _context.DeptTbls.FindAsync(id);

            if (deptTbl == null)
            {
                return NotFound();
            }

            return deptTbl;
        }

        // PUT: api/DeptTbls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeptTbl(int id, DepartmentDTO deptTblDTO)
        {
            DeptTbl deptTbl = new DeptTbl();
            deptTbl.DeptId =deptTblDTO.DeptId;
            deptTbl.DeptName =deptTblDTO.DeptName;
            deptTbl.LibId =deptTblDTO.LibId;

            if (id != deptTbl.DeptId)
            {
                return BadRequest();
            }

            _context.Entry(deptTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeptTblExists(id))
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

        // POST: api/DeptTbls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeptTbl>> PostDeptTbl(DepartmentDTO deptTblDTO)
        {
            DeptTbl deptTbl = new DeptTbl();
            deptTbl.DeptId = deptTblDTO.DeptId;
            deptTbl.DeptName = deptTblDTO.DeptName;
            deptTbl.LibId = deptTblDTO.LibId;

            _context.DeptTbls.Add(deptTbl);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeptTblExists(deptTbl.DeptId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDeptTbl", new { id = deptTbl.DeptId }, deptTbl);
        }

        // DELETE: api/DeptTbls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeptTbl(int id)
        {
            var deptTbl = await _context.DeptTbls.FindAsync(id);
            if (deptTbl == null)
            {
                return NotFound();
            }

            _context.DeptTbls.Remove(deptTbl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeptTblExists(int id)
        {
            return _context.DeptTbls.Any(e => e.DeptId == id);
        }
    }
}
