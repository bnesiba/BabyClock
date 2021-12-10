using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NannyTimeAPI.Models;
using NannyTimeAPI.Repositories;
using NannyTimeAPI.Utils;
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
        [Route("SetState")]
        public bool  SetState(TimeState state)
        {
            return StorageRepository.SetState(state);
        }

        [HttpGet]
        [Route("GetState")]
        public TimeState GetCurrentState()
        {
            return StorageRepository.GetState();
        }

        [HttpGet]
        [Route("GetPaymentInfo")]
        public PaymentResult GetPaymentsForDays(DateTime startDate, DateTime endDate)
        {
            return PaymentUtil.GetPaymentInfo(startDate, endDate);

        }
    }
}
