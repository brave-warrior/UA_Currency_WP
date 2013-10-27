using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UkrainianCurrency.Navigation
{
    public interface INavigationService
    {
        void Navigate(string uri);
        void Navigate(string uri, IDictionary<string, string> parameters);
    }
}
