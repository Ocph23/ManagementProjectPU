using System;

using MonitoringPU.Models;

namespace MonitoringPU.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Project Item { get; set; }
        public ItemDetailViewModel(Project item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
