using Core.Models;
using Core.Services;
using Database;
using Frontend.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace frontend
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default <see cref="Application"/> class.
    /// </summary>
    public sealed partial class App : Application
    {

        public static IServiceProvider Services { get; private set; }

        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            Suspending += OnSuspending;
        }



        /// <inheritdoc/>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

            //var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "kaana.db");
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kaana.db");


            System.Diagnostics.Debug.WriteLine(dbPath);

            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

            services.AddScoped<IFarmerService, FarmerService>();
            services.AddScoped<IAdvanceService, AdvanceService>();
            services.AddScoped<ICollectionCenterService, CollectionCenterService>();
            services.AddScoped<ICollectionPeriodService, CollectionPeriodService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IMilkBuyerService, MilkBuyerService>();
            services.AddScoped<IMilkCollectionService, MilkCollectionService>();
            services.AddScoped<IMilkLoadingService, MilkLoadingService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IDashBoardService, DashboardService>();

            services.AddTransient<FarmerViewModel>();
            services.AddTransient<AdvanceViewModel>();
            services.AddTransient<CollectionCenterViewModel>();
            services.AddTransient<CollectionPeriodViewModel>();
            services.AddTransient<ExpenseViewModel>();
            services.AddTransient<MilkBuyerViewModel>();
            services.AddTransient<MilkCollectionViewModel>();
            services.AddTransient<MilkLoadingViewModel>();
            services.AddTransient<PaymentViewModel>();
            services.AddTransient<UserViewModel>();
            services.AddTransient<FarmerReportViewModel>();
            services.AddTransient<DashboardViewModel>();

            Services = services.BuildServiceProvider();

            using (var scope = Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active.
            if (Window.Current.Content is not Frame rootFrame)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page, configuring
                    // the new page by passing required information as a navigation parameter.
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails.
        /// </summary>
        /// <param name="sender">The Frame which failed navigation.</param>
        /// <param name="e">Details about the navigation failure.</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception($"Failed to load page '{e.SourcePageType.FullName}'.");
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
