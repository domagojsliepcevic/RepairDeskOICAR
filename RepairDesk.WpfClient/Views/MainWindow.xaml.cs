using Microsoft.Extensions.DependencyInjection;
using RepairDesk.ApiClient;
using RepairDesk.WpfClient.ViewModels;
using RepairDesk.WpfClient.Views;
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
using System.Windows.Shapes;

namespace RepairDesk.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = ((App)Application.Current).MainViewModel;
            MainGrid.DataContext = DataContext;

			//((MainViewModel)DataContext).ShowProductsList();
		}
	}
}
