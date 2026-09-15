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

**Cài mới:** chạy file `sql/script.sql`. Script sẽ tự tạo database `movie` (nếu chưa có), tạo các bảng, index và thêm dữ liệu mẫu.

- **Bằng SSMS:** mở file `sql/script.sql` → bấm **Execute**.
- **Bằng dòng lệnh:**

  ```bash
  sqlcmd -S .\SQLEXPRESS -E -i sql\script.sql
  ```

**Đã có database tạo bằng script bản cũ:** không chạy lại `script.sql` (lệnh `CREATE TABLE` sẽ báo lỗi vì bảng đã tồn tại). Thay vào đó chạy `sql/cap_nhat_csdl.sql` để thêm các cột và index mới:

```bash
sqlcmd -S .\SQLEXPRESS -E -i sql\cap_nhat_csdl.sql
```

Script cập nhật chạy lại nhiều lần cũng không sao, phần nào đã có thì tự bỏ qua. Mật khẩu cũ chưa băm sẽ được ứng dụng tự băm lại khi người dùng đăng nhập thành công.

> File `cinema system/script.sql` là bản sao giống hệt `sql/script.sql`.

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

- Tài khoản khách hàng được tạo ở tab **ĐĂNG KÝ**; ô **Tên** chính là tên đăng nhập.
- Có thể đăng nhập bằng **tên đăng nhập, email hoặc số điện thoại**.
- Tài khoản nhân viên mới được tạo trong màn hình admin → **Quản lý tài khoản**.

> Nên đổi mật khẩu của các tài khoản mặc định sau khi cài đặt.

## Chức năng

### Đăng nhập / Đăng ký
- Đăng nhập bằng tên đăng nhập, email hoặc số điện thoại, có captcha (không phân biệt hoa thường).
- Đăng ký tài khoản khách hàng: không cho trùng tên đăng nhập / email / số điện thoại, kiểm tra ngày sinh, captcha và điều khoản.
- **Tìm lại mật khẩu:** nhập đúng tên đăng nhập + email + số điện thoại đã đăng ký thì được đặt mật khẩu mới.
- Mật khẩu được băm bằng **PBKDF2-SHA256** có salt, không lưu dạng văn bản thường (`cinema system/PasswordHasher.cs`).

### Khách hàng
- **Vé:** xem các phim có suất chiếu theo ngày (10 ngày tính từ hôm nay), chọn giờ chiếu để mở sơ đồ ghế.
- **Sơ đồ ghế:** chọn ghế Thường / VIP / Sweetbox, tự tính tổng tiền, bấm **ĐẶT VÉ** để lưu vé. Ghế đã có người đặt hiện màu xám.
- **Hoàn vé:** xem vé của mình và hoàn vé của các suất chiếu chưa bắt đầu.
- **Thông tin chung / Thay đổi thông tin:** xem, sửa họ tên, email, số điện thoại và đổi mật khẩu.

### Nhân viên
- **Vé:** bán vé tại quầy, dùng chung màn hình Vé và sơ đồ ghế với khách hàng.
- **Hoàn vé:** tìm vé theo tên đăng nhập, họ tên, email, số điện thoại hoặc tên phim; hoàn một hoặc nhiều vé cùng lúc.
- **Phim:** thêm / sửa / xóa phim, giá vé và ảnh poster.
- **Suất chiếu:** thêm / sửa / xóa suất chiếu, bắt buộc chọn phòng.
- **Phòng chiếu:** gán phim cho phòng; nút **Quản lý phòng** để thêm / đổi tên / xóa phòng.

### Admin
- **Quản lý tài khoản:** thêm / sửa / xóa tài khoản nhân viên. Khi sửa, để trống ô mật khẩu nếu không muốn đổi.
- **Thống kê:** chọn khoảng ngày để xem số vé đã bán, doanh thu, số vé đã hoàn, số suất chiếu; xem chi tiết theo phim hoặc theo ngày.
- **Phim** và **Suất chiếu:** dùng chung màn hình với nhân viên.

## Quy tắc nghiệp vụ

- **Giá vé:** ghế Thường = giá vé của phim; ghế VIP = giá phim + 30.000đ; ghế Sweetbox = giá phim + 80.000đ. Phim chưa nhập giá thì tính 70.000đ. Giá của từng vé được lưu lại lúc đặt nên đổi giá phim không ảnh hưởng vé đã bán.
- **Sơ đồ ghế:** mỗi phòng mặc định 10 hàng × 12 ghế (hàng A–C ghế Thường, D–I ghế VIP, J ghế Sweetbox). Ghế được tạo khi thêm phòng, hoặc tự tạo khi mở sơ đồ ghế lần đầu với phòng chưa có ghế.
- **Đặt vé:** chỉ đặt được suất chiếu đã gán phòng và chưa bắt đầu. Vé được lưu trong một transaction có khóa, cùng với unique index, nên 2 người không thể đặt trùng một ghế.
- **Hoàn vé:** chỉ hoàn được trước giờ chiếu. Vé hoàn không bị xóa mà đánh dấu `IsBooked = 0`, nên ghế được mở bán lại và số liệu vẫn còn cho thống kê.
- **Suất chiếu:** một phòng không có 2 suất cùng ngày, cùng giờ bắt đầu. Không thêm / sửa suất vào thời điểm đã qua. Suất đã bán vé thì không sửa được; suất đã có vé (kể cả vé đã hoàn) thì không xóa được.
- **Thống kê:** tính theo thời điểm đặt vé.
- **Poster:** ảnh được thu nhỏ và lưu thẳng trong database (cột `Movies.Poster`), nên máy nào dùng chung database cũng thấy. Phim cũ chỉ có đường dẫn file vẫn hiển thị nếu file còn; bấm **Sửa** phim đó để chuyển ảnh vào database.

## Cấu trúc thư mục

```
system-cinema/
├── cinema system.sln
├── sql/
│   ├── script.sql              # Tạo database mới + dữ liệu mẫu
│   └── cap_nhat_csdl.sql       # Nâng cấp database tạo bằng script bản cũ
└── cinema system/
    ├── App.config              # Chuỗi kết nối SQL Server
    ├── Program.cs              # Điểm khởi động, chuyển đổi giữa các form
    ├── Db.cs                   # Chuỗi kết nối, kiểm tra trùng tài khoản
    ├── Session.cs              # Tài khoản đang đăng nhập
    ├── PasswordHasher.cs       # Băm / kiểm tra mật khẩu
    ├── PosterImage.cs          # Đọc / lưu ảnh poster
    ├── đăng nhập/              # Đăng nhập, đăng ký, tìm lại mật khẩu
    ├── khách hàng/             # Màn hình khách hàng, sơ đồ ghế, thông tin tài khoản
    ├── nhân viên/              # Vé, hoàn vé, phim, suất chiếu, phòng chiếu
    ├── admin/                  # Màn hình admin, quản lý tài khoản, thống kê
    ├── Properties/
    └── Resources/              # Hình ảnh
```

## Cơ sở dữ liệu

| Bảng | Mô tả |
| --- | --- |
| `TaiKhoan` | Tài khoản đăng nhập; `VaiTro` là `admin`, `staff` hoặc `user`; `Pass` lưu mật khẩu đã băm |
| `Movies` | Phim: tên, giá vé (giá ghế thường), ảnh poster |
| `Showtimes` | Suất chiếu: phim, phòng, ngày chiếu, giờ chiếu |
| `Rooms` | Phòng chiếu (script tạo sẵn Phòng 1–3) |
| `RoomMovies` | Phim được gán cho phòng nào |
| `Seats` | Ghế của từng phòng: tên ghế (A1, B5...) và loại ghế |
| `BookedSeats` | Vé: suất chiếu, ghế, người đặt, giá, thời điểm đặt; `IsBooked = 0` là vé đã hoàn |

## Hạn chế hiện tại

- Chưa có thời lượng phim, nên chỉ chặn được 2 suất trùng đúng giờ bắt đầu trong cùng phòng, chưa chặn được các suất chiếu chồng lên nhau.
- Mọi phòng dùng chung sơ đồ ghế 10 × 12. Muốn sơ đồ riêng thì phải sửa trực tiếp bảng `Seats` (trước khi phòng có vé).
- Phụ thu ghế VIP / Sweetbox đang cố định trong code (`khách hàng/phòng chiếu.cs`).
- Tìm lại mật khẩu chỉ xác minh bằng thông tin đã đăng ký, chưa gửi email / OTP. Tài khoản không có email hoặc số điện thoại thì phải nhờ admin đặt lại.
- Hoàn vé trả lại toàn bộ tiền, chưa có phí hoàn.
- Trang chào khách chưa đăng nhập (**Quay lại** ở màn hình đăng nhập) chỉ hiển thị giờ chiếu mẫu; bấm vào sẽ yêu cầu đăng nhập.
- Form `drink` / `Order thức uống` (đặt đồ uống) mới có giao diện, chưa được dùng.

## Lỗi thường gặp

- **"Không kết nối được cơ sở dữ liệu"** hoặc lỗi *network-related or instance-specific error*: kiểm tra dịch vụ SQL Server đã chạy chưa và `Data Source` trong `App.config` đã đúng chưa.
- **"Cannot open database movie"**: chưa chạy `sql/script.sql`, hoặc tài khoản Windows đang dùng không có quyền trên database.
- **"Invalid column name 'Poster'"**, **'IDTaiKhoan'**, **'BookedAt'**...: database được tạo bằng script bản cũ, chạy `sql/cap_nhat_csdl.sql`.
- **Không đăng nhập được bằng mật khẩu mặc định sau khi đã đổi:** dùng **Bạn muốn tìm lại mật khẩu?** (cần email và số điện thoại), hoặc nhờ admin đặt lại trong **Quản lý tài khoản** (chỉ áp dụng cho tài khoản nhân viên).
