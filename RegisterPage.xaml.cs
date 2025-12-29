using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Text.RegularExpressions;

namespace Prototype
{
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text.Trim();
            string phone = PhoneTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = ShowPasswordToggle.IsChecked == true ? PasswordTextBox.Text : PasswordBox.Password;

            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Пожалуйста, введите ФИО.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Regex.IsMatch(phone, @"^\+?\d{10,15}$"))
            {
                MessageBox.Show("Пожалуйста, введите корректный номер телефона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Пожалуйста, введите корректный e-mail.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool success = UserDatabase.AddUser(fullName, phone, email, password);

            if (success)
            {
                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                FullNameTextBox.Clear();
                PhoneTextBox.Clear();
                EmailTextBox.Clear();
                PasswordBox.Clear();
                PasswordTextBox.Clear();
                ShowPasswordToggle.IsChecked = false;
            }
            else
            {
                MessageBox.Show("Пользователь с таким e-mail уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (ShowPasswordToggle.IsChecked == true)
            {
                PasswordTextBox.Text = PasswordBox.Password;
            }
        }

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ShowPasswordToggle.IsChecked == true)
            {
                PasswordBox.Password = PasswordTextBox.Text;
            }
        }

        private void ShowPasswordToggle_Checked(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Text = PasswordBox.Password;
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Collapsed;
            PasswordTextBox.Focus();
            PasswordTextBox.SelectionStart = PasswordTextBox.Text.Length;
        }

        private void ShowPasswordToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = PasswordTextBox.Text;
            PasswordBox.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Collapsed;
            PasswordBox.Focus();
        }
    }
}
