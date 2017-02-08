namespace Movie.API.Services
{
    public interface IMovieIdentityService
    {
        string CurrentUser { get; }
    }
}