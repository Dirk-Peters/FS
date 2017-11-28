using System;
using System.Security.Cryptography;
using System.Text;

namespace chat_server.Domain
{
    public sealed class HashedString
    {
        private readonly string salt;
        private readonly string value;

        private HashedString(string value, string salt)
        {
            this.value = value;
            this.salt = salt;
        }

        public bool Equals(string unhashedValue) => Equals(Hash(unhashedValue, salt));

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is HashedString s && Equals(s);
        }

        private bool Equals(HashedString other)
            => string.Equals(salt, other.salt) && string.Equals(value, other.value);

        public override int GetHashCode()
        {
            unchecked
            {
                return ((salt != null ? salt.GetHashCode() : 0) * 397) ^ (value != null ? value.GetHashCode() : 0);
            }
        }

        public static HashedString Hash(string rawString)
            => Hash(rawString, Guid.NewGuid().ToString());

        private static HashedString Hash(string rawString, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawString + salt));
                return new HashedString(Convert.ToBase64String(hashed), salt);
            }
        }
    }
}