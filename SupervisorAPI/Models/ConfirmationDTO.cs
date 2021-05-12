using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupervisorAPI.Models
{
    public class ConfirmationDTO
    {
        public int OrderId { get; set; }
        public Guid AgentId { get; set; }
        public string OrderStatus { get; set; }
    }
}