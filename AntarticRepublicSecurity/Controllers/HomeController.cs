using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AntarticRepublicSecurity.Models;
using AntarticRepublicSecurity.Views;
using AntarticRepublicSecurity;
using Newtonsoft.Json;

namespace AntarticRepublicSecurity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var listWords = new List<string>();
            int amountTimes = 20;
            var listFibonacciSequenceModel = new List<FibonacciSequenceModel>();
            var responseList = new List<ResponseStatusModel>();
            for (int i = 0; i < amountTimes; i++)
            {
                var guid = Guid.NewGuid();
                listFibonacciSequenceModel.Add(
                    MapJsonToModel<FibonacciSequenceModel>("http://internal-devchallenge-2-dev.apphb.com/values/" + guid));
                IEncrypt encrpyter = SantaEncrypter.GetCorrespondingEncrypter(listFibonacciSequenceModel[i].Algorithm,
                    listFibonacciSequenceModel[i]);
                listWords.Add(encrpyter.Encrypt());
                if (listWords.Count > i)
                {
                    PayLoadRequestModel request = new PayLoadRequestModel()
                    {
                        EncodedValue = listWords[i],
                        EmailAddress = "erickdcb10@gmail.com",
                        Name = "Erick Caballero",
                        RepoUrl = "https://github.com/erickdc/DevChallenge2",
                        WebHookUrl = "http://antarticchallengedev2.apphb.com"

                    };
                    ResponseStatusModel response =
                        PostPayLoad<ResponseStatusModel>("http://internal-devchallenge-2-dev.apphb.com/values/" + guid +
                                                         "/" + listFibonacciSequenceModel[i].Algorithm, request);
                    responseList.Add(response);
                }
            }
            return View();
    }
    public T MapJsonToModel<T>(string url)
    {
        var cli = new WebClient { Headers = {[HttpRequestHeader.ContentType] = "application/json" } };
        var response = cli.DownloadString(url);
        return JsonConvert.DeserializeObject<T>(response);
    }

        public T PostPayLoad<T>(string url,PayLoadRequestModel payLoadModel)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(payLoadModel);
              
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result= streamReader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(result);
        }
    public ActionResult About()
    {
        ViewBag.Message = "Your application description page.";

        return View();
    }

    public ActionResult Contact()
    {
        ViewBag.Message = "Your contact page.";

        return View();
    }
}



    }
