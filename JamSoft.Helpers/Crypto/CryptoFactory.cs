using System.Security.Cryptography;

namespace JamSoft.Helpers.Crypto
{
    /// <summary>
    /// Provides access to the various Crypto classes
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CryptoFactory : ICryptoFactory
    {
        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto"/> using the provided keys.<para />
        /// You can provide both keys or just the private key for signing, or just the public key for verification<para />
        /// The keys can be extracted in xml format from the <seealso cref="IRsaCrypto.PublicKey"/> and <seealso cref="IRsaCrypto.PrivateKey"/> properties<para />
        /// NEVER DISTRIBUTE PRIVATE KEYS
        /// </summary>
        /// <param name="privateKeyXml">The private key xml.</param>
        /// <param name="publicKeyXml">The public key xml.</param>
        /// <returns></returns>
        public IRsaCrypto Create(string privateKeyXml, string publicKeyXml)
        {
            return new RsaCrypto(privateKeyXml, publicKeyXml);
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /> using the provided RSAParameters objects.<para />
        /// You can provide both keys or just the private key for signing, or just the public key for verification
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="publicKey">The public key.</param>
        /// <returns></returns>
        public IRsaCrypto Create(RSAParameters privateKey, RSAParameters publicKey)
        {
            return new RsaCrypto(privateKey, publicKey);
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /> using the provided RSAParameters objects.<para />
        /// </summary>
        /// <returns></returns>
        public IRsaCrypto Create()
        {
            var c = new RsaCrypto();
            c.KeyGen();
            return c;
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /> using the provided key size
        /// </summary>
        /// <returns></returns>
        public IRsaCrypto Create(int keySize)
        {
            var c = new RsaCrypto(keySize);
            c.KeyGen();
            return c;
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /> and uses the provided RSA implementation
        /// </summary>
        /// <param name="rsa">The RSA.</param>
        /// <returns></returns>
        public IRsaCrypto Create(RSA rsa)
        {
            return new RsaCrypto(rsa);
        }
    }
}
