using BookWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Controllers
{
    public class DanhMucSanPhamController : Controller
    {
        private readonly BookwebContext _context;

        public DanhMucSanPhamController(BookwebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List(string MaDanhMuc = "")
        {
            var danhmuc = await _context.DanhMucSanPhams
                .FirstOrDefaultAsync(c => c.MaDanhMuc == MaDanhMuc);

            if (danhmuc == null)
                return RedirectToAction("Home");

            var sanphamByDanhMuc = await _context.SanPhams
                .Where(sp => sp.MaDanhMuc == MaDanhMuc)
                .OrderByDescending(sp => sp.MaSanPham)
                .ToListAsync();

            ViewBag.DanhMuc = danhmuc;
            return View(sanphamByDanhMuc);
        }

        public IActionResult Index() => View(_context.DanhMucSanPhams.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(DanhMucSanPham model)
        {
            if (ModelState.IsValid)
            {
                _context.DanhMucSanPhams.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(string id) => View(_context.DanhMucSanPhams.Find(id));

        [HttpPost]
        public IActionResult Edit(DanhMucSanPham model)
        {
            _context.DanhMucSanPhams.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var danhMuc = _context.DanhMucSanPhams.FirstOrDefault(d => d.MaDanhMuc == id);
            if (danhMuc == null)
                return NotFound();

            return View(danhMuc);
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(string id)
        {
            var item = _context.DanhMucSanPhams.Find(id);
            if (item != null)
            {
                _context.DanhMucSanPhams.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            var item = _context.DanhMucSanPhams.Find(id);
            return View(item);
        }

    }
}
