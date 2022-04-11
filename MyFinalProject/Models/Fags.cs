using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Models
{
    public class Fags
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:500)]
        public string Information { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Title { get; set; }

        public bool isShipping { get; set; }
        public bool isOrder { get; set; }
        public bool isPayment { get; set; }
        
    }
}
