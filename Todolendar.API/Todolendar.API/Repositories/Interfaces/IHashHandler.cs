namespace Todolendar.API.Repositories.Interfaces
{
    public class HashObject
    {
        public string Hash { get; set; }
        public byte[] Salt { get; set; }

        public HashObject(string hash, byte[] salt)
        {
            Hash = hash;
            Salt = salt;    
        }
    }

    public interface IHashHandler
    {
        Task<HashObject> HashPassword(string password);
        Task<string> ValidateHashedPassword(string password);
    }
}
