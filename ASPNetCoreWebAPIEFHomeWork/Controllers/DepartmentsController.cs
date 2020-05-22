using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNetCoreWebAPIEFHomeWork.Models;
using Microsoft.Data.SqlClient;

namespace ASPNetCoreWebAPIEFHomeWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ContosouniversityContext _context;

        public DepartmentsController(ContosouniversityContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.Where(x=>x.IsDeleted==false).ToListAsync();
        }


        // GET: api/DepartmentCourseCount (VwDepartmentCourseCount)
        [HttpGet("DepartmentCourseCount")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> GetDepartmentCourseCount()
        {
            //https://www.learnentityframeworkcore.com/raw-sql

            string sql = " select * from VwDepartmentCourseCount";
          

           
            return await _context.VwDepartmentCourseCounts.FromSqlRaw(sql).ToListAsync();
        }


        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null || department.IsDeleted)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
             

            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            using (SqlConnection sql = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                //copy from https://gavilan.blog/2019/05/10/asp-net-core-using-stored-procedures-with-ado-net/
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Department_Update]", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@DepartmentID", id));
                        cmd.Parameters.Add(new SqlParameter("@Name", department.Name));
                        cmd.Parameters.Add(new SqlParameter("@Budget", department.Budget));
                        cmd.Parameters.Add(new SqlParameter("@StartDate", department.StartDate));
                        cmd.Parameters.Add(new SqlParameter("@InstructorID", department.InstructorId));
                        cmd.Parameters.Add(new SqlParameter("@RowVersion_Original", department.RowVersion));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                    }
                }
                catch(Exception ex)
                {

                    if (!DepartmentExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
           

            using (SqlConnection sql = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Department_Insert]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", department.Name));
                    cmd.Parameters.Add(new SqlParameter("@Budget", department.Budget));
                    cmd.Parameters.Add(new SqlParameter("@StartDate", department.StartDate));
                    cmd.Parameters.Add(new SqlParameter("@InstructorID", department.InstructorId));
                  
                     
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                  
                }
            }
            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null || department.IsDeleted)
            {
                return NotFound();
            } 
 

          using (SqlConnection sql = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                //copy from https://gavilan.blog/2019/05/10/asp-net-core-using-stored-procedures-with-ado-net/
                try
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Department_Delete]", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@DepartmentID", id));
                        cmd.Parameters.Add(new SqlParameter("@RowVersion_Original", department.RowVersion));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                    }
                }
                catch(Exception ex)
                {

                    if (!DepartmentExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }


            return department;
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }
    }
}
