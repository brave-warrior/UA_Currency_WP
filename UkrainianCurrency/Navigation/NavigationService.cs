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
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.Text;

namespace UkrainianCurrency.Navigation
{
    /// <summary>
    /// Provides navigation to the View
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Navigates to the view by Uri
        /// </summary>
        /// <param name="uri">View uri</param>
        public void Navigate(string uri)
        {
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            rootFrame.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Navigates to the view by Uri with parameters
        /// </summary>
        /// <param name="uri">View uri</param>
        /// <param name="parameters">Parameters</param>
        public void Navigate(string uri, IDictionary<string, string> parameters)
        {
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (rootFrame != null)
            {
                StringBuilder fullUri = new StringBuilder();
                fullUri.Append(uri);
                if (parameters != null && parameters.Count > 0)
                {
                    fullUri.Append("?");
                    bool isAppendAmp = false;
                    foreach (KeyValuePair<string, string> keyValuePair in parameters)
                    {
                        if (isAppendAmp)
                        {
                            fullUri.Append("&");
                        }
                        fullUri.AppendFormat("{0}={1}", keyValuePair.Key, keyValuePair.Value);
                        isAppendAmp = true;
                    }
                }
                uri = fullUri.ToString();
                rootFrame.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
            }
        }
    }
}
