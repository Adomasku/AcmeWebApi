using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AcmeWebApi
{
    public class Word
    {
        public Word(string wordLetters, int count)
        {
            WordLetters = wordLetters;
            Count = count;
        }

        public string WordLetters { get; set; }
        public int Count { get; set; }

        /// <summary>
        /// Caltulates frequency of the words by ignoring accent
        /// </summary>
        /// <param name="text">Input sentence</param>
        /// <returns>List of words with frequency</returns>
        public static List<Word> GetWords(string text)
        {
            //We get existing words delimeters. We assume that delimiter is anything but a letter or a digit.
            string delimeters = new string(text.Where(a => !char.IsLetterOrDigit(a)).Distinct().ToArray());
            var words = text.Split(delimeters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            WordsWithAccentComparer comparer = new WordsWithAccentComparer();

            return words.GroupBy(a => a, comparer).Select(a => new Word(a.Key, a.Count())).OrderByDescending(a => a.Count).ToList();
        }

    }

    public class Sentence
    {
        public Sentence()
        {

        }

        [StringLength(50)]
        [Required]
        public string SentenceText { get; set; }
    }

    public class WordsWithAccentComparer : EqualityComparer<string>
    {
        public override bool Equals(string x, string y)
        {
            return string.Compare(x.ToLower(), y.ToLower(), System.Globalization.CultureInfo.InvariantCulture, System.Globalization.CompareOptions.IgnoreNonSpace) == 0;
        }

        //This is hack. Because first comparer checks if hash codes are equal and only if they are equal calls Equals. By returning 1 it will always call Equals
        public override int GetHashCode(string obj)
        {
            return 1;
        }

    }
}
