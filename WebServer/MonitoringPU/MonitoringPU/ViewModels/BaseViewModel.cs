using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using MonitoringPU.Models;
using MonitoringPU.Services;

namespace MonitoringPU.ViewModels
{
    public class BaseViewModel:BaseNotify
    {
        public IDataStore<project> Projects => DependencyService.Get<IDataStore<project>>() ?? new ProjectDataStore();
        public IAuthService<AuthenticationToken> AuthDataService => DependencyService.Get<IAuthService<AuthenticationToken>>() ?? new AuthService();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
     
    }
}
