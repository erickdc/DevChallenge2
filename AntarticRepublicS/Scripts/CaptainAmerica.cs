using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using AntarticRepublicS.Models;

namespace AntarticRepublicS.Scripts
{
    public class CaptainAmerica:IEncrypter
    {
        private static double _previousFibonacci;
        public static FibonacciSequenceModel WordModel { get; set; }

        FibonacciSequenceModel IEncrypter.WordModel
        {
            get { return WordModel; }
            set { WordModel = value; }
        }
        public CaptainAmerica(FibonacciSequenceModel model)
        {
            WordModel = model;
            SetPreviousFibonacciSequence((int)WordModel.StartingFibonacciNumber);

        }
        public string Encrypt()
        {
            var listWords = SantaEncrypter.ShiftVowels(WordModel.Words, new[] { 'a', 'e', 'i', 'o', 'u', 'y' });
            SantaEncrypter.OrderArrayWords(listWords, SantaEncrypter.Order.Ascending);
            SantaEncrypter.OrderArrayWords(listWords, SantaEncrypter.Order.Reverse);
            var listWordsFibo = ChangeVowelsByNumber(listWords, WordModel.StartingFibonacciNumber) as string[];
            var concatenatedString = SantaEncrypter.GetStringByAsciiDelimiter(listWordsFibo);
            return SantaEncrypter.EncodeBase64(concatenatedString);
        }

        public static ICollection ChangeVowelsByNumber(string[] arrayWords, double startFibonacciNumber)
        {
            for (int i = 0; i < arrayWords.Length; i++)
            {
                for (int j = 0; j < arrayWords[i].Length; j++)
                {
                    if (SantaEncrypter.BelongGroupLetters(arrayWords[i][j], new[] { 'a', 'e', 'i', 'o', 'u', 'y' }))
                    {
                        var number = GetNextFibonacciSequence();
                        int amount = 1;
                        arrayWords[i] = arrayWords[i].Remove(j, amount);
                        arrayWords[i] = arrayWords[i].Insert(j, number.ToString(CultureInfo.InvariantCulture));

                    }
                }
            }
            return arrayWords;
        }

        private static void SetPreviousFibonacciSequence(int num)
        {
            int a, b = 1;
            var fibonacciN = a = 0;
            while (num != fibonacciN)
            {
                fibonacciN = a + b;
                a = b;
                b = fibonacciN;
            }
            _previousFibonacci = a;
        }
        private static double GetNextFibonacciSequence()
        {
            double previousFibonacciTemp = _previousFibonacci;
            double currentFibonacciTemp = WordModel.StartingFibonacciNumber;
            _previousFibonacci = WordModel.StartingFibonacciNumber;
            WordModel.StartingFibonacciNumber = previousFibonacciTemp + WordModel.StartingFibonacciNumber;
            return currentFibonacciTemp;
        }

    }
}