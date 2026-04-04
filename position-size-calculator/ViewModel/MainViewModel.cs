using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace PositionSizeCalculator.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public double AccountSizeValue
        {
            get => accountSizeValue;
            set
            {
                if(value != accountSizeValue)
                {
                    accountSizeValue = value;
                    TryCalculatePositionSize();
                }
            }
        }

        public double RiskPercentage
        {
            get => riskPercentage;
            set
            {
                if (value != riskPercentage)
                {
                    riskPercentage = value;
                    TryCalculatePositionSize();
                }
            }
        }

        public double EntryPrice
        {
            get => entryPrice;
            set
            {
                if (value != entryPrice)
                {
                    entryPrice = value;
                    TryCalculatePositionSize();
                }
            }
        }

        public double StopLossPrice
        {
            get => stopLossPrice;
            set
            {
                if (value != stopLossPrice)
                {
                    stopLossPrice = value;
                    TryCalculatePositionSize();
                }
            }
        }

        [ObservableProperty]
        private int sharesAmount;
        [ObservableProperty]
        private double sharesValue;
        [ObservableProperty]
        private double riskValue;

        private double accountSizeValue;
        private double riskPercentage;
        private double entryPrice;
        private double stopLossPrice;
        private bool isLong;

        public MainViewModel()
        {
        }

        [RelayCommand]
        private void ToggleLong()
        {
            isLong = true;
        }

        [RelayCommand]
        private void ToggleShort()
        {
            isLong = false;
        }

        private void TryCalculatePositionSize()
        {
            if (accountSizeValue == 0 || riskPercentage == 0 || entryPrice == 0 || stopLossPrice == 0)
            {
                return;
            }

            RiskValue = accountSizeValue * (riskPercentage / 100);
            double riskPerShare = entryPrice - stopLossPrice;
            SharesAmount = (int)(RiskValue / riskPerShare);
            SharesValue = SharesAmount * entryPrice;
            Math.Round(SharesValue, 2);
        }
    }
}
