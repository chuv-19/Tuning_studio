using System;
using System.Collections.Generic;           // <-- правильное пространство имён для IEnumerable<>
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.SQLite; // Подключи пакет System.Data.SQLite через NuGet
using System.IO;

namespace Prototype
{
    public partial class ServicesPage : Page
    {
        public ObservableCollection<ServiceViewModel> Services { get; set; }

        private string currentUserId = "user-123"; // Для примера

        private string dbPath = "order.db";

        public ServicesPage()
        {
            InitializeComponent();
            DataContext = this;

            Services = new ObservableCollection<ServiceViewModel>
            {
                new ServiceViewModel
                {
                    Name = "Чип-тюнинг",
                    SubServices = new ObservableCollection<ServiceViewModel>
                    {
                        new ServiceViewModel { Name = "Чип-тюнинг бензиновых двигателей" },
                        new ServiceViewModel { Name = "Чип-тюнинг дизельных двигателей" },
                        new ServiceViewModel { Name = "Чип-тюнинг атмосферных двигателей" },
                        new ServiceViewModel { Name = "Чип-тюнинг турбированных двигателей" },
                        new ServiceViewModel { Name = "Чип-тюнинг тюнинг КПП" },
                    }
                },
                new ServiceViewModel
                {
                    Name = "Выхлопная система",
                    SubServices = new ObservableCollection<ServiceViewModel>
                    {
                        new ServiceViewModel { Name = "Удаление катализатора" },
                        new ServiceViewModel { Name = "Установка Downpipe" },
                        new ServiceViewModel { Name = "Ремонт выхлопа" },
                        new ServiceViewModel { Name = "Замена гофры глушителя" },
                        new ServiceViewModel { Name = "Раздвоение выхлопа" },
                        new ServiceViewModel { Name = "Установка выхлопных насадок" },
                        new ServiceViewModel { Name = "Управляемый выхлоп" },
                        new ServiceViewModel { Name = "Установка готовых OEM трасс" }
                    }
                },
                new ServiceViewModel
                {
                    Name = "Тюнинг",
                    SubServices = new ObservableCollection<ServiceViewModel>
                    {
                        new ServiceViewModel { Name = "Установка доводчиков дверей" },
                        new ServiceViewModel { Name = "Диски" },
                        new ServiceViewModel { Name = "Обвесы" },
                        new ServiceViewModel { Name = "Тюнинг тормозов" },
                        new ServiceViewModel { Name = "Доработка подвески" },
                        new ServiceViewModel { Name = "Доработка систем охлаждения" },
                        new ServiceViewModel { Name = "Установка турбо комплектов" }
                    }
                }
            };

            CreateOrdersTableIfNotExists();
        }

        private void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedServices = Services
                .SelectMany(s => Flatten(s))
                .Where(s => s.IsSelected)
                .ToList();

            if (!selectedServices.Any())
            {
                MessageBox.Show("Выберите хотя бы одну услугу.");
                return;
            }

            foreach (var service in selectedServices)
            {
                AddOrder(currentUserId, service.Name, DateTime.Now);
            }

            MessageBox.Show("Заказ успешно добавлен!");
        }

        private IEnumerable<ServiceViewModel> Flatten(ServiceViewModel service)
        {
            yield return service;
            foreach (var sub in service.SubServices.SelectMany(Flatten))
                yield return sub;
        }

        private void CreateOrdersTableIfNotExists()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                string sql = @"
                    CREATE TABLE IF NOT EXISTS Orders (
                        order_id TEXT PRIMARY KEY,
                        user_id TEXT NOT NULL,
                        selected_service TEXT NOT NULL,
                        check_in_date TEXT NOT NULL,
                        order_status TEXT NOT NULL
                    )";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AddOrder(string userId, string selectedService, DateTime checkInDate, string orderStatus = "в обработке")
        {
            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                string orderId = Guid.NewGuid().ToString();

                string sql = @"
                    INSERT INTO Orders(order_id, user_id, selected_service, check_in_date, order_status)
                    VALUES (@order_id, @user_id, @selected_service, @check_in_date, @order_status)";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@order_id", orderId);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@selected_service", selectedService);
                    cmd.Parameters.AddWithValue("@check_in_date", checkInDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@order_status", orderStatus);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
