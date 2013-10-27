using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using UkrainianCurrency.Models;
using System.Collections.ObjectModel;

namespace UkrainianCurrency.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {

        /// <summary>
        /// Provides list of cities
        /// </summary>
        private ObservableCollection<String> iCityList = new ObservableCollection<string>();
        public ObservableCollection<String> CityList
        {
            get { return iCityList; }
            set
            {
                iCityList = value;
                RaisePropertyChanged("CityList");
            }
        }

        /// <summary>
        /// Provides list of banks
        /// </summary>
        private ObservableCollection<String> iBankList = new ObservableCollection<string>();
        public ObservableCollection<String> BankList
        {
            get { return iBankList; }
            set
            {
                iBankList = value;
                RaisePropertyChanged("BankList");
            }
        }


        const int DEFAULT_VALUE = 0;

        public SettingsViewModel()
        {
            // values
            Bank = Settings.GetBank(DEFAULT_VALUE);
            City = Settings.GetCity(DEFAULT_VALUE);

            foreach (String str in Tables.CITY_NAMES)
            {
                CityList.Add(str);
            }

            foreach (String str in Tables.BANK_NAMES)
            {
                BankList.Add(str);
            }

            SettingsChanged = false;
        }

        /// <summary>
        /// Bank
        /// </summary>
        private int iBank;
        public int Bank
        {
            get
            {
                return iBank;
            }
            set
            {
                if (iBank != value)
                {
                    iBank = value;
                    RaisePropertyChanged("Bank");

                    // store settings
                    Settings.SaveBank(iBank);

                    SettingsChanged = true;
                }

            }
        }

        /// <summary>
        /// City
        /// </summary>
        private int iCity;
        public int City
        {
            get
            {
                return iCity;
            }
            set
            {
                if (iCity != value)
                {
                    iCity = value;
                    RaisePropertyChanged("City");

                    // store settings
                    Settings.SaveCity(iCity);

                    SettingsChanged = true;
                }
                
            }
        }

        private bool iSettingsChanged;
        public bool SettingsChanged { get; set; }
    }
}
