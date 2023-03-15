namespace Todolendar.API.Repositories.Interfaces
{
    public interface IHashHandler
    {
        HashObject HashPassword(string password);
        bool ValidateHashedPassword(string password, string hash, string salt);
    }
}
