using AutoMapper.Configuration.Annotations;
using System.Security.Cryptography;
using System.Text;
using Todolendar.API.Repositories.Interfaces;

namespace Todolendar.API.Repositories
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

            return new HashObject(Convert.ToHexString(hash), salt);
        }

        public bool ValidateHashedPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(Encoding.ASCII.GetBytes(password), salt, iterations, hashAlgorithm, keySize);
            var hashToCompareString = Convert.ToHexString(hashToCompare);

            return hashToCompareString.Equals(hash);
        }
    }
}
