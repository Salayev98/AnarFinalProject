﻿using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.ViewModels
{
    public class MemberProfileViewModel
    {
        public MemberUpdateViewModel Member { get; set; }
        public List<Order> Orders { get; set; }
    }
}
