using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlpFileConverter
{
    public class ConverterBase
    {
        protected Encoding _encoding = Encoding.Default;

        public ConverterBase(string encodingName)
        {
            _encoding = GetEncoding(encodingName);
        }

        protected Encoding GetEncoding(string encodingName)
        {
            if (string.IsNullOrEmpty(encodingName))
            {
                return Encoding.UTF8;
            }
            return Encoding.GetEncoding(encodingName);
        }
    }
}
