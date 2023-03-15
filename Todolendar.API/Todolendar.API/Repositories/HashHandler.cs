using System.Security.Cryptography;
using System.Text;
using Todolendar.API.Repositories.Interfaces;

namespace Todolendar.API.Repositories
{
    public class HashHandler : IHashHandler
    {
        public HashObject HashPassword(string password)
        {
            const int keySize = 64;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            var salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return new HashObject(Convert.ToHexString(hash), salt);
        }

        public string ValidateHashedPassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}
