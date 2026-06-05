using Core.Models;
using Frontend;
using Frontend.ViewModels;
using Frontend.Views;
using Frontend.Views.CollectionCenterViews;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MilkLoading = Frontend.Views.MilkLoading;

namespace frontend
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();
            Frame.Navigate(typeof(DashBoard));
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem item)
            {
                string tag = item.Tag.ToString();

                switch (tag)
                {
                    case "DashBoard":
                        Frame.Navigate(typeof(DashBoard));
                        break;

                    case "Expenses":
                        Frame.Navigate(typeof(Expenses));
                        break;

                    case "MilkCollections":
                        Frame.Navigate(typeof(MilkCollections));
                        break;

                    case "Farmers":
                        Frame.Navigate(typeof(Farmers));
                        break;

                    case "CollectionCenters":
                        Frame.Navigate(typeof(CollectionCenterView));
                        break;
                    case "MilkBuyers":
                        Frame.Navigate(typeof(MilkBuyers));
                        break;
                    case "MilkLoading":
                        Frame.Navigate(typeof(MilkLoading));
                        break;
                    case "Payments":
                        Frame.Navigate(typeof(Payments));
                        break;
                    case "Periods":
                        Frame.Navigate(typeof(CollectionPeriods));
                        break;
                    case "Reports":
                        Frame.Navigate(typeof(FarmerReportPage));
                        break;
                    case "AdvancePayments":
                        Frame.Navigate(typeof(AdvancePayments));
                        break;

                }
            }
        }
    }
}
