using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SidebarMenuCache
    {
        public SidebarMenuCache()
        {
            SidebarMenuItems = new Dictionary<int, string>();
        }
        public Dictionary<int,string> SidebarMenuItems { get; private set; }

        public void AddSidebarMenuItems(Dictionary<int, string> menuItems)
        {
            this.SidebarMenuItems = menuItems;
        }

        public void Flush()
        {
            this.SidebarMenuItems = new Dictionary<int, string>();
        }
    }
}
