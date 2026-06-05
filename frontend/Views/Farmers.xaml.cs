using Core.Models;
using frontend;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Frontend.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Farmers : Page
    {
        public FarmerViewModel ViewModel { get; }
        public ObservableCollection<Farmer> RecentFarmers { get; set; } = new ObservableCollection<Farmer>();
        public Farmers()
        {
            this.InitializeComponent();
            ViewModel = App.Services.GetService<FarmerViewModel>();
            this.DataContext = ViewModel;
            _ = ViewModel.Initialize();
        }
    }
}
