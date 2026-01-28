using BookWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace BookWeb.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly BookwebContext _context;
        private const string CartSessionKey = "CartId";

        public SanPhamController(BookwebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sanPhams = _context.SanPhams
        .Include(s => s.MaDanhMucNavigation)
        .Include(s => s.MaNhaXuatBanNavigation)
        .ToList();

            return View(sanPhams);
        }

        private long GetOrCreateCartId()
        {
            var cartIdStr = HttpContext.Session.GetString(CartSessionKey);
            if (long.TryParse(cartIdStr, out long cartId))
            {
                var cart = _context.GioHangs.FirstOrDefault(g => g.MaGio == cartId && g.TrangThai == 0);
                if (cart != null) return cartId;
            }

            var newCart = new GioHang
            {
                MaNguoiDung = null, // Nếu có user đăng nhập, gán ID user ở đây
                TrangThai = 0,
                NgayTao = DateTime.Now
            };

            _context.GioHangs.Add(newCart);
            _context.SaveChanges();

            HttpContext.Session.SetString(CartSessionKey, newCart.MaGio.ToString());

            return newCart.MaGio;
        }

        [HttpPost]
        public IActionResult AddToCart(long id, int quantity = 1)
        {
            var cartId = GetOrCreateCartId();

            var product = _context.SanPhams.FirstOrDefault(p => p.MaSanPham == id);
            if (product == null) return NotFound();

            var existingItem = _context.ChiTietGios
                .FirstOrDefault(c => c.MaGio == cartId && c.MaSanPham == id);

            if (existingItem != null)
            {
                existingItem.SoLuong = (short)(existingItem.SoLuong + quantity);
                existingItem.NgayCapNhat = DateTime.Now;
                _context.ChiTietGios.Update(existingItem);
            }
            else
            {
                var newItem = new ChiTietGio
                {
                    MaGio = cartId,
                    MaSanPham = id,
                    Gia = product.GiaTien,
                    GiamGia = 0,
                    SoLuong = (short)quantity,
                    NgayTao = DateTime.Now
                };
                _context.ChiTietGios.Add(newItem);
            }

            _context.SaveChanges();

            return Ok(new { message = "Đã thêm sản phẩm vào giỏ hàng" });
        }


        public async Task<IActionResult> Details(long? MaSanPham)
        {
            if (MaSanPham == null)
            {
                return RedirectToAction("Index");
            }
            var sanPham = await _context.SanPhams
                .FirstOrDefaultAsync(m => m.MaSanPham == MaSanPham);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {
            // Nếu không nhập gì thì trả tất cả
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                var allProducts = await _context.SanPhams
                    .Include(s => s.MaDanhMucNavigation)
                    .Include(s => s.MaNhaXuatBanNavigation)
                    .ToListAsync();

                ViewBag.Keyword = "Tất cả sản phẩm";
                return View("Index", allProducts);
            }

            // Chuẩn hóa từ khóa tìm kiếm
            string keyword = RemoveDiacritics(searchTerm.Trim().ToLower());

            // Lấy toàn bộ sản phẩm và tìm kiếm sau khi bỏ dấu
            var products = await _context.SanPhams
                .Include(s => s.MaDanhMucNavigation)
                .Include(s => s.MaNhaXuatBanNavigation)
                .ToListAsync();

            var filteredProducts = products
                .Where(p =>
                    RemoveDiacritics(p.TenSanPham.ToLower()).Contains(keyword) ||
                    RemoveDiacritics(p.TieuDePhu.ToLower()).Contains(keyword) ||
                    (!string.IsNullOrEmpty(p.TomTat) && RemoveDiacritics(p.TomTat.ToLower()).Contains(keyword))
                )
                .ToList();

            ViewBag.Keyword = $"Kết quả cho '{searchTerm}'";
            return View("Index", filteredProducts);
        }

        // Hàm bỏ dấu tiếng Việt
        private string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }



    }
}
