using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntarticRepublicS.Models
{
    public class PayLoadRequestModel
    {
        public string EncodedValue { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string WebHookUrl { get; set; }
        public string RepoUrl { get; set; }
    }
}