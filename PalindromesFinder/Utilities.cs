
namespace SoftwareCodingChallenge.PalindromesFinder
{
    using System;
    using System.Text.RegularExpressions;

    public static class Utilities
    {
        /// <summary>
        /// This function finds all palindromes in a given string using Manacher's algorithm 
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>A 2 x (N + 1) matrix, containing the "radius" of the longest palindrome at a given position i for both even and odd-length palindromes 
        /// (stored in each row respectively)</returns>
        /// <remarks>More on this algorithm here: https://en.wikipedia.org/wiki/Longest_palindromic_substring</remarks>
        public static int[,] FindAllPalindromesInString(string s)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentException("The input string must contain at least one character.");
            if (!IsInputStringValid(s))
                throw new ArgumentException("The input string can only contain lower case characters from the English alphabet.");

            int N = s.Length;
            int i, j, k;
            int radius;
            int[,] radii = new int[2, N + 1];

            s = "@" + s + "#"; //Adding these boundaries makes the search easier and cleaner (avoid special treatments for "boundary conditions" at i = 1 and i = N)

            for (j = 0; j <= 1; j++)
            {
                radii[j, 0] = radius = 0; i = 1;
                while (i <= N)
                {
                    while (s[i - radius - 1] == s[i + j + radius])
                    {
                        radius++;
                    }
                    radii[j, i] = radius;
                    k = 1;
                    while ((radii[j, i - k] != radius - k) && (k < radius))
                    {
                        radii[j, i + k] = Math.Min(radii[j, i - k], radius - k);
                        k++;
                    }
                    radius = Math.Max(radius - k, 0);
                    i += k;
                }
            }
            return radii;
        }

        /// <summary>
        /// Checks the validity of the input string. Strings containing characters that are upper case OR non present in English alphabet OR strings that are null/empty are NOT valid
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>True if valid, false if not</returns>
        public static bool IsInputStringValid(string s)
        {
            return Regex.Match(s, "^[a-z]+$").Success;
        }
    }

}
