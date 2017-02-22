namespace Movie.API.Services
{
    using System.Security.Principal;

    public interface IMovieIdentityService
    {
        IIdentity CurrentUser { get; }
    }
}