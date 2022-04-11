using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:30)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }

    }
}
