
namespace SoftwareCodingChallenge.UnitTests
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PalindromesFinder;

    [TestClass]
    public class UnitTest1
    {
        private PalindromesFinder model = null;

        [TestInitialize]
        public void Init()
        {
            model = new PalindromesFinder();
        }

        [TestMethod]
        public void TestDefault()
        {
            model.String = "sqrrqabccbatudefggfedvwhijkllkjihxymnnmzpop";
            model.Run();
            var res = model.GetResults().OrderByDescending(p => p.Palindrome.Length).GroupBy(p => p.Palindrome)
                        .Where(group => group.Count() == 1).SelectMany(o => o).Take(3).ToList();
            Assert.IsTrue(res[0].Palindrome.Equals("hijkllkjih") && res[0].Index == 23);
            Assert.IsTrue(res[1].Palindrome.Equals("defggfed") && res[1].Index == 13);
            Assert.IsTrue(res[2].Palindrome.Equals("abccba") && res[2].Index == 5);
        }

        [TestMethod]
        public void TestNonUniquePalindromes()
        {
            //In this case, the longest palindrome "hijkllkjih" should not be returned, as it is not unique (two occurences)
            model.String = "sqrrqabccbatudefggfedvwhijkllkjihxymnnmzpophijkllkjih";
            model.Run();
            var res = model.GetResults().OrderByDescending(p => p.Palindrome.Length).GroupBy(p => p.Palindrome)
                        .Where(group => group.Count() == 1).SelectMany(o => o).Take(3).ToList();
            Assert.IsTrue(res[0].Palindrome.Equals("defggfed") && res[0].Index == 13);
            Assert.IsTrue(res[1].Palindrome.Equals("abccba") && res[1].Index == 5);
            Assert.IsTrue(res[2].Palindrome.Equals("qrrq") && res[2].Index == 1);
        }

        [TestMethod]
        public void TestLongString()
        {
            //Generate a string of random lowercase letters
            Random random = new Random();
            var sb = new StringBuilder();
            for (int i = 0; i < 1000000; i++)
            {
                int num = random.Next(0, 26);
                sb.Append((char)('a' + num));
            }
            model.String = sb.ToString();
            model.Run();
            var res = model.GetResults().ToList();
            foreach (var palindrome in res)
            {
                Trace.WriteLine($"{palindrome.Palindrome}");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "The input string must contain at least one character.")]
        public void TestEmptyString()
        {
            model.String = string.Empty;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "The input string cannot be null or empty and must only contain lower case characters from the English alphabet.")]
        public void TestNullString()
        {
            model.String = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "The input string cannot be null or empty and must only contain lower case characters from the English alphabet.")]
        public void TestWrongString()
        {
            model.String = "AsdrfrdsA"; //Cannot contain upper case letters
        }

        [TestMethod]
        public void TestSinglePalindrome()
        {
            model.String = "kayak";
            model.Run();
            var res = model.GetResults().ToList();
            Assert.IsTrue(res.Count == 1);
            Assert.IsTrue(res[0].Palindrome.Equals("kayak") && res[0].Index == 0);
        }


        [TestMethod]
        public void TestSingleLetter()
        {
            model.String = "a";
            model.Run();
            var res = model.GetResults().ToList();
            Assert.IsTrue(res.Count == 1);
            Assert.IsTrue(res[0].Palindrome == "a");
        }
    }
}
