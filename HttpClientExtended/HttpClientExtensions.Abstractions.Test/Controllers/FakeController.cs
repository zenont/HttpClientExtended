using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientExtensions.Abstractions.Test.Controllers
{
    [Route("api/test")]
    public class FakeController:Controller
    {

        [HttpGet("")]
        public IActionResult Get([FromQuery] int? id)
        {
            return Ok(new FakePayload
            {
                Id = id ?? -1
            });
        }
    }
}
