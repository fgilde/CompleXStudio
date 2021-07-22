using System.IO;
using System.Text;
using CompleX.Services;

namespace CompleX.Classes
{
    public class ComplexOutPutWriter : TextWriter
    {
        public override void WriteLine(string s)
        {
           OutputService.WriteLine(s);
        }

        public override void Write(string value)
        {
            OutputService.Write(value);
        }

        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }
    }
}
