using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAppPelda.Models;
using WebAppPelda.Services;

namespace WebAppPelda.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Sajat()
        {
            List<Vasarlo> vasarlok = new VasarloService().GetAllVasarlok();
            return View(vasarlok);
        }

        public IActionResult Vasarlo(int id)
        {
            Vasarlo kivalasztott = new VasarloService().GetById(id);

            return View(kivalasztott);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult UjVasarlo()
        {
            var model = new Vasarlo();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult UjVasarlo(Vasarlo vasarlo)
        {
            if (!ModelState.IsValid)
            {
                return View(vasarlo);
            }

            string result = new VasarloService().PostVasarlo(vasarlo);

            TempData["SuccessMessage"] = result;

            return RedirectToAction(nameof(UjVasarlo));

        }



        [HttpGet]
        public IActionResult VasarloModositas(int id)
        {
            var model = new VasarloService().GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult VasarloModositas(Vasarlo vasarlo)
        {
            if (!ModelState.IsValid)
            {
                return View(vasarlo);
            }

            string result = new VasarloService().PutVasarlo(vasarlo);

            TempData["SuccessMessage"] = result;

            return RedirectToAction(nameof(VasarloModositas));

        }


        [HttpGet]
        public IActionResult DeleteVasarlo(int id)
        {
            var model = new VasarloService().GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteVasarlo(Vasarlo vasarlo)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(vasarlo);
            //}

            string result = new VasarloService().DeleteVasarlo(vasarlo.Id);

            TempData["SuccessMessage"] = result;

            List<Vasarlo> vasarlok = new VasarloService().GetAllVasarlok();
            return View("Sajat", vasarlok);

        }

    }
}
