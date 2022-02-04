namespace Trie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public class TriePerformanceTester
    {
        private const string VocabularyPath = "vocabulary.txt";

        public static void Main()
        {
            var words = LoadWords(VocabularyPath).ToArray();

            Console.WriteLine("Words count: {0}", words.Length);

            const int prefixLength = 2;

            var data = Enumerable
                .Range('A', 'Z' - 'A' + 1)
                .Select(i => (char)i)
                .ToArray();

            var prefixes = GetAllMatches(data, prefixLength).ToArray();

            Console.WriteLine("Search prefixes count: {0}", prefixes.Length);
            Console.WriteLine();

            var stopWatch = Stopwatch.StartNew();
            var matchesCount = 0;

            foreach (var prefix in prefixes)
            {
                var resultArray = words
                    .Where(w => w.StartsWith(prefix))
                    .ToArray();

                matchesCount += resultArray.Length;
            }

            Console.WriteLine("Found {0} matches", matchesCount);

            stopWatch.Stop();

            Console.WriteLine("Regular string matching time: {0} ms", stopWatch.ElapsedMilliseconds);
            Console.WriteLine();

            stopWatch.Restart();

            var trie = new Trie<bool>();

            foreach (var item in words)
            {
                trie.Insert(item, true);
            }

            stopWatch.Stop();

            Console.WriteLine("Build trie time: {0} ms", stopWatch.ElapsedMilliseconds);
            Console.WriteLine();

            stopWatch.Restart();
            matchesCount = 0;

            foreach (var prefix in prefixes)
            {
                var resultTrie = trie.GetByPrefix(prefix).ToArray();
                matchesCount += resultTrie.Length;
            }

            Console.WriteLine("Found {0} matches", matchesCount);

            stopWatch.Stop();

            Console.WriteLine("Trie find prefixes time: {0} ms", stopWatch.ElapsedMilliseconds);
        }

        private static IEnumerable<string> GetAllMatches(char[] chars, int length)
        {
            var indexes = new int[length];
            var current = new char[length];

            for (var i = 0; i < length; i++)
            {
                current[i] = chars[0];
            }

            do
            {
                yield return new string(current);
            }
            while (Increment(indexes, current, chars));
        }

        private static bool Increment(int[] indexes, char[] current, char[] chars)
        {
            var position = indexes.Length - 1;

            while (position >= 0)
            {
                indexes[position]++;
                if (indexes[position] < chars.Length)
                {
                    current[position] = chars[indexes[position]];
                    return true;
                }

                indexes[position] = 0;
                current[position] = chars[0];
                position--;
            }

            return false;
        }

        /// <summary>
        /// Returns distinct set of words. <remarks>This method returns 58110 English words.</remarks>
        /// </summary>
        /// <returns>Distinct set of words.</returns>
        private static IEnumerable<string> LoadWords(string fileName)
        {
            var path = Path.Combine(@"..\..\", fileName);
            return File.ReadAllLines(path);
        }
    }
}
