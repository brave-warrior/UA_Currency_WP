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
using System.IO.IsolatedStorage;

namespace UkrainianCurrency.Models
{
    public class Settings
    {
        // The isolated storage key names of our settings
        const string CITY_KEY = "City";
        const string BANK_KEY = "Country";

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="aKey"></param>
        /// <param name="aValue"></param>
        /// <returns></returns>
        public static bool AddOrUpdateValue(string aKey, Object aValue)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            bool valueChanged = false;

            // If the key exists
            if (settings.Contains(aKey))
            {
                // If the value has changed
                if (settings[aKey] != aValue)
                {
                    // Store the new value
                    settings[aKey] = aValue;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                settings.Add(aKey, aValue);
                valueChanged = true;
            }
            return valueChanged;
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aKey"></param>
        /// <param name="aDefaultValue"></param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(string aKey, T aDefaultValue)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            T value;

            // If the key exists, retrieve the value.
            if (settings.Contains(aKey))
            {
                value = (T)settings[aKey];
            }
            // Otherwise, use the default value.
            else
            {
                value = aDefaultValue;
            }
            return value;
        }

        /// <summary>
        /// Gets bank
        /// </summary>
        /// <param name="aDefault">Default value</param>
        /// <returns></returns>
        public static int GetBank(int aDefault)
        {
            return GetValueOrDefault<int>(BANK_KEY, aDefault);
        }

        /// <summary>
        /// Saves bank
        /// </summary>
        /// <param name="aNewState">New bank</param>
        public static void SaveBank(int aNewState)
        {
            if (AddOrUpdateValue(BANK_KEY, aNewState))
            {
                Save();
            }
        }

        /// <summary>
        /// Gets City
        /// </summary>
        /// <param name="aDefault">Default</param>
        /// <returns></returns>
        public static int GetCity(int aDefault)
        {
            return GetValueOrDefault<int>(CITY_KEY, aDefault);
        }

        /// <summary>
        /// Saves city
        /// </summary>
        /// <param name="aNewState">New city</param>
        public static void SaveCity(int aNewState)
        {
            if (AddOrUpdateValue(CITY_KEY, aNewState))
            {
                Save();
            }
        }

        /// <summary>
        /// Stores data
        /// </summary>
        public static void Save()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Save();
        }
    }
}
