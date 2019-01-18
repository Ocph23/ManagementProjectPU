using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonitoringPU.Models;

namespace MonitoringPU.Services
{
    public class ProjectDataStore : IDataStore<Project>
    {
        List<Project> items;

        public ProjectDataStore()
        {
          
        }

        public async Task<bool> AddItemAsync(Project item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Project item)
        {
            var oldItem = items.Where((Project arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Project arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Project> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Project>> GetItemsAsync(bool forceRefresh = false)
        {
            items = new List<Project>();
            var mockItems = new List<Project>
            {
                new Project { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." }

            };

            foreach (var item in mockItems)
            {
                item.ListPenilaian = new System.Collections.ObjectModel.ObservableCollection<ItemPenilaian>();
                item.ListPenilaian.Add(new ItemPenilaian { Capaian = 10, Target = 15, Description = "Rincian Pekerjaan di rincikan disini agar lebih jelas apa yang akan dikerjakan", Nama = "Pekerjaan Dasar", Id = 1 });
                item.ListPenilaian.Add(new ItemPenilaian { Capaian = 17, Target = 20, Description = "Rincian Pekerjaan di rincikan disini agar lebih jelas apa yang akan dikerjakan", Nama = "Pekerjaan Pengatakan", Id = 2 });
                item.ListPenilaian.Add(new ItemPenilaian { Capaian = 13, Target = 60, Description = "Rincian Pekerjaan di rincikan disini agar lebih jelas apa yang akan dikerjakan", Nama = "Pekerjaan Penyelesaan", Id = 3 });
                items.Add(item);
            }
            return await Task.FromResult(items);
        }
    }
}