using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Models
{
    public class OrderDto
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string OrderText { get; set; }
    }
}
