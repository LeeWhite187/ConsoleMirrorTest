using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OGA.ConsoleHelper
{
    /// <summary>
    /// Simple wrapper around TextWriter that acts as a TextWrite that can be assigned to the Console's SetOut delegate, to capture console output.
    /// This captures output from both write and write line calls of console output.
    /// </summary>
    public class ConsoleCapture : TextWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }

        public override void Write(string value)
        {
            if (OnWrite != null)
                this.OnWrite(value);
        }

        public override void WriteLine(string value)
        {
            if (OnWriteLine != null)
                this.OnWriteLine(value);
        }

        public Action<string> OnWrite { get; set; }
        public Action<string> OnWriteLine { get; set; }
    }
}
