using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyRental
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
