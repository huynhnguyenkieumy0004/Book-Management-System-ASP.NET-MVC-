using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookWeb.Data;

public class DonHangController : Controller
{
    private readonly BookwebContext _context;
    private const string CartSessionKey = "CartId";

    public DonHangController(BookwebContext context)
    {
        _context = context;
    }

    private long? GetCartId()
    {
        var cartIdStr = HttpContext.Session.GetString(CartSessionKey);
        if (long.TryParse(cartIdStr, out long cartId))
            return cartId;
        return null;
    }

    // Hiển thị form thanh toán
    [HttpGet]
    public IActionResult Checkout()
    {
        var cartId = GetCartId();
        if (cartId == null) return RedirectToAction("Index", "GioHang");

        var cartItems = _context.ChiTietGios
            .Where(c => c.MaGio == cartId)
            .Include(c => c.MaSanPhamNavigation)
            .ToList();

        if (!cartItems.Any()) return RedirectToAction("Index", "GioHang");

        return View(cartItems);
    }

    // Xử lý khi user gửi form thanh toán
    [HttpPost]
    public IActionResult Checkout(string tenNguoiDung, string dienThoai, string email, string diaChi, string thanhPho, string quocGia)
    {
        var cartId = GetCartId();
        if (cartId == null) return RedirectToAction("Index", "GioHang");

        var cartItems = _context.ChiTietGios
            .Where(c => c.MaGio == cartId)
            .Include(c => c.MaSanPhamNavigation)
            .ToList();

        if (!cartItems.Any()) return RedirectToAction("Index", "GioHang");

        // Tính tổng tạm tính
        double tamTinh = cartItems.Sum(i => i.Gia * i.SoLuong);
        double giamGiaMuc = 0;
        double thue = tamTinh * 0.1; // 10% VAT
        double vanChuyen = 30000;    // phí vận chuyển tạm tính
        double tong = tamTinh - giamGiaMuc + thue + vanChuyen;
        double giamGia = 0;          // Giảm giá voucher (nếu có)
        double tongCong = tong - giamGia;

        var donHang = new DonHang
        {
            TrangThai = 0,
            TamTinh = tamTinh,
            GiamGiaMuc = giamGiaMuc,
            Thue = thue,
            VanChuyen = vanChuyen,
            Tong = tong,
            GiamGia = giamGia,
            TongCong = tongCong,
            TenNguoiDung = tenNguoiDung,
            DienThoai = dienThoai,
            Email = email,
            DiaChi = diaChi,
            ThanhPho = thanhPho,
            QuocGia = quocGia,
            NgayTao = DateTime.Now,
        };

        _context.DonHangs.Add(donHang);
        _context.SaveChanges();

        // Tạo chi tiết đơn
        foreach (var item in cartItems)
        {
            var chiTietDon = new ChiTietDon
            {
                MaDon = donHang.MaDon,
                MaSanPham = item.MaSanPham,
                Gia = item.Gia,
                GiamGia = item.GiamGia,
                SoLuong = item.SoLuong,
                NgayTao = DateTime.Now,
            };
            _context.ChiTietDons.Add(chiTietDon);
        }

        _context.SaveChanges();

        // Xóa giỏ hàng và chi tiết giỏ
        _context.ChiTietGios.RemoveRange(cartItems);

        var gioHang = _context.GioHangs.FirstOrDefault(g => g.MaGio == cartId);
        if (gioHang != null)
        {
            _context.GioHangs.Remove(gioHang);
        }

        _context.SaveChanges();

        // Xóa session giỏ hàng
        HttpContext.Session.Remove(CartSessionKey);

        return RedirectToAction("Details", new { id = donHang.MaDon });
    }

    // Xem chi tiết đơn hàng
    public IActionResult Details(long id)
    {
        var donHang = _context.DonHangs
            .Include(d => d.ChiTietDons)
            .ThenInclude(ct => ct.MaSanPhamNavigation)
            .FirstOrDefault(d => d.MaDon == id);

        if (donHang == null) return NotFound();

        return View(donHang);
    }
}
