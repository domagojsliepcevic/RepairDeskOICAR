using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for LoaderComponent.xaml
    /// </summary>
    public partial class LoaderComponent : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty IsLoadingProperty 
            = DependencyProperty.Register("IsLoading", typeof(bool), typeof(LoaderComponent), new PropertyMetadata(false));

        public LoaderComponent()
        {
            InitializeComponent();
        }

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); OnPropertyChanged(nameof(IsLoading)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
