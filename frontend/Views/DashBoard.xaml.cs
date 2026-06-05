using frontend;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Frontend.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashBoard : Page
    {
        public DashboardViewModel ViewModel { get; }
        public DashBoard()
        {
            this.InitializeComponent();
            ViewModel = App.Services.GetService<DashboardViewModel>();

            this.DataContext = ViewModel;

            this.Loaded += DashboardPage_Loaded;
        }

        private async void DashboardPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDashboardAsync();
        }
    }
}
