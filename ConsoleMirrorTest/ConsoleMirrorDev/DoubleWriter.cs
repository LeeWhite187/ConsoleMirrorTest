using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OGA.ConsoleHelper
{
    /// <summary>
    /// Simple stream class that exposes itself as a TextWriter, and publishes any received data to both streams.
    /// This is used to mirror console output to somewhere else, while still outputing to console.
    /// </summary>
    public class DoubleWriter : TextWriter
    {
        TextWriter one;
        TextWriter two;

        public DoubleWriter(TextWriter one, TextWriter two)
        {
            this.one = one;
            this.two = two;
        }

        public override Encoding Encoding
        {
            get { return one.Encoding; }
        }

        public override void Flush()
        {
            one.Flush();
            two.Flush();
        }

        public override void WriteLine(string value)
        {
            one.WriteLine(value);
            two.WriteLine(value);
        }

        public override void Write(string value)
        {
            one.Write(value);
            two.Write(value);
        }

        public override void Write(char value)
        {
            one.Write(value);
            two.Write(value);
        }
    }
}
