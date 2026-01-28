using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookWeb.Data;

public partial class NhaXuatBan
{
    [Display(Name = "Mã NXB")]
    public string MaNhaXuatBan { get; set; } = null!;

    [Display(Name = "Tên NXB")]
    public string TenNhaXuatBan { get; set; } = null!;

    [Display(Name = "Thuộc mảng")]
    public string? ThuocMang { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
