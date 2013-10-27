using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using UkrainianCurrency.ViewModels;
using UkrainianCurrency.Models;
using Microsoft.Phone.Net.NetworkInformation;

namespace UkrainianCurrency
{
    public partial class MainPage : PhoneApplicationPage
    {
        public IList<Currency> iCurrencyItems = new List<Currency>();
        public IList<Currency> CurrencyItems
        {
            get
            {
                return iCurrencyItems;
            }
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();
            CurrencyListbox.DataContext = DataContext;

            DbEngine dbEngine = new DbEngine(DbEngine.DBConnectionString);

           // Currency item = new Currency("USD");
           // item.BuyCourse = 543;
           // item.BuyDelta = 0.001;
           // item.SellCourse = 55.5;
           // item.SellDelta = -0.002;
           //// iCurrencyItems.Add(item);

           // Currency item2 = new Currency("RUB");
           // item2.BuyCourse = 2.55;
           // item2.BuyDelta = -0.056789;
           // item2.SellCourse = 6.22;
           // item2.SellDelta = 0.0123456;
           //// iCurrencyItems.Add(item2);

           // Currency item3 = new Currency("CHF");
           // item3.BuyCourse = 543;
           // item3.BuyDelta = 0.0;
           // item3.SellCourse = 55.5;
           // item3.SellDelta = 0.0;
           //// iCurrencyItems.Add(item3);

           // Currency item4 = new Currency("RUB");
           // item4.BuyCourse = 2.55;
           // item4.BuyDelta = -0.05;
           // item4.SellCourse = 6.22;
           // item4.SellDelta = 0.002;
           //// iCurrencyItems.Add(item4);

           // Currency item5 = new Currency("EUR");
           // item5.BuyCourse = 543;
           // item5.BuyDelta = 0.001;
           // item5.SellCourse = 55.5;
           // item5.SellDelta = -0.002;
           //// iCurrencyItems.Add(item5);

           // Currency item6 = new Currency("JPY");
           // item6.BuyCourse = 2.55;
           // item6.BuyDelta = -0.05;
           // item6.SellCourse = 6.22;
           // item6.SellDelta = 0.002;
            //iCurrencyItems.Add(item6);

            if (!dbEngine.DatabaseExists())
            {
                dbEngine.CreateDatabase();
            }

            //dbEngine.AddCurrency(item);
            //dbEngine.AddCurrency(item2);
            //dbEngine.AddCurrency(item3);
            //dbEngine.AddCurrency(item4);
            //dbEngine.AddCurrency(item5);
            //dbEngine.AddCurrency(item6);

            // IList<Currency> items = dbEngine.GetCurrency();
            //CurrencyListbox.DataContext = CurrencyItems;


        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            // check whether application settings are changed or not
            ((MainPageViewModel)DataContext).CheckSettings();
        }
    }
}