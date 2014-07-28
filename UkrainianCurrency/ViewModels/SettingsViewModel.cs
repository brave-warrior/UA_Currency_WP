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
using System.Collections.Generic;

namespace UkrainianCurrency.ViewModels
{
    /// <summary>
    /// Viewmodel for the page Settings
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        private const int DEFAULT_CITY_INDEX = 0;
        private const int DEFAULT_BANK_INDEX = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingsViewModel()
        {
            // values
            Bank = Settings.GetBank(DEFAULT_BANK_INDEX);
            City = Settings.GetCity(DEFAULT_CITY_INDEX);

            // prepare list of cities
            foreach (String str in Tables.CITY_NAMES)
            {
                CityList.Add(str);
            }

            // prepare list of banks
            foreach (String str in Tables.BANK_NAMES)
            {
                BankList.Add(str);
            }
        }

        /// <summary>
        /// Provides list of cities
        /// </summary>
        private IList<String> iCityList = new List<string>();
        public IList<String> CityList
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
        private IList<String> iBankList = new List<string>();
        public IList<String> BankList
        {
            get { return iBankList; }
            set
            {
                iBankList = value;
                RaisePropertyChanged("BankList");
            }
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
                }
                
            }
        }

    }
}
