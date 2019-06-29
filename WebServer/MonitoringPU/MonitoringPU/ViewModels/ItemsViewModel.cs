using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MonitoringPU.Models;
using MonitoringPU.Views;

namespace MonitoringPU.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<project> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public INavigation Navigation { get; }

        public ItemsViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Title = "Proyek";
            Items = new ObservableCollection<project>();
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
                    item.ProgressCommand = new Command(async(x) => {
                        project param = x as project;
                        if(param!=null)
                        {
                          await  Navigation.PushAsync(new ProgressView(param));

                        }
                    });

                    item.PenilaianCommand = new Command(async (x) => {
                        project param = x as project;
                        if (param != null)
                        {
                            await Navigation.PushAsync(new PenilaianView(param));

                        }
                    });


                    item.LocationCommand = new Command(async (x) => {
                        project param = x as project;
                        if (param != null)
                        {
                            await Navigation.PushModalAsync(new LocationView(param));

                        }
                    });
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