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
        public IEnumerable<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student
                {
                    Id = 1,
                    StudentName = "John",
                    Email = "john@example.com",
                    Address = "123 Main St"
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Mary",
                    Email = "mary@example.com",
                    Address = "456 Elm St"
                }
                
            };
        }
    }
}