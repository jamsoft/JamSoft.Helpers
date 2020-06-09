using System.Security.Cryptography;

namespace JamSoft.Helpers.Crypto
{
    /// <summary>
    /// Facilitates the registration of a factory in a DI container
    /// </summary>
    public interface ICryptoFactory
    {
        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto"/> using the provided keys in Xml format.<para />
        /// You can provide both keys or just the private key for signing, or just the public key for verification
        /// </summary>
        /// <param name="privateKeyXml">The private key Xml.</param>
        /// <param name="publicKeyXml">The public key Xml.</param>
        /// <returns></returns>
        IRsaCrypto Create(string privateKeyXml, string publicKeyXml);

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
        IRsaCrypto Create(string privateKeyXml, string publicKeyXml, HashAlgorithmName hashAlgorithmName, RSASignaturePadding padding);

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto"/> using the provided RSAParameters objects.<para />
        /// You can provide both keys or just the private key for signing, or just the public key for verification
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="publicKey">The public key.</param>
        /// <returns></returns>
        IRsaCrypto Create(RSAParameters privateKey, RSAParameters publicKey);

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /> using the provided RSAParameters objects.<para />
        /// You can provide both keys or just the private key for signing, or just the public key for verification<para />
        /// Defaults to using <see cref="HashAlgorithmName.SHA512"/> and <see cref="RSASignaturePadding.Pkcs1"/> with a 2048-bit key<para />
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="hashAlgorithmName">The hashing algorithm to use</param>
        /// <param name="padding">The padding to be used</param>
        /// <returns>An instance of <seealso cref="IRsaCrypto"/></returns>
        IRsaCrypto Create(RSAParameters privateKey, RSAParameters publicKey, HashAlgorithmName hashAlgorithmName, RSASignaturePadding padding);

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto"/>.
        /// </summary>
        /// <returns></returns>
        IRsaCrypto Create();

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto"/>.
        /// </summary>
        /// <returns></returns>
        IRsaCrypto Create(int keySize);

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto" /> using the provided key size
        /// Defaults to using <see cref="HashAlgorithmName.SHA512"/> and <see cref="RSASignaturePadding.Pkcs1"/> and using the provided key size<para />
        /// </summary>
        /// <param name="keySize">The integer key size to use</param>
        /// <param name="hashAlgorithmName">The hashing algorithm to use</param>
        /// <param name="padding">The padding to be used</param>
        /// <returns>An instance of <seealso cref="IRsaCrypto"/></returns>
        IRsaCrypto Create(int keySize, HashAlgorithmName hashAlgorithmName, RSASignaturePadding padding);

        /// <summary>
        /// Creates an instance of <seealso cref="IRsaCrypto"/> and uses the provided RSA implementation
        /// </summary>
        /// <param name="rsa">The RSA.</param>
        /// <returns></returns>
        IRsaCrypto Create(RSA rsa);
    }
}