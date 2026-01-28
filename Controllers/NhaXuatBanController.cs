using BookWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Controllers
{
    public class NhaXuatBanController : Controller
    {
        private readonly BookwebContext _context;

        public NhaXuatBanController(BookwebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List(string MaNhaXuatBan = "")
        {
            // Tìm nhà xuất bản theo MaNhaXuatBan
            var nxb = await _context.NhaXuatBans
                .FirstOrDefaultAsync(c => c.MaNhaXuatBan == MaNhaXuatBan);

            // Nếu không tìm thấy thì quay về trang Home
            if (nxb == null)
                return RedirectToAction("Home");

            // Lấy danh sách sản phẩm thuộc nhà xuất bản đó
            var sanphamByNXB = await _context.SanPhams
                .Where(sp => sp.MaNhaXuatBan == MaNhaXuatBan)
                .OrderByDescending(sp => sp.MaSanPham)
                .ToListAsync();

            // Truyền thông tin nhà xuất bản sang View
            ViewBag.NhaXuatBan = nxb;
            return View(sanphamByNXB);
        }
        public IActionResult Index() => View(_context.NhaXuatBans.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(NhaXuatBan model)
        {
            if (ModelState.IsValid)
            {
                _context.NhaXuatBans.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(string id) => View(_context.NhaXuatBans.Find(id));

        [HttpPost]
        public IActionResult Edit(NhaXuatBan model)
        {
            _context.NhaXuatBans.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var nxb = _context.NhaXuatBans.FirstOrDefault(n => n.MaNhaXuatBan == id);
            if (nxb == null)
                return NotFound();

            return View(nxb);
        }

        public IActionResult Delete(string id)
        {
            var item = _context.NhaXuatBans.Find(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string id)
        {
            var item = _context.NhaXuatBans.Find(id);
            if (item != null)
            {
                _context.NhaXuatBans.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
