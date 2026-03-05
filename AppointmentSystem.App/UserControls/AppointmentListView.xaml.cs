using AppointmentSystem.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

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
