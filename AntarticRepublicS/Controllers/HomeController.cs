﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AntarticRepublicS.Models;
using AntarticRepublicS.Scripts;
using Newtonsoft.Json;
using RestSharp;

namespace AntarticRepublicS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
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
                IEncrypter encrpyter = SantaEncrypter.GetCorrespondingEncrypter(listFibonacciSequenceModel[i].Algorithm,
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
                        PostPayLoad<ResponseStatusModel>("http://internal-devchallenge-2-dev.apphb.com" , request,guid, listFibonacciSequenceModel[i].Algorithm);
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

        public T PostPayLoad<T>(string url, PayLoadRequestModel payLoadModel,Guid guid, string name)
        {

            var client = new RestClient(url);
            var request = new RestRequest("/values/" + guid + "/" + name, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddBody(payLoadModel);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}