using System;
using System.Collections.Generic;
using CollegeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(CollegeRepository.Students);
        }


        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        public ActionResult<Student> GetStudentbyId(int id)


        {
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound("Student not found with the specified id ");

            return Ok(student);
        }


        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        public ActionResult<Student> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = CollegeRepository.Students.Find(n => n.StudentName == name);
            if (student == null)
                return NotFound("Student not found with the specified name ");
            return Ok(student);
        }


        [HttpDelete]
        [Route("{id:int}", Name = "DeleteStudentById")]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest();


            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"Student not found with the specified {id} ");
           

            CollegeRepository.Students.Remove(student);
            return Ok();
        }
    }
}