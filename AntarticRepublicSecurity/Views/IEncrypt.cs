using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntarticRepublicSecurity.Models;

namespace AntarticRepublicSecurity.Views
{
    public interface IEncrypt
    {
        FibonacciSequenceModel WordModel { get; set; }
        string Encrypt();
    }
}