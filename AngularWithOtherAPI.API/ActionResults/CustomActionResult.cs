using AngularWithOtherAPI.API.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.ActionResults
{
    public class CustomActionResult : IActionResult
    {
        private readonly CustomActionResultViewModel _customAction;

        public CustomActionResult(CustomActionResultViewModel customAction)
        {
            _customAction = customAction;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_customAction.Exception ?? _customAction.Country as object) 
            { 
                StatusCode = _customAction.Exception != null ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK,
            };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
