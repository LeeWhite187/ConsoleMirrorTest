using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OGA.ConsoleHelper
{
    /// <summary>
    /// Allows redirection and mirroring of console output.
    /// Call the EnableCapture method to mirror output.
    /// Call the Dispose method to restore normal console output.
    /// </summary>
    public class ConsoleMirror : IDisposable
    {
        /// <summary>
        /// Holds the redirect stream.
        /// </summary>
        ConsoleCapture _consolecapture;
        
        /// <summary>
        /// The mirroring text stream.
        /// Gets assigned to the console's output during mirroring.
        /// </summary>
        TextWriter doubleWriter;

        /// <summary>
        /// Original console output stream.
        /// We hold a reference to it, so we can output captured text to it.
        /// </summary>
        TextWriter oldOut;

        /// <summary>
        /// Call this method to mirror console output.
        /// </summary>
        public void EnableCapture(Action<string> writecallback, Action<string> writelinecallback)
        {
            // Get a reference to the current console's output, so we can still send output to it, and can restore it when needed.
            oldOut = Console.Out;

            try
            {
                // Create our redirect instance...
                _consolecapture = new ConsoleCapture();

                // Assign callbacks, to capture output...
                _consolecapture.OnWrite = writecallback;
                _consolecapture.OnWriteLine = writelinecallback;

                // Create the double writer that will publish to our mirroring output and the original console output...
                doubleWriter = new DoubleWriter(_consolecapture, oldOut);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open file for writing");
                Console.WriteLine(e.Message);
                return;
            }

            // Assign our double writer to the console's standard output...
            Console.SetOut(doubleWriter);
        }

        public void Dispose()
        {
            // Dispose the output mirror...
            this.doubleWriter.Flush();

            // Reassign the original output to the console...
            Console.SetOut(oldOut);

            // Now, dispose the double writer...
            this.doubleWriter.Dispose();
            this.doubleWriter = null;

            // Close down the console capture instance...
            if (_consolecapture != null)
            {
                _consolecapture.Flush();
                _consolecapture.OnWrite = null;
                _consolecapture.OnWriteLine = null;
                _consolecapture.Close();
                _consolecapture = null;
            }
        }
    }
}
