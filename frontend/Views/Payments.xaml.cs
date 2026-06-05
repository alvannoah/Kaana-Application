using frontend;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Frontend.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Payments : Page
    {
        public PaymentViewModel ViewModel { get; }
        public Payments()
        {
            this.InitializeComponent();
            ViewModel = App.Services.GetService<PaymentViewModel>();
            DataContext = ViewModel;

            _ = ViewModel.Initialize();
        }

        public static string FormatShortDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
