using System.Diagnostics;
using BookWeb.Data;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookwebContext _context;

        private readonly ILogger<HomeController> _logger;
        private int statuscode;

        public HomeController(ILogger<HomeController> logger, BookwebContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var sanpham = _context.SanPhams.Include(sp => sp.MaDanhMucNavigation).Include(sp => sp.MaNhaXuatBanNavigation).ToList();
            return View(sanpham);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if(statuscode == 404)
            {
                return View("NotFound");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
