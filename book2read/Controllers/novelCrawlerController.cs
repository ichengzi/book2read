using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using HtmlAgilityPack;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace book2read.Controllers
{
    [Route("api/[controller]")]
    public class NovelCrawlerController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";

            var client = new System.Net.Http.HttpClient();
            var html = client.GetAsync("http://www.piaotian.com/html/8/8253/index.html").Result.Content.ToString() ;

            


        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage result = await client.GetAsync("http://www.piaotian.com/html/8/8253/index.html");

            Stream stream = await result.Content.ReadAsStreamAsync();

            HtmlDocument doc = new HtmlDocument();

            doc.Load(stream);

            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");
            //the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 

            return View(links);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
