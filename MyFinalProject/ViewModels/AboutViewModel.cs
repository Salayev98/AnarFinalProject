using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.ViewModels
{
    public class AboutViewModel
    {
        public Dictionary<string,string> Settings { get; set; }
        public List<OurTeam> OurTeams { get; set; }
    }
}
