namespace WebUI.Components.DeleteModal
{
    public class DeleteModalModel
    {
        public DeleteModalModel(int id, Func<int, Task<bool>> deleteFuction)
        {
            Id = id;
            DeleteFuction = deleteFuction;
        }

        public int Id { get; set; }

        public Func<int, Task<bool>> DeleteFuction { get; set; } = default!;
    }
}
