using frontend;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Frontend
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModifierFarmer : Page
    {
        public FarmerViewModel ViewModel { get; }

        public ModifierFarmer()
        {
            this.InitializeComponent();
            ViewModel = App.Services.GetService<FarmerViewModel>();
            this.DataContext = ViewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var farmerId = (long)e.Parameter;

            await ViewModel.LoadFarmer(farmerId);
            System.Diagnostics.Debug.WriteLine(ViewModel.SelectedFarmer.FirstName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await ViewModel.UpdateFarmer();
        }
    }
}
