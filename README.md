# MAT Software Team Coding Challenge

## Synopsis
Application that finds the 3 longest unique palindromes in a supplied string. The application returns the 3 longest palindromes, their respective start index and length, in descending order of length.

* The program only keeps track of the longest palindromes for a given center (i.e. character in input string).
* Only latin letters from English alphabet can be used (in lower case!)

My first implementation of this problem had O(n^2) complexity. Noticing that palindrome is centered either on a letter or between two letters, I was scanning all 2N+1 possible centres and kept track of the longest palindrome for that centre (we are not interested in the "children" palindromes within a "parent" palindrome) 

After a bit of research, I did find out that same results could be achieved in linear time using Manacher's algorithm (would have never found it myself, this is why I have left my previous -less elegant implementation in the code).

## Code Example

`PalindromesFinder` is the model that allows to find all the palindromes in a given string. The model allows client code to then further filter the resulting palindromes (stored in `Result` object)

For instance, to get all the distinct palindromes:

    var model = new PalindromesFinder();
    model.Run();
    model.GetResults().GroupBy(p => p.Palindromes)
      .Select(grp => grp.First());;
    
It is client code's responsibility to catch error thrown by `PalindromesFinder`

## Installation
Just clone the git repository onto your machine. 
The code has been written in C# using Visual Studio 2017 (free version) - it has also been tested using Visual Studio 2015

## Tests
* One letter palindrome
* Very long string (one million characters)
* Default string "sqrrqabccbatudefggfedvwhijkllkjihxymnnmzpop"
* Single palindrome "hannah"
* Wrong input (emtpy or null string, invalid character(s))

## Contributors
Valentin Roy (valentin.aj.roy@gmail.com)

## Links
[Manacher's algorithm](https://en.wikipedia.org/wiki/Longest_palindromic_substring)


