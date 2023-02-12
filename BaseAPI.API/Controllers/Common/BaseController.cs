using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BaseAPI.API.Controllers.Common
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ResponseOK(object result = null)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            if (result == null)
                return Ok();

            return Ok(result);
        }

        protected IActionResult ResponseFail(Exception ex = null)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            if (ex == null)
                return BadRequest();

            return Ok(new FailDto(ex));
        }

        protected string GetClaim(string claimType) => User?.Claims?.FirstOrDefault(x => x.Type == claimType)?.Value;
    }
}
