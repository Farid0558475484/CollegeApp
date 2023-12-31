﻿using System;
using System.Collections.Generic;
using CollegeApp.Models;
using Microsoft.AspNetCore.JsonPatch;
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

            return Ok(students);
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
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            // if(!ModelState.IsValid)
            //     return BadRequest(ModelState);

            if (model == null)
                return BadRequest();

            // if (model.AdmissionDate == DateTime.Now)
            // {
            //     ModelState.AddModelError("AdmissionDate", "Admission date cannot be the current date");
            //     return BadRequest(ModelState);
            // }

            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address
            };

            CollegeRepository.Students?.Add(student);
            model.Id = student.Id;
            return CreatedAtRoute("GetStudentById", new { id = student.Id }, model);
        }


        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
                return BadRequest();

            var existingStudent = CollegeRepository.Students?.Where(n => n.Id == model.Id).FirstOrDefault();
            if (existingStudent == null)
                return NotFound($"Student not found with the specified {model.Id} ");
            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;
            return NoContent();
        }
        
        
        [HttpPatch]
        [Route("{id}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                return BadRequest();

            var existingStudent = CollegeRepository.Students?.Where(n => n.Id == id).FirstOrDefault();
            if (existingStudent == null)
                return NotFound();
            
            var studentDTO = new StudentDTO()
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address
            };
            
            patchDocument.ApplyTo(studentDTO, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO.Address;
            

            return NoContent();
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