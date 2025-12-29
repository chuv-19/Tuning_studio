using System.Windows;
using System.Windows.Controls;

namespace Prototype
{
    public partial class ExhaustPage : Page
    {
        public ExhaustPage()
        {
            InitializeComponent();
        }

        private void ShowCatalystRemoval(object sender, RoutedEventArgs e)
        {
            new CatalystRemoval().ShowDialog();
        }

        private void ShowDownpipeInstall(object sender, RoutedEventArgs e)
        {
            new DownpipeInstall().ShowDialog();
        }

        private void ShowExhaustRepair(object sender, RoutedEventArgs e)
        {
            new ExhaustRepair().ShowDialog();
        }

        private void ShowGofraReplacement(object sender, RoutedEventArgs e)
        {
            new GofraReplacement().ShowDialog();
        }

        private void ShowExhaustSplit(object sender, RoutedEventArgs e)
        {
            new ExhaustSplit().ShowDialog();
        }

        private void ShowExhaustTipsInstall(object sender, RoutedEventArgs e)
        {
            new ExhaustTipsInstall().ShowDialog();
        }

        private void ShowControlledExhaust(object sender, RoutedEventArgs e)
        {
            new ControlledExhaust().ShowDialog();  
        }

        private void ShowOEMExhaustInstall(object sender, RoutedEventArgs e)
        {
            new OEMExhaustInstall().ShowDialog();
        }
    }
}
