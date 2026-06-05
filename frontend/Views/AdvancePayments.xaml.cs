using frontend;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Frontend.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdvancePayments : Page
    {
        public AdvanceViewModel ViewModel { get; }
        public AdvancePayments()
        {
            this.InitializeComponent();
            ViewModel = App.Services.GetService<AdvanceViewModel>();
            DataContext = ViewModel;

            _ = ViewModel.Initialize();
        }

        public static string FormatShortDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
