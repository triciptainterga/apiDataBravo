﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiBravo.Models
{
    public partial class TrmChannel
    {
        public string Status { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Na { get; set; }
        public string TicketIdcode { get; set; }
        public string UserCreate { get; set; }
        public DateTime? DateCreate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
    }
}