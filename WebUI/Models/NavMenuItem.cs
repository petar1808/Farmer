namespace WebUI.Models
{
    public class NavMenuItem
    {
        public string Name { get; set; } = default!;
        public string Icon { get; set; } = default!;
        public string Path { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool Expanded { get; set; }
        public IEnumerable<NavMenuItem> Children { get; set; } = new List<NavMenuItem>();
        public IEnumerable<string> Tags { get; set; } = new List<string>();
    }
}
