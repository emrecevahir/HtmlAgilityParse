using HtmlAgilityPack;
using HtmlAgilityParse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace HtmlAgilityParse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

   
        public IActionResult Index()
        {
            var a = new ViewModel();
            
            var web = new WebClient();
            web.Encoding = Encoding.UTF8;
            var html = web.DownloadString("https://deprem.afad.gov.tr/last-earthquakes.html");
            var afad = new HtmlDocument();
            afad.LoadHtml(html);

            var location = afad.DocumentNode.SelectNodes("//tr//td[position()=7]");
            var enlem = afad.DocumentNode.SelectNodes("//tr//td[position()=2]");
            var boylam = afad.DocumentNode.SelectNodes("//tr//td[position()=3]");
            var siddeti = afad.DocumentNode.SelectNodes("//tr//td[position()=6]");
            var date = afad.DocumentNode.SelectNodes("//tr//td[position()=1]");
            foreach (var item in location)
            {
                
                a.locations.Add(item.InnerText);
            }
            foreach (var item in enlem)
            {
                a.enlems.Add(item.InnerText);
            }
            foreach (var item in boylam)
            {
                a.boylams.Add(item.InnerText);
            }
            foreach (var item in siddeti)
            {
                a.siddets.Add(item.InnerText);
            }
            foreach (var item in date)
            {
                a.dates.Add(item.InnerText);
            }
            return View("Index",a);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
