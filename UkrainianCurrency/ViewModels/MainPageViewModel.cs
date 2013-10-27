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
using UkrainianCurrency.Navigation;
using System.Collections.ObjectModel;
using UkrainianCurrency.Models;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using UkrainianCurrency.Resources;
using UkrainianCurrency.Utils;
using Microsoft.Phone.Net.NetworkInformation;
using System.Globalization;

namespace UkrainianCurrency.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private const string URL_REQUEST = @"http://cashexchange.com.ua/XmlApi.ashx";

        public RelayCommand UpdateCommand { get; private set; }
        public RelayCommand SettingsCommand { get; private set; }
        public RelayCommand AboutCommand { get; private set; }

        private DbEngine iDbEngine;

        public MainPageViewModel()
        {
            // handlers
            UpdateCommand = new RelayCommand(MakeUpdate);
            SettingsCommand = new RelayCommand(GoToSettings);
            AboutCommand = new RelayCommand(GoToAbout);

            iDbEngine = new DbEngine(DbEngine.DBConnectionString);
            if (!iDbEngine.DatabaseExists())
            {
                iDbEngine.CreateDatabase();
            }

            UpdateCurrencyList();
            UpdateDownloadTime();
            IsInProgress = false;

            if (IsDataOld())
            {
                MakeUpdate();
            }
        }

        /// <summary>
        /// Page title
        /// </summary>
        private string iPageTitle;
        public string PageTitle
        {
            get
            {
                return iPageTitle;
            }
            set
            {
                iPageTitle = value;
                RaisePropertyChanged("PageTitle");
            }
        }

        /// <summary>
        /// Shows/hides listbox empty control
        /// </summary>
        private bool iIsNoData;
        public bool IsNoData 
        { 
            get
            {
                return iIsNoData;
            }
            set
            {
                iIsNoData = value;
                RaisePropertyChanged("IsNoData");
            }
        }

        /// <summary>
        /// Sets update time
        /// </summary>
        private string iUpdatedTime;
        public string UpdatedTime
        {
            get
            {
                return iUpdatedTime;
            }
            set
            {
                iUpdatedTime = value;
                RaisePropertyChanged("UpdatedTime");
            }
        }

        /// <summary>
        /// Shows/hides update time
        /// </summary>
        private bool iHasUpdateTime;
        public bool HasUpdateTime
        {
            get
            {
                return iHasUpdateTime;
            }
            set
            {
                iHasUpdateTime = value;
                RaisePropertyChanged("HasUpdateTime");
            }
        }

        /// <summary>
        /// Shows/hides progress indicator
        /// </summary>
        private bool iIsInProgress;
        public bool IsInProgress
        {
            get
            {
                return iIsInProgress;
            }
            set
            {
                iIsInProgress = value;
                RaisePropertyChanged("IsInProgress");
            }
        }

        /// <summary>
        /// Provides the collection of listbox items
        /// </summary>
        private ObservableCollection<Currency> iCurrencyList = new ObservableCollection<Currency>();
        public ObservableCollection<Currency> CurrencyItems
        {
            get
            {
                return iCurrencyList;
            }
            set
            {
                iCurrencyList = value;
                RaisePropertyChanged("CurrencyItems");
            }
        }

        /// <summary>
        /// Sets text of empty list
        /// </summary>
        private string iEmptyListText;
        public string EmptyListText
        {
            get
            {
                return iEmptyListText;
            }
            set
            {
                iEmptyListText = value;
                RaisePropertyChanged("EmptyListText");
            }
        }

        /// <summary>
        /// Updates the list of currency
        /// </summary>
        private void UpdateCurrencyList()
        {
            IList<Currency> list = iDbEngine.GetCurrency();
            CurrencyItems.Clear();
            foreach (Currency curr in list)
            {
                CurrencyItems.Add(curr);
            }

            // show/hide empty control
            IsNoData = list.Count == 0;
            EmptyListText = Labels.NoData;
        }

        /// <summary>
        /// Updates last download time
        /// </summary>
        private void UpdateDownloadTime()
        {
            IList<DownloadData> downloadData = iDbEngine.GetDownloadData();
            if (downloadData.Count == 0)
            {
                // hide updated time
                HasUpdateTime = false;
            }
            else
            {
                HasUpdateTime = true;
                // use always only the firs record. The other records are invalid
                long updateTime = downloadData[0].UpdateTime;
                UpdatedTime = Labels.UpdatedTime + DateTimeUtils.GetDateTime(updateTime);
            }
        }

        /// <summary>
        /// Checks whether data is old or not
        /// </summary>
        /// <returns></returns>
        private bool IsDataOld()
        {
            bool isDataOld = true;
            DateTime nowUtc = DateTime.UtcNow;
            int currDay = nowUtc.DayOfYear;

            IList<DownloadData> downloadData = iDbEngine.GetDownloadData();
            if (downloadData.Count > 0)
            {
                long updateTime = downloadData[0].UpdateTime;
                DateTime updatedDate = new DateTime(updateTime);
                int updatedDay = updatedDate.DayOfYear;

                isDataOld = currDay != updatedDay;
            }

            return isDataOld;
        }

        /// <summary>
        /// Prepares request string
        /// </summary>
        /// <returns></returns>
        private string prepareRequestString()
        {
            String baseRequest = URL_REQUEST;

            int currCity = Settings.GetCity(0);
            int currBank = Settings.GetBank(0);

            string[] region = Tables.CITIES[currCity];
            string district = region[Tables.REGION];
            string city = region[Tables.CITY];

            bool regionSet = false;
            if (district.Length > 0 && city.Length > 0)
            {
                baseRequest += "?district=" + district + "&city=" + city;
                regionSet = true;
            }

            string bank = Tables.BANKS[currBank];
            if (bank.Length > 0)
            {
                if (regionSet)
                {
                    baseRequest += "&company=" + bank;
                }
                else
                {
                    baseRequest += "?company=" + bank;
                }
            }

            return baseRequest;
        }

        private void MakeUpdate()
        {
            // Check network connection
            bool hasNetworkConnection = NetworkInterface.NetworkInterfaceType != NetworkInterfaceType.None;

            if (hasNetworkConnection)
            {
                DoUpdate();
            }
            else
            {
                if (IsNoData)
                {
                    EmptyListText = Labels.NoNetwork;
                }
                else
                {
                    MessageBox.Show(Labels.NoNetwork);
                }
            }
        }

        /// <summary>
        /// Does update request
        /// </summary>
        private void DoUpdate()
        {
            // show progress bar
            IsInProgress = true;

            // clear previous data
            iDbEngine.DeleteAllCurrency();
            UpdateCurrencyList();

            // set loading text
            EmptyListText = Labels.LoadingText;

            // Make request to the selected country/city
            String urlRequest = prepareRequestString();
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(OnUpdateFinished);
            client.DownloadStringAsync(new Uri(urlRequest));
        }

        /// <summary>
        /// Handles update finished
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUpdateFinished(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                // Clear all previous data
                iDbEngine.DeleteAllCurrency();
                iDbEngine.DeleteAllDownloadData();

                // parsing results
                XDocument results = XDocument.Parse(e.Result);             
                XElement elements = results.Element("Results");
                var items = from item in elements.Elements("Element") select item;
                foreach (XElement item in items)
                {
                    Currency currency = new Currency();
                    currency.CurrencyName = item.Element("Currency").Value;
                    currency.BuyCourse = Double.Parse(item.Element("Buy").Value, CultureInfo.InvariantCulture);
                    currency.SellCourse = Double.Parse(item.Element("Sale").Value, CultureInfo.InvariantCulture);
                    currency.BuyDelta = Double.Parse(item.Element("BuyDelta").Value, CultureInfo.InvariantCulture);
                    currency.SellDelta = Double.Parse(item.Element("SaleDelta").Value, CultureInfo.InvariantCulture);
                    iDbEngine.AddCurrency(currency);
                }
                UpdateCurrencyList();

                DownloadData data = new DownloadData();
                data.City = Settings.GetCity(0);
                data.Bank = Settings.GetBank(0);
                DateTime now = DateTime.UtcNow;
                data.UpdateTime = now.Ticks;

                iDbEngine.AddDownloadData(data);
                UpdateDownloadTime();
            }
            else
            {
                UpdateCurrencyList();
                MessageBox.Show(Labels.GeneralError);
            }

            IsInProgress = false;
        }

        /// <summary>
        /// Navigates to settings
        /// </summary>
        private void GoToSettings()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate("/Views/SettingsPage.xaml");
        }

        /// <summary>
        /// Navigates to about
        /// </summary>
        private void GoToAbout()
        {
            //iDbEngine.DeleteAllCurrency();
            //iDbEngine.DeleteAllDownloadData();
            //UpdateCurrencyList();
            //UpdateDownloadTime();

            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate("/Views/AboutPage.xaml");
        }

        /// <summary>
        /// Checks whether application settings are changed or not
        /// </summary>
        public void CheckSettings()
        {
            int currCity = Settings.GetCity(0);
            int currBank = Settings.GetBank(0);

            string city = Tables.CITY_NAMES[currCity];
            string bank = Tables.BANK_NAMES[currBank];

            PageTitle = city + ", " + bank;

            IList<DownloadData> downloadData = iDbEngine.GetDownloadData();
            if (downloadData.Count > 0)
            {
                int storedCity = downloadData[0].City;
                int storedBank = downloadData[0].Bank;

                if (currCity != storedCity || currBank != storedBank)
                {
                    iDbEngine.DeleteAllCurrency();
                    UpdateCurrencyList();

                    bool hasNetworkConnection = NetworkInterface.NetworkInterfaceType != NetworkInterfaceType.None;

                    if (hasNetworkConnection)
                    {
                        DoUpdate();
                    }
                    else
                    {
                        EmptyListText = Labels.NoNetwork;
                    }
                    
                }
            }

            
        }
        
    }
}
