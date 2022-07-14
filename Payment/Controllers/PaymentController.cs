using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMq;

namespace Payment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController:ControllerBase
    {
        IEventBus eventBus;
        public PaymentController(IEventBus _eventBus)
        {
            this.eventBus = _eventBus;
        }

        [HttpGet("PaymentDetails")]
        public void GetPaymentDetails()
        {
            
            eventBus.publish("SurinderSharma");
            //"Surinder Sharma Payment";

        }
    }
}