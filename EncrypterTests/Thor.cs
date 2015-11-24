using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntarticRepublicSecurity.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AntarticRepublicSecurity
{
    [TestClass]
    public class Thor
    {
        [TestMethod]
        public void SplitCombinedEnglishWord()
        {
            string[] englishWords = {"home", "notch", "light", "saw"};
            string[] combinedEnglishWords = {"homenotch", "lightsaw"};
            
            SantaEncrypter santaE = new SantaEncrypter(englishWords);
            CollectionAssert.AreEqual(englishWords, SantaEncrypter.SplitEnglishWords(combinedEnglishWords,englishWords));
        }

        [TestMethod]
        public void OrderValuesAlphabeticOrder()
        {
            string[] arrayWords = { "dog", "cat", "5zebra", "bird" };
            string[] expectedArrayWords = { "5zebra", "bird", "cat", "dog" };
            var santaE = new SantaEncrypter(arrayWords);
            SantaEncrypter.OrderArrayWords(santaE.ArrayWords, SantaEncrypter.Order.Ascending);
            CollectionAssert.AreEqual(expectedArrayWords, arrayWords);
        }

        [TestMethod]
        public void AlternateConstantLetters()
        {
            string[] arrayWords = { "DoG", "CaT", "BiRd" };
            string[] expectedArrayWords = { "Dog", "Cat", "BirD" };
            var santaE = new SantaEncrypter(arrayWords);
          
            CollectionAssert.AreEqual(expectedArrayWords, SantaEncrypter.AlternateConstantLetters(arrayWords));
        }

  
    }

}
   