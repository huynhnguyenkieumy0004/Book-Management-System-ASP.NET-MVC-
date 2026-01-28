using System;
using System.Collections.Generic;

namespace BookWeb.Data;

public partial class ChiTietGio
{
    public long MaChiTiet { get; set; }

    public long MaSanPham { get; set; }

    public long MaGio { get; set; }

    public double Gia { get; set; }

    public double GiamGia { get; set; }

    public short SoLuong { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public string? NoiDung { get; set; }

    public virtual GioHang MaGioNavigation { get; set; } = null!;

    public virtual SanPham MaSanPhamNavigation { get; set; } = null!;
}
