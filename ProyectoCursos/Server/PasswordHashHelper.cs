using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHashHelper
{
    public static string GetHashedPassword(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hash = sha256.ComputeHash(passwordBytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

    public static string GenerateSalt()
    {
        byte[] randomBytes = new byte[16];
        using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
        {
            rngCryptoServiceProvider.GetBytes(randomBytes);
        }
        return Convert.ToBase64String(randomBytes);
    }
}

