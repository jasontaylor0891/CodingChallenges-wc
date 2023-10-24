using System;
using System.IO;
using System.IO.Enumeration;
using System.Text;
using CommandLine;

namespace wc
{
    internal class Program
    {
        public class Options
        {
            [Option('c', "bytes", Required = false, HelpText = "print the byte counts.")]
            public bool wcBytes { get; set; }
            [Option('l', "lines", Required = false, HelpText = "print the newlines counts.")]
            public bool wcLines { get; set; }
            [Option('w', "words", Required = false, HelpText = "print the word counts.")]
            public bool wcWords { get; set; }
            [Option('m', "chars", Required = false, HelpText = "print the character counts.")]
            public bool wcChars { get; set; }

        }

        static long GetByteCount(StreamReader sReader)
        {
            byte[] fileOutput = Encoding.UTF8.GetBytes(sReader.ReadToEnd());
            return fileOutput.Length;
        }

        static long GetNewLineCount(StreamReader sReader)
        {
            return sReader.ReadToEnd().Split(new char[] { '\n' }).Length;
        }

        static long GetWordCount(StreamReader sReader)
        {
            string fileOutput = sReader.ReadToEnd();
            return fileOutput.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        static long GetCharacterCount(StreamReader sReader)
        { 
            return sReader.ReadToEnd().Length;
        }

        static void Main(string[] args)
        {
            int counter = -1;
            string filename = "";
            bool isStdin = false;
            StreamReader sr = null;
            StreamReader sr1 = null;

            foreach (string arg in args)
            {
                counter++;
                if (arg.EndsWith(".txt"))
                {
                    break;
                }
                
            }
            if(counter == 0)
            {
                isStdin = true;
            }
            else
            {
                filename = args[counter]; 
            }

            Parser.Default.ParseArguments<Options>(args)
               .WithParsed<Options>(o =>
               {
                   if(isStdin)
                   {
                       sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
                   }
                   else
                   {
                       sr1 = new StreamReader(filename);
                   }

                   if (o.wcBytes)
                   {
                       if (isStdin)
                       {
                           Console.WriteLine("{0}", GetByteCount(sr));
                       }
                       else
                       {
                           Console.WriteLine("{0} {1}", GetByteCount(sr1), filename);
                       }
                   }
                   else if (o.wcLines)
                   {
                       if(isStdin)
                       {
                           Console.WriteLine("{0}", GetNewLineCount(sr));
                       }
                       else
                       {
                           Console.WriteLine("{0} {1}", GetNewLineCount(sr1), filename);
                       }
                   }
                   else if (o.wcWords)
                   {
                       if (isStdin)
                       {
                           Console.WriteLine("{0}", GetWordCount(sr));
                       }
                       else
                       {
                           Console.WriteLine("{0} {1}", GetWordCount(sr1), filename);
                       }
                   }
                   else if (o.wcChars)
                   {
                       if (isStdin)
                       {
                           Console.WriteLine("{0}", GetCharacterCount(sr));
                       }
                       else
                       {
                           Console.WriteLine("{0} {1}", GetCharacterCount(sr1), filename);
                       }
                   }
                   else
                   {
                      Console.WriteLine("default");
                   }
               });
        }
    }
}