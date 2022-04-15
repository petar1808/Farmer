namespace Web.ViewModels.Components
{
    public class SidebarMenuViewModel
    {
        public string Title { get; set; } = default!;

        public string MenuOpen { get; set; } = "";

        public string Icon { get; set; } = default!;

        public List<SidebarChildElentViewModel> Childs { get; set; } = new List<SidebarChildElentViewModel>();
    }

    public class SidebarChildElentViewModel
    {
        public string Title { get; set; } = default!;

        public int? Raute { get; set; }

        public string ControllerName { get; set; } = default!;

        public string ActionName { get; set; } = default!;

        public string Active { get; set; } = "";
    }
}
