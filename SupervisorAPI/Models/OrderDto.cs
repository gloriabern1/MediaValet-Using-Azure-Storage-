﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupervisorAPI.Models
{
    public class OrderDto
    {
        [Required]
        public string OrderText { get; set; }
    }
}
