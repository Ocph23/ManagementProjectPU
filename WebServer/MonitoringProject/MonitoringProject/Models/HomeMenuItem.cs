using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringProject.Models
{
    public enum MenuItemType
    {
        Home,
        Projects,
        Browse,
        About

    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
