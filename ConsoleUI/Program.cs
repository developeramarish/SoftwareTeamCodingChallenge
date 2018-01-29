
namespace SoftwareCodingChallenge.ConsoleUI
{
    using System;
    using System.Linq;
    using System.Text;
    using PalindromesFinder;

    class Program
    {
        static void Main(string[] args)
        {
            PalindromesFinder model = new PalindromesFinder();
            StringBuilder sb = new StringBuilder();

            while (true)
            {
                sb.Clear();
                Console.WriteLine("Enter a string and press enter (Ctrl+C to quit)");
                while (true)
                {
                    var pressedKey = Console.ReadKey(true);
                    if (char.IsLetter(pressedKey.KeyChar))
                    {
                        var @char = Char.ToLower(pressedKey.KeyChar);
                        Console.Write(@char);
                        sb.Append(@char);
                    }
                    else if (pressedKey.Key == ConsoleKey.Backspace && sb.Length > 0)
                    {
                        Console.Write("\b \b");
                        sb.Length--;
                    }
                    else if (pressedKey.Key == ConsoleKey.Enter)
                    {
                        Console.Write('\n');
                        Console.WriteLine("------------------");
                        break;
                    }
                }
                try
                {
                    model.String = sb.ToString();
                    model.Run();

                    //Filter out the UNIQUE palindromes from the returned list of palindromes, and return the three longest ones
                    var results = model.GetResults().OrderByDescending(p => p.Palindrome.Length).GroupBy(p => p.Palindrome)
                        .Where(group => group.Count() == 1).SelectMany(o => o).Take(3);

                    foreach (var palindrome in results)
                    {
                        Console.WriteLine($"Text: {palindrome.Palindrome}, Index: {palindrome.Index}, Length: {palindrome.Palindrome.Length}");
                        Console.WriteLine("------------------");
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"An error occured: {exc.Message}");
                    continue;
                }
            }
        }
    }
}
