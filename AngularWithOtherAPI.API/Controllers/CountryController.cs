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
using AngularWithOtherAPI.API.ActionResults;

namespace AngularWithOtherAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryRepository _repository;
        private ILogger<CountryController> _logger;
        public CountryController(ICountryRepository repository, ILogger<CountryController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("get-country-dropdown-list")]
        public IActionResult GetCountryDropdownList()
        {
            try
            {
                var list = _repository.GetCountriesList();
                _logger.LogInformation("Call API get data using GetCountriesList()");
                return Ok(list);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + " Call API get with error in GetCountryDropdownList()");
                return NotFound(ex.Message);
            }
        }

       [HttpPost("add-country")]
       public IActionResult AddCountry([FromBody]CountryViewModel model)
        {
            try
            {
                if (StringStartWithNumber(model.Name)) throw new NameException("Country name start with number ", model.Name);
                var objCountry = new Country()
                {
                    Name = model.Name,
                    IsActive = model.IsActive
                };
                var result = _repository.Add(objCountry);
                _logger.LogInformation("Call for inserting data using AddCountry()");
                return Created(nameof(AddCountry), result);
            }
            catch (NameException ex)
            {
                _logger.LogInformation(ex.Message + " Error in AddCountry()");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message + "  - Error in inserting data using AddCountry()");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-country-by-id/{id}")]
        public IActionResult GetCountryById(int id)
        {
            try
            {
                var country = _repository.GetById(id);
                if (country != null)
                {
                    _logger.LogInformation("Call API get data using GetCountryById()");
                    return Ok(country);
                }
                else
                {
                    _logger.LogInformation("No record found for -" + id + " GetCountryById()");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message + " Call API get with error in GetCountryById()");
                return NotFound();
            }
        }

        [HttpDelete("delete-country/{id}")]
        public IActionResult DeleteCountry(int id)
        {
            try
            {
                _repository.Delete(id);
                _logger.LogInformation("Country successfully deleted");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("No record found for this country id " + id + ' ' + ex.Message);
                return BadRequest("No record found for this country id " + id + ' ' + ex.Message);
            }
        }

        [HttpGet("get-country-by-custom-result-id/{id}")]
        public CustomActionResult GetCountryByCustom(int id)
        {
            var country = _repository.GetById(id);
            if (country != null)
            {
                var responseObj = new CustomActionResultViewModel
                {
                    Country = country
                };
                return new CustomActionResult(responseObj);
            }
            else
            {
                var responseObj = new CustomActionResultViewModel
                {
                    Exception = new Exception("This is comming from get Country controller")
                };
                return new CustomActionResult(responseObj);
            }
        }

        private bool StringStartWithNumber(string name)
        {
            return (Regex.IsMatch(name, @"^\d"));
        }
    }
}
