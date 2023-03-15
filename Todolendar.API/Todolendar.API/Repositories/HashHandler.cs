using AutoMapper.Configuration.Annotations;
using System.Security.Cryptography;
using System.Text;
using Todolendar.API.Repositories.Interfaces;

namespace Todolendar.API.Repositories
{
    public class HashObject
    {
        public string Hash { get; set; }
        public string Salt { get; set; }

        public HashObject(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }

    public class HashHandler : IHashHandler
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public HashObject HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            var saltString = Encoding.Default.GetString(salt);

            return new HashObject(Convert.ToHexString(hash), saltString);
        }

        public bool ValidateHashedPassword(string password, string hash, string salt)
        {

            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(Encoding.ASCII.GetBytes(password), Encoding.ASCII.GetBytes(salt), iterations, hashAlgorithm, keySize);

            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
    }
}
