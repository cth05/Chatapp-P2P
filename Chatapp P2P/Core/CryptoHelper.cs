using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chatapp_P2P.Core
{
    public static class CryptoHelper
    {
        private static RSA rsa;
        public static byte[] SessionKey;
        private static bool ready = false;

        public static string EncryptedSessionKey { get; private set; }

        public static string GetPublicKey()
        {
            rsa = RSA.Create();
            var param = rsa.ExportParameters(false);
            return JsonConvert.SerializeObject(param);
        }

        public static void ImportRemotePublicKey(string json)
        {
            var param = JsonConvert.DeserializeObject<RSAParameters>(json);
            var rsaRemote = RSA.Create();
            rsaRemote.ImportParameters(param);

            SessionKey = RandomBytes(32);
            var enc = rsaRemote.Encrypt(SessionKey, RSAEncryptionPadding.OaepSHA1);
            EncryptedSessionKey = Convert.ToBase64String(enc);
        }

        public static void ImportEncryptedSessionKey(string encB64)
        {
            try
            {
                var enc = Convert.FromBase64String(encB64);
                SessionKey = rsa.Decrypt(enc, RSAEncryptionPadding.OaepSHA1);
                ready = true;
            }
            catch
            {
                ready = false;
                throw;
            }
        }

        public static bool IsReady => SessionKey != null && ready;

        public static string Encrypt(string plain)
        {
            if (!IsReady) return plain;

            byte[] iv = RandomBytes(16);
            byte[] cipher;

            using (var aes = new AesManaged())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = SessionKey;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plain);
                    cipher = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                }
            }

            byte[] mac;
            using (var hmac = new HMACSHA256(SessionKey))
            {
                mac = hmac.ComputeHash(iv.Concat(cipher).ToArray());
            }

            System.Diagnostics.Debug.WriteLine("Encrypt Plain: " + plain);
            System.Diagnostics.Debug.WriteLine("Encrypt IV: " + Convert.ToBase64String(iv));
            System.Diagnostics.Debug.WriteLine("Encrypt Cipher: " + Convert.ToBase64String(cipher));
            System.Diagnostics.Debug.WriteLine("Encrypt MAC: " + Convert.ToBase64String(mac));

            return ToB64(iv, cipher, mac);
        }

        public static string Decrypt(string b64)
        {
            if (!IsReady) return b64;

            try
            {
                var parts = FromB64(b64);
                if (parts.Length < 3) return b64;

                var iv = parts[0];
                var cipher = parts[1];
                var mac = parts[2];

                using (var hmac = new HMACSHA256(SessionKey))
                {
                    var check = hmac.ComputeHash(iv.Concat(cipher).ToArray());
                    if (!check.SequenceEqual(mac))
                        throw new CryptographicException("HMAC check failed!");
                }

                using (var aes = new AesManaged())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = SessionKey;
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor())
                    {
                        byte[] plain = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);
                        return Encoding.UTF8.GetString(plain);
                    }
                }
            }
            catch
            {
                return "[Decrypt Error]";
            }
        }

        private static string ToB64(params byte[][] parts)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                foreach (var p in parts)
                {
                    var len = BitConverter.GetBytes(p.Length);
                    ms.Write(len, 0, 4);
                    ms.Write(p, 0, p.Length);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        private static byte[][] FromB64(string b64)
        {
            if (string.IsNullOrWhiteSpace(b64))
                return Array.Empty<byte[]>();

            byte[] all;
            try
            {
                all = Convert.FromBase64String(b64.Trim());
            }
            catch
            {
                return Array.Empty<byte[]>();
            }

            using (var ms = new System.IO.MemoryStream(all))
            {
                var list = new System.Collections.Generic.List<byte[]>();
                while (ms.Position < ms.Length)
                {
                    var lenBytes = new byte[4];
                    if (ms.Read(lenBytes, 0, 4) != 4) break;

                    int len = BitConverter.ToInt32(lenBytes, 0);
                    if (len <= 0 || len > ms.Length - ms.Position) break;

                    var part = new byte[len];
                    if (ms.Read(part, 0, len) != len) break;

                    list.Add(part);
                }
                return list.ToArray();
            }
        }

        private static byte[] RandomBytes(int size)
        {
            var b = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(b);
            }
            return b;
        }
    }
}
