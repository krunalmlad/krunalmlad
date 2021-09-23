using AngularWithOtherAPI.API.Data.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private ILogsRespository _repository;
        public LogsController(ILogsRespository repository)
        {
            _repository = repository;
        }

        [HttpGet("get-all-logs-from-db")]
        public IActionResult GetAllLogsFromDB()
        {
            try
            {
                var allLog = _repository.GetAll();
                return Ok(allLog);
            }
            catch (Exception ex)
            {
                return BadRequest("Could not load log from the db " + ex.Message);
            }
        }
    }
}
