using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMq;
namespace Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController:ControllerBase
    {
        IEventBus eventBus;
        public OrderController(IEventBus _eventBus)
        {
            this.eventBus = _eventBus;
        }

        [HttpGet("OrderDetails")]
        public string Details()
        {
            GetOrderDetails();
            return "Surinder Sharma Order";
        }

        private void GetOrderDetails()
        {
            eventBus.subscribe();
        }
    }
}