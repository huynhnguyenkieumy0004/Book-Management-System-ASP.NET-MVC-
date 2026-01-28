using System;
using System.Collections.Generic;

namespace BookWeb.Data;

public partial class DanhGiaSanPham
{
    public long MaDanhGia { get; set; }

    public long MaSanPham { get; set; }

    public long? MaCha { get; set; }

    public string TieuDe { get; set; } = null!;

    public int DanhGia { get; set; }

    public int CongKhai { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? NgayCongKhai { get; set; }

    public string? NoiDung { get; set; }

    public virtual ICollection<DanhGiaSanPham> InverseMaChaNavigation { get; set; } = new List<DanhGiaSanPham>();

    public virtual DanhGiaSanPham? MaChaNavigation { get; set; }

    public virtual SanPham MaSanPhamNavigation { get; set; } = null!;
}
