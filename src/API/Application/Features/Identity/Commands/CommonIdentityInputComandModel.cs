namespace Application.Features.Identity.Commands
{
    public class CommonIdentityInputComandModel
    {
        public string UserEmail { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string ActivateUserUrl { get; set; } = default!;
    }
}
