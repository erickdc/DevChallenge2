using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using AntarticRepublicSecurity.Models;

namespace AntarticRepublicSecurity.Views
{
    public class Thor : IEncrypt
    {
        public static FibonacciSequenceModel WordModel { get; set; }
        public string[] ListEnglishWords ={"drool","cats","clean","code","dogs","materials","needed","this","is","hard","what","are","you","smoking","shot","gun","down","river","super","man","rule","acklen","developers","are","amazing" };
        private static double _previousFibonacci;
      

        FibonacciSequenceModel IEncrypt.WordModel
        {
            get { return WordModel; }
            set { WordModel = value; }
        }

        public Thor(FibonacciSequenceModel model)
        {
            WordModel = model;
            SetPreviousFibonacciSequence((int) WordModel.StartingFibonacciNumber);

        }
        public string Encrypt()
        {
            string[] listWords = SantaEncrypter.SplitEnglishWords(WordModel.Words, ListEnglishWords) as string[];
            SantaEncrypter.OrderArrayWords(listWords, SantaEncrypter.Order.Ascending);
            SantaEncrypter.AlternateConstantLetters(listWords);
            string [] listWordsFibo = ChangeVowelsByNumber(WordModel.Words, WordModel.StartingFibonacciNumber) as string[];
            string asteriskConctS = SantaEncrypter.GetStringsByDelimiter(listWordsFibo, '*');
            return SantaEncrypter.EncodeBase64(asteriskConctS);
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

        private static double SetPreviousFibonacciSequence(int num)
        {
            int a,b=1;
            var fibonacciN = a = 0;
            while (num!=fibonacciN)
            {
                fibonacciN = a + b;
                a = b;
                b = fibonacciN;
            }
            return a;
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