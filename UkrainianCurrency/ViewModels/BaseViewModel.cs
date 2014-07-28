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
using GalaSoft.MvvmLight;
using UkrainianCurrency.Navigation;

namespace UkrainianCurrency.ViewModels
{
    /// <summary>
    /// Base class for all ViewModels
    /// </summary>
    public class BaseViewModel : ViewModelBase
    {
        public T GetService<T>() where T : class
        {
            if (typeof(T) == typeof(INavigationService))
            {
                return new NavigationService() as T;
            }
            return null;
        }
    }
}
