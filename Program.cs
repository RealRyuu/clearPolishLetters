using System;
using System.IO;
using System.Text;

namespace ClearSubs
{
    class Program
    {
        const string tmpFileName = "convertedFile.tmp";

        static void Main(string[] args)
        {
            if (!(args.Length == 2 || args.Length == 1))
            {
                Console.WriteLine("Run from command line, usage:");
                Console.WriteLine("1 argument - file name: converts and replaces original file");
                Console.WriteLine("2 arguments - file name, output file: converts and saves to output file");
                Console.ReadKey();
                return;
            }

            StreamReader originalFile = new StreamReader(args[0], Encoding.Default);
            StreamWriter newFile;

            if (args.Length == 1)
                newFile = new StreamWriter(new FileStream(tmpFileName, FileMode.Append), Encoding.ASCII);
            else
                newFile = new StreamWriter(new FileStream(args[1], FileMode.Append), Encoding.ASCII);

            string[] polishLetters = { "ą", "Ą", "ć", "Ć", "ę", "Ę", "ł", "Ł", "ń", "Ń", "ó", "Ó", "ś", "Ś", "ź", "Ź", "ż", "Ż" };
            string[] latinLetters = { "a", "A", "c", "C", "e", "E", "l", "L", "n", "N", "o", "O", "s", "S", "z", "Z", "z", "Z" };
            string line;

            while ((line = originalFile.ReadLine()) != null)
            {
                for (int i = 0; i < polishLetters.Length; i++)
                    line = line.Replace(polishLetters[i], latinLetters[i]);

                newFile.WriteLine(line);
            }

            originalFile.Close();
            newFile.Close();

            if (args.Length == 1)
            {
                File.Delete(args[0]);
                File.Move(tmpFileName, args[0]);
            }

        }
    }
}
