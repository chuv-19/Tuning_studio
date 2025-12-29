using System.Windows;

namespace Prototype
{
    /// <summary>
    /// Логика взаимодействия для PetrolInfoWindow.xaml
    /// </summary>
    public partial class PetrolInfoWindow : Window
    {
        public PetrolInfoWindow()
        {
            InitializeComponent();
        }

        // Метод для закрытия окна по кнопке (если добавите кнопку закрытия)
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Пример обработчика загрузки окна
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Можно добавить инициализацию данных или анимации
        }
    }
}
