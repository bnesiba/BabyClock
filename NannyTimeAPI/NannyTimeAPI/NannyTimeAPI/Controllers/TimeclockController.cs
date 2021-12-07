using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NannyTimeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyTimeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeclockController : ControllerBase
    {
        [HttpPost]
        public void SetState(TimeState state)
        {
            return; 
        }

        [HttpGet]
        public TimeState GetCurrentState()
        {
            return new TimeState();
        }
    }
}
