using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace JamSoft.Helpers.Crypto
{
    internal sealed class RsaCrypto : IRsaCrypto
    {
        private bool _disposed;
        private readonly RSA _rsa;

        public string PrivateKey { get; private set; }

        public string PublicKey { get; private set; }

        private RSAParameters _privateKey;
        private RSAParameters _publicKey;
        private int _keySize = 2048;
        private HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA512;
        private RSASignaturePadding _rsaSignaturePadding = RSASignaturePadding.Pkcs1;

        public RsaCrypto(string privateKeyXml, string publicKeyXml, HashAlgorithmName hashAlgorithmName, RSASignaturePadding padding)
            : this(privateKeyXml, publicKeyXml)
        {
            _hashAlgorithmName = hashAlgorithmName;
            _rsaSignaturePadding = padding;
        }

        public RsaCrypto(string privateKeyXml, string publicKeyXml)
        {
            if (string.IsNullOrEmpty(privateKeyXml) &&
                string.IsNullOrEmpty(publicKeyXml))
            {
                throw new ArgumentException($"Ctor requires at least one key, {nameof(privateKeyXml)} and {nameof(publicKeyXml)} are null or empty");
            }

            if (!string.IsNullOrEmpty(privateKeyXml))
            {
                _privateKey = StringToRsaParam(privateKeyXml);
                PrivateKey = privateKeyXml;
            }

            if (!string.IsNullOrEmpty(publicKeyXml))
            {
                _publicKey = StringToRsaParam(publicKeyXml);
                PublicKey = publicKeyXml;
            }
        }

        public RsaCrypto(RSAParameters privateKey, RSAParameters publicKey, HashAlgorithmName hashAlgorithmName, RSASignaturePadding padding)
            :this (privateKey, publicKey)
        {
            _hashAlgorithmName = hashAlgorithmName;
            _rsaSignaturePadding = padding;
        }

        public RsaCrypto(RSAParameters privateKey, RSAParameters publicKey)
        {
            if (privateKey.IsNull() && publicKey.IsNull())
            {
                throw new ArgumentException($"Ctor requires at least one key, {nameof(privateKey)} and {nameof(publicKey)} are null or empty");
            }

            _privateKey = privateKey;
            PrivateKey = RsaParamToString(_privateKey);
            _publicKey = publicKey;
            PublicKey = RsaParamToString(_publicKey);
        }

        public RsaCrypto() { }

        public RsaCrypto(HashAlgorithmName hashAlgorithmName, RSASignaturePadding padding)
        {
            _hashAlgorithmName = hashAlgorithmName;
            _rsaSignaturePadding = padding;
        }

        public RsaCrypto(int keySize, HashAlgorithmName hashAlgorithmName, RSASignaturePadding padding)
            :this(keySize)
        {
            _hashAlgorithmName = hashAlgorithmName;
            _rsaSignaturePadding = padding;
        }

        public RsaCrypto(int keySize)
        {
            _keySize = keySize;
        }

        public RsaCrypto(RSA rsa)
        {
            _rsa = rsa;

            SetKeys(_rsa);
        }

        private void SetKeys(RSA rsa)
        {
            _privateKey = rsa.ExportParameters(true);
            PrivateKey = RsaParamToString(_privateKey);

            _publicKey = rsa.ExportParameters(false);
            PublicKey = RsaParamToString(_publicKey);
        }

        public string SignData(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return null;
            }

            byte[] signedBytes;
            using (var rsa = CreateCrypto())
            {
                var encoder = new UTF8Encoding();
                byte[] originalData = encoder.GetBytes(message);

                if (!_privateKey.IsNull())
                {
                    rsa.ImportParameters(_privateKey);
                }
                else
                {
                    // make new keys accesible to consumers for storage
                    SetKeys(rsa);
                }

                signedBytes = rsa.SignData(originalData, 0, originalData.Length, _hashAlgorithmName, _rsaSignaturePadding);
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

            if (_publicKey.IsNull())
            {
                throw new ArgumentException("The public key value for verification has not been provided, you must either provide them or call SignData before attempting to verify data", nameof(PublicKey));
            }

            bool success;
            using (var rsa = CreateCrypto())
            {
                var encoder = new UTF8Encoding();
                byte[] bytesToVerify = encoder.GetBytes(originalMessage);
                byte[] signedBytes = Convert.FromBase64String(signedMessage);

                rsa.ImportParameters(_publicKey);

                success = rsa.VerifyData(bytesToVerify, signedBytes, _hashAlgorithmName, _rsaSignaturePadding);
            }

            return success;
        }

        public string SignHash(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return null;
            }

            byte[] signedBytes;
            using (var rsa = CreateCrypto())
            {
                var encoder = new UTF8Encoding();
                byte[] originalData = encoder.GetBytes(message);

                if (!_privateKey.IsNull())
                {
                    rsa.ImportParameters(_privateKey);
                }
                else
                {
                    // make new keys accesible to consumers for storage
                    SetKeys(rsa);
                }

                using (var hash = GetHasher(_hashAlgorithmName))
                {
                    signedBytes = rsa.SignHash(hash.ComputeHash(originalData), _hashAlgorithmName, _rsaSignaturePadding);
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

            if (_publicKey.IsNull())
            {
                throw new ArgumentException("The public key value for verification has not been provided, you must either provide them or call SignData before attempting to verify data", nameof(PublicKey));
            }

            bool success;
            using (var rsa = CreateCrypto())
            {
                var encoder = new UTF8Encoding();
                byte[] bytesToVerify = encoder.GetBytes(originalMessage);
                byte[] signedBytes = Convert.FromBase64String(signedMessage);

                rsa.ImportParameters(_publicKey);
                using (var hasher = GetHasher(_hashAlgorithmName))
                {
                    var h = hasher.ComputeHash(bytesToVerify);
                    success = rsa.VerifyHash(h, signedBytes, _hashAlgorithmName, _rsaSignaturePadding);
                }
            }

            return success;
        }

        // ToXmlString() is not supported in .Net Core 2.x so have implemented for compatibility
        private string RsaParamToString(RSAParameters p)
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, p);
            return sw.ToString();
        }

        private RSAParameters StringToRsaParam(string p)
        {
            var sr = new StringReader(p);
            var xs = new XmlSerializer(typeof(RSAParameters));
            var key = xs.Deserialize(sr);
            return (RSAParameters)key;
        }

        private HashAlgorithm GetHasher(HashAlgorithmName name)
        {
            HashAlgorithm algo;
            switch (name.Name)
            {
                case "MD5":
                    algo = MD5.Create();
                    break;
                case "SHA1":
                    algo = SHA1.Create();
                    break;
                case "SHA256":
                    algo = SHA256.Create();
                    break;
                case "SHA384":
                    algo = SHA384.Create();
                    break;
                default:
                    algo = SHA512.Create();
                    break;
            }

            return algo;
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    PublicKey = null;
                    PrivateKey = null;
                }

                _rsa?.Clear();
                _disposed = true;
            }
        }
    }
}
