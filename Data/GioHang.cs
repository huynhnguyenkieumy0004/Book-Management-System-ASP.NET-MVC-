using System;
using System.Collections.Generic;

namespace BookWeb.Data;

public partial class GioHang
{
    public long MaGio { get; set; }

    public long? MaNguoiDung { get; set; }

    public short TrangThai { get; set; }

    public string? TenNguoiDung { get; set; }

    public string? DienThoai { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public string? ThanhPho { get; set; }

    public string? QuocGia { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public string? NoiDung { get; set; }

    public virtual ICollection<ChiTietGio> ChiTietGios { get; set; } = new List<ChiTietGio>();

    public virtual NguoiDung? MaNguoiDungNavigation { get; set; }
}
