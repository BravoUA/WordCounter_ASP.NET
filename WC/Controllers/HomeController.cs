using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WC.Models;
using WordCounter.Models;

namespace WC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        List<WordModel> _words;
        public IActionResult Index()
        {

           

            return View(_words);
        }


        

        public IActionResult Result(InputPropertiesModel obj)
        {
            WordCounterModel C = new WordCounterModel(obj.topWordCount, obj.topWordResaulCount, obj.ollText);
            _words = C.Resultat;
            return View(_words);
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