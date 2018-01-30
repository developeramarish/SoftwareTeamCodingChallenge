# MAT Software Team Coding Challenge

## Synopsis

`ConsoleUI` is an application that uses the `PalindromesFinder` tool to find the 3 longest unique palindromes in a supplied string. The application returns the 3 longest palindromes, their respective start index and length, in descending order of length.

* The program only keeps track of the longest palindromes for a given center (i.e. character at given index in input string). For instance, at index 2 in the string "kayak", "kayak" will be returned but "aya" and "y" won't.
* Only latin letters from English alphabet can be used (in lower case!) - no spacing allowed between character

All the palindromes are found using Manacher's algorithm (see **Links** section below), which is O(n^2) in time. They are then stored into a List (linear time and space), which is then filtered in order to only retain the 3 longest *unique* palindromes.

## Code Example

`PalindromesFinder` is the model that allows to find all the palindromes in a given string. Client code can then further filter the resulting palindromes (stored in `Result` object), by using Linq queries or else.

For instance, to get all the *distinct* palindromes:

    var model = new PalindromesFinder();
    model.Run();
    model.GetResults().GroupBy(p => p.Palindromes)
      .Select(grp => grp.First());;
    
...or to get the longest palindromes first:

    model.GetResults().OrderByDescending(p => p.Palindromes.Length);

...or to get the *unique* palindromes only:

    model.GetResults().GroupBy(p => p.Palindromes)
        .Where(grp => grp.Count() == 1).SelectMany(o => o);

It is client code's responsibility to catch error thrown by `PalindromesFinder`

    try
    {
        model.String = ...
        model.Run();
    } catch (...)

## Installation

* Just clone the git repository onto your machine, or download a zip file 
* The code has been written in C# using Visual Studio 2017 (free version) - it has also been tested using Visual Studio 2015
* You might have to manually choose ConsoleUI as starting project.

## Tests
* One letter palindrome
* Very long string (one million characters - the idea was to run several of those with one million chars, one thousands, a hundred, and try to observe quadratic (on average) time complexity but I did not have time)
* Default string "sqrrqabccbatudefggfedvwhijkllkjihxymnnmzpop"
* Single palindrome "kayak"
* Wrong input (emtpy or null string, invalid character(s))
* Palindrome unicity (if there exist two or more palindromes that are equal in the string, they should not be returned)

## Contributors
Valentin Roy (valentin.aj.roy@gmail.com)

## Links
[Manacher's algorithm](https://en.wikipedia.org/wiki/Longest_palindromic_substring)

