using System.Windows;

namespace Prototype
{
    /// <summary>
    /// Логика взаимодействия для DieselInfoWindow.xaml
    /// </summary>
    public partial class DieselInfoWindow : Window
    {
        public DieselInfoWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация или анимации при загрузке окна
        }
    }
}
