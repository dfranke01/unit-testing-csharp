using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace anagramTest
{
    using anagramLib;
    public class anagramTest
    {
        [TestFixture]
        public class anagramFixture
        {
            //private Dictionary<char, ulong> characterMap;
            
            [TestFixtureSetUpAttribute]               // setup once for all tests
            public void testInitializeCharacterMap()  
            {
                DictionaryData.InitializeCharacterMap();
            }

            [Test]
            public void testGetPrime()
            {
                ulong testChar = DictionaryData.getPrime('F');
                Assert.AreEqual(13, testChar);
            }

            [Test]
            public void testGetPrimeFactorization()
            {
                string[] strings = new string[] { "one", "two", "parsec"};
                Assert.AreEqual(22231, DictionaryData.GetPrimeFactorization(strings[0]));
                Assert.AreEqual(276971, DictionaryData.GetPrimeFactorization(strings[1]));
                Assert.AreEqual(23827210, DictionaryData.GetPrimeFactorization(strings[2]));
            }

            [Test]
            public void testLoadDictionaryFile()
            {
                Dictionary<ulong, List<string>> dictionaryIndex = new Dictionary<ulong, List<string>>();
                bool retval = true;
                string nonExistantFilename = "tHiSfIlEnAmEhAdBeTtErNoTeXiSt";
                retval = DictionaryData.LoadDictionaryFile(nonExistantFilename, out dictionaryIndex);
                Assert.AreEqual(retval, false);
                Assert.AreEqual(dictionaryIndex, null);

                retval = false;
                string defaultDictionaryPath = @"C:\Temp\WordLookup.txt";
                retval = DictionaryData.LoadDictionaryFile(defaultDictionaryPath, out dictionaryIndex);

                Assert.AreEqual(retval, true);
                Assert.AreNotEqual(dictionaryIndex, null);

                // test a few values 9409346, stain, saint, satin
                retval = false;
                List<string> testList = new List<string>();
                retval = dictionaryIndex.TryGetValue(9409346, out testList);
                Assert.AreEqual(retval, true);
                Assert.That(testList.Contains("stain")); 
                Assert.That(testList.Contains("saint"));
                Assert.That(testList.Contains("satin"));
            }

            [Test]
            public void testGetAnagrams()
            {
                Dictionary<ulong, List<string>> dictionaryIndex = new Dictionary<ulong, List<string>>();
                string defaultDictionaryPath = @"C:\Temp\WordLookup.txt";
                DictionaryData.LoadDictionaryFile(defaultDictionaryPath, out dictionaryIndex);
                List<string> retList = new List<string>();
                string stain = "stain";
                string parsec = "parsec";
                string player = "player";

                retList = DictionaryData.GetAnagrams(stain, dictionaryIndex);
                foreach (string str in retList) { Console.WriteLine(str); }
                Console.WriteLine();
                Assert.That(retList.Contains("saint"));
                Assert.That(retList.Contains("satin"));

                retList = DictionaryData.GetAnagrams(parsec, dictionaryIndex);
                foreach(string str in retList ) { Console.WriteLine(str);}
                Console.WriteLine();
                Assert.That(retList.Contains("capers"));
                Assert.That(retList.Contains("pacers"));
                Assert.That(retList.Contains("scrape"));
                Assert.That(retList.Contains("spacer"));
                Assert.That(retList.Contains("escarp"));
                Assert.That(retList.Contains("sparce"));

                retList = DictionaryData.GetAnagrams(player, dictionaryIndex);
                foreach (string str in retList) { Console.WriteLine(str); }
                Console.WriteLine();
                Assert.That(retList.Contains("pearly"));
                Assert.That(retList.Contains("replay"));
                Assert.That(retList.Contains("parley"));
            }
        }
    }
}
