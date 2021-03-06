﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntarticRepublicS.Models;

namespace AntarticRepublicS.Scripts
{
    public class IronMan :IEncrypter
    {
        public FibonacciSequenceModel WordModel { get; set; }

        public IronMan(FibonacciSequenceModel model)
        {
            WordModel = model;
        }
        public string Encrypt()
        {
            SantaEncrypter.OrderArrayWords(WordModel.Words, SantaEncrypter.Order.Ascending);
            var listWords = SantaEncrypter.ShiftVowels(WordModel.Words, new[] { 'a', 'e', 'i', 'o', 'u', 'y' });
            string concatenatedString = SantaEncrypter.GetStringByAsciiDelimiter(listWords);
            return SantaEncrypter.EncodeBase64(concatenatedString);
        }
    }
}