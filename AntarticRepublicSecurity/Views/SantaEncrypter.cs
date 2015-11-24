using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntarticRepublicSecurity.Models;

namespace AntarticRepublicSecurity.Views
{
    public class SantaEncrypter
    {
       
        public enum Order
        {
            Ascending,
            Reverse
        }

        public string[] ArrayWords;
        private static FibonacciSequenceModel _fiboModel;
        private static double _previousFibonacci;
        public SantaEncrypter(string[] arrayWords)
        {
            ArrayWords = arrayWords;
        }

        public SantaEncrypter(FibonacciSequenceModel model)
        {
            _fiboModel = model;
        }
     

        public static void OrderArrayWords(string[] arrayWords, Order order)
        {
            if (order == Order.Ascending)
            {
                Array.Sort(arrayWords);
            }
            else
            {
                Array.Reverse(arrayWords);
            }
        }

        public static string[] ShiftVowels(string[] arrayWords, char[] c)
        {
            var newList = new List<string>();
            for (var i = 0; i < arrayWords.Length; i++)
            {
                newList.Add(Shift(arrayWords[i], c));
            }

            return newList.ToArray();
        }

        private static string Shift(string word, char[] c)
        {
            var sizeWord = word.Length;
            var newWordString = new StringBuilder();
            var newWord = word.ToCharArray();
            for (var i = 0; i < sizeWord; i++)
            {
                if (i == sizeWord - 1)
                {
                    if (BelongGroupLetters(newWord[i], c))
                    {
                        var temp = newWordString.ToString();
                        newWordString.Clear();
                        newWordString.Append(newWord[i]);
                        newWordString.Append(temp);
                    }
                    else
                    {
                        newWordString.Append(newWord[i]);
                    }
                }
                else
                {
                    if (BelongGroupLetters(newWord[i], c))
                    {
                        newWordString.Append(newWord[i + 1]);
                        newWordString.Append(newWord[i]);
                        i++;
                    }
                    else
                    {
                        newWordString.Append(newWord[i]);
                    }
                }
            }
            return newWordString.ToString();
        }

        public static bool BelongGroupLetters(char letter, char[] c)
        {
            return c.Contains(char.ToLower(letter));
        }

        public static string GetStringByAsciiDelimiter(string[] arrayWords)
        {
            var amountWords = arrayWords.Length;
            var newWordString = new StringBuilder();
            for (var i = 0; i < amountWords; i++)
            {
                newWordString.Append(arrayWords[i]);
                AppendDelimeter(arrayWords, i, amountWords, newWordString);
            }
            return newWordString.ToString();
        }

        public static string GetStringsByDelimiter(string[] arrayWords, char delimiter)
        {
            var amountWords = arrayWords.Length;
            var newWordString = new StringBuilder();
            for (var i = 0; i < amountWords; i++)
            {
                newWordString.Append(arrayWords[i]);
                if(i<amountWords-1)
                    newWordString.Append(delimiter);
            }
            return newWordString.ToString();
        }

        private static void AppendDelimeter(string[] arrayWords, int i, int amountWords, StringBuilder newWordString)
        {
            if (i == 0 && amountWords > 1)
            {
                newWordString.Append(((int)arrayWords[amountWords - 1][0]));
            }
            else
            {
                newWordString.Append(((int)arrayWords[i - 1][0]));
            }
        }

        public static string EncodeBase64(string valToEncode)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(valToEncode);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static ICollection SplitEnglishWords(string[] combinedEnglishWords, string[] englishWords)
        {
            List<string> newCollectionEnglishWord = new  List<string>();
            for (int i = 0; i < combinedEnglishWords.Length; i++)
            {
                int amountAddedToList = 0;
                for (int j = 0; j < englishWords.Length; j++)
                {
                    int indexWord = combinedEnglishWords[i].IndexOf(englishWords[j], StringComparison.Ordinal);
                    int totalWordSize = indexWord + englishWords[j].Length;
                    if (indexWord < 0) continue;
                    amountAddedToList++;
                    Console.WriteLine(englishWords[j]);
                    combinedEnglishWords[i] = combinedEnglishWords[i].Substring(totalWordSize);
                    newCollectionEnglishWord.Add(englishWords[j]);
                }
                if(amountAddedToList ==0)
                    newCollectionEnglishWord.Add(combinedEnglishWords[i]);
                
                    
                
               
            }
            return newCollectionEnglishWord.ToArray();
        }

        public static ICollection AlternateConstantLetters(string[] arrayWords)
        {
            int actualState = char.IsUpper(arrayWords[0][0])?1:0;

            for (int i = 0; i < arrayWords.Length; i++)
            {
                for (int j = 0; j < arrayWords[i].Length; j++)
                {
                    int amount = 1;
                    if (!BelongGroupLetters(arrayWords[i].ElementAt(j),new char[] {'a','e','i','o','u','y'}))
                    {
                        char tempLetter;
                        if (actualState == 0)
                        {
                            tempLetter = arrayWords[i][j];
                            arrayWords[i] = arrayWords[i].Remove(j, amount);
                            arrayWords[i]= arrayWords[i].Insert(j, char.ToLower(tempLetter).ToString());
                            actualState = 1;
                        }
                        else
                        {
                            tempLetter = arrayWords[i][j];
                            arrayWords[i] = arrayWords[i].Remove(j, amount);
                            arrayWords[i] = arrayWords[i].Insert(j, char.ToUpper(tempLetter).ToString());
                            actualState = 0;
                        }
                    }
                }

                

            }
            return arrayWords;

        }

        public static IEncrypt GetCorrespondingEncrypter(string algorithmName,FibonacciSequenceModel listFibonacciSequenceModel)
        {
            if (algorithmName.ToLower().Equals("theincrediblehulk"))
            {
                return new TheIncredibleHulk(listFibonacciSequenceModel);
            }
            if (algorithmName.ToLower().Equals("captainamerica"))
            {
                return new CaptainAmerica(listFibonacciSequenceModel);
            }
            if (algorithmName.ToLower().Equals("thor"))
            {
                return new Thor(listFibonacciSequenceModel);
            }
            return new IronMan(listFibonacciSequenceModel);
        }

      


    }
}