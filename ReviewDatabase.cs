using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace Prototype
{
    public static class ReviewDatabase
    {
        // Папка для хранения базы (можно оставить AppDomain.CurrentDomain.BaseDirectory, если хотите в папке с приложением)
        private static readonly string dbFolder = AppDomain.CurrentDomain.BaseDirectory;

        // Путь к файлу базы данных
        private static readonly string dbPath = Path.Combine(dbFolder, "review.db");

        // Строка подключения
        private static readonly string connectionString = $"Data Source={dbPath};Version=3;";

        static ReviewDatabase()
        {
            try
            {
                if (!Directory.Exists(dbFolder))
                    Directory.CreateDirectory(dbFolder);

                if (!File.Exists(dbPath))
                {
                    SQLiteConnection.CreateFile(dbPath);
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(@"
                            CREATE TABLE Reviews (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name TEXT NOT NULL,
                                Text TEXT NOT NULL,
                                Rating INTEGER NOT NULL
                            );", connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка инициализации базы данных: " + ex.Message);
            }
        }

        public static void AddReview(string name, string text, int rating)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand("INSERT INTO Reviews (Name, Text, Rating) VALUES (@name, @text, @rating);", connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@text", text);
                        command.Parameters.AddWithValue("@rating", rating);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении отзыва: " + ex.Message);
            }
        }

        public static List<Review> GetAllReviews()
        {
            var reviews = new List<Review>();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand("SELECT Name, Text, Rating FROM Reviews", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reviews.Add(new Review
                                {
                                    Name = reader.GetString(0),
                                    Text = reader.GetString(1),
                                    RatingStars = new string('★', reader.GetInt32(2))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке отзывов: " + ex.Message);
            }

            return reviews;
        }
    }
}
