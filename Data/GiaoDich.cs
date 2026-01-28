using System;
using System.Collections.Generic;

namespace BookWeb.Data;

public partial class GiaoDich
{
    public long MaGiaoDich { get; set; }

    public long MaNguoiDung { get; set; }

    public long MaDon { get; set; }

    public string MaMa { get; set; } = null!;

    public byte Loai { get; set; }

    public byte HinhThucThanhToan { get; set; }

    public byte TrangThai { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public string? NoiDung { get; set; }

    public virtual DonHang MaDonNavigation { get; set; } = null!;

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
