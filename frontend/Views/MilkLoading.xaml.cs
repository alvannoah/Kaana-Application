using frontend;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Frontend.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MilkLoading : Page
    {
        public MilkLoadingViewModel ViewModel { get; }
        public MilkLoading()
        {
            this.InitializeComponent();
            ViewModel = App.Services.GetService<MilkLoadingViewModel>();
            DataContext = ViewModel;

            _ = ViewModel.Initialize();
        }

        public static string FormatShortDate(DateTime date) => date.ToString("yyyy-MM-dd");
    }
}
