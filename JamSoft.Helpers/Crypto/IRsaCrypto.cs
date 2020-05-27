namespace JamSoft.Helpers.Crypto
{
    /// <summary>
    /// An interface for making RSA Crypto a bit easier
    /// </summary>
    public interface IRsaCrypto
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IRsaCrypto"/> instance is ready or use.
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialised; otherwise, <c>false</c>.
        /// </value>
        bool Initialised { get; }

        /// <summary>
        /// Gets the private key.
        /// </summary>
        /// <value>
        /// The private key.
        /// </value>
        string PrivateKey { get; }

        /// <summary>
        /// Gets the public key.
        /// </summary>
        /// <value>
        /// The public key.
        /// </value>
        string PublicKey { get; }

        /// <summary>
        /// Creates a new  public private key pair
        /// </summary>
        void KeyGen();

        /// <summary>
        /// Signs the data.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        string SignData(string message);

        /// <summary>
        /// Verifies the data.
        /// </summary>
        /// <param name="originalMessage">The original message.</param>
        /// <param name="signedMessage">The signed message.</param>
        /// <returns></returns>
        bool VerifyData(string originalMessage, string signedMessage);

        /// <summary>
        /// Signs the hash.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        string SignHash(string message);

        /// <summary>
        /// Verifies the hash.
        /// </summary>
        /// <param name="originalMessage">The original message.</param>
        /// <param name="signedMessage">The signed message.</param>
        /// <returns></returns>
        bool VerifyHash(string originalMessage, string signedMessage);
    }
}