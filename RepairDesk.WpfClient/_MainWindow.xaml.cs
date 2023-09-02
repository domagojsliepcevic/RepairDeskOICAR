using Microsoft.Extensions.DependencyInjection;
using RepairDesk.WpfClient.ViewModels;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RepairDesk.WpfClient
{
    /// <summary>
    /// Interaction logic for _MainWindow.xaml
    /// </summary>
    public partial class _MainWindow : Window
    {
        public _MainWindow(_MainWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

    }
}
