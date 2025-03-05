using System.Security.Cryptography;

namespace apiStock.BLL.Services
{
    public class _encriptService
    {
        // Genera un hash de la contraseña con salt
        public static string HashPassword(string password)
        {
            // Genera un salt aleatorio
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Crea el hash usando PBKDF2 con 100,000 iteraciones
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20); // 20 bytes para el hash

            // Combina el salt y el hash
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convierte a base64 para almacenamiento
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        // Verifica si la contraseña coincide con el hash almacenado
        public static bool VerifyPassword(string password, string savedPasswordHash)
        {
            // Convierte el hash almacenado de base64 a bytes
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            // Extrae el salt
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Calcula el hash de la contraseña proporcionada
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);

            // Compara los hashes
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }
            return true;
        }
    }
}
