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
        public IDataStore<Project> Projects => DependencyService.Get<IDataStore<Project>>() ?? new ProjectDataStore();

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
