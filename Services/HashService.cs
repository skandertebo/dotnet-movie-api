using System.Security.Cryptography;
using System.Text;

namespace TP1.Services
{
    public interface IHashingService
    {
        string ComputeHash(string input);
        bool VerifyHash(string input, string storedHash);
    }

    public class HashingService : IHashingService
    {
        public string ComputeHash(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }

        public bool VerifyHash(string input, string storedHash)
        {
            string computedHash = ComputeHash(input);
            return computedHash.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }

}
