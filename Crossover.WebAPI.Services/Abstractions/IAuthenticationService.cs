namespace Crossover.WebAPI.Services.Abstractions
{
    internal interface IAuthenticationService
    {
        bool Authenticate(string user, string password);
    }
}