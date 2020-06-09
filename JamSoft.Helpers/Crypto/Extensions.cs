using System.Security.Cryptography;

namespace JamSoft.Helpers.Crypto
{
    /// <summary>
    /// Some helper extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Determines whether this instance is null.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <c>true</c> if the specified parameters is null; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNull(this RSAParameters parameters)
        {
            return parameters.D == null && 
                   parameters.DP == null && 
                   parameters.DQ == null &&
                   parameters.Exponent == null && 
                   parameters.InverseQ == null && 
                   parameters.Modulus == null &&
                   parameters.P == null && 
                   parameters.Q == null;
        }
    }
}
