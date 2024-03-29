using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentTest.Data;
using StudentTest.Models;

namespace StudentTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        #region GetStudentList
        [HttpGet]
        [Route("GetStudentList")]
        public async Task<IActionResult> GetStudentList()
        {
            var studentsList = _context.Students.ToList();
            return studentsList.Count > 0 ? Ok(studentsList) : NotFound();
        }
        #endregion

        #region GetStudentDetails
        [HttpGet]
        [Route("GetStudentDetails")]
        public async Task<IActionResult> GetStudentDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        #endregion

        #region CreateStudent
        [HttpPost]
        [Route("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] Student studentData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentData);
                await _context.SaveChangesAsync();
            }
            return Ok(studentData);
        }
        #endregion

        #region DeleteStudent
        [HttpDelete]
        [Route("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == 0 || id < 0)
                {
                    return NotFound();
                }
                else
                {
                    // find student
                    var student = await _context.Students.FindAsync(id);
                    if (student == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        _context.Students.Remove(student);
                        await _context.SaveChangesAsync();
                        return Ok("Student Deleted Successfully");
                    }
                }
            }
            return BadRequest("Internal Server Error");
        }
        #endregion

        #region UpdateStudent
        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody]Student student)
        {
            if (id != student.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    return Ok(string.Concat("Student with Student id {0} has been updated", student.id));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Students.Any(e => e.id == student.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return BadRequest("Internal Server Error");
        }
        #endregion
    }
}
