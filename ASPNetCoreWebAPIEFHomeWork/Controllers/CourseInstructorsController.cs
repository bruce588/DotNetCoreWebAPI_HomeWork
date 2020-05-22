using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNetCoreWebAPIEFHomeWork.Models;

namespace ASPNetCoreWebAPIEFHomeWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseInstructorsController : ControllerBase
    {
        private readonly ContosouniversityContext _context;

        public CourseInstructorsController(ContosouniversityContext context)
        {
            _context = context;
        }

        // GET: api/CourseInstructors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseInstructor>>> GetCourseInstructors()
        {
            return await _context.CourseInstructors.ToListAsync();
        }

        // GET: api/CourseInstructors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseInstructor>> GetCourseInstructor(int id)
        {
            var courseInstructor = await _context.CourseInstructors.FindAsync(id);

            if (courseInstructor == null)
            {
                return NotFound();
            }

            return courseInstructor;
        }

        // PUT: api/CourseInstructors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseInstructor(int id, CourseInstructor courseInstructor)
        {
            if (id != courseInstructor.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(courseInstructor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseInstructorExists(id))
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

        // POST: api/CourseInstructors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CourseInstructor>> PostCourseInstructor(CourseInstructor courseInstructor)
        {
            _context.CourseInstructors.Add(courseInstructor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseInstructorExists(courseInstructor.CourseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCourseInstructor", new { id = courseInstructor.CourseId }, courseInstructor);
        }

        // DELETE: api/CourseInstructors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseInstructor>> DeleteCourseInstructor(int id)
        {
            var courseInstructor = await _context.CourseInstructors.FindAsync(id);
            if (courseInstructor == null)
            {
                return NotFound();
            }

            _context.CourseInstructors.Remove(courseInstructor);
            await _context.SaveChangesAsync();

            return courseInstructor;
        }

        private bool CourseInstructorExists(int id)
        {
            return _context.CourseInstructors.Any(e => e.CourseId == id);
        }
    }
}
