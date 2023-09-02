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
	/// Interaction logic for MainGrid.xaml
	/// </summary>
	public partial class MainGrid : UserControl
	{
		public MainGrid()
		{
			InitializeComponent();
		}

		//private void Button_Click(object sender, RoutedEventArgs e)
		//{
		//	products.Visibility = Visibility.Collapsed;
		//	product.Visibility = Visibility.Collapsed;
		//	order.Visibility = Visibility.Collapsed;
		//	ticket.Visibility = Visibility.Collapsed;

		//	var button = sender as Button;
		//	if (button != null && button.CommandParameter != null)
		//	{
		//		var controlToShow = this.FindName(button.CommandParameter.ToString()) as UIElement;
		//		if (controlToShow != null) controlToShow.Visibility = Visibility.Visible;
		//	}
  //      }
    }
}
