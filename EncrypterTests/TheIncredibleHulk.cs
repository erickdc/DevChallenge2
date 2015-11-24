using System;
using System.Text;
using AntarticRepublicSecurity.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AntarticRepublicSecurity
{
    [TestClass]
    public class TheIncredibleHulk
    {
        [TestMethod]
        public void ShiftVowelsToTheRight()
        {
            string[] arrayWords = { "hEllo", "bOok", "read", "NeEd", "paliNdromE", "happy" };
            string[] expectedArrayWords = { "ohlEl", "boOk", "raed", "NEed", "EplaNidrmo", "yhpap" };
            SantaEncrypter santaE = new SantaEncrypter(arrayWords);

            CollectionAssert.AreEqual(expectedArrayWords, SantaEncrypter.ShiftVowels(santaE.ArrayWords, new[] { 'a', 'e', 'i', 'o', 'u', 'y' }));
        }

        [TestMethod]
        public void DescendingOrderArrayOfWords()
        {
            string[] arrayWords = { "bird", "cat", "dog" };
            string[] expectedArrayWords = { "dog", "cat", "bird" };
            SantaEncrypter santaE = new SantaEncrypter(arrayWords);
            SantaEncrypter.OrderArrayWords(santaE.ArrayWords, SantaEncrypter.Order.Reverse);
            CollectionAssert.AreEqual(expectedArrayWords, arrayWords);
        }

        [TestMethod]
        public void ConcatenateArrayOfWordsWithAsterisks()
        {
            string[] arrayWords = { "bird", "cat", "dog" };
            string expectedResult = "bird*cat*dog";
            SantaEncrypter santaE = new SantaEncrypter(arrayWords);
          
            Assert.AreEqual(expectedResult, SantaEncrypter.GetStringsByDelimiter(santaE.ArrayWords, '*'));
        }

       

    }
}
