using System;
using System.Collections.Generic;

namespace BookWeb.Data;

public partial class NguoiDung
{
    public long MaNguoiDung { get; set; }

    public string? TenNguoiDung { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? DienThoai { get; set; }

    public string? Email { get; set; }

    public int Admin { get; set; }

    public DateTime NgayDangKy { get; set; } = DateTime.Now;

    public DateTime? LanDangNhapCuoi { get; set; }

    public string? GioiThieu { get; set; }

    public string? HoSo { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual ICollection<GiaoDich> GiaoDiches { get; set; } = new List<GiaoDich>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();


}
