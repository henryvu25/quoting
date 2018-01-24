using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace quoting_proj.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;
 
        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }
       
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("process")]
        public IActionResult Process(string name, string quote)
        {
            _dbConnector.Execute($"INSERT INTO quotes (name, quote) VALUES ('{name}', '{quote}')");
            return RedirectToAction("Display");
        }
        [HttpGet]
        [Route("quotes")]
        public IActionResult Display()
        {
            List<Dictionary<string, object>> allQuotes = _dbConnector.Query("SELECT * FROM quotes ORDER BY id DESC");
            ViewBag.quotes = allQuotes;
            return View("Quotes");
        }
    }
}
