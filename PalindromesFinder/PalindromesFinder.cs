
namespace SoftwareCodingChallenge.PalindromesFinder
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class searches all the palindromes in a given word and stores them in a List 
    /// </summary>
    public class PalindromesFinder
    {
        private string _string;
        private bool _isInputValid;
        private List<Results> _palindromes = null;

        public PalindromesFinder()
        {
            _isInputValid = false;
            _palindromes = new List<Results>();
        }

        public PalindromesFinder(string word)
        {
            String = word;
            _palindromes = new List<Results>();
        }

        public string String
        {
            get { return _string; }
            set
            {
                if (String.IsNullOrEmpty(value) || !Utilities.IsInputStringValid(value))
                {
                    _isInputValid = false;
                    throw new ArgumentException(
                        "The input string cannot be null or empty and must only contain lower case characters from the English alphabet.");
                }
                else
                {
                    _string = value; _isInputValid = true;
                    if (value != _string) _palindromes.Clear();
                }
            }
        }

        public void Run()
        {
            if (_isInputValid)
            {
                //Use Manacher's algorithm to compute the radii of palindromes around each letter of input string
                var radii = Utilities.FindAllPalindromesInString(_string);

                //Store all obtained palindromes in a List
                _palindromes.Clear();
                _palindromes.Add(new Results { Index = 0, Palindrome = _string[0].ToString() }); //first character of the string IS a palindrome!
                for (int i = 1; i < _string.Length; i++)
                {
                    for (int j = 0; j <= 1; j++)
                    {
                        int radius = radii[j, i];
                        int start = i - 1 - radius;
                        if (radius > 0)
                        {
                            _palindromes.Add(new Results { Index = start, Palindrome = _string.Substring(start, 2 * radius + j) });
                        }
                    }
                    _palindromes.Add(new Results { Index = i, Palindrome = _string[i].ToString() }); //single letters ARE palindromes!
                }
            }
            else
            {
                throw new Exception("You must specify a valid string before running the model.");
            }
        }

        public IEnumerable<Results> GetResults()
        {
            return _palindromes;
        }
    }
}
