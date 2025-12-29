using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Prototype
{
    public partial class Autorization : Page
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Пожалуйста, введите корректный e-mail.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (UserDatabase.ValidateUser(email, password))
            {
                MessageBox.Show("Успешная авторизация!");

                // Сохраняем email текущего пользователя для последующего использования
                Application.Current.Properties["CurrentUserEmail"] = email;

                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.NavigateToServices();
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavigateToRegister();
            }
            else
            {
                MessageBox.Show("Не удалось перейти на страницу регистрации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
