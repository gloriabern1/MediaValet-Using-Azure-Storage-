using AzureStorageServices.Entities;
using AzureStorageServices.Services;
using SupervisorAPI.Extension;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SupervisorAPI.Models
{

    public class OrderController : ApiController
    {

        private readonly ITableService _tableService;
        public OrderController()
        {
            // Would use dependency Injection is real projects
            _tableService = new TableService();
        }

        // POST api/order
        public async Task<IHttpActionResult> PostOrder([System.Web.Http.FromBody]OrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Orders orders = new Orders( order.OrderText);

            bool result = await orders.ProcessOrder();
            Console.WriteLine($"Send order {orders.OrderId} with random number {orders.RandomNumber}");

            if (result) return Ok();

            return Conflict();
        }

        // GET api/order
        public async Task<IEnumerable<ConfirmationDTO>> GetOrderConfirmation()
        {
            IEnumerable<ConfirmationDTO> confirmationDTO = new List<ConfirmationDTO>();
            IEnumerable<Confirmations> confirmations = await _tableService.GetConfirmationOrders();

            if (confirmations == null)
            {
                Console.WriteLine("No Orders have been processed");
            }
            else
            {
                confirmationDTO = confirmations.ToDTO();
           
            }

           
            return confirmationDTO ;
        }
    }
}