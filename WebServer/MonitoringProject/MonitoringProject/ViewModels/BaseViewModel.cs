using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using MonitoringProject.Models;
using MonitoringProject.Services;

namespace MonitoringProject.ViewModels
{
    public class BaseViewModel :PropertyChange
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
        public IDataStore<Project> Projects => DependencyService.Get<IDataStore<Project>>() ?? new ProjectStore();

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
