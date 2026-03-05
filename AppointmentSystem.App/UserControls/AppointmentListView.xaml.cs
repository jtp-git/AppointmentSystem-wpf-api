using AppointmentSystem.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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

namespace AppointmentSystem.App.UserControls
{
    /// <summary>
    /// Interaction logic for AppointmentListView.xaml
    /// </summary>
    public partial class AppointmentListView : UserControl
    {
        public AppointmentListView()
        {
            InitializeComponent();
            DataContext = App.Services.GetService<AppointmentListViewModel>();
        }
    }
}
