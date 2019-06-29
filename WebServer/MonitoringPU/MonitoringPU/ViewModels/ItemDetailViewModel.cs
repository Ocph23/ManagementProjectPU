using System;

using MonitoringPU.Models;

namespace MonitoringPU.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public project Item { get; set; }
        public ItemDetailViewModel(project item = null)
        {
            Title = item?.NamaPekerjaan;
            Item = item;
        }
    }
}
