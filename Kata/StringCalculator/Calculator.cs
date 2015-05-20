using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kata.StringCalculator
{
    public class Calculator //http://osherove.com/tdd-kata-1/
    {
        public int Add(string input)
        {
            return string.IsNullOrEmpty(input) ? 0 : AddStringNumbers(input);
        }

        private static int AddStringNumbers(string input)
        {
            var numbers = ExtractNumbersAsList(input);
            GuardAgainstNegativeNumbers(numbers);
            return numbers.Where(LessThan1000()).Sum();
        }

        private static List<int> ExtractNumbersAsList(string input)
        {
            var numbers = new List<int>();
            var delimiters = new []{","};
            using(var reader = new StringReader(input))
            {
                var line = reader.ReadLine();
                if(HasDefinedDelimiters(line))
                {
                    delimiters = ExtractDelimiters(line);
                    line = reader.ReadLine();
                }
                while(line != null)
                {
                    numbers.AddRange(ExtractNumbersFromLine(line, delimiters));
                    line = reader.ReadLine();
                }
            }
            return numbers;
        }

        private static bool HasDefinedDelimiters(string line)
        {
            return line.StartsWith("//");
        }

        private static string [] ExtractDelimiters(string line)
        {
            var delimiter = line.Substring(2).Replace("\n", string.Empty);
            var match = Regex.Matches(delimiter, "\\[(.*?)\\]"); ;
            return match.Count > 0 ? ExtractDelimiters(match) : new[] { delimiter };
        }

        private static string[] ExtractDelimiters(MatchCollection match)
        {
            var delimiters = new List<string>();
            foreach(var del in match)
            {
                delimiters.Add(del.ToString().Trim(new []{'[',']'}));
            }
            return delimiters.ToArray();
        }

        private static IEnumerable<int> ExtractNumbersFromLine(string line, string[] delimiters)
        {
            var split = line.Split(delimiters, StringSplitOptions.None);
            return split.Select(Int32.Parse);
        }

        private static void GuardAgainstNegativeNumbers(IEnumerable<int> numbers)
        {
            var negatives = numbers.Where(x => x < 0).ToList();
            if(negatives.Any())
            {
                throw new ArgumentException(string.Join(",",negatives));
            }
        }

        private static Func<int, bool> LessThan1000()
        {
            return x => x < 1000;
        }
    }
}