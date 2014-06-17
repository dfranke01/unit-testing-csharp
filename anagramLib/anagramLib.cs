using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace anagramLib
{
    public static class DictionaryData
    {
        private static Dictionary<char, ulong> characterMap = new Dictionary<char, ulong>();
        static public void InitializeCharacterMap()
          {
           characterMap.Add('A', 2);  characterMap.Add('B', 3);  characterMap.Add('C', 5);
           characterMap.Add('D', 7);  characterMap.Add('E', 11); characterMap.Add('F', 13);
           characterMap.Add('G', 17); characterMap.Add('H', 19); characterMap.Add('I', 23);
           characterMap.Add('J', 29); characterMap.Add('K', 31); characterMap.Add('L', 37);
           characterMap.Add('M', 41); characterMap.Add('N', 43); characterMap.Add('O', 47);
           characterMap.Add('P', 53); characterMap.Add('Q', 59); characterMap.Add('R', 61);
           characterMap.Add('S', 67); characterMap.Add('T', 71); characterMap.Add('U', 73);
           characterMap.Add('V', 79); characterMap.Add('W', 83); characterMap.Add('X', 89);
           characterMap.Add('Y', 97); characterMap.Add('Z', 101);
          }

          public static ulong getPrime(char c)
          {
           ulong retval = new ulong();
           characterMap.TryGetValue(Char.ToUpper(c), out retval);
           return retval;
          }

          public static ulong GetPrimeFactorization(string str)
          {
              char[] charArray = str.ToCharArray();
              ulong primeFact = 1;
              foreach (char c in str)
              {
                  primeFact *= getPrime(c);
              }
              return primeFact;
          }

        public static bool LoadDictionaryFile(string fileName, out Dictionary<ulong, List<string>> wordIndex)
        {
         // load a dictionary from file into an index in memory
         FileInfo dictFile = new FileInfo(fileName);
         if (dictFile.Exists)
         {
          // read file and populate wordIndex 
          using (FileStream fs = new FileStream(fileName, FileMode.Open))
           {
            using (StreamReader sr = new StreamReader(fs))
             {
              Dictionary<ulong, List<string>> temp = new Dictionary<ulong, List<string>>();
              wordIndex = temp;
              string textstring = null;
              do
               {
                textstring = sr.ReadLine();
                if (textstring != null)
                 {
                  List<string> tempValue;
                  if (wordIndex.TryGetValue(GetPrimeFactorization(textstring), out tempValue))
                  {
                   // this key already exists, we can just add the word to the list (i.e. add to the value) 
                   wordIndex[GetPrimeFactorization(textstring)].Add(textstring);
                  }
                  else
                  {
                   // this key isn't in the index yet, we need to add both key and value
                   List<string> newList = new List<string>();
                   newList.Add(textstring);
                   wordIndex.Add(GetPrimeFactorization(textstring), newList);
                  }
                 }
               }
              while (textstring != null);
             }
          }
         return true;
         }
         else
         {
          // file does not exist
          wordIndex = null;
          return false;
         }           
       }
        
        // given a word and a dictionary, return the anagrams
        public static List<string> GetAnagrams(string word, Dictionary<ulong, List<string>> wordIndex)
        {
            List<string> anagramList = new List<string>();
            List<string> retList = new List<string>();

            if (wordIndex.TryGetValue(GetPrimeFactorization(word), out anagramList))
            {
                retList = anagramList;
                retList.Remove(word);  // remove the word we entered
                return retList;
            }
            else
            {
                List<string> anagramList2 = new List<string>();
                anagramList2.Add(word); // return what was sent
                return anagramList2;
            }
        }
    }     
}
