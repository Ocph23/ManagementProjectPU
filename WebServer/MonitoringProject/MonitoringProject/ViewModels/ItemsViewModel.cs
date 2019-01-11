using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MonitoringProject.Models;
using MonitoringProject.Views;

namespace MonitoringProject.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Project> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Projects";
            Items = new ObservableCollection<Project>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Project>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Project;
            //    Items.Add(newItem);
            //    await Projects.AddItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await Projects.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}