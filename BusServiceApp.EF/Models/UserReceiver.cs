﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusServiceApp.EF.Models
{
    public class UserReceiver
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
    }
}