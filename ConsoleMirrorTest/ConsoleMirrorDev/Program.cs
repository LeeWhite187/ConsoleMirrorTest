using OGA.ConsoleHelper;
using System;
using System.Collections.Generic;

namespace ConsoleMirrorDev
{
    public class Program
    {
        /// <summary>
        /// Provides redirection and mirroring of console output, so output can be sent to the Station Service as results.
        /// </summary>
        static public ConsoleMirror _consolemirror;

        static public List<string> OutputCatcher = new List<string>();


        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("1. WriteLine Sent to console");
                Console.Write("2. Write Sent to console");

                // Enable console mirroring...
                Enable_ConsoleMirroring();

                Console.WriteLine("3. Write Sent to output and capture");
                Console.Write("4. WriteLine Sent to output and capture");

                Console.WriteLine("5. Write Sent to output and capture");
                Console.Write("6. WriteLine Sent to output and capture");

                DisableConsoleMirror();

                Console.WriteLine("7. Write Sent just to output");
                Console.Write("8. WriteLine Sent just to output ");

                // Once execution reaches, here, the console will have all 8 outputs.
                // And, the output cather list will include 3, 4, 5, and 6.
                // And, outputs 4 and 5 will be concatenated to a single entry, just like the console output is.
                int x = 0;
            }
            catch (Exception e)
            {
                int bitbucket = 0;
            }
            finally
            {
                // Restore console output...
                DisableConsoleMirror();
            }
        }


        #region Console Redirection

        /// <summary>
        /// Enables console mirroring to results data.
        /// </summary>
        static public void Enable_ConsoleMirroring()
        {
            // Create the output mirror...
            _consolemirror = new ConsoleMirror();

            // Enable the mirror, and give it callbacks, that capture its output...
            _consolemirror.EnableCapture(CALLBACK_Write, CALLBACK_WriteLine);
            //_consolemirror.EnableCapture(null, null);
        }

        /// <summary>
        /// This callback allows us to capture data written to the console.
        /// Called when the Console.WriteLine is called.
        /// Will include any waiting partial write line with the writeline output.
        /// And, will clear the partial string, so it can be populated by the next Write call.
        /// </summary>
        /// <param name="val"></param>
        private static void CALLBACK_WriteLine(string val)
        {
            OutputCatcher.Add(partialoutput + val);

            partialoutput = "";
        }

        /// <summary>
        /// Stores any partial write output.
        /// </summary>
        static private string partialoutput;

        /// <summary>
        /// This callback allows us to capture data written to the console.
        /// Called when the Console.Write is called.
        /// </summary>
        /// <param name="val"></param>
        private static void CALLBACK_Write(string val)
        {
            // Append received write to our waiting output...
            partialoutput += val;
        }

        /// <summary>
        /// Call this to dispose of any console redirection.
        /// This method also sends any waiting partial write string to the output before closing it.
        /// </summary>
        static public void DisableConsoleMirror()
        {
            if (_consolemirror == null)
                return;

            // Output any straggling output...
            if(!string.IsNullOrEmpty(partialoutput))
            {
                OutputCatcher.Add(partialoutput);

                partialoutput = "";
            }

            _consolemirror.Dispose();
        }

        #endregion
    }
}
