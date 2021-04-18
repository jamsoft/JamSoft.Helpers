using System;

namespace JamSoft.Helpers.Crypto
{
    /// <summary>
    /// An interface for making RSA Crypto a bit easier
    /// </summary>
    public interface IRsaCrypto : IDisposable
    {
        /// <summary>
        /// Gets the private key in XML format.
        /// </summary>
        /// <value>
        /// The private key.
        /// </value>
        string PrivateKey { get; }

        /// <summary>
        /// Gets the public key in XML format.
        /// </summary>
        /// <value>
        /// The public key.
        /// </value>
        string PublicKey { get; }

        /// <summary>
        /// Signs the data.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The signed data encoded in Base64</returns>
        string SignData(string message);

        /// <summary>
        /// Verifies the signed data.
        /// </summary>
        /// <param name="originalMessage">The original message.</param>
        /// <param name="signedMessage">The signed message.</param>
        /// <returns></returns>
        bool VerifyData(string originalMessage, string signedMessage);

        /// <summary>
        /// Signs the hashed data .
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        string SignHash(string message);

        /// <summary>
        /// Verifies the hashed data.
        /// </summary>
        /// <param name="originalMessage">The original message.</param>
        /// <param name="signedMessage">The signed message.</param>
        /// <returns></returns>
        bool VerifyHash(string originalMessage, string signedMessage);
    }
}