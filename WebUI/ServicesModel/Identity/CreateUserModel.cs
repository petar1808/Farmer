namespace WebUI.ServicesModel.Identity
{
    public class CreateUserModel
    {
        public string UserEmail { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string ActivateUserUrl { get; set; } = default!;
    }
}
