using frontend;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Frontend.Views.CollectionCenterViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CollectionCenterView : Page
    {

        public CollectionCenterViewModel ViewModel { get; }
        public CollectionCenterView()
        {
            this.InitializeComponent();
            ViewModel = App.Services.GetService<CollectionCenterViewModel>();
            this.DataContext = ViewModel;

            _ = ViewModel.Load();
        }
    }
}
