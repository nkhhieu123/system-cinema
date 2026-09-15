# Hệ thống quản lý rạp chiếu phim

Ứng dụng desktop quản lý rạp chiếu phim viết bằng **C# WinForms (.NET Framework 4.7.2)**, dữ liệu lưu trên **SQL Server**.
Hệ thống có 3 vai trò: **khách hàng**, **nhân viên** và **admin**.

## Yêu cầu

- Windows
- Visual Studio 2019/2022 có workload **.NET desktop development** (kèm .NET Framework 4.7.2)
- SQL Server (bản Express hoặc LocalDB đều được)
- SQL Server Management Studio (SSMS) — không bắt buộc, dùng để chạy script cho tiện

## Cài đặt và chạy

### 1. Tạo cơ sở dữ liệu

Chạy file `sql/script.sql`. Script sẽ tự tạo database `movie` (nếu chưa có), tạo các bảng và thêm dữ liệu mẫu.

- **Bằng SSMS:** mở file `sql/script.sql` → bấm **Execute**.
- **Bằng dòng lệnh:**

  ```bash
  sqlcmd -S .\SQLEXPRESS -E -i sql\script.sql
  ```

> File `cinema system/script.sql` là bản sao giống hệt `sql/script.sql`.
> Script chỉ nên chạy trên database mới: nếu các bảng đã tồn tại, lệnh `CREATE TABLE` sẽ báo lỗi.

### 2. Cấu hình chuỗi kết nối

Mở `cinema system/App.config` và sửa `Data Source` cho đúng tên SQL Server trên máy:

```xml
<connectionStrings>
    <add name="movie"
         connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=movie;Integrated Security=True;Encrypt=False"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

Một số giá trị `Data Source` thường gặp:

| SQL Server đang dùng | Data Source |
| --- | --- |
| SQL Server Express | `.\SQLEXPRESS` hoặc `TEN-MAY\SQLEXPRESS` |
| LocalDB | `(localdb)\MSSQLLocalDB` |
| SQL Server bản thường (instance mặc định) | `.` hoặc `localhost` |

Toàn bộ ứng dụng đọc chuỗi kết nối này qua lớp `Db` (`cinema system/Db.cs`), nên chỉ cần sửa một chỗ.

### 3. Chạy ứng dụng

Mở `cinema system.sln` bằng Visual Studio → bấm **F5**. Màn hình đầu tiên là **Đăng nhập hệ thống**.

## Tài khoản mặc định

| Vai trò | Tên đăng nhập | Mật khẩu |
| --- | --- | --- |
| Admin | `admin` | `admin123` |
| Nhân viên | `staff` | `staff123` |

Tài khoản khách hàng được tạo ở tab **ĐĂNG KÝ**. Khi đăng nhập, dùng giá trị đã nhập ở ô **Tên** lúc đăng ký làm tên đăng nhập.
Có thể thêm tài khoản nhân viên mới trong màn hình admin → **Quản lý tài khoản**.

> Nên đổi mật khẩu của các tài khoản mặc định sau khi cài đặt.

## Chức năng

### Đăng nhập / Đăng ký
- Đăng nhập có captcha (không phân biệt hoa thường), tự chuyển tới màn hình theo vai trò.
- Đăng ký tài khoản khách hàng: kiểm tra trùng tên đăng nhập, ngày sinh hợp lệ, captcha và điều khoản.

### Khách hàng
- **Vé:** xem các phim có suất chiếu theo ngày (10 ngày tính từ hôm nay), chọn giờ chiếu để mở sơ đồ ghế.
- **Sơ đồ ghế:** chọn ghế Thường / VIP / Sweetbox, tự tính tổng tiền.
- **Thông tin chung:** xem thông tin tài khoản.
- **Thay đổi thông tin:** sửa họ tên, email, số điện thoại và đổi mật khẩu.

### Nhân viên
- **Vé:** giống màn hình Vé của khách hàng.
- **Phim:** thêm / sửa / xóa phim, giá vé và ảnh poster. Chọn một dòng để đổ dữ liệu lên form.
- **Suất chiếu:** thêm / sửa / xóa suất chiếu, không cho trùng phim + ngày + giờ.
- **Phòng chiếu:** gán phim cho phòng, đổi phim, xóa phim khỏi phòng.

### Admin
- **Quản lý tài khoản:** thêm / sửa / xóa tài khoản nhân viên. Khi sửa, để trống ô mật khẩu nếu không muốn đổi.
- **Phim** và **Suất chiếu:** dùng chung màn hình với nhân viên.

## Cấu trúc thư mục

```
system-cinema/
├── cinema system.sln
├── sql/
│   └── script.sql              # Script tạo database + dữ liệu mẫu
└── cinema system/
    ├── App.config              # Chuỗi kết nối SQL Server
    ├── Program.cs              # Điểm khởi động, chuyển đổi giữa các form
    ├── Db.cs                   # Đọc chuỗi kết nối dùng chung
    ├── CarouselControl.cs      # Control trình chiếu ảnh
    ├── đăng nhập/              # Đăng nhập, đăng ký
    ├── khách hàng/             # Màn hình khách hàng, sơ đồ ghế, thông tin tài khoản
    ├── nhân viên/              # Quản lý phim, suất chiếu, phòng chiếu, đặt vé
    ├── admin/                  # Màn hình admin, quản lý tài khoản nhân viên
    ├── Properties/
    └── Resources/              # Hình ảnh
```

## Cơ sở dữ liệu

| Bảng | Mô tả |
| --- | --- |
| `TaiKhoan` | Tài khoản đăng nhập; cột `VaiTro` là `admin`, `staff` hoặc `user` |
| `Movies` | Phim: tên, giá vé, đường dẫn poster |
| `Showtimes` | Suất chiếu: phim, phòng, ngày chiếu, giờ chiếu |
| `Rooms` | Phòng chiếu (script tạo sẵn Phòng 1–3) |
| `RoomMovies` | Phim được gán cho phòng nào |
| `Seats` | Ghế theo phòng và loại ghế |
| `BookedSeats` | Ghế đã đặt theo suất chiếu |

## Chưa hoàn thiện / hạn chế hiện tại

- Sơ đồ ghế chỉ chọn ghế và tính tiền, **chưa lưu vé vào database** (bảng `Seats`, `BookedSeats` chưa được dùng); nút PREVIOUS / NEXT chưa có chức năng.
- Giá ghế đang cố định trong code (Thường 70.000đ, VIP 100.000đ, Sweetbox 150.000đ), chưa lấy theo giá phim.
- Nút **Hoàn vé** (nhân viên, khách hàng) và **Thống kê** (admin) chưa có chức năng.
- Suất chiếu chưa chọn phòng chiếu; chưa có màn hình thêm / sửa phòng.
- Ô đăng nhập ghi "Email hoặc số điện thoại" nhưng thực tế đăng nhập bằng **tên đăng nhập**; link "Bạn muốn tìm lại mật khẩu?" chưa có chức năng.
- Mật khẩu đang lưu dạng văn bản thường, chưa mã hóa.
- Poster phim lưu **đường dẫn tuyệt đối** tới file ảnh, nên sang máy khác sẽ không hiện ảnh.

## Lỗi thường gặp

- **"Không kết nối được cơ sở dữ liệu"** hoặc lỗi *network-related or instance-specific error*: kiểm tra dịch vụ SQL Server đã chạy chưa và `Data Source` trong `App.config` đã đúng chưa.
- **"Cannot open database movie"**: chưa chạy `sql/script.sql`, hoặc tài khoản Windows đang dùng không có quyền trên database.
- **Không hiện ảnh poster**: file ảnh đã bị xóa / di chuyển, hoặc dữ liệu được tạo trên máy khác. Chọn lại ảnh trong màn hình **Phim** rồi bấm **Sửa**.
