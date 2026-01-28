using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookWeb.Data;

public partial class DanhMucSanPham
{
    [Display(Name = "Mã danh mục")]
    public string MaDanhMuc { get; set; } = null!;

    [Display(Name = "Tên danh mục")]
    public string TenDanhMuc { get; set; } = null!;

    [Display(Name = "Mô tả")]
    public string? MoTa { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
