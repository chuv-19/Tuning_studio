using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Prototype
{
    public partial class MainWindow : Window
    {
        private bool isMenuOpen = false;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainTextPage());
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen)
                HideMenu();
            else
                ShowMenu();
        }

        private void ShowMenu()
        {
            SideMenu.Visibility = Visibility.Visible;

            var animation = new DoubleAnimation
            {
                From = 250,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            SideMenuTransform.BeginAnimation(System.Windows.Media.TranslateTransform.XProperty, animation);
            isMenuOpen = true;
        }

        private void HideMenu()
        {
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 250,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };

            animation.Completed += (s, e) => SideMenu.Visibility = Visibility.Collapsed;
            SideMenuTransform.BeginAnimation(System.Windows.Media.TranslateTransform.XProperty, animation);
            isMenuOpen = false;
        }

        private void ChipTuning_Click(object sender, RoutedEventArgs e)
        {
            ServicesPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new ChipTuningPage());
            HideMenu();
        }

        private void Exhaust_Click(object sender, RoutedEventArgs e)
        {
            ServicesPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new ExhaustPage());
            HideMenu();
        }

        private void AutoTuning_Click(object sender, RoutedEventArgs e)
        {
            ServicesPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new AutoTuningPage());
            HideMenu();
        }

        private void Contacts_Click(object sender, RoutedEventArgs e)
        {
            ServicesPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new ContactsPage());
            HideMenu();
        }

        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
            ServicesPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new ReviewsPage());
            HideMenu();
        }

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            ServicesPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new Autorization());
            HideMenu();
        }

        public void NavigateToRegister()
        {
            ServicesPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new RegisterPage());
            HideMenu();
        }

        public void NavigateToAutorization()
        {
            ServicesPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new Autorization());
            HideMenu();
        }

        public void NavigateToServices()
        {
            ServicesPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new ServicesPage());
            HideMenu();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            ServicesPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new Autorization());
            HideMenu();
        }

        private void LogoImage_Click(object sender, MouseButtonEventArgs e)
        {
            ServicesPanel.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new MainTextPage());

            if (isMenuOpen)
                HideMenu();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var source = e.OriginalSource as FrameworkElement;
            if (isMenuOpen && source != null && source != LogoImage)
            {
                HideMenu();
            }
        }
    }
}
