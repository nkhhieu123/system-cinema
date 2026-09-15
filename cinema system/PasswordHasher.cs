using System;
using System.Security.Cryptography;

namespace cinema_system
{
    // Băm mật khẩu bằng PBKDF2-SHA256 có salt ngẫu nhiên.
    // Chuỗi lưu trong cột TaiKhoan.Pass có dạng: PBKDF2$<số vòng lặp>$<salt base64>$<hash base64>
    internal static class PasswordHasher
    {
        private const string Prefix = "PBKDF2";
        private const int Iterations = 100000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public static string Hash(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = Derive(password, salt, Iterations, HashSize);
            return string.Join("$", Prefix, Iterations, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        // needsRehash = true khi mật khẩu trong DB còn là văn bản thường (dữ liệu cũ) và nhập đúng,
        // lúc đó nên lưu lại bằng Hash() để không còn mật khẩu chưa băm trong DB.
        public static bool Verify(string password, string stored, out bool needsRehash)
        {
            needsRehash = false;
            if (string.IsNullOrEmpty(stored))
                return false;

            string[] parts = stored.Split('$');
            if (parts.Length != 4 || parts[0] != Prefix)
            {
                bool match = stored == password;
                needsRehash = match;
                return match;
            }

            int iterations;
            byte[] salt, expected;
            try
            {
                iterations = int.Parse(parts[1]);
                salt = Convert.FromBase64String(parts[2]);
                expected = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }

            byte[] actual = Derive(password, salt, iterations, expected.Length);
            return FixedTimeEquals(actual, expected);
        }

        private static byte[] Derive(string password, byte[] salt, int iterations, int length)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(length);
            }
        }

        // So sánh không dừng sớm để tránh đoán mật khẩu qua thời gian phản hồi
        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
