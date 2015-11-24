using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntarticRepublicSecurity.Models;

namespace AntarticRepublicSecurity.Views
{
    public class TheIncredibleHulk : IEncrypt
    {
        public FibonacciSequenceModel WordModel { get; set; }

        public TheIncredibleHulk(FibonacciSequenceModel model)
        {
            WordModel = model;
        }
        public string Encrypt()
        {
            var listWords = SantaEncrypter.ShiftVowels(WordModel.Words, new[] { 'a', 'e', 'i', 'o', 'u', 'y' });
            SantaEncrypter.OrderArrayWords(listWords, SantaEncrypter.Order.Reverse);
            string concatenatedString = SantaEncrypter.GetStringsByDelimiter(listWords,'*');
            return SantaEncrypter.EncodeBase64(concatenatedString);

        }
    }
}