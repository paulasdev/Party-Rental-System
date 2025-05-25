using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;

namespace PartyRental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RentalData db = new RentalData();
        private Equipment selectedEquipment;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbxCategory.ItemsSource = Enum.GetValues(typeof(Category))
                                          .Cast<object>()
                                          .Prepend("All")
                                          .ToList(); 
            cbxCategory.SelectedIndex = 0;

            dpStartDate.SelectedDate = DateTime.Today;
            dpEndDate.SelectedDate = DateTime.Today.AddDays(2);

            LoadAllEquipment();
            LoadBookings();

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string selected = cbxCategory.SelectedItem.ToString();

            List<Equipment> equipmentList;

            if (selected == "All")
            {
                // Show all equipment
                equipmentList = db.Equipments.ToList();
            }
            else
            {
                // Filter by selected category only
                equipmentList = db.Equipments
                    .Where(eq => eq.Category.ToString() == selected)
                    .ToList();
            }

            lbxResults.ItemsSource = equipmentList;
        }

        private void lbxResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEquipment = lbxResults.SelectedItem as Equipment;

            if (selectedEquipment != null)
            {
                string rentalStart = dpStartDate.SelectedDate?.ToString("dd/MM/yyyy") ?? "-";
                string rentalEnd = dpEndDate.SelectedDate?.ToString("dd/MM/yyyy") ?? "-";

                txtDetails.Text = $"Equipment ID: {selectedEquipment.EquipmentId}\n" +
                                  $"Type: {selectedEquipment.Category}\n" +
                                  $"Equipment: {selectedEquipment.Name}\n" +
                                  $"Rental Date: {rentalStart}\n" +
                                  $"Return Date: {rentalEnd}";

                if (!string.IsNullOrWhiteSpace(selectedEquipment.ImagePath))
                {
                    try
                    {
                        // Build full path to image in the 'Images' folder
                        string basePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                        string fullImagePath = System.IO.Path.Combine(basePath, selectedEquipment.ImagePath);

                        if (File.Exists(fullImagePath))
                        {
                            imgEquipment.Source = new BitmapImage(new Uri(fullImagePath));
                            imgEquipment.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            MessageBox.Show($"Image not found: {fullImagePath}");
                            imgEquipment.Source = null;
                            imgEquipment.Visibility = Visibility.Collapsed;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}");
                        imgEquipment.Source = null;
                        imgEquipment.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    imgEquipment.Source = null;
                    imgEquipment.Visibility = Visibility.Collapsed;
                }
            }
        }
        
        private void dpStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
{
             lbxResults_SelectionChanged(null, null);
}

        private void dpEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
{
             lbxResults_SelectionChanged(null, null);
}

        private void Book_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEquipment == null || dpStartDate.SelectedDate == null || dpEndDate.SelectedDate == null) return;

            var newBooking = new Booking
            {
                EquipmentId = selectedEquipment.EquipmentId,
                StartDate = dpStartDate.SelectedDate.Value,
                EndDate = dpEndDate.SelectedDate.Value
            };

            db.Bookings.Add(newBooking);
            db.SaveChanges();

            MessageBox.Show($"Booking Confirmation\n\n" +
                            $"ID: {selectedEquipment.EquipmentId}\n" +
                            $"Name: {selectedEquipment.Name}\n" +
                            $"Category: {selectedEquipment.Category}\n" +
                            $"Rental Date: {newBooking.StartDate:d}\n" +
                            $"Return Date: {newBooking.EndDate:d}");

            LoadBookings();
            LoadAllEquipment();
        }

        private void LoadAllEquipment()
        {
            var all = db.Equipments.ToList();
            var tableData = all.Select(equip => new
            {
                Id = equip.EquipmentId,
                Name = equip.Name,
                Category = equip.Category.ToString(),
                Bookings = equip.Bookings.Count
            }).ToList();

            dataGridAllEquipment.ItemsSource = tableData;
        }

        private void LoadBookings()
        {
            var bookings = db.Bookings.Include("Equipment").ToList();
            var tableData = bookings.Select(bk => new
            {
                BookingID = bk.BookingId,
                StartDate = bk.StartDate,
                EndDate = bk.EndDate,
                EquipmentID = bk.EquipmentId,
                Name = bk.Equipment.Name,
                Category = bk.Equipment.Category.ToString()
            }).ToList();

            dataGridBookings.ItemsSource = tableData;
        }

        private void DeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridBookings.SelectedItem == null) return;

            dynamic selectedRow = dataGridBookings.SelectedItem;
            int bookingId = selectedRow.BookingID;

            var bookingToDelete = db.Bookings.Find(bookingId);
            if (bookingToDelete != null)
            {
                db.Bookings.Remove(bookingToDelete);
                db.SaveChanges();
                LoadBookings();
                LoadAllEquipment();
            }
        }

     }
}
