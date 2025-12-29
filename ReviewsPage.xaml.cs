using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Prototype
{
    public partial class ReviewsPage : Page
    {
        private int selectedRating = 0;
        public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();

        public ReviewsPage()
        {
            InitializeComponent();
            ReviewsList.ItemsSource = Reviews;
            LoadReviewsFromDatabase();
            HighlightStars(0);

            NameTextBox.TextChanged += NameTextBox_TextChanged;
            ReviewTextBox.TextChanged += ReviewTextBox_TextChanged;
        }

        private void LoadReviewsFromDatabase()
        {
            Reviews.Clear();
            var dbReviews = ReviewDatabase.GetAllReviews();
            foreach (var review in dbReviews)
            {
                Reviews.Add(review);
            }
        }

        private void Star_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock star && int.TryParse(star.Tag.ToString(), out int starValue))
            {
                HighlightStars(starValue);
            }
        }

        private void Star_MouseLeave(object sender, MouseEventArgs e)
        {
            HighlightStars(selectedRating);
        }

        private void Star_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock star && int.TryParse(star.Tag.ToString(), out int starValue))
            {
                selectedRating = starValue;
                HighlightStars(selectedRating);
            }
        }

        private void HighlightStars(int count)
        {
            if (StarsPanel != null)
            {
                for (int i = 0; i < StarsPanel.Children.Count; i++)
                {
                    if (StarsPanel.Children[i] is TextBlock star)
                    {
                        star.Foreground = (i < count)
                            ? new SolidColorBrush(Color.FromRgb(255, 243, 0))
                            : Brushes.Gray;
                    }
                }
            }
        }

        private void SendReview_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();
            string text = ReviewTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Пожалуйста, введите имя.");
                return;
            }

            if (selectedRating == 0)
            {
                MessageBox.Show("Пожалуйста, выберите рейтинг.");
                return;
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Пожалуйста, введите отзыв.");
                return;
            }

            ReviewDatabase.AddReview(name, text, selectedRating);

            Reviews.Add(new Review
            {
                Name = name,
                Text = text,
                RatingStars = new string('★', selectedRating)
            });

            NameTextBox.Text = "";
            ReviewTextBox.Text = "";
            selectedRating = 0;
            HighlightStars(0);
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NamePlaceholder != null)
                NamePlaceholder.Visibility = string.IsNullOrWhiteSpace(NameTextBox.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }

        private void ReviewTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ReviewPlaceholder != null)
                ReviewPlaceholder.Visibility = string.IsNullOrWhiteSpace(ReviewTextBox.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }
    }

    public class Review
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string RatingStars { get; set; }
    }
}
