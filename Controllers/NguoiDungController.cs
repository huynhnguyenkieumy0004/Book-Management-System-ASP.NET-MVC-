using BookWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace BookWeb.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly BookwebContext _context;

        public NguoiDungController(BookwebContext context)
        {
            _context = context;
        }

        // ================== LOGIN ==================
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(NguoiDung model)
        {
            if (string.IsNullOrEmpty(model.TenDangNhap) || string.IsNullOrEmpty(model.MatKhau))
            {
                TempData["error"] = "Vui lòng nhập đầy đủ thông tin!";
                return View(model);
            }

            string hashedPassword = ComputeSha256Hash(model.MatKhau);

            var user = await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.TenDangNhap == model.TenDangNhap && u.MatKhau == hashedPassword);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.MaNguoiDung.ToString());
                HttpContext.Session.SetString("Username", user.TenDangNhap);
                HttpContext.Session.SetString("Role", user.Admin == 1 ? "Admin" : "User");

                if (user.Admin == 1)
                    return RedirectToAction("Index", "SanPham", new { area = "Admin" });
                else
                    return RedirectToAction("Index", "Home");
            }

            TempData["error"] = "Sai tên đăng nhập hoặc mật khẩu!";
            return View(model);
        }


        // ================== REGISTER ==================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NguoiDung nguoiDung)
        {
            if (!ModelState.IsValid)
            {
                return View(nguoiDung);
            }

            // Kiểm tra tên đăng nhập đã tồn tại chưa
            bool existUser = await _context.NguoiDungs
                .AnyAsync(u => u.TenDangNhap == nguoiDung.TenDangNhap);

            if (existUser)
            {
                TempData["error"] = "Tên đăng nhập đã tồn tại!";
                return View(nguoiDung);
            }

            // Hash mật khẩu
            nguoiDung.MatKhau = ComputeSha256Hash(nguoiDung.MatKhau);
            nguoiDung.NgayDangKy = DateTime.Now;
            nguoiDung.Admin = 0; // mặc định user

            _context.NguoiDungs.Add(nguoiDung);
            await _context.SaveChangesAsync();

            TempData["success"] = "Tạo tài khoản thành công!";
            return RedirectToAction("Login");
        }


        // ================== FORGOT PASSWORD ==================
        [HttpGet]
        public IActionResult ForgotPass()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string username, string newPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
            {
                TempData["error"] = "Vui lòng nhập đầy đủ thông tin!";
                return RedirectToAction("ForgotPass");
            }

            var user = await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.TenDangNhap == username);

            if (user == null)
            {
                TempData["error"] = "Tên đăng nhập không tồn tại!";
                return RedirectToAction("ForgotPass");
            }

            user.MatKhau = ComputeSha256Hash(newPassword);
            _context.Update(user);
            await _context.SaveChangesAsync();

            TempData["success"] = "Cập nhật mật khẩu thành công!";
            return RedirectToAction("Login");
        }

        // ================== HELPER ==================
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
