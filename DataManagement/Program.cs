using PartyRental;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace DataManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (var db = new RentalData())
            {
              
      


                var e1 = new Equipment { Name = "Balloon Pack", Category = Category.Decorations, PricePerDay = 15, ImagePath = "balloons.png" };
                var e2 = new Equipment { Name = "Speaker Set", Category = Category.Audio, PricePerDay = 40, ImagePath = "speakerset.png" };
                var e3 = new Equipment { Name = "Buffet Table", Category = Category.Catering, PricePerDay = 60, ImagePath = "BuffetTable.png" };

                db.Equipments.AddRange(new[] { e1, e2, e3 });
                db.SaveChanges();
            }
        }
    }
}
