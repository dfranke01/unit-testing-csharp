using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace anagramMain
{
    using anagramLib;
    class anagramMain
    {
        public static readonly string defaultDictionaryPath = @"C:\Temp\WordLookup.txt";
        static void Main(string[] args)
        {
            Dictionary<ulong, List<string>> dictionaryIndex = new Dictionary<ulong, List<string>>();
            FileInfo dictFileInfo = new FileInfo(defaultDictionaryPath);
            DictionaryData.InitializeCharacterMap();
            try
            {
                if (!dictFileInfo.Exists)
                {
                    throw new FileNotFoundException("The file was not found.", defaultDictionaryPath);
                }
                else
                {
                    Console.WriteLine("Dictionary file found: {0}", dictFileInfo.FullName);
                    Console.WriteLine("Creating Index......");
                    Console.WriteLine();

                    // initialize data       
                    bool retval = true;
                    retval = DictionaryData.LoadDictionaryFile(defaultDictionaryPath, out dictionaryIndex);
                }
            }
            catch
            {
                Console.WriteLine("Dictionary file not found");
            }
                
            // main program loop
            string word;
            do 
            {
                Console.WriteLine("Enter a word or (-1) to quit\n");
                word = Console.ReadLine();
                List<string> anagrams = new List<string>();
                anagrams = DictionaryData.GetAnagrams(word, dictionaryIndex);
                foreach(string anagram in anagrams)
                {
                    Console.WriteLine("{0}", anagram);
                }
                Console.WriteLine();
            } while (word != "-1");

            return; // close the program
        }
    }
}
