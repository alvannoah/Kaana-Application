using Core.DTOs;
using Core.Services;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly IDashBoardService _dashboardService;

        public DashboardViewModel(IDashBoardService dashboardService)
        {
            _dashboardService = dashboardService;

            RefreshCommand =
                new RelayCommand<object>(async _ => await LoadDashboardAsync());
        }

        #region Summary Cards

        private int totalFarmers;
        public int TotalFarmers
        {
            get => totalFarmers;
            set
            {
                totalFarmers = value;
                OnPropertyChanged();
            }
        }

        private decimal totalMilkCollected;
        public decimal TotalMilkCollected
        {
            get => totalMilkCollected;
            set
            {
                totalMilkCollected = value;
                OnPropertyChanged();
            }
        }

        private decimal totalRevenue;
        public decimal TotalRevenue
        {
            get => totalRevenue;
            set
            {
                totalRevenue = value;
                OnPropertyChanged();
            }
        }

        private decimal totalExpenses;
        public decimal TotalExpenses
        {
            get => totalExpenses;
            set
            {
                totalExpenses = value;
                OnPropertyChanged();
            }
        }

        private decimal netProfit;
        public decimal NetProfit
        {
            get => netProfit;
            set
            {
                netProfit = value;
                OnPropertyChanged();
            }
        }

        private decimal outstandingAdvances;
        public decimal OutstandingAdvances
        {
            get => outstandingAdvances;
            set
            {
                outstandingAdvances = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Current Period

        private string currentPeriodName;
        public string CurrentPeriodName
        {
            get => currentPeriodName;
            set
            {
                currentPeriodName = value;
                OnPropertyChanged();
            }
        }

        private bool isPeriodClosed;
        public bool IsPeriodClosed
        {
            get => isPeriodClosed;
            set
            {
                isPeriodClosed = value;
                OnPropertyChanged();
            }
        }

        private int activeFarmersThisPeriod;
        public int ActiveFarmersThisPeriod
        {
            get => activeFarmersThisPeriod;
            set
            {
                activeFarmersThisPeriod = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Collections

        public ObservableCollection<TopFarmerDto> TopFarmers { get; }
            = new();

        public ObservableCollection<OutstandingAdvanceDto> OutstandingAdvancesList { get; }
            = new();

        #endregion

        public ICommand RefreshCommand { get; }

        public async Task LoadDashboardAsync()
        {
            var dashboard = await _dashboardService.GetDashboardAsync();

            if (dashboard == null)
                return;


            TotalFarmers = dashboard.TotalFarmers;
            TotalMilkCollected = dashboard.TotalMilkCollected;
            TotalRevenue = dashboard.TotalRevenue;
            TotalExpenses = dashboard.TotalExpenses;
            NetProfit = dashboard.NetProfit;
            OutstandingAdvances = dashboard.OutstandingAdvances;


            CurrentPeriodName = dashboard.CurrentPeriodName;
            IsPeriodClosed = dashboard.IsPeriodClosed;
            ActiveFarmersThisPeriod = dashboard.ActiveFarmersThisPeriod;

            TopFarmers.Clear();

            foreach (var farmer in dashboard.TopFarmers)
                TopFarmers.Add(farmer);


            OutstandingAdvancesList.Clear();

            foreach (var advance in dashboard.OutstandingAdvancesList)
                OutstandingAdvancesList.Add(advance);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(
            [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}