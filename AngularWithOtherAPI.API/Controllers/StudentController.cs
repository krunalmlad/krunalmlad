using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AngularWithOtherAPI.API.Data.Models;
using AngularWithOtherAPI.API.Data.Repositories.IRepositories;
using AngularWithOtherAPI.API.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AngularWithOtherAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentRepository _repository;
        private ILogger<StudentController> _logger; 
        public StudentController(IStudentRepository repository, ILogger<StudentController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("get-all-student")]

        public IActionResult GetAllStudents()
        {
            try
            {
                _logger.LogInformation("Call API get data using GetAllStudents()");
                var list = _repository.GetStudentGridList();
                return Ok(list);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message +" Call API get with error in GetAllStudents()");
                return NotFound(ex.Message);
            }
        }

        [HttpPost("add-student")]
        public IActionResult AddStudent([FromBody] StudentViewModel model)
        {
            try
            {
                //if (StringStartWithNumber(model.FirstName)) throw new NameException("Standard name start with number ", model.FirstName);
                var objStudent = new Student()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    Address = model.Address,
                    City= model.City,
                    State = model.State,
                    CountryId = model.CountryId,
                    StandardId = model.StandardId
                };
                var result = _repository.Add(objStudent);
                _logger.LogInformation("Call for inserting data using AddStudent()");
                return Created(nameof(AddStudent),result);
            }
            //catch (NameException ex)
            //{
            //    _logger.LogInformation(ex.Message + " Error in AddStudent()");
            //    return BadRequest(ex.Message);
            //}
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message + "  - Error in inserting data using AddStudent()");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-student-by-id/{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _repository.GetById(id);
            if(student != null)
            {
                _logger.LogInformation("Call API get data using GetStudentById()");
                return Ok(student);
            }
            else
            {
                _logger.LogInformation("No record found for -" + id + " GetStudentById()");
                throw new Exception("This an exception that will be handle by middleware");
                //return NotFound();
            }
        }

        [HttpPut("update-student/{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] StudentViewModel studentVM)
        {
            try
            {
                var studentObj = new Student()
                {
                    FirstName = studentVM.FirstName,
                    LastName = studentVM.LastName,
                    Gender = studentVM.Gender,
                    Address = studentVM.Address,
                    City = studentVM.City,
                    State = studentVM.State,
                    CountryId = studentVM.CountryId,
                    StandardId = studentVM.StandardId
                };
                _repository.UpdateStudentModel(studentObj, id);
                _logger.LogInformation("Call for updating data using UpdateStudent()");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message + "  - Error in updating data using UpdateStudent()");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-student/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _repository.Delete(id);
                _logger.LogInformation("Student successfully deleted");
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogInformation("No record found for this student id " + id + ' ' + ex.Message);
                return BadRequest("No record found for this student id "+ id + ' ' + ex.Message);
            }
        }

        private bool StringStartWithNumber(string name)
        {
            return (Regex.IsMatch(name, @"^\d"));
        }
    }
}
