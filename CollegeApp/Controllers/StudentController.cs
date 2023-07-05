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
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            var students = CollegeRepository.Students.Select(s => new StudentDTO()
                {
                    Id = s.Id,
                    StudentName = s.StudentName,
                    Email = s.Email,
                    Address = s.Address,
                }
            );

            return Ok(CollegeRepository.Students);
        }


        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<Student> GetStudentbyId(int id)


        {
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students?.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound("Student not found with the specified id ");

            return Ok(student);
        }


        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<Student> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = CollegeRepository.Students.Find(n => n.StudentName == name);
            if (student == null)
                return NotFound("Student not found with the specified name ");
            return Ok(student);
        }
        
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<Student> CreateStudent([FromBody] Student model)
        {
            if (model == null)
                return BadRequest();

            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address
            };

            CollegeRepository.Students?.Add(student);
            model.Id=student.Id;
            return Ok(model);

        }


        [HttpDelete]
        [Route("{id:int}", Name = "DeleteStudentById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest();


            var student = CollegeRepository.Students?.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"Student not found with the specified {id} ");


            CollegeRepository.Students?.Remove(student);
            return Ok();
        }
    }
}