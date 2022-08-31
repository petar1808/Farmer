namespace WebUI.Components.DeleteModal
{
    public class DeleteModalModel
    {
        public DeleteModalModel(int id, Action<int> deleteAction)
        {
            Id = id;
            DeleteAction = deleteAction;
        }

        public int Id { get; set; }

        public Action<int> DeleteAction { get; set; } = default!;
    }
}
