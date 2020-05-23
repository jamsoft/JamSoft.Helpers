using System.Text;

namespace JamSoft.Helpers.Serialization
{
    /// <summary>
    /// Adds an uppercase UTF-8 XML declaration for conformance
    /// </summary>
    /// <seealso cref="System.Text.UTF8Encoding" />
    public class UpperCaseUtf8Encoding : UTF8Encoding
    {
        /// <summary>
        /// When overridden in a derived class, gets the name registered with the Internet Assigned Numbers Authority (IANA) for the current encoding.
        /// </summary>
        public override string WebName => base.WebName.ToUpper();
    }
}
