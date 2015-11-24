using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntarticRepublicS.Models;

namespace AntarticRepublicS.Scripts
{
    public interface IEncrypter
    {
        FibonacciSequenceModel WordModel { get; set; }
        string Encrypt();
    }
}
