using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace JamSoft.Helpers.Crypto
{
    internal class RsaCrypto : IRsaCrypto
    {
        private readonly RSA _rsa;

        public bool Initialised { get; private set; }

        public string PrivateKey { get; private set; }

        public string PublicKey { get; private set; }

        private RSAParameters _privateKey;
        private RSAParameters _publicKey;
        private int _keySize = 2048;

        public RsaCrypto(string privateKeyXml, string publicKeyXml)
        {
            if (string.IsNullOrEmpty(privateKeyXml) &&
                string.IsNullOrEmpty(publicKeyXml))
            {
                throw new ArgumentException("Ctor requires either a public or private key");
            }

            if (!string.IsNullOrEmpty(privateKeyXml))
            {
                _privateKey = StringToRsaParam(privateKeyXml);
            }

            if (!string.IsNullOrEmpty(publicKeyXml))
            {
                _publicKey = StringToRsaParam(publicKeyXml);
            }
        }

        public RsaCrypto(RSAParameters privateKey, RSAParameters publicKey)
        {
            _privateKey = privateKey;
            _publicKey = publicKey;
            Initialised = true;
        }

        public RsaCrypto() { }

        public RsaCrypto(int keySize)
        {
            _keySize = keySize;
        }

        public RsaCrypto(RSA rsa)
        {
            _rsa = rsa;

            SetKeys(_rsa);

            Initialised = true;
        }

        private void SetKeys(RSA rsa)
        {
            _privateKey = rsa.ExportParameters(true);
            PrivateKey = rsa.ToXmlString(true);

            _publicKey = rsa.ExportParameters(false);
            PublicKey = rsa.ToXmlString(false);
        }

        public void KeyGen()
        {
            using (var rsa = CreateCrypto())
            {
                SetKeys(rsa);
                Initialised = true;
            }
        }

        public string SignData(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return null;
            }

            if (!Initialised)
            {
                return null;
            }

            byte[] signedBytes;
            using (var rsa = CreateCrypto())
            {
                var encoder = new UTF8Encoding();
                byte[] originalData = encoder.GetBytes(message);

                rsa.ImportParameters(_privateKey);

                signedBytes = rsa.SignData(originalData, 0, originalData.Length, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
            }

            return Convert.ToBase64String(signedBytes);
        }

        public bool VerifyData(string originalMessage, string signedMessage)
        {
            if (string.IsNullOrEmpty(originalMessage))
            {
                return false;
            }

            if (string.IsNullOrEmpty(signedMessage))
            {
                return false;
            }

            if (!Initialised)
            {
                return false;
            }

            bool success;
            using (var rsa = CreateCrypto())
            {
                var encoder = new UTF8Encoding();
                byte[] bytesToVerify = encoder.GetBytes(originalMessage);
                byte[] signedBytes = Convert.FromBase64String(signedMessage);

                rsa.ImportParameters(_publicKey);

                success = rsa.VerifyData(bytesToVerify, signedBytes, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
            }

            return success;
        }

        public string SignHash(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return null;
            }

            if (!Initialised)
            {
                return null;
            }

            byte[] signedBytes;
            using (var rsa = CreateCrypto())
            {
                var encoder = new UTF8Encoding();
                byte[] originalData = encoder.GetBytes(message);

                rsa.ImportParameters(_privateKey);

                using (var hash = SHA512.Create())
                {
                    signedBytes = rsa.SignHash(hash.ComputeHash(originalData), HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
                }
            }

            return Convert.ToBase64String(signedBytes);
        }

        public bool VerifyHash(string originalMessage, string signedMessage)
        {
            if (string.IsNullOrEmpty(originalMessage))
            {
                return false;
            }

            if (string.IsNullOrEmpty(signedMessage))
            {
                return false;
            }

            if (!Initialised)
            {
                return false;
            }

            bool success;
            using (var rsa = CreateCrypto())
            {
                var encoder = new UTF8Encoding();
                byte[] bytesToVerify = encoder.GetBytes(originalMessage);
                byte[] signedBytes = Convert.FromBase64String(signedMessage);

                rsa.ImportParameters(_publicKey);

                using (var hash = SHA512.Create())
                {
                    var h = hash.ComputeHash(bytesToVerify);
                    success = rsa.VerifyHash(h, signedBytes, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
                }
            }

            return success;
        }

        private RSAParameters StringToRsaParam(string p)
        {
            var sr = new StringReader(p);
            var xs = new XmlSerializer(typeof(RSAParameters));
            var key = xs.Deserialize(sr);
            Initialised = true;
            return (RSAParameters)key;
        }

        private RSA CreateCrypto()
        {
            // if the default has been overridden, return that implementation
            if (_rsa != null)
            {
                return _rsa;
            }
#if NET46
            // If your baseline is .NET 4.6.2 or higher prefer RSACng
            // or 4.6+ if you are never giving the object back to the framework
            // (4.6.2 improved the framework's handling of those objects)
            // On older versions RSACryptoServiceProvider is the only way to go.
            return new RSACng(_keySize);
#else
            var c = RSA.Create();
            c.KeySize = _keySize;
            return c;
#endif
        }
    }
}
