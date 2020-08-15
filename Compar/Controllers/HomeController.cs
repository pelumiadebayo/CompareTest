using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Compar.Models;

namespace Compar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICompareService _compareService;
       
        public HomeController(ILogger<HomeController> logger, ICompareService compareService)
        {
            _logger = logger;
            _compareService = compareService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard(string Id)
        {
            ViewBag.Email = Id;
            return View();
        }
        public IActionResult Compare()
        {
            return View();
        }
        public IActionResult Comparison(string FN, string FF, string SN, string SF)
        {
            Random random = new Random();
            int percentage = random.Next(1, 99);
            var result = new History
            {
                FirstStudentName = FN,
                FirstStudentFile =FF,
                SecondStudentName =SN,
                SecondStudentFile = SF,
                ComapisonPercentage = percentage
            };
           
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Compare(Compare Input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {

                    var result = await _compareService.CompareInput(Input);

                    if (result.Success == true)
                    {

                        return RedirectToAction("Comparison", new { FN = result.Data.FirstStudentName,
                        FF = result.Data.SecondStudentFile, SN = result.Data.SecondStudentName, SF = result.Data.SecondStudentFile
                        });
                    }
                    else
                    {
                        return View(result.Data);
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return View(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Login Input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {

                    var result = await _compareService.AdminLogin(Input);

                    if (result.Success == true)
                    {

                        return RedirectToAction("Dashboard", new { Id = result.Data.Email });
                    }
                    else
                    {
                        return View(result.Data);
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return View(e);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
