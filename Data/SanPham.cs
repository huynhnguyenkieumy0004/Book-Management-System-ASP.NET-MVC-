using BookWeb.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWeb.Data;

public partial class SanPham
{
    [Display(Name = "Mã Sản Phẩm")]
    public long MaSanPham { get; set; }

    [Display(Name = "Mã Danh Mục")]
    public string MaDanhMuc { get; set; } = null!;

    [Display(Name = "Tên Sản Phẩm")]
    public string TenSanPham { get; set; } = null!;

    [Display(Name = "Tiêu đề phụ")]
    public string TieuDePhu { get; set; } = null!;

    [Display(Name = "Mã NXB")]
    public string MaNhaXuatBan { get; set; } = null!;

    [Display(Name = "Hình ảnh")]
    public string? DuongDan { get; set; }

    [Display(Name = "Tóm tắt")]
    public string? TomTat { get; set; }

    [Display(Name = "Giá tiền")]
    public double GiaTien { get; set; }

    [Display(Name = "Giảm giá")]
    public double GiamGia { get; set; }

    [Display(Name = "Số lượng")]
    public int SoLuong { get; set; }

    [Display(Name = "Tình trạng")]
    public string TinhTrang { get; set; } = null!;

    [Display(Name = "Ngày tạo")]
    public DateTime NgayTao { get; set; }

    [Display(Name = "Ngày cập nhật")]
    public DateTime? NgayCapNhat { get; set; }

    [Display(Name = "Ngày công bố")]
    public DateTime? NgayCongBo { get; set; }

    [Display(Name = "Ngày bắt đầu")]
    public DateTime? NgayBatDau { get; set; }

    [Display(Name = "Ngày kết thúc")]
    public DateTime? NgayKetThuc { get; set; }

    [Display(Name = "Nội dung")]
    public string? NoiDung { get; set; }

    public virtual ICollection<ChiTietDon> ChiTietDons { get; set; } = new List<ChiTietDon>();

    public virtual ICollection<ChiTietGio> ChiTietGios { get; set; } = new List<ChiTietGio>();

    public virtual ICollection<DanhGiaSanPham> DanhGiaSanPhams { get; set; } = new List<DanhGiaSanPham>();

    public virtual DanhMucSanPham MaDanhMucNavigation { get; set; } = null!;

    public virtual NhaXuatBan MaNhaXuatBanNavigation { get; set; } = null!;

    


}
