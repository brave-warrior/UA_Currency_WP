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
using System.Linq;
using System.Data.Linq.Mapping;
using System.Collections.Generic;

namespace UkrainianCurrency.Models
{
    /// <summary>
    /// Provides operations with the Database
    /// </summary>
    public class DbEngine : DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        public static string DBConnectionString = @"Data Source=isostore:/CurrencyDb.sdf";

        // Pass the connection string to the base class.
        public DbEngine(string connectionString)
            : base(connectionString)
        {
            
        }

        /// <summary>
        /// Gets the data according to the Download
        /// </summary>
        public Table<DownloadData> DownloadData
        {
            get { return this.GetTable<DownloadData>(); }
        }

        /// <summary>
        /// Gets the data about Currency
        /// </summary>
        public Table<Currency> CurrencyList
        {
            get
            {
                Table<Currency> table = this.GetTable<Currency>();
                return table;
            }
        }

        /// <summary>
        /// Gets list of currence from DB
        /// </summary>
        /// <returns></returns>
        public IList<Currency> GetCurrency()
        {
            List<Currency> currencyList = new List<Currency>();
            using (var db = new DbEngine(DBConnectionString))
            {
                var query = from currency in db.CurrencyList select currency;
                currencyList = query.ToList();
            }

            return currencyList;
        }

        /// <summary>
        /// Adds currency to DB
        /// </summary>
        /// <param name="aCurrency"></param>
        public void AddCurrency(Currency aCurrency)
        {
            using (var db = new DbEngine(DBConnectionString))
            {
                db.CurrencyList.InsertOnSubmit(aCurrency);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Deletes all currency from DB
        /// </summary>
        public void DeleteAllCurrency()
        {
            using (var db = new DbEngine(DBConnectionString))
            {
                var query = from currency in db.CurrencyList select currency;
                db.CurrencyList.DeleteAllOnSubmit<Currency>(query);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Adds additional data to DB
        /// </summary>
        /// <param name="aData"></param>
        public void AddDownloadData(DownloadData aData)
        {
            using (var db = new DbEngine(DBConnectionString))
            {
                db.DownloadData.InsertOnSubmit(aData);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Gets list of download data from DB
        /// </summary>
        /// <returns></returns>
        public IList<DownloadData> GetDownloadData()
        {
            List<DownloadData> dataList = new List<DownloadData>();
            using (var db = new DbEngine(DBConnectionString))
            {
                var query = from data in db.DownloadData select data;
                dataList = query.ToList();
            }

            return dataList;
        }

        /// <summary>
        /// Deletes all service data from DB
        /// </summary>
        public void DeleteAllDownloadData()
        {
            using (var db = new DbEngine(DBConnectionString))
            {
                var query = from data in db.DownloadData select data;
                db.DownloadData.DeleteAllOnSubmit<DownloadData>(query);
                db.SubmitChanges();
            }
        }

    }

}
