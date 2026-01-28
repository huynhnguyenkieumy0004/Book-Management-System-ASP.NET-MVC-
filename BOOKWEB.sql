-- XÓA DATABASE CŨ (nếu có) và tạo lại
DROP DATABASE IF EXISTS [BOOKWEB];
GO

CREATE DATABASE BOOKWEB;
GO

USE BOOKWEB;
GO

CREATE TABLE nguoi_dung (
    ma_nguoi_dung BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    ten_nguoi_dung NVARCHAR(150) NULL DEFAULT NULL,
    ten_dang_nhap VARCHAR(50) NOT NULL,
    mat_khau VARCHAR(255) NOT NULL,
    dien_thoai VARCHAR(15) NULL,
    email VARCHAR(50) NULL,
    admin INT NOT NULL DEFAULT 0, -- mặc định khi đăng ký là user 
    ngay_dang_ky DATETIME NOT NULL,
    lan_dang_nhap_cuoi DATETIME NULL DEFAULT NULL,
    gioi_thieu TEXT NULL DEFAULT NULL,
    ho_so TEXT NULL DEFAULT NULL,
    CONSTRAINT uq_email UNIQUE (email)
);

INSERT INTO nguoi_dung (ten_nguoi_dung, ten_dang_nhap, mat_khau, dien_thoai, email, admin, ngay_dang_ky)
VALUES 
(N'Nguyễn Thị Trúc Mai', 'admin', 'ad', '0123456789', 'nq2019.nguyenthitrucmai180504@gmail.com', 1, GETDATE()),
(N'Huỳnh Nguyễn Kiều My', 'user', 'us', '0123456788', 'huynhnguyenkieumy123@gmail.com', 0, GETDATE());


CREATE TABLE danh_muc_san_pham ( 
   ma_danh_muc VARCHAR(30) NOT NULL PRIMARY KEY,
   ten_danh_muc NVARCHAR(100) NOT NULL, -- Tên của danh mục
   mo_ta NVARCHAR(500) NULL, 
);

INSERT INTO danh_muc_san_pham (ma_danh_muc, ten_danh_muc, mo_ta)
VALUES 
('sach_van_hoc', N'Sách văn học', N'Gồm các tác phẩm văn học cổ điển và hiện đại, phản ánh đời sống, cảm xúc và văn hóa qua lăng kính nghệ thuật ngôn từ.'),
('sach_thieu_nhi', N'Sách thiếu nhi', N'Dành cho trẻ em, với nội dung giáo dục, giải trí nhẹ nhàng, giúp phát triển tư duy, đạo đức và trí tưởng tượng.'),
('sach_giao_khoa', N'Sách giáo khoa', N'Tài liệu chính thức dùng trong giảng dạy tại trường học, theo chương trình của Bộ Giáo dục, bao gồm nhiều môn học.'),
('sach_chuyen_nganh_khoa_hoc', N'Sách chuyên ngành, khoa học', N'Tập trung vào kiến thức chuyên sâu thuộc các lĩnh vực như y học, công nghệ, kỹ thuật, xã hội học…'),
('sach_ky_nang_self_help', N'Sách kỹ năng, self-help', N'Hướng dẫn phát triển bản thân, kỹ năng sống, quản lý thời gian, tư duy tích cực, thường có tính ứng dụng cao.'),
('tieu_thuyet', N'Tiểu thuyết', N'Tác phẩm hư cấu có cốt truyện dài, khai thác tâm lý, hành động hoặc xã hội, thuộc nhiều thể loại như lãng mạn, trinh thám, viễn tưởng…'),
('truyen_tranh', N'Truyện tranh', N'Kết hợp hình ảnh và lời thoại, thường hướng đến cả trẻ em lẫn người lớn, nội dung phong phú từ hài hước đến phiêu lưu, hành động.');


CREATE TABLE nha_xuat_ban ( 
   ma_nha_xuat_ban VARCHAR(30) NOT NULL PRIMARY KEY,
   ten_nha_xuat_ban NVARCHAR(100) NOT NULL, -- Tên của NXB
   thuoc_mang NVARCHAR(255),
);

INSERT INTO nha_xuat_ban (ma_nha_xuat_ban, ten_nha_xuat_ban, thuoc_mang) VALUES
('nxb_giao_duc', N'NXB Giáo Dục', N'Sách tham khảo, Sách thiếu nhi, Sách kỹ năng, Sách chuyên ngành'),
('nxb_kim_dong', N'NXB Kim Đồng', N'Sách thiếu nhi, Truyện tranh, Sách kỹ năng, Tiểu thuyết'),
('nxb_tre', N'NXB Trẻ', N'Sách kỹ năng, Tiểu thuyết, Sách tham khảo, Truyện tranh, Sách thiếu nhi'),
('nxb_van_hoc', N'NXB Văn Học', N'Sách văn học, Tiểu thuyết, Sách tham khảo'),
('nxb_tong_hop', N'NXB Tổng hợp TP.HCM', N'Sách kỹ năng, Sách văn học, Sách tham khảo, Sách chuyên ngành'),
('nxb_khkt', N'NXB Khoa học & Kỹ thuật', N'Sách chuyên ngành, Sách tham khảo'),
('nxb_the_gioi', N'NXB Thế Giới', N'Sách kỹ năng, Sách thiếu nhi, Truyện tranh, Sách tham khảo, Tiểu thuyết'),
('nxb_lao_dong', N'NXB Lao Động', N'Sách tiểu thuyết, Sách kỹ năng, Sách thiếu nhi, Sách chuyên ngành');

CREATE TABLE san_pham (
    ma_san_pham BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    ma_danh_muc VARCHAR(30) NOT NULL, -- Khóa ngoại 
    ten_san_pham NVARCHAR(75) NOT NULL,
    tieu_de_phu NVARCHAR(100) NOT NULL, -- có thể là mô tả phụ hoặc slogan
    ma_nha_xuat_ban VARCHAR(30) NOT NULL,
    duong_dan VARCHAR(100) NULL,
    tom_tat NVARCHAR(50) NULL, -- Mô tả ngắn gọn về sản phẩm (hiển thị ngoài danh sách)
    gia_tien FLOAT NOT NULL DEFAULT 0,
    giam_gia FLOAT NOT NULL DEFAULT 0,
    so_luong INT NOT NULL DEFAULT 0,
    tinh_trang VARCHAR(100) NOT NULL, -- sold out hoặc là in stock

    ngay_tao DATETIME NOT NULL,
    ngay_cap_nhat DATETIME NULL DEFAULT NULL,
    ngay_cong_bo DATETIME NULL DEFAULT NULL,
    ngay_bat_dau DATETIME NULL DEFAULT NULL,
    ngay_ket_thuc DATETIME NULL DEFAULT NULL,

    noi_dung NVARCHAR(225) NULL, -- Mô tả chi tiết đầy đủ của sản phẩm (giống phần mô tả dài trong Shopee/Lazada).

      CONSTRAINT fk_san_pham_danh_muc FOREIGN KEY (ma_danh_muc)
        REFERENCES danh_muc_san_pham(ma_danh_muc)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,

    CONSTRAINT fk_san_pham_nxb FOREIGN KEY (ma_nha_xuat_ban)
        REFERENCES nha_xuat_ban(ma_nha_xuat_ban)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION

);

INSERT INTO san_pham (ma_danh_muc, ten_san_pham, tieu_de_phu, ma_nha_xuat_ban, duong_dan, tom_tat, gia_tien, giam_gia, so_luong, tinh_trang, 
    ngay_tao, ngay_cap_nhat, ngay_cong_bo, ngay_bat_dau, ngay_ket_thuc, noi_dung)
VALUES 
-- Sách văn học
('sach_van_hoc', N'Truyện Kiều', N'Tác phẩm kinh điển của Nguyễn Du', 'nxb_van_hoc', 'product1.png',
 N'Tác phẩm văn học nổi tiếng của Việt Nam.', 65000, 5000, 30, 'in stock',
 GETDATE(), NULL, GETDATE(), GETDATE(), DATEADD(DAY, 90, GETDATE()),
 N'Một kiệt tác văn học với lối thơ lục bát và nội dung sâu sắc.'),

-- Sách thiếu nhi
('sach_thieu_nhi', N'Những người bạn trong rừng xanh', N'Truyện kể dành cho bé từ 3 tuổi', 'nxb_kim_dong', 'product2.png',
 N'Truyện ngắn dễ thương giúp trẻ yêu thiên nhiên.', 48000, 3000, 50, 'in stock',
 GETDATE(), NULL, GETDATE(), GETDATE(), DATEADD(DAY, 60, GETDATE()),
 N'Cuốn sách minh họa đẹp mắt, dạy trẻ em về tình bạn và thiên nhiên.'),

-- Sách tham khảo
('sach_giao_khoa', N'Bách khoa toàn thư trẻ em', N'Tri thức nền tảng cho lứa tuổi học sinh', 'nxb_giao_duc', 'product3.png',
 N'Tổng hợp kiến thức cơ bản đa lĩnh vực.', 145000, 10000, 25, 'in stock',
 GETDATE(), NULL, GETDATE(), GETDATE(), DATEADD(DAY, 120, GETDATE()),
 N'Một tài liệu tham khảo tổng hợp kiến thức khoa học, tự nhiên và xã hội.'),

-- Sách chuyên ngành
('sach_chuyen_nganh_khoa_hoc', N'Giáo trình Cơ sở dữ liệu', N'Sách chuyên ngành CNTT', 'nxb_khkt', 'product4.png',
 N'Tài liệu học tập môn cơ sở dữ liệu.', 115000, 15000, 20, 'in stock',
 GETDATE(), NULL, GETDATE(), GETDATE(), DATEADD(DAY, 100, GETDATE()),
 N'Phân tích, thiết kế và truy vấn cơ sở dữ liệu quan hệ.'),

-- Sách kỹ năng
('sach_ky_nang_self_help', N'Tư duy nhanh và chậm', N'Sách kỹ năng tư duy kinh điển', 'nxb_tre', 'product5.png',
 N'Tác phẩm nổi tiếng của Daniel Kahneman.', 138000, 8000, 40, 'in stock',
 GETDATE(), NULL, GETDATE(), GETDATE(), DATEADD(DAY, 90, GETDATE()),
 N'Nghiên cứu sâu sắc về cách con người đưa ra quyết định trong cuộc sống.'),

--  Tiểu thuyết
('tieu_thuyet', N'Mắt biếc', N'Tác phẩm nổi tiếng của Nguyễn Nhật Ánh', 'nxb_tre', 'product6.png',
 N'Tiểu thuyết học đường, tình cảm nhẹ nhàng.', 79000, 5000, 60, 'in stock',
 GETDATE(), NULL, GETDATE(), GETDATE(), DATEADD(DAY, 75, GETDATE()),
 N'Câu chuyện tuổi thơ và tình yêu không trọn vẹn giữa Ngạn và Hà Lan.'),

-- Truyện tranh
('truyen_tranh', N'Thám tử lừng danh Conan - Tập 1', N'Vụ án đầu tiên', 'nxb_kim_dong', 'product7.png',
 N'Truyện trinh thám hấp dẫn.', 28000, 0, 100, 'in stock',
 GETDATE(), NULL, GETDATE(), GETDATE(), DATEADD(DAY, 180, GETDATE()),
 N'Những vụ án gay cấn, phá án tài tình của Conan và Ran.');


CREATE TABLE don_hang (
    ma_don BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    ma_nguoi_dung BIGINT NULL DEFAULT NULL,
    trang_thai SMALLINT NOT NULL DEFAULT 0, -- 0 = mới tạo, 1 = đã thanh toán, 2 = đã giao, 3 = đơn hàng thành công, 4 = đơn hàng đã bị hủy, 5 = hoàn hàng
    
    tam_tinh FLOAT NOT NULL DEFAULT 0, -- Tổng tiền hàng trước giảm giá, thuế, vận chuyển
    giam_gia_muc FLOAT NOT NULL DEFAULT 0, -- Tổng giảm giá từ sản phẩm
    thue FLOAT NOT NULL DEFAULT 0, -- Thuế VAT nếu có
    van_chuyen FLOAT NOT NULL DEFAULT 0, -- Chi phí vận chuyển
    tong FLOAT NOT NULL DEFAULT 0, -- = subTotal - itemDiscount + tax + shipping
    ma_khuyen_mai VARCHAR(50) NULL DEFAULT NULL, -- Ví dụ: "SUMMER2025"
    giam_gia FLOAT NOT NULL DEFAULT 0,
    tong_cong FLOAT NOT NULL DEFAULT 0,

    ten_nguoi_dung VARCHAR(150) NULL DEFAULT NULL,
    dien_thoai VARCHAR(15) NULL,
    email VARCHAR(50) NULL,

    dia_chi NVARCHAR(50) NULL DEFAULT NULL,
    thanh_pho NVARCHAR(50) NULL DEFAULT NULL,
    quoc_gia NVARCHAR(50) NULL DEFAULT NULL,

    ngay_tao DATETIME NOT NULL,
    ngay_cap_nhat DATETIME NULL DEFAULT NULL,
    noi_dung TEXT NULL DEFAULT NULL,

    INDEX idx_don_hang_nguoi (ma_nguoi_dung ASC),
    CONSTRAINT fk_don_hang_nguoi
        FOREIGN KEY (ma_nguoi_dung)
        REFERENCES nguoi_dung(ma_nguoi_dung)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE TABLE chi_tiet_don (
    ma_chi_tiet BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    ma_san_pham BIGINT NOT NULL,
    ma_don BIGINT NOT NULL,
    
    gia FLOAT NOT NULL DEFAULT 0,
    giam_gia FLOAT NOT NULL DEFAULT 0,
    so_luong SMALLINT NOT NULL DEFAULT 0,
    ngay_tao DATETIME NOT NULL,
    ngay_cap_nhat DATETIME NULL DEFAULT NULL,
    noi_dung TEXT NULL DEFAULT NULL,

    INDEX idx_ct_don_san_pham (ma_san_pham ASC),
    INDEX idx_ct_don_don (ma_don ASC),

    CONSTRAINT fk_ct_don_san_pham
        FOREIGN KEY (ma_san_pham)
        REFERENCES san_pham(ma_san_pham)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,

    CONSTRAINT fk_ct_don_don
        FOREIGN KEY (ma_don)
        REFERENCES don_hang(ma_don)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE TABLE gio_hang (
    ma_gio BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    ma_nguoi_dung BIGINT NULL DEFAULT NULL,
    trang_thai SMALLINT NOT NULL DEFAULT 0,

    ten_nguoi_dung VARCHAR(150) NULL DEFAULT NULL,
    dien_thoai VARCHAR(15) NULL,
    email VARCHAR(50) NULL,

    dia_chi NVARCHAR(50) NULL DEFAULT NULL,
    thanh_pho NVARCHAR(50) NULL DEFAULT NULL,
    quoc_gia NVARCHAR(50) NULL DEFAULT NULL,

    ngay_tao DATETIME NOT NULL,
    ngay_cap_nhat DATETIME NULL DEFAULT NULL,
    noi_dung TEXT NULL DEFAULT NULL,

    INDEX idx_gio_hang_nguoi (ma_nguoi_dung ASC),
    CONSTRAINT fk_gio_hang_nguoi
        FOREIGN KEY (ma_nguoi_dung)
        REFERENCES nguoi_dung(ma_nguoi_dung)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE TABLE chi_tiet_gio (
    ma_chi_tiet BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    ma_san_pham BIGINT NOT NULL,
    ma_gio BIGINT NOT NULL,
    gia FLOAT NOT NULL DEFAULT 0,
    giam_gia FLOAT NOT NULL DEFAULT 0,
    so_luong SMALLINT NOT NULL DEFAULT 0,

    ngay_tao DATETIME NOT NULL,
    ngay_cap_nhat DATETIME NULL DEFAULT NULL,
    noi_dung TEXT NULL DEFAULT NULL,

    INDEX idx_ct_gio_san_pham (ma_san_pham ASC),
    INDEX idx_ct_gio_gio (ma_gio ASC),

    CONSTRAINT fk_ct_gio_san_pham
        FOREIGN KEY (ma_san_pham)
        REFERENCES san_pham(ma_san_pham)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,

    CONSTRAINT fk_ct_gio_gio
        FOREIGN KEY (ma_gio)
        REFERENCES gio_hang(ma_gio)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE TABLE danh_gia_san_pham (
    ma_danh_gia BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    ma_san_pham BIGINT NOT NULL,
    ma_cha BIGINT NULL DEFAULT NULL, -- để reply đánh giá
    tieu_de NVARCHAR(100) NOT NULL,
    danh_gia INT NOT NULL DEFAULT 0, -- rating từ 1 đến 5
    cong_khai INT NOT NULL DEFAULT 0, -- đã duyệt hiển thị hay chưa

    ngay_tao DATETIME NOT NULL,
    ngay_cong_khai DATETIME NULL DEFAULT NULL,
    noi_dung TEXT NULL DEFAULT NULL,

    INDEX idx_dg_san_pham (ma_san_pham ASC),
    INDEX idx_dg_cha (ma_cha ASC),

    CONSTRAINT fk_dg_san_pham
        FOREIGN KEY (ma_san_pham)
        REFERENCES san_pham(ma_san_pham)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,

    CONSTRAINT fk_dg_cha
        FOREIGN KEY (ma_cha)
        REFERENCES danh_gia_san_pham(ma_danh_gia)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE TABLE giao_dich (
    ma_giao_dich BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    ma_nguoi_dung BIGINT NOT NULL,
    ma_don BIGINT NOT NULL,
    ma_ma VARCHAR(100) NOT NULL, -- Mã giao dịch, ví dụ mã từ ngân hàng hoặc hệ thống thanh toán
    loai TINYINT NOT NULL DEFAULT 0, -- loại giao dịch 1-mua hàng 2-hoàn tiền 3-thanh toán hóa đơn
    hinh_thuc_thanh_toan TINYINT NOT NULL DEFAULT 0, -- hình thức thanh toán (mode) 1 = COD, 2 = momo
    trang_thai TINYINT NOT NULL DEFAULT 0, -- trạng thái: 0-chờ, 1-thành công, 2-đã hủy giao dịch
    
    ngay_tao DATETIME NOT NULL,
    ngay_cap_nhat DATETIME NULL DEFAULT NULL,
    noi_dung TEXT NULL DEFAULT NULL,

    INDEX idx_gd_nguoi_dung (ma_nguoi_dung ASC),
    INDEX idx_gd_don (ma_don ASC),

    CONSTRAINT fk_gd_nguoi_dung
        FOREIGN KEY (ma_nguoi_dung)
        REFERENCES nguoi_dung(ma_nguoi_dung)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,

    CONSTRAINT fk_gd_don
        FOREIGN KEY (ma_don)
        REFERENCES don_hang(ma_don)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

