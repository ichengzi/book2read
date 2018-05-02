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

        #region MyRegion
        public async Task<IActionResult> ReadBook3(int count)
        {
            var mycount = 10;
            var max = 50;
            if (count > 0 && count <= max)
                mycount = count;
            else if (count > max)
                mycount = max;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            HttpClient client = new HttpClient();
            HttpResponseMessage result = await client.GetAsync("https://www.piaotian.com/html/8/8253/index.html");
            Stream stream = await result.Content.ReadAsStreamAsync();


            HtmlDocument doc = new HtmlDocument();
            doc.Load(stream, System.Text.Encoding.GetEncoding("GBK"));


            //var contentDiv = doc.DocumentNode.SelectSingleNode("//div[@class='centent']");
            var items = doc.DocumentNode.SelectNodes("//ul//li/a").Reverse().Take(mycount);
            //the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 

            var nodes = items.Select(x => new NovelPageViewModel
            {
                Name = x.InnerText,
                //Url = "https://www.piaotian.com/html/8/8253/" + x.Attributes["href"].Value
                Url = "/home/readbook4?articleid=" + x.Attributes["href"].Value.Replace(".html", "")
            }).ToList();
            ViewData["Title"] = "圣墟";
            ViewData["bookTitle"] = "圣墟";
            return View("ReadBook3", nodes);
        }

        public async Task<IActionResult> ReadBook4(string articleId)
        {
            if (articleId == null)
            {
                ViewData["Title"] = "Error";
                return View("ReadBook4", new List<string> { "ArticleId is empty!" });
            }

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(5);
            var url = $"https://www.piaotian.com/html/8/8253/{articleId}.html";
            HttpResponseMessage result = await client.GetAsync(url);
            var articleUpdatedDate = result.Content.Headers.LastModified;//文章更新日期
            Stream stream = await result.Content.ReadAsStreamAsync();
            client.Dispose();

            HtmlDocument doc = new HtmlDocument();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            doc.Load(stream, System.Text.Encoding.GetEncoding("GBK"));

            //var contentDiv = doc.DocumentNode.SelectSingleNode("//div[@id='content']");
            //非标准html文档， 且经过js修改后才有这个id 的div
            var body = doc.DocumentNode.InnerText;
            var liststr = body.Split("\r\n");
            var title = liststr[40];
            var content = liststr[53].Replace("&nbsp;&nbsp;&nbsp;&nbsp;", "\r\n");
            var list = content.Split("\r\n").Select(x => x.Trim());
            ViewData["Title"] = title;

            //var utctime = articleUpdatedDate.ToUniversalTime();
            //TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Shanghai");
            //DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(utctime.DateTime, cstZone);
            //ViewData["articleUpdatedDate"] = cstTime == default(DateTime) ?
            //    "" : cstTime.ToString("yyyy-MM-dd HH:mm:ss z");
            ViewData["articleUpdatedDate"] = articleUpdatedDate?.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
            return View("ReadBook4", list);
        } 
        #endregion

        #region 凡人
        public async Task<IActionResult> ReadBook5(int count)
        {
            var mycount = 10;
            var max = 50;
            if (count > 0 && count <= max)
                mycount = count;
            else if (count > max)
                mycount = max;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            HttpClient client = new HttpClient();
            HttpResponseMessage result = await client.GetAsync("https://www.piaotian.com/html/9/9102/index.html");
            Stream stream = await result.Content.ReadAsStreamAsync();


            HtmlDocument doc = new HtmlDocument();
            doc.Load(stream, System.Text.Encoding.GetEncoding("GBK"));


            //var contentDiv = doc.DocumentNode.SelectSingleNode("//div[@class='centent']");
            var items = doc.DocumentNode.SelectNodes("//ul//li/a").Reverse().Take(mycount);
            //the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 

            var nodes = items.Select(x => new NovelPageViewModel
            {
                Name = x.InnerText,
                //Url = "https://www.piaotian.com/html/9/9102/" + x.Attributes["href"].Value
                Url = "/home/readbook6?articleid=" + x.Attributes["href"].Value.Replace(".html", "")
            }).ToList();
            ViewData["Title"] = "凡人修仙之仙界篇";
            ViewData["bookTitle"] = "凡人修仙之仙界篇";
            return View("ReadBook3", nodes);
        }

        public async Task<IActionResult> ReadBook6(string articleId)
        {
            if (articleId == null)
            {
                ViewData["Title"] = "Error";
                return View("ReadBook4", new List<string> { "ArticleId is empty!" });
            }

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(5);
            var url = $"https://www.piaotian.com/html/9/9102/{articleId}.html";
            HttpResponseMessage result = await client.GetAsync(url);
            var articleUpdatedDate = result.Content.Headers.LastModified;//文章更新日期
            Stream stream = await result.Content.ReadAsStreamAsync();
            client.Dispose();

            HtmlDocument doc = new HtmlDocument();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            doc.Load(stream, System.Text.Encoding.GetEncoding("GBK"));

            //var contentDiv = doc.DocumentNode.SelectSingleNode("//div[@id='content']");
            //非标准html文档， 且经过js修改后才有这个id 的div
            var body = doc.DocumentNode.InnerText;
            var liststr = body.Split("\r\n");
            var title = liststr[40];
            var content = liststr[53].Replace("&nbsp;&nbsp;&nbsp;&nbsp;", "\r\t");
            var list = content.Split("\r\t").Select(x => x.Trim());
            ViewData["Title"] = title;

            //var utctime = articleUpdatedDate.ToUniversalTime();
            //TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            //DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(utctime.DateTime, cstZone);
            //ViewData["articleUpdatedDate"] = cstTime == default(DateTime) ?
            //    "" : cstTime.ToString("yyyy-MM-dd HH:mm:ss z");
            ViewData["articleUpdatedDate"] = articleUpdatedDate?.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
            return View("ReadBook4", list);
        } 
        #endregion
    }
}
