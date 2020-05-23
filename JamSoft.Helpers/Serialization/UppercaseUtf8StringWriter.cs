using System.IO;
using System.Text;

namespace JamSoft.Helpers.Serialization
{
    /// <summary>
    /// Adds upper case UTF in XML declaration
    /// </summary>
    /// <seealso cref="System.IO.StringWriter" />
    public class UppercaseUtf8StringWriter : StringWriter
    {
        /// <summary>
        /// Gets the <see cref="T:System.Text.Encoding"></see> in which the output is written.
        /// </summary>
        public override Encoding Encoding => new UpperCaseUtf8Encoding();
    }
}
