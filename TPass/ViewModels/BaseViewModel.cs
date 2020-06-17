using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TPass.XPInterfaces;
using Xamarin.Forms;

namespace TPass.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public IView Nav;

        bool isBusy;

        public bool IsBusy {
            get { return isBusy; }
            set {

                SetProperty(ref isBusy, value);
            }
        }

        public bool IsNotBusy {
            get { return !IsBusy; }
        }

        protected void SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {

            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
