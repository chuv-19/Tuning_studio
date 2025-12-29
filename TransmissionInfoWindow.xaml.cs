using System.Windows;

namespace Prototype
{
    public partial class TransmissionInfoWindow : Window
    {
        public TransmissionInfoWindow()
        {
            InitializeComponent();
        }

        // Пример обработчика загрузки окна (если понадобится)
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Можно добавить инициализацию данных, анимацию и т.д.
        }

        // Пример обработчика кнопки закрытия (если добавите кнопку)
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
