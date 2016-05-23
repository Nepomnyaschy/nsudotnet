using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nepomnyaschy.Nsudotnet.LinesCounter
{
    class Program
    {
        private static int LineCounter(string filename)
        {
            int count = 0;
            bool commentMultiLine = false;
            bool countLine = true;
            using (var reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    countLine = true;
                    var readline = reader.ReadLine();
                    if (readline == null) return count;
                    readline = readline.Trim();
                    if (string.IsNullOrWhiteSpace(readline)) continue;

                    if (readline.StartsWith("//"))
                    {
                        countLine = false;
                    }
                    if (readline.StartsWith("/*"))
                    {
                        countLine = false;
                        commentMultiLine = true;
                    }
                    if (readline.Contains("/*"))
                    {
                        commentMultiLine = true;
                    }
                    if (readline.Contains("*/"))
                    {
                        commentMultiLine = false;
                    }

                    if (countLine && !commentMultiLine) count++;
                }
                return count;
            }

        }

        static void Main(string[] args)
        {
            if (args.Length != 1) return;

            string[] filepaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*." + args[0], SearchOption.AllDirectories);

            int allLines = 0;

            foreach (var file in filepaths)
            {
                allLines += LineCounter(file);
            }

            Console.WriteLine($"Total lines: {allLines}");
        }
    }
}
