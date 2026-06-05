using Core.DTOs;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class FarmerReportViewModel : INotifyPropertyChanged
    {
        private readonly IReportService _reportService;
        private readonly ICollectionPeriodService _periodService;

        private bool _isPeriodClosed;
        public bool IsPeriodClosed
        {
            get => _isPeriodClosed;
            private set { _isPeriodClosed = value; OnPropertyChanged(); }
        }

        private string _periodName = "Current Billing Cycle";
        public string PeriodName
        {
            get => _periodName;
            private set { _periodName = value; OnPropertyChanged(); }
        }

        public ObservableCollection<FarmerStatementDto> Statements { get; } = new();


        private decimal _totalPeriodLitres;
        public decimal TotalPeriodLitres
        {
            get => _totalPeriodLitres;
            private set { _totalPeriodLitres = value; OnPropertyChanged(); }
        }

        private string _totalPeriodGross = "UGX 0.00";
        public string TotalPeriodGross
        {
            get => _totalPeriodGross;
            private set { _totalPeriodGross = value; OnPropertyChanged(); }
        }

        private string _totalPeriodDeductions = "UGX 0.00";
        public string TotalPeriodDeductions
        {
            get => _totalPeriodDeductions;
            private set { _totalPeriodDeductions = value; OnPropertyChanged(); }
        }

        private string _totalPeriodBalance = "UGX 0.00";
        public string TotalPeriodBalance
        {
            get => _totalPeriodBalance;
            private set { _totalPeriodBalance = value; OnPropertyChanged(); }
        }

        private string _totalAdvancesIssued = "UGX 0.00";
        public string TotalAdvancesIssued
        {
            get => _totalAdvancesIssued;
            private set { _totalAdvancesIssued = value; OnPropertyChanged(); }
        }

        private string _totalAdvancesRecovered = "UGX 0.00";
        public string TotalAdvancesRecovered
        {
            get => _totalAdvancesRecovered;
            private set { _totalAdvancesRecovered = value; OnPropertyChanged(); }
        }

        private string _netAfterAdvances = "UGX 0.00";
        public string NetAfterAdvances
        {
            get => _netAfterAdvances;
            private set { _netAfterAdvances = value; OnPropertyChanged(); }
        }

        public ICommand RefreshReportCommand { get; }

        public FarmerReportViewModel(
            IReportService reportService,
            ICollectionPeriodService periodService)
        {
            _reportService = reportService;
            _periodService = periodService;

            RefreshReportCommand = new RelayCommand<object>(async _ => await LoadReportAsync());
        }

        public async Task LoadReportAsync()
        {
            try
            {
                var activePeriod = _periodService.GetCurrentPeriod(DateTime.Now);
                if (activePeriod == null)
                    return;

                IsPeriodClosed = activePeriod.IsClosed;
                PeriodName = activePeriod.Name;

                var reportData = await _reportService.GetFarmerStatement(activePeriod.Id)
                                ?? new List<FarmerStatementDto>();

                Statements.Clear();

                decimal totalLitres = 0;
                decimal totalGross = 0;
                decimal totalDeductions = 0;
                decimal totalBalance = 0;

                decimal totalAdvancesIssued = 0;
                decimal totalAdvancesRecovered = 0;

                foreach (var statement in reportData)
                {
                    Statements.Add(statement);

                    totalLitres += statement.TotalLitres;
                    totalGross += statement.GrossAmount;
                    totalDeductions += statement.TotalDeductions;
                    totalBalance += statement.Balance;

                    totalAdvancesIssued += statement.AdvanceIssued;
                    totalAdvancesRecovered += statement.AdvanceRecovered;
                }

                TotalPeriodLitres = totalLitres;

                TotalPeriodGross = $"UGX {totalGross:N2}";
                TotalPeriodDeductions = $"UGX {totalDeductions:N2}";
                TotalPeriodBalance = $"UGX {totalBalance:N2}";

                TotalAdvancesIssued = $"UGX {totalAdvancesIssued:N2}";
                TotalAdvancesRecovered = $"UGX {totalAdvancesRecovered:N2}";

                var netAfterAdvancesCalc = totalGross - totalDeductions - totalAdvancesRecovered;
                NetAfterAdvances = $"UGX {netAfterAdvancesCalc:N2}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Error compiling statements report dashboard: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}