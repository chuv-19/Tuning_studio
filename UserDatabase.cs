using System;
using System.Data.SQLite;
using System.IO;

namespace Prototype
{
    public static class UserDatabase
    {
        private static readonly string dbPath = "user.db";

        static UserDatabase()
        {
            if (!File.Exists(dbPath))
            {
                CreateDatabase();
            }
        }

        private static void CreateDatabase()
        {
            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        CREATE TABLE IF NOT EXISTS Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            FullName TEXT NOT NULL,
                            Phone TEXT NOT NULL,
                            Email TEXT NOT NULL UNIQUE,
                            Password TEXT NOT NULL
                        );
                    ";
                    command.ExecuteNonQuery();
                }
            }
        }

        public static bool IsEmailExists(string email)
        {
            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(1) FROM Users WHERE Email = @Email;";
                    command.Parameters.AddWithValue("@Email", email);

                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result) > 0;
                }
            }
        }

        public static bool AddUser(string fullName, string phone, string email, string password)
        {
            if (IsEmailExists(email))
                return false; // Email уже существует

            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        INSERT INTO Users (FullName, Phone, Email, Password)
                        VALUES (@fullName, @phone, @email, @password);
                    ";
                    command.Parameters.AddWithValue("@fullName", fullName);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password); // Рекомендуется хранить хэш пароля

                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public static bool ValidateUser(string email, string password)
        {
            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                        SELECT COUNT(1)
                        FROM Users
                        WHERE Email = @Email AND Password = @Password;
                    ";
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result) > 0;
                }
            }
        }
    }
}
