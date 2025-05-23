﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyRental
{
    public enum Category { Decorations, Audio, Catering }

    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public decimal PricePerDay { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}