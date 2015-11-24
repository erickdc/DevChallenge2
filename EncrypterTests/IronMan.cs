using AntarticRepublicSecurity.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AntarticRepublicSecurity
{
    [TestClass]
    public class IronMan
    {
        [TestMethod]
        public void OrderArrayOfWords()
        {
            string[] arrayWords = { "dog", "cat", "5zebra", "bird" };
            string[] expectedArrayWords = { "5zebra", "bird", "cat", "dog" };
            var santaE = new SantaEncrypter(arrayWords);
            SantaEncrypter.OrderArrayWords(santaE.ArrayWords, SantaEncrypter.Order.Ascending);
            CollectionAssert.AreEqual(expectedArrayWords, arrayWords);
        }

        [TestMethod]
        public void ShiftVowelsToRight()
        {
            string[] arrayWords = { "hEllo", "bOok", "read", "NeEd", "paliNdromE", "happy" };
            string[] expectedArrayWords = { "ohlEl", "boOk", "raed", "NEed", "EplaNidrmo", "yhpap" };
            var santaE = new SantaEncrypter(arrayWords);

            CollectionAssert.AreEqual(expectedArrayWords,
                SantaEncrypter.ShiftVowels(santaE.ArrayWords, new[] { 'a', 'e', 'i', 'o', 'u', 'y' }));
        }

        [TestMethod]
        public void SeparateWordByDelimeterAsciiNumber()
        {
            string[] arrayWords = { "dog", "cat", "bird" };

            var santaE = new SantaEncrypter(arrayWords);
            Assert.AreEqual("dog98cat100bird99", SantaEncrypter.GetStringByAsciiDelimiter(arrayWords));
        }

        [TestMethod]
        public void EncodeBase64()
        {
            string[] arrayWords = { "dog", "cat", "bird" };
            var newString = "dog98cat100bird99";

            var santaE = new SantaEncrypter(arrayWords);

            Assert.AreEqual("ZG9nOThjYXQxMDBiaXJkOTk=", SantaEncrypter.EncodeBase64(newString));
        }
    }
}
