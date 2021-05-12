using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AzureStorageServices.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrderAPI.Models;


namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        public IConfiguration config;
        public OrderController(IConfiguration _config)
        {
            config = _config;
        }

        [HttpPost]
        public async Task<ActionResult> PostOrder([FromBody]OrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var check = config.GetConnectionString("DefaultConnection");
            var output = ConfigurationManager.AppSettings["DefaultConnection"];
            Orders orders = new Orders(order.OrderId, order.OrderText);

            bool result = await orders.ProcessOrder();
            Console.WriteLine($"Send order {orders.OrderId} with random number {orders.RandomNumber}");

            if (result) return Ok();
            return Conflict();
        }
    }
}