using System.Security.Cryptography;

namespace CUBETestAPI.Helpers
{
    public class RsaCryptoService
    {
        private readonly RSA _rsa;

        public RsaCryptoService()
        {
            _rsa = RSA.Create(2048);
        }

        public string Encrypt(string data)
        {
            var dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
            var encryptedData = _rsa.Encrypt(dataBytes, RSAEncryptionPadding.OaepSHA256);
            return System.Convert.ToBase64String(encryptedData);
        }

        public string Decrypt(string data)
        {
            var dataBytes = System.Convert.FromBase64String(data);
            var decryptedData = _rsa.Decrypt(dataBytes, RSAEncryptionPadding.OaepSHA256);
            return System.Text.Encoding.UTF8.GetString(decryptedData);
        }
        public string GetPublicKey()
        {
            return Convert.ToBase64String(_rsa.ExportRSAPublicKey());
        }
        public void ImportPublicKey(string publicKey)
        {
            var keyBytes = System.Convert.FromBase64String(publicKey);
            _rsa.ImportRSAPublicKey(keyBytes, out _);
        }
    }
}
