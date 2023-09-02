using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RepairDesk.WpfClient.Components
{
    /// <summary>
    /// Interaction logic for StarRatingControl.xaml
    /// </summary>
    public partial class StarRatingControl : UserControl
    {
        public static readonly DependencyProperty RatingProperty = DependencyProperty
            .Register("Rating", typeof(int), typeof(StarRatingControl), new PropertyMetadata(default(int), RatingChangedCallback));

        private static void RatingChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (StarRatingControl)d;
            control.UpdateStars();
        }

        public int Rating
        {
            get { return (int)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }


        public StarRatingControl()
        {
            InitializeComponent();
            UpdateStars();
        }

        private void UpdateStars()
        {
            StarPanel.Children.Clear();

            for (var i = 0; i < 5; i++)
            {
                var star = new TextBlock { FontSize = 50, Text = i < Rating ? "★" : "☆" };
                StarPanel.Children.Add(star);
            }
        }
    }
}
