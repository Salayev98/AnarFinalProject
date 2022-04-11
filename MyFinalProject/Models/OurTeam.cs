using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Models
{
    public class OurTeam
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public string GoogleUrl { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
