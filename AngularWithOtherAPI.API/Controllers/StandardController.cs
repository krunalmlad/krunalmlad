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
using AngularWithOtherAPI.API.Exceptions;

namespace AngularWithOtherAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandardController : ControllerBase
    {
        private IStandardRepository _repository;
        private ILogger<StandardController> _logger;
        public StandardController(IStandardRepository repository, ILogger<StandardController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("get-standard-dropdown-list")]
        public IActionResult GetStandardDropdownList()
        {
            try
            {
                var list = _repository.GetStandardList();
                _logger.LogInformation("Call API get data using GetStandardDropdownList()");
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message + " Call API get with error in GetStandardDropdownList()");
                return NotFound(ex.Message);
            }
        }

        [HttpPost("add-standard")]
        public IActionResult AddStandard([FromBody] StandardViewModel model)
        {
            try
            {
                if (StringStartWithNumber(model.Name)) throw new NameException("Standard name start with number ", model.Name);
                var objStandard = new Standard()
                {
                    Name = model.Name,
                    IsActive = model.IsActive
                };
                var result = _repository.Add(objStandard);
                _logger.LogInformation("Call for inserting data using AddStandard()");
                return Created(nameof(AddStandard), result);
            }
            catch (NameException ex)
            {
                _logger.LogInformation(ex.Message + " Error in AddStandard()");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message + "  - Error in inserting data using AddStandard()");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-standard-by-id/{id}")]
        public IActionResult GetStandardById(int id)
        {
           // try
            //{
                throw new Exception("This is an exception that will be handle by middleware");
                var standard = _repository.GetById(id);
                if (standard != null)
                {
                    _logger.LogInformation("Call API get data using GetStandardById()");
                    return Ok(standard);
                }
                else
                {
                    _logger.LogInformation("No record found for -" + id + " GetStandardById()");
                    return NotFound();
                }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogInformation(ex.Message + " Call API get with error in GetStandardById()");
            //    return NotFound();
            //}
        }

        [HttpDelete("delete-standard/{id}")]
        public IActionResult DeleteStandard(int id)
        {
            try
            {
                _repository.Delete(id);
                _logger.LogInformation("Standard successfully deleted");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("No record found for this standard id " + id + ' ' + ex.Message);
                return BadRequest("No record found for this standard id " + id + ' ' + ex.Message);
            }
        }

        private bool StringStartWithNumber(string name)
        {
            return (Regex.IsMatch(name, @"^\d"));
        }
    }
}
