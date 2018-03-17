using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using book2read.Models;
using System.Text;
using System.Net.Http;
using System.IO;
using HtmlAgilityPack;

namespace book2read.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ReadBook()
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //HttpClient client = new HttpClient();
            //HttpResponseMessage result = await client.GetAsync("https://www.piaotian.com/html/8/8253/index.html");
            //Stream stream = await result.Content.ReadAsStreamAsync();
            //client.Dispose();

            //HtmlDocument doc = new HtmlDocument();
            //doc.Load(stream, System.Text.Encoding.GetEncoding("GBK"));


            //var contentDiv = doc.DocumentNode.SelectSingleNode("//div[@class='centent']");
            //var items = contentDiv.SelectNodes("//a");
            ////the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 

            //var nodes = items.Select(x => new {name = x.InnerText, value =x.Attributes["href"].Value});

            //return Json(nodes);
            return View();
        }

        public async Task<JsonResult> ReadBook2()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            HttpClient client = new HttpClient();
            HttpResponseMessage result = await client.GetAsync("https://www.piaotian.com/html/8/8253/index.html");
            Stream stream = await result.Content.ReadAsStreamAsync();


            HtmlDocument doc = new HtmlDocument();
            doc.Load(stream, System.Text.Encoding.GetEncoding("GBK"));


            //var contentDiv = doc.DocumentNode.SelectSingleNode("//div[@class='centent']");
            var items = doc.DocumentNode.SelectNodes("//ul//li/a").Reverse();
            //the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 

            var nodes = items.Select(x => new
            {
                con = x.InnerText,
                url = "https://www.piaotian.com/html/8/8253/" + x.Attributes["href"].Value
            });

            //var last = items.Last();
            //var htmldoc = await client.GetAsync("https://www.piaotian.com/html/8/8253/"+last.Attributes["href"].Value);
            //Stream stream2 = await result.Content.ReadAsStreamAsync();
            //var doc2 = new HtmlDocument();
            //doc2.Load(stream2, System.Text.Encoding.GetEncoding("gb2312"));
            //var yy = doc2.DocumentNode.SelectSingleNode("//div[@id='content']");

            //client.Dispose();
            return Json(nodes);
        }
    }
}
