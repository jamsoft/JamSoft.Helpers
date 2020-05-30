using System;
using System.Security.Cryptography;

namespace JamSoft.Helpers.Crypto
{
    /// <summary>
    /// Creates instances of the RsaCrypto class and implements <see cref="ICryptoFactory"/><para />
    /// Designed as a DI container friendly factory class
    /// </summary>
    public sealed class CryptoFactory : ICryptoFactory
    {
        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /><para />
        /// Defaults to using <see cref="HashAlgorithmName.SHA512"/> and <see cref="RSASignaturePadding.Pkcs1"/> with a 2048-bit key<para />
        /// </summary>
        /// <returns>An instance of <seealso cref="IRsaCrypto"/></returns>
        public IRsaCrypto Create()
        {
            var c = new RsaCrypto();
            return c;
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /> using the provided key size<para />
        /// Defaults to using <see cref="HashAlgorithmName.SHA512"/> and <see cref="RSASignaturePadding.Pkcs1"/> and using the provided key size
        /// </summary>
        /// <param name="keySize">The integer key size to use</param>
        /// <returns>An instance of <seealso cref="IRsaCrypto"/></returns>
        public IRsaCrypto Create(int keySize)
        {
            var c = new RsaCrypto(keySize);
            return c;
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto"/> using the provided keys.<para />
        /// You can provide both keys or just the private key for signing, or just the public key for verification<para />
        /// Defaults to using <see cref="HashAlgorithmName.SHA512"/> and <see cref="RSASignaturePadding.Pkcs1"/> with a 2048-bit key<para />
        /// The keys can be extracted in xml format from the <seealso cref="IRsaCrypto.PublicKey"/> and <seealso cref="IRsaCrypto.PrivateKey"/> properties<para />
        /// </summary>
        /// <param name="privateKeyXml">The private key xml.</param>
        /// <param name="publicKeyXml">The public key xml.</param>
        /// <returns>An instance of <seealso cref="IRsaCrypto"/></returns>
        /// <exception cref="ArgumentException">thrown when both the public key and private keys a null or empty</exception>
        public IRsaCrypto Create(string privateKeyXml, string publicKeyXml)
        {
            return new RsaCrypto(privateKeyXml, publicKeyXml);
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto"/> using the provided keys.<para />
        /// You can provide both keys or just the private key for signing, or just the public key for verification<para />
        /// The keys can be extracted in xml format from the <seealso cref="IRsaCrypto.PublicKey"/> and <seealso cref="IRsaCrypto.PrivateKey"/> properties<para />
        /// </summary>
        /// <param name="privateKeyXml">The private key xml.</param>
        /// <param name="publicKeyXml">The public key xml.</param>
        /// <param name="hashAlgorithmName">The hashing algorithm to use</param>
        /// <param name="padding">The padding to be used</param>
        /// <returns>An instance of <seealso cref="IRsaCrypto"/></returns>
        public IRsaCrypto Create(string privateKeyXml, string publicKeyXml, HashAlgorithmName hashAlgorithmName, RSASignaturePadding padding)
        {
            return new RsaCrypto(privateKeyXml, publicKeyXml, hashAlgorithmName, padding);
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /> using the provided RSAParameters objects.<para />
        /// You can provide both keys or just the private key for signing, or just the public key for verification<para />
        /// Defaults to using <see cref="HashAlgorithmName.SHA512"/> and <see cref="RSASignaturePadding.Pkcs1"/> with a 2048-bit key<para />
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="publicKey">The public key.</param>
        /// <returns>An instance of <seealso cref="IRsaCrypto"/></returns>
        public IRsaCrypto Create(RSAParameters privateKey, RSAParameters publicKey)
        {
            return new RsaCrypto(privateKey, publicKey);
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /> using the provided RSAParameters objects.<para />
        /// You can provide both keys or just the private key for signing, or just the public key for verification<para />
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="hashAlgorithmName">The hashing algorithm to use</param>
        /// <param name="padding">The padding to be used</param>
        /// <returns>An instance of <seealso cref="IRsaCrypto"/></returns>
        public IRsaCrypto Create(RSAParameters privateKey, RSAParameters publicKey, HashAlgorithmName hashAlgorithmName, RSASignaturePadding padding)
        {
            return new RsaCrypto(privateKey, publicKey, hashAlgorithmName, padding);
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /><para />
        /// Defaults to using <see cref="HashAlgorithmName.SHA512"/> and <see cref="RSASignaturePadding.Pkcs1"/> with a 2048-bit key<para />
        /// </summary>
        /// <param name="hashAlgorithmName">The hashing algorithm to use</param>
        /// <param name="padding">The padding to be used</param>
        /// <returns>An instance of <seealso cref="IRsaCrypto"/></returns>
        public IRsaCrypto Create(HashAlgorithmName hashAlgorithmName, RSASignaturePadding padding)
        {
            var c = new RsaCrypto(hashAlgorithmName, padding);
            return c;
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /> using the provided key size, hashing and padding values<para />
        /// </summary>
        /// <param name="keySize">The integer key size to use</param>
        /// <param name="hashAlgorithmName">The hashing algorithm to use</param>
        /// <param name="padding">The padding to be used</param>
        /// <returns>An instance of <seealso cref="IRsaCrypto"/></returns>
        public IRsaCrypto Create(int keySize, HashAlgorithmName hashAlgorithmName, RSASignaturePadding padding)
        {
            var c = new RsaCrypto(keySize, hashAlgorithmName, padding);
            return c;
        }

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /> and uses the provided RSA implementation
        /// </summary>
        /// <param name="rsa">The RSA.</param>
        /// <returns>An instance of <seealso cref="IRsaCrypto"/></returns>
        public IRsaCrypto Create(RSA rsa)
        {
            return new RsaCrypto(rsa);
        }
    }
}
