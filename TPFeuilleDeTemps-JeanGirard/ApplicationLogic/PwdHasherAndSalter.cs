using System.Security.Cryptography;
using System.Text;

namespace TPFeuilleDeTemps_JeanGirard.ApplicationLogic
{
    public static class PwdHasherAndSalter
    {
        public static string ComputeSha256SaltedHash(string email, string pwd)
        {
            string saltedPwd = email + ";" + pwd;
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedPwd));

            StringBuilder builder = new();
            foreach (byte t in bytes) builder.Append(t.ToString("x2"));

            return builder.ToString();
        }
    }
}