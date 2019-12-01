using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Closest.Models
{
    public class input
    {
        [Required]

        [MaxLength(140)]

        public string Address { get; set; }
    }
}