using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyRental
{
    public class RentalData : DbContext
    {
        public RentalData() : base("MyRentalData") { }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    
    }
}
