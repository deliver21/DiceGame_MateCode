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

        public string GenerateHmac(int message)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message.ToString());
                byte[] hash = hmac.ComputeHash(messageBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public string RevealKey() => BitConverter.ToString(key).Replace("-", "").ToLower();

        private static byte[] GenerateSecureKey()
        {
            var key = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }

        public int GenerateSecureRandom(int range)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomNumber = new byte[1];
                do
                {
                    rng.GetBytes(randomNumber);
                } while (randomNumber[0] >= (256 - (256 % range)));
                return randomNumber[0] % range;
            }
        }
    }
}
