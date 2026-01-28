using System;
using System.Collections.Generic;

namespace BookWeb.Data;

public partial class DonHang
{
    public long MaDon { get; set; }

    public long? MaNguoiDung { get; set; }

    public short TrangThai { get; set; }

    public double TamTinh { get; set; }

    public double GiamGiaMuc { get; set; }

    public double Thue { get; set; }

    public double VanChuyen { get; set; }

    public double Tong { get; set; }

    public string? MaKhuyenMai { get; set; }

    public double GiamGia { get; set; }

    public double TongCong { get; set; }

    public string? TenNguoiDung { get; set; }

    public string? DienThoai { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public string? ThanhPho { get; set; }

    public string? QuocGia { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public string? NoiDung { get; set; }

    public virtual ICollection<ChiTietDon> ChiTietDons { get; set; } = new List<ChiTietDon>();

    public virtual ICollection<GiaoDich> GiaoDiches { get; set; } = new List<GiaoDich>();

    public virtual NguoiDung? MaNguoiDungNavigation { get; set; }
}
