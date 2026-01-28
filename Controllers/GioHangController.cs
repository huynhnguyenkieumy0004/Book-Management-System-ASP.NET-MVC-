using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookWeb.Data;
using System;
using System.Linq;

public class GioHangController : Controller
{
    private readonly BookwebContext _context;
    private const string CartSessionKey = "CartId";

    public GioHangController(BookwebContext context)
    {
        _context = context;
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

    public IActionResult AddToCart(long id, int quantity = 1)
    {
        var cartId = GetOrCreateCartId();

        var product = _context.SanPhams.FirstOrDefault(p => p.MaSanPham == id);
        if (product == null) return NotFound();

        var existingItem = _context.ChiTietGios.FirstOrDefault(c => c.MaGio == cartId && c.MaSanPham == id);
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

        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        var cartIdStr = HttpContext.Session.GetString(CartSessionKey);
        if (!long.TryParse(cartIdStr, out long cartId))
        {
            return View(new List<ChiTietGio>());
        }

        var cartItems = _context.ChiTietGios
            .Where(c => c.MaGio == cartId)
            .Include(c => c.MaSanPhamNavigation)
            .ToList();

        return View(cartItems);
    }

    public IActionResult Increase(long id)
    {
        var cartIdStr = HttpContext.Session.GetString(CartSessionKey);
        if (!long.TryParse(cartIdStr, out long cartId)) return RedirectToAction("Index");

        var item = _context.ChiTietGios.FirstOrDefault(c => c.MaGio == cartId && c.MaSanPham == id);
        if (item != null)
        {
            item.SoLuong++;
            item.NgayCapNhat = DateTime.Now;
            _context.ChiTietGios.Update(item);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Decrease(long id)
    {
        var cartIdStr = HttpContext.Session.GetString(CartSessionKey);
        if (!long.TryParse(cartIdStr, out long cartId)) return RedirectToAction("Index");

        var item = _context.ChiTietGios.FirstOrDefault(c => c.MaGio == cartId && c.MaSanPham == id);
        if (item != null && item.SoLuong > 1)
        {
            item.SoLuong--;
            item.NgayCapNhat = DateTime.Now;
            _context.ChiTietGios.Update(item);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Remove(long id)
    {
        var cartIdStr = HttpContext.Session.GetString(CartSessionKey);
        if (!long.TryParse(cartIdStr, out long cartId)) return RedirectToAction("Index");

        var items = _context.ChiTietGios.Where(c => c.MaGio == cartId && c.MaSanPham == id).ToList();
        if (items.Any())
        {
            _context.ChiTietGios.RemoveRange(items);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Clear()
    {
        var cartIdStr = HttpContext.Session.GetString(CartSessionKey);
        if (long.TryParse(cartIdStr, out long cartId))
        {
            var items = _context.ChiTietGios.Where(c => c.MaGio == cartId).ToList();
            _context.ChiTietGios.RemoveRange(items);

            var cart = _context.GioHangs.FirstOrDefault(g => g.MaGio == cartId);
            if (cart != null)
            {
                _context.GioHangs.Remove(cart);
            }
            _context.SaveChanges();

            HttpContext.Session.Remove(CartSessionKey);
        }

        return RedirectToAction("Index");
    }
}
