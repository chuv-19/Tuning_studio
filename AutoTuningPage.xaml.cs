using System.Windows;
using System.Windows.Controls;

namespace Prototype
{ 
    public partial class AutoTuningPage : Page
    {
        public AutoTuningPage()
        {
            InitializeComponent();
        }

        private void ShowBrakesTuning(object sender, RoutedEventArgs e)
        {
            new BrakesTuning().ShowDialog(); 

        }

        private void ShowSuspensionTuning(object sender, RoutedEventArgs e)
        {
            new SuspensionTuning().ShowDialog(); 
        }
        private void ShowCoolingTuning(object sender, RoutedEventArgs e)
        {
            new CoolingTuning().ShowDialog();


        }

        private void ShowTurboKitInstall(object sender, RoutedEventArgs e)
        {
            new TurboKitInstall().ShowDialog();

        }

        private void ShowComfortTuning(object sender, RoutedEventArgs e)
        {
            new ComfortTuning().ShowDialog();

        }
    }
}
