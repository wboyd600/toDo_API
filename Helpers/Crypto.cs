using System.Security.Cryptography;
using System.Text;

namespace toDo_API.Helpers {
    class Crypto {
        private const int SALT_SIZE = 8;
        private const int NUM_ITERATIONS = 1000;

        public static string ComputeHash(string password, string salt)
        {
            
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            return hashPassword;
        }

        public static string GenerateSalt() {
            byte[] buf = new byte[SALT_SIZE];
            var random = new Random();
            random.NextBytes(buf);
            string salt = Convert.ToBase64String(buf);
            return salt;
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(enteredPassword), Encoding.UTF8.GetBytes(storedSalt), 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
    }
}