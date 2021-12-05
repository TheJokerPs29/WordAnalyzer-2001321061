using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace WordAnalylizing
{
    class Program
    {
        private static string text;
        private static string[] ArrayoFWords;

        static void Main(string[] args)
        {
            text = ReadText();
            ArrayoFWords = GetWordsFromText();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            WordAnalyzer(); 
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        static void WordAnalyzerParallel()
        {
            List<Thread> threads = getTheadsList();

            foreach (var t in threads) t.Join();
        }

        static List<Thread> getTheadsList()
        {
            List<Thread> threads = new List<Thread>();

            Thread PrintNumberOfWordsThread = new Thread(PrintNumberOfWords);
            threads.Add(PrintNumberOfWordsThread);
            PrintNumberOfWordsThread.Start();

            Thread PrintShortestWordThread = new Thread(PrintShortestWord);
            threads.Add(PrintShortestWordThread);
            PrintShortestWordThread.Start();

            Thread PrintLongestWordThread = new Thread(PrintLongestWord);
            threads.Add(PrintLongestWordThread);
            PrintLongestWordThread.Start();

            Thread PrintAverageWordLengthThread = new Thread(PrintAverageWordLength);
            threads.Add(PrintAverageWordLengthThread);
            PrintAverageWordLengthThread.Start();

            Thread PrintFiveMostCommonWordsThread = new Thread(PrintFiveMostCommonWords);
            threads.Add(PrintFiveMostCommonWordsThread);
            PrintFiveMostCommonWordsThread.Start();

            Thread PrintFiveLeastCommonWordsThread = new Thread(PrintFiveLeastCommonWords);
            threads.Add(PrintFiveLeastCommonWordsThread);
            PrintFiveLeastCommonWordsThread.Start();

            return threads;
        }

        static void WordAnalyzer()
        {
            PrintNumberOfWords();
            PrintShortestWord();
            PrintLongestWord();
            PrintAverageWordLength();
            PrintFiveMostCommonWords();
            PrintFiveLeastCommonWords();
        }

        static string[] GetWordsFromText()
        {
            return text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        static string ReadText()
        {
            return System.IO.File.ReadAllText(
                @"C:\Users\ACER\Desktop\WordAnalyzer\WordAnalyzer\WordsAnalyzering\WordsAnalyzering/book.txt");
        }

        static void PrintNumberOfWords()
        {
            Console.WriteLine("Number of words: " + ArrayoFWords.Length);
            Console.WriteLine();
        }

        static void PrintShortestWord()
        {
            string Shortest = ArrayoFWords.OrderBy(word => word.Length).First();
            Console.WriteLine($"The shortest word is {Shortest}");
            Console.WriteLine();
        }

        static void PrintLongestWord()
        {
            string Longest = ArrayoFWords.OrderByDescending(word => word.Length).First();
            Console.WriteLine($"The longest word is {Longest}");
            Console.WriteLine();
        }

        static void PrintAverageWordLength()
        {
            int wordsLenght = ArrayoFWords.Sum(word => word.Length);
            int numOfWords = ArrayoFWords.Length;
            Console.WriteLine($"Average word length is {wordsLenght / numOfWords}");
            Console.WriteLine();
        }

        static void PrintFiveMostCommonWords()
        {
            var wordGroups = ArrayoFWords.GroupBy(word => word)
                .Select(group => new { group.Key, Count = group.Count() })
                .OrderByDescending(group => group.Count);

            Console.WriteLine("Five most common words are");
            foreach (var wordGroup in wordGroups.Take(5))
            {
                Console.WriteLine($"Word: {wordGroup.Key} repeated {wordGroup.Count} times");
            }

            Console.WriteLine();
        }

        static void PrintFiveLeastCommonWords()
        {
            var wordGroups = ArrayoFWords.GroupBy(word => word)
                .Select(group => new { group.Key, Count = group.Count() })
                .OrderBy(group => group.Count);

            Console.WriteLine("Five least common words are");
            foreach (var wordGroup in wordGroups.Take(5))
            {
                Console.WriteLine($"Word: {wordGroup.Key} repeated {wordGroup.Count} times");
            }

            Console.WriteLine();
        }
    }
}