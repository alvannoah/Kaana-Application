using Core.Services;
using frontend;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Frontend.Views
{
    public partial class FarmerReportPage : Page
    {
        public FarmerReportViewModel ViewModel { get; set; }

        public FarmerReportPage()
        {
            this.InitializeComponent();

            var reportService = App.Services.GetService<IReportService>();
            var periodService = App.Services.GetService<ICollectionPeriodService>();

            this.ViewModel = new FarmerReportViewModel(reportService, periodService);
            this.DataContext = this.ViewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await ViewModel.LoadReportAsync();
        }

        public static Brush GetBalanceBackground(decimal balance)
        {
            return balance > 0
                ? new SolidColorBrush(Color.FromArgb(255, 255, 235, 235))
                : new SolidColorBrush(Colors.Transparent);
        }

        public static Brush GetBalanceForeground(decimal balance)
        {
            return balance > 0
                ? new SolidColorBrush(Color.FromArgb(255, 198, 40, 40))
                : new SolidColorBrush(Colors.Black);
        }

        private void OnPrintClick(object sender, Windows.UI.Xaml.RoutedEventArgs e) { }
        private void OnExportClick(object sender, Windows.UI.Xaml.RoutedEventArgs e) { }
    }
}