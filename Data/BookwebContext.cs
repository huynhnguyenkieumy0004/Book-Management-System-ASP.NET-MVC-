using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Data;

public partial class BookwebContext : DbContext
{
    public BookwebContext()
    {
    }

    public BookwebContext(DbContextOptions<BookwebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDon> ChiTietDons { get; set; }

    public virtual DbSet<ChiTietGio> ChiTietGios { get; set; }

    public virtual DbSet<DanhGiaSanPham> DanhGiaSanPhams { get; set; }

    public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GiaoDich> GiaoDiches { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-E27QIPK\\SQLEXPRESS;Initial Catalog=BOOKWEB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDon>(entity =>
        {
            entity.HasKey(e => e.MaChiTiet).HasName("PK__chi_tiet__CD8DB514AD53FF5D");

            entity.ToTable("chi_tiet_don");

            entity.HasIndex(e => e.MaDon, "idx_ct_don_don");

            entity.HasIndex(e => e.MaSanPham, "idx_ct_don_san_pham");

            entity.Property(e => e.MaChiTiet).HasColumnName("ma_chi_tiet");
            entity.Property(e => e.Gia).HasColumnName("gia");
            entity.Property(e => e.GiamGia).HasColumnName("giam_gia");
            entity.Property(e => e.MaDon).HasColumnName("ma_don");
            entity.Property(e => e.MaSanPham).HasColumnName("ma_san_pham");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("ngay_cap_nhat");
            entity.Property(e => e.NgayTao)
                .HasColumnType("datetime")
                .HasColumnName("ngay_tao");
            entity.Property(e => e.NoiDung)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("text")
                .HasColumnName("noi_dung");
            entity.Property(e => e.SoLuong).HasColumnName("so_luong");

            entity.HasOne(d => d.MaDonNavigation).WithMany(p => p.ChiTietDons)
                .HasForeignKey(d => d.MaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ct_don_don");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietDons)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ct_don_san_pham");
        });

        modelBuilder.Entity<ChiTietGio>(entity =>
        {
            entity.HasKey(e => e.MaChiTiet).HasName("PK__chi_tiet__CD8DB514E7D27483");

            entity.ToTable("chi_tiet_gio");

            entity.HasIndex(e => e.MaGio, "idx_ct_gio_gio");

            entity.HasIndex(e => e.MaSanPham, "idx_ct_gio_san_pham");

            entity.Property(e => e.MaChiTiet).HasColumnName("ma_chi_tiet");
            entity.Property(e => e.Gia).HasColumnName("gia");
            entity.Property(e => e.GiamGia).HasColumnName("giam_gia");
            entity.Property(e => e.MaGio).HasColumnName("ma_gio");
            entity.Property(e => e.MaSanPham).HasColumnName("ma_san_pham");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("ngay_cap_nhat");
            entity.Property(e => e.NgayTao)
                .HasColumnType("datetime")
                .HasColumnName("ngay_tao");
            entity.Property(e => e.NoiDung)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("text")
                .HasColumnName("noi_dung");
            entity.Property(e => e.SoLuong).HasColumnName("so_luong");

            entity.HasOne(d => d.MaGioNavigation).WithMany(p => p.ChiTietGios)
                .HasForeignKey(d => d.MaGio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ct_gio_gio");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietGios)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ct_gio_san_pham");
        });

        modelBuilder.Entity<DanhGiaSanPham>(entity =>
        {
            entity.HasKey(e => e.MaDanhGia).HasName("PK__danh_gia__75DAD6554911AF3A");

            entity.ToTable("danh_gia_san_pham");

            entity.HasIndex(e => e.MaCha, "idx_dg_cha");

            entity.HasIndex(e => e.MaSanPham, "idx_dg_san_pham");

            entity.Property(e => e.MaDanhGia).HasColumnName("ma_danh_gia");
            entity.Property(e => e.CongKhai).HasColumnName("cong_khai");
            entity.Property(e => e.DanhGia).HasColumnName("danh_gia");
            entity.Property(e => e.MaCha)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("ma_cha");
            entity.Property(e => e.MaSanPham).HasColumnName("ma_san_pham");
            entity.Property(e => e.NgayCongKhai)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("ngay_cong_khai");
            entity.Property(e => e.NgayTao)
                .HasColumnType("datetime")
                .HasColumnName("ngay_tao");
            entity.Property(e => e.NoiDung)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("text")
                .HasColumnName("noi_dung");
            entity.Property(e => e.TieuDe)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tieu_de");

            entity.HasOne(d => d.MaChaNavigation).WithMany(p => p.InverseMaChaNavigation)
                .HasForeignKey(d => d.MaCha)
                .HasConstraintName("fk_dg_cha");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.DanhGiaSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dg_san_pham");
        });

        modelBuilder.Entity<DanhMucSanPham>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__danh_muc__70624BDD3218EDFB");

            entity.ToTable("danh_muc_san_pham");

            entity.Property(e => e.MaDanhMuc)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ma_danh_muc");
            entity.Property(e => e.MoTa)
                .HasMaxLength(500)
                .HasColumnName("mo_ta");
            entity.Property(e => e.TenDanhMuc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ten_danh_muc");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDon).HasName("PK__don_hang__057D6CE19A8A6BAB");

            entity.ToTable("don_hang");

            entity.HasIndex(e => e.MaNguoiDung, "idx_don_hang_nguoi");

            entity.Property(e => e.MaDon).HasColumnName("ma_don");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("dia_chi");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("dien_thoai");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.GiamGia).HasColumnName("giam_gia");
            entity.Property(e => e.GiamGiaMuc).HasColumnName("giam_gia_muc");
            entity.Property(e => e.MaKhuyenMai)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("ma_khuyen_mai");
            entity.Property(e => e.MaNguoiDung)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("ma_nguoi_dung");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("ngay_cap_nhat");
            entity.Property(e => e.NgayTao)
                .HasColumnType("datetime")
                .HasColumnName("ngay_tao");
            entity.Property(e => e.NoiDung)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("text")
                .HasColumnName("noi_dung");
            entity.Property(e => e.QuocGia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("quoc_gia");
            entity.Property(e => e.TamTinh).HasColumnName("tam_tinh");
            entity.Property(e => e.TenNguoiDung)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("ten_nguoi_dung");
            entity.Property(e => e.ThanhPho)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("thanh_pho");
            entity.Property(e => e.Thue).HasColumnName("thue");
            entity.Property(e => e.Tong).HasColumnName("tong");
            entity.Property(e => e.TongCong).HasColumnName("tong_cong");
            entity.Property(e => e.TrangThai).HasColumnName("trang_thai");
            entity.Property(e => e.VanChuyen).HasColumnName("van_chuyen");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("fk_don_hang_nguoi");
        });

        modelBuilder.Entity<GiaoDich>(entity =>
        {
            entity.HasKey(e => e.MaGiaoDich).HasName("PK__giao_dic__FB80ED3222F276FF");

            entity.ToTable("giao_dich");

            entity.HasIndex(e => e.MaDon, "idx_gd_don");

            entity.HasIndex(e => e.MaNguoiDung, "idx_gd_nguoi_dung");

            entity.Property(e => e.MaGiaoDich).HasColumnName("ma_giao_dich");
            entity.Property(e => e.HinhThucThanhToan).HasColumnName("hinh_thuc_thanh_toan");
            entity.Property(e => e.Loai).HasColumnName("loai");
            entity.Property(e => e.MaDon).HasColumnName("ma_don");
            entity.Property(e => e.MaMa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ma_ma");
            entity.Property(e => e.MaNguoiDung).HasColumnName("ma_nguoi_dung");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("ngay_cap_nhat");
            entity.Property(e => e.NgayTao)
                .HasColumnType("datetime")
                .HasColumnName("ngay_tao");
            entity.Property(e => e.NoiDung)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("text")
                .HasColumnName("noi_dung");
            entity.Property(e => e.TrangThai).HasColumnName("trang_thai");

            entity.HasOne(d => d.MaDonNavigation).WithMany(p => p.GiaoDiches)
                .HasForeignKey(d => d.MaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_gd_don");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.GiaoDiches)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_gd_nguoi_dung");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGio).HasName("PK__gio_hang__072D17C8BFC6A5C7");

            entity.ToTable("gio_hang");

            entity.HasIndex(e => e.MaNguoiDung, "idx_gio_hang_nguoi");

            entity.Property(e => e.MaGio).HasColumnName("ma_gio");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("dia_chi");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("dien_thoai");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.MaNguoiDung)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("ma_nguoi_dung");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("ngay_cap_nhat");
            entity.Property(e => e.NgayTao)
                .HasColumnType("datetime")
                .HasColumnName("ngay_tao");
            entity.Property(e => e.NoiDung)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("text")
                .HasColumnName("noi_dung");
            entity.Property(e => e.QuocGia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("quoc_gia");
            entity.Property(e => e.TenNguoiDung)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("ten_nguoi_dung");
            entity.Property(e => e.ThanhPho)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("thanh_pho");
            entity.Property(e => e.TrangThai).HasColumnName("trang_thai");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("fk_gio_hang_nguoi");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__nguoi_du__19C32CF791C479DD");

            entity.ToTable("nguoi_dung");

            entity.HasIndex(e => e.Email, "uq_email").IsUnique();

            entity.Property(e => e.MaNguoiDung).HasColumnName("ma_nguoi_dung");
            entity.Property(e => e.Admin).HasColumnName("admin");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("dien_thoai");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.GioiThieu)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("text")
                .HasColumnName("gioi_thieu");
            entity.Property(e => e.HoSo)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("text")
                .HasColumnName("ho_so");
            entity.Property(e => e.LanDangNhapCuoi)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("lan_dang_nhap_cuoi");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mat_khau");
            entity.Property(e => e.NgayDangKy)
                .HasColumnType("datetime")
                .HasColumnName("ngay_dang_ky");
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ten_dang_nhap");
            entity.Property(e => e.TenNguoiDung)
                .HasMaxLength(150)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("ten_nguoi_dung");
        });

        modelBuilder.Entity<NhaXuatBan>(entity =>
        {
            entity.HasKey(e => e.MaNhaXuatBan).HasName("PK__nha_xuat__400B441AA357FD63");

            entity.ToTable("nha_xuat_ban");

            entity.Property(e => e.MaNhaXuatBan)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ma_nha_xuat_ban");
            entity.Property(e => e.TenNhaXuatBan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ten_nha_xuat_ban");
            entity.Property(e => e.ThuocMang)
                .HasMaxLength(255)
                .HasColumnName("thuoc_mang");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham).HasName("PK__san_pham__9D25990CBD326167");

            entity.ToTable("san_pham");

            entity.Property(e => e.MaSanPham).HasColumnName("ma_san_pham");
            entity.Property(e => e.DuongDan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("duong_dan");
            entity.Property(e => e.GiaTien).HasColumnName("gia_tien");
            entity.Property(e => e.GiamGia).HasColumnName("giam_gia");
            entity.Property(e => e.MaDanhMuc)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ma_danh_muc");
            entity.Property(e => e.MaNhaXuatBan)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ma_nha_xuat_ban");
            entity.Property(e => e.NgayBatDau)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("ngay_bat_dau");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("ngay_cap_nhat");
            entity.Property(e => e.NgayCongBo)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("ngay_cong_bo");
            entity.Property(e => e.NgayKetThuc)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("ngay_ket_thuc");
            entity.Property(e => e.NgayTao)
                .HasColumnType("datetime")
                .HasColumnName("ngay_tao");
            entity.Property(e => e.NoiDung)
                .HasColumnType("text")
                .HasColumnName("noi_dung");
            entity.Property(e => e.SoLuong).HasColumnName("so_luong");
            entity.Property(e => e.TenSanPham)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("ten_san_pham");
            entity.Property(e => e.TieuDePhu)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tieu_de_phu");
            entity.Property(e => e.TinhTrang)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tinh_trang");
            entity.Property(e => e.TomTat)
                .HasColumnType("text")
                .HasColumnName("tom_tat");

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDanhMuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_san_pham_danh_muc");

            entity.HasOne(d => d.MaNhaXuatBanNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNhaXuatBan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_san_pham_nxb");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
