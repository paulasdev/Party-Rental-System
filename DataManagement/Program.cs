using PartyRental;
using System;

namespace DataManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RentalData db = new RentalData();

            using (db)
            {
                Equipment e1 = new Equipment() { EquipmentId = 1, Name = "Balloon Pack", Category = Category.Decorations, PricePerDay = 15 };
                Booking b1 = new Booking() { BookingId = 1, EquipmentId = 1, Equipment = e1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2) };
                Booking b2 = new Booking() { BookingId = 2, EquipmentId = 1, Equipment = e1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2) };

                Equipment e2 = new Equipment { EquipmentId = 2, Name = "Speaker Set", Category = Category.Audio, PricePerDay = 40 };
                Booking b3 = new Booking() { BookingId = 3, EquipmentId = 2, Equipment = e2, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2) };
                Booking b4 = new Booking() { BookingId = 4, EquipmentId = 2, Equipment = e2, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2) };

                Equipment e3 = new Equipment { EquipmentId = 3, Name = "Buffet Table", Category = Category.Catering, PricePerDay = 60 };
                Booking b5 = new Booking() { BookingId = 5, EquipmentId = 3, Equipment = e3, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2) };
                Booking b6 = new Booking() { BookingId = 6, EquipmentId = 3, Equipment = e3, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2) };

                db.Equipments.Add(e1);
                db.Equipments.Add(e2);
                db.Equipments.Add(e3);

                Console.WriteLine("Added equipament to database");

                db.Bookings.Add(b1);
                db.Bookings.Add(b2);
                db.Bookings.Add(b3);
                db.Bookings.Add(b4);
                db.Bookings.Add(b5);
                db.Bookings.Add(b6);

                Console.WriteLine("Added booking to database");

                db.SaveChanges();

                Console.WriteLine("Saved to database");
            }
        }
    }
}
