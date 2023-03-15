namespace Todolendar.API.Repositories.Interfaces
{
    public interface IHashHandler
    {
        HashObject HashPassword(string password);
        string ValidateHashedPassword(string password);
    }
}
