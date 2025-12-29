using System.Windows;
using System.Windows.Controls;

namespace Prototype
{
    public partial class ChipTuningPage : Page
    {
        public ChipTuningPage()
        {
            InitializeComponent();
        }

        private void ShowPetrolInfo(object sender, RoutedEventArgs e)
        {
            new PetrolInfoWindow().ShowDialog();
        }

        private void ShowDieselInfo(object sender, RoutedEventArgs e)
        {
            new DieselInfoWindow().ShowDialog();
        }

        private void ShowAtmosphericInfo(object sender, RoutedEventArgs e)
        {
            new AtmosphericInfoWindow().ShowDialog();
        }

        private void ShowTurboInfo(object sender, RoutedEventArgs e)
        {
            new TurboInfoWindow().ShowDialog();
        }

        private void ShowTransmissionInfo(object sender, RoutedEventArgs e)
        {
            new TransmissionInfoWindow().ShowDialog();
        }
    }
}
