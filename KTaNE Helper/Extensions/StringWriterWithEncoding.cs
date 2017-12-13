using System.IO;
using System.Text;

namespace KTaNE_Helper.Extensions
{
	public sealed class StringWriterWithEncoding : StringWriter
	{
		public StringWriterWithEncoding(Encoding encoding)
		{
			this.encoding = encoding;
		}

		public override Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		private readonly Encoding encoding;
	}
}
