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
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace UkrainianCurrency.Models
{
    [Table]
    public class Currency
    {
        private const int ROUND_VALUE = 4;

        private string iCurrencyName;
        private double iSellCourse;
        private double iBuyCourse;
        private double iSellDelta;
        private double iBuyDelta;

        public Currency() { }

        public Currency(String aCurrencyName)
        {
            CurrencyName = aCurrencyName.ToUpper();

            CurrImg = GetCurrencyImage(CurrencyName);
        }

        /// <summary>
        /// Gets currency image
        /// </summary>
        /// <param name="aCurrencyName">Currency name</param>
        /// <returns>Path to currency image</returns>
        private string GetCurrencyImage(String aCurrencyName)
        {
            String imgPath = "";
            switch (aCurrencyName)
            {
                case "AUD":
                    imgPath = "/Images/flags/aud.png";
                    break;
                case "CAD":
                    imgPath = "/Images/flags/cad.png";
                    break;
                case "CHF":
                    imgPath = "/Images/flags/chf.png";
                    break;
                case "CZK":
                    imgPath = "/Images/flags/czk.png";
                    break;
                case "DKK":
                    imgPath = "/Images/flags/dkk.png";
                    break;
                case "EUR":
                    imgPath = "/Images/flags/eur.png";
                    break;
                case "GBP":
                    imgPath = "/Images/flags/gbp.png";
                    break;
                case "GEL":
                    imgPath = "/Images/flags/gel.png";
                    break;
                case "JPY":
                    imgPath = "/Images/flags/jpy.png";
                    break;
                case "MDL":
                    imgPath = "/Images/flags/mdl.png";
                    break;
                case "NOK":
                    imgPath = "/Images/flags/nok.png";
                    break;
                case "PLN":
                    imgPath = "/Images/flags/pln.png";
                    break;
                case "RUB":
                    imgPath = "/Images/flags/rub.png";
                    break;
                case "SEK":
                    imgPath = "/Images/flags/sek.png";
                    break;
                case "USD":
                    imgPath = "/Images/flags/usd.png";
                    break;
                case "EEK":
                    imgPath = "/Images/flags/eek.png";
                    break;
                default:
                    break;
            }
            return imgPath;
        }

        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column()]
        public string CurrencyName 
        {
            get
            {
                return iCurrencyName;
            }
            set
            {
                iCurrencyName = value;
                iCurrencyName = iCurrencyName.ToUpper();
                CurrImg = GetCurrencyImage(iCurrencyName);
            }
        }

        [Column()]
        public double SellCourse 
        {
            get
            {
                return iSellCourse;
            }
            set
            {
                iSellCourse = value;
                iSellCourse = Math.Round(iSellCourse, ROUND_VALUE);
            }
        }

        [Column()]
        public double BuyCourse 
        {
            get
            {
                return iBuyCourse;
            }
            set
            {
                iBuyCourse = value;
                iBuyCourse = Math.Round(iBuyCourse, ROUND_VALUE);
            }
        }

        [Column()]
        public double SellDelta 
        {
            get
            {
                return iSellDelta;
            }
            set
            {
                iSellDelta = value;
                iSellDelta = Math.Round(iSellDelta, ROUND_VALUE);

                SellDeltaColor = GetDeltaColor(iSellDelta);
            }
        }

        [Column()]
        public double BuyDelta 
        {
            get
            {
                return iBuyDelta;
            }
            set
            {
                iBuyDelta = value;
                iBuyDelta = Math.Round(iBuyDelta, ROUND_VALUE);

                BuyDeltaColor = GetDeltaColor(iBuyDelta);
            }
        }

        public string CurrImg { get; set; }

        public string SellDeltaColor { get; set; }

        public string BuyDeltaColor { get; set; }

        /// <summary>
        /// Determines color of delta value
        /// </summary>
        /// <param name="aDeltaValue">Delta value</param>
        /// <returns>Delta color</returns>
        private string GetDeltaColor(double aDeltaValue)
        {
            string deltaColor = aDeltaValue.ToString();
            if (aDeltaValue > 0)
            {
                deltaColor = Color.FromArgb(255, 0, 255, 0).ToString();
            }
            else if (aDeltaValue < 0)
            {
                deltaColor = Color.FromArgb(255, 255, 0, 0).ToString();
            }
            else
            {
                deltaColor = Color.FromArgb(255, 125, 125, 125).ToString();
            }
            return deltaColor;
        }
    }
}
