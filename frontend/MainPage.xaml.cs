using Core.Models;
using Frontend;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace frontend
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public FarmerViewModel ViewModel { get; }

        public MainPage()
        {
            InitializeComponent();

            ViewModel = App.Services.GetRequiredService<FarmerViewModel>();

            this.DataContext = ViewModel;
            _ = ViewModel.LoadFarmers();
        }

        private async void AddFarmer_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.AddFarmer();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var farmer = (Farmer)e.ClickedItem;
            Frame.Navigate(typeof(ModifierFarmer), farmer.Id);
        }
    }
}
