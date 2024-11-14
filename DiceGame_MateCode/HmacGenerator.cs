using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame_MateCode
{
    public class HmacGenerator
    {
        private readonly byte[] key; 

        public HmacGenerator()
        {
            key = GenerateSecureKey();
        }

        public static byte[] GenerateSecureKey()
        {
            using(var rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[32];
                rng.GetBytes(key);
                return key;
            }
        }

        public int GenerateSecureRandom(int range)
        {
            using(var rng = RandomNumberGenerator.Create())
            {
                byte[] buffer = new byte[4];
                rng.GetBytes(buffer);
                int value = BitConverter.ToInt32(buffer, 0) & int.MaxValue;
                return value % range;
            }
        }

        public string GenerateHmac(int message)
        {
            using(var hmac = new HMACSHA256(key))
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message.ToString());
                byte[] hash = hmac.ComputeHash(messageBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public string RevealKey()
        {
            return BitConverter.ToString(key).Replace("-", "").ToLower();
        }
    }
}
