using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoshanDemo1.Data;
using RoshanDemo1.Models;
using System.Diagnostics;

namespace RoshanDemo1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;

        }

        public IActionResult Index()
        {
            //var questions = _dbContext.Questions.Include(q => q.Options).ToList();
            var questions = _dbContext.Questions
            .Include(q => q.Options)
            .ThenInclude(o => o.Question)
            .ToList();
            return View(questions);
        }

        [HttpPost]
        public IActionResult SaveAnswers(List<int> answers)
        {
            
            return RedirectToAction("Index");
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