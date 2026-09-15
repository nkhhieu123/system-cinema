# Hệ thống quản lý rạp chiếu phim

Ứng dụng desktop quản lý rạp chiếu phim viết bằng **C# WinForms (.NET Framework 4.7.2)**, dữ liệu lưu trên **SQL Server**.
Hệ thống có 3 vai trò: **khách hàng**, **nhân viên** và **admin**; khách chưa đăng nhập vẫn xem được lịch chiếu.

## Yêu cầu

- Windows
- Visual Studio 2019/2022 có workload **.NET desktop development** (kèm .NET Framework 4.7.2)
- SQL Server (bản Express hoặc LocalDB đều được)
- SQL Server Management Studio (SSMS) — không bắt buộc, dùng để chạy script cho tiện
- Tài khoản email có SMTP (vd: Gmail) — không bắt buộc, chỉ cần cho chức năng quên mật khẩu

## Cài đặt và chạy

### 1. Tạo cơ sở dữ liệu

**Cài mới:** chạy file `sql/script.sql`. Script sẽ tự tạo database `movie` (nếu chưa có), tạo các bảng, index và thêm dữ liệu mẫu (tài khoản, phòng, bắp nước, cài đặt).

- **Bằng SSMS:** mở file `sql/script.sql` → bấm **Execute**.
- **Bằng dòng lệnh:**

  ```bash
  sqlcmd -S .\SQLEXPRESS -E -i sql\script.sql
  ```

**Đã có database tạo bằng script bản cũ:** không chạy lại `script.sql` (lệnh `CREATE TABLE` sẽ báo lỗi vì bảng đã tồn tại). Thay vào đó chạy `sql/cap_nhat_csdl.sql` để thêm các cột, bảng và index mới:

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

### 3. Cấu hình gửi email (cho chức năng quên mật khẩu)

Cũng trong `App.config`, điền phần `appSettings`. Để trống thì ứng dụng vẫn chạy, chỉ là không gửi được mã xác nhận.

```xml
<appSettings>
    <add key="SmtpHost" value="smtp.gmail.com" />
    <add key="SmtpPort" value="587" />
    <add key="SmtpEnableSsl" value="true" />
    <add key="SmtpUser" value="ten.rap@gmail.com" />
    <add key="SmtpPassword" value="mat-khau-ung-dung-16-ky-tu" />
    <add key="SmtpFrom" value="" />
    <add key="SmtpDisplayName" value="Rạp chiếu phim" />
</appSettings>
```

Với Gmail: bật **xác minh 2 bước** cho tài khoản, rồi tạo **Mật khẩu ứng dụng** (App Password) và dùng mật khẩu đó cho `SmtpPassword`, không dùng mật khẩu Gmail thường.

> Không commit mật khẩu email thật lên Git.

### 4. Chạy ứng dụng

Mở `cinema system.sln` bằng Visual Studio → bấm **F5**. Màn hình đầu tiên là **Đăng nhập hệ thống**; nút **Quay lại** mở trang lịch chiếu cho khách chưa đăng nhập.

## Tài khoản mặc định

| Vai trò | Tên đăng nhập | Mật khẩu |
| --- | --- | --- |
| Admin | `admin` | `admin123` |
| Nhân viên | `staff` | `staff123` |

- Tài khoản khách hàng được tạo ở tab **ĐĂNG KÝ**; ô **Tên** chính là tên đăng nhập.
- Có thể đăng nhập bằng **tên đăng nhập, email hoặc số điện thoại**.
- Admin tạo / sửa / đặt lại mật khẩu cho mọi loại tài khoản trong **Quản lý tài khoản**.

> Nên đổi mật khẩu của các tài khoản mặc định sau khi cài đặt, và bổ sung email để dùng được chức năng quên mật khẩu.

## Chức năng

### Khách chưa đăng nhập
- Xem lịch chiếu thật theo ngày, thời lượng phim, giờ bắt đầu – kết thúc.
- Chọn giờ chiếu để xem sơ đồ ghế trống; bấm **ĐĂNG NHẬP ĐỂ ĐẶT VÉ** để chuyển sang đăng nhập.

### Đăng nhập / Đăng ký
- Đăng nhập bằng tên đăng nhập, email hoặc số điện thoại, có captcha (không phân biệt hoa thường).
- Đăng ký tài khoản khách hàng: không cho trùng tên đăng nhập / email / số điện thoại, kiểm tra ngày sinh, captcha và điều khoản.
- **Tìm lại mật khẩu:** nhập tên đăng nhập / email / số điện thoại → nhận **mã xác nhận 6 số qua email** → nhập mã và mật khẩu mới.
- Mật khẩu được băm bằng **PBKDF2-SHA256** có salt, không lưu dạng văn bản thường (`cinema system/PasswordHasher.cs`).

### Khách hàng
- **Vé:** xem các phim có suất chiếu theo ngày (10 ngày tính từ hôm nay), chọn giờ chiếu để mở sơ đồ ghế.
- **Sơ đồ ghế:** chọn ghế Thường / VIP / Sweetbox, tự tính tổng tiền, bấm **ĐẶT VÉ** để lưu vé. Ghế đã có người đặt hiện màu xám.
- **Bắp nước:** đặt vé xong có thể đặt thêm bắp nước cho suất chiếu đó.
- **Hoàn vé:** xem vé của mình và hoàn vé (có phí hoàn, trước giờ chiếu một khoảng thời gian).
- **Thông tin chung / Thay đổi thông tin:** xem, sửa họ tên, email, số điện thoại và đổi mật khẩu.

### Nhân viên
- **Vé:** bán vé tại quầy, dùng chung màn hình Vé và sơ đồ ghế với khách hàng.
- **Hoàn vé:** tìm vé theo tên đăng nhập, họ tên, email, số điện thoại hoặc tên phim; hoàn một hoặc nhiều vé cùng lúc.
- **Phim:** thêm / sửa / xóa phim, giá vé, **thời lượng** và ảnh poster.
- **Suất chiếu:** thêm / sửa / xóa suất chiếu, bắt buộc chọn phòng; bảng hiện giờ kết thúc và số vé đã bán.
- **Phòng chiếu:** gán phim cho phòng; **Quản lý phòng** để thêm (chọn số hàng × số ghế) / đổi tên / xóa phòng; **Sơ đồ ghế** để chỉnh loại từng ghế.
- **Bắp nước:** thêm / sửa / xóa món, giá, ảnh, ngừng bán; **Bán tại quầy** để lập đơn bắp nước không kèm vé.

### Admin
- **Quản lý tài khoản:** chọn loại tài khoản (Nhân viên / Khách hàng / Admin) rồi thêm / sửa / xóa; khi sửa, nhập mật khẩu mới để đặt lại, để trống nếu không đổi. Không xóa được tài khoản đang đăng nhập và admin cuối cùng.
- **Thống kê:** chọn khoảng ngày để xem vé đã bán, doanh thu vé, vé đã hoàn và phí hoàn thu được, doanh thu bắp nước, tổng doanh thu, số suất chiếu; chi tiết theo phim, theo ngày hoặc theo món bắp nước.
- **Cài đặt:** phụ thu ghế VIP / Sweetbox, giá vé mặc định, thời lượng mặc định, thời gian dọn phòng, phí hoàn vé và hạn hoàn vé.
- **Phim** và **Suất chiếu:** dùng chung màn hình với nhân viên.

## Quy tắc nghiệp vụ

Các con số dưới đây là giá trị mặc định, admin đổi được trong **Cài đặt** (lưu ở bảng `CaiDat`).

- **Giá vé:** ghế Thường = giá vé của phim; ghế VIP = giá phim + phụ thu VIP (30.000đ); ghế Sweetbox = giá phim + phụ thu Sweetbox (80.000đ). Phim chưa nhập giá thì tính 70.000đ. Giá của từng vé được lưu lúc đặt nên đổi giá / phụ thu không ảnh hưởng vé đã bán.
- **Suất chiếu:** một suất chiếm phòng từ giờ bắt đầu đến hết **thời lượng phim + thời gian dọn phòng** (15 phút). Hai suất trong cùng phòng không được chồng lên nhau, kể cả suất kéo qua nửa đêm. Phim cũ chưa nhập thời lượng thì tính 120 phút. Đổi thời lượng phim mà làm các suất sắp chiếu bị chồng giờ thì không cho lưu. Không thêm / sửa suất vào thời điểm đã qua; suất đã bán vé thì không sửa được, suất đã có vé (kể cả vé đã hoàn) thì không xóa được.
- **Sơ đồ ghế:** mỗi phòng có sơ đồ riêng, tối đa 26 hàng (A–Z) × 30 ghế. Phòng mới được chia sẵn: khoảng 30% hàng đầu ghế Thường, hàng cuối Sweetbox, còn lại VIP. Trong **Sơ đồ ghế** có thể đổi loại từng ghế hoặc cả hàng, hoặc đánh dấu **Không dùng** (lối đi / ghế hỏng: không hiển thị, không bán). Phòng đã có vé thì vẫn đổi được loại ghế nhưng không tạo lại số hàng / số ghế; ghế đang có vé của suất chưa chiếu thì không chuyển sang Không dùng được.
- **Đặt vé:** chỉ đặt được suất chiếu đã gán phòng và chưa bắt đầu. Vé được lưu trong một transaction có khóa, cùng với unique index, nên 2 người không thể đặt trùng một ghế.
- **Hoàn vé:** chỉ hoàn được trước giờ chiếu ít nhất 30 phút; rạp giữ lại **phí hoàn 10%** giá vé, phần còn lại trả khách. Vé hoàn không bị xóa mà đánh dấu `IsBooked = 0` và lưu số tiền đã trả (`RefundAmount`), nên ghế được mở bán lại và phí hoàn được tính vào doanh thu.
- **Bắp nước:** giá từng món được lưu theo đơn lúc bán, đổi giá sau đó không ảnh hưởng đơn cũ. Món đã có trong đơn thì không xóa được, chỉ ngừng bán.
- **Tìm lại mật khẩu:** mã 6 số có hiệu lực 10 phút, nhập sai tối đa 5 lần, 60 giây mới được gửi lại. Ứng dụng chỉ giữ bản băm của mã. Tài khoản chưa có email thì nhờ admin đặt lại mật khẩu.
- **Thống kê:** vé tính theo thời điểm đặt vé, bắp nước theo thời điểm đặt đơn.
- **Poster / ảnh món:** ảnh được thu nhỏ và lưu thẳng trong database, nên máy nào dùng chung database cũng thấy.

## Cấu trúc thư mục

```
system-cinema/
├── cinema system.sln
├── sql/
│   ├── script.sql              # Tạo database mới + dữ liệu mẫu
│   └── cap_nhat_csdl.sql       # Nâng cấp database tạo bằng script bản cũ
└── cinema system/
    ├── App.config              # Chuỗi kết nối SQL Server, cấu hình SMTP
    ├── Program.cs              # Điểm khởi động, chuyển đổi giữa các form
    ├── Db.cs                   # Chuỗi kết nối, kiểm tra trùng tài khoản
    ├── Session.cs              # Tài khoản đang đăng nhập
    ├── AppSettings.cs          # Đọc / lưu bảng CaiDat
    ├── ShowtimeSchedule.cs     # Kiểm tra suất chiếu chồng giờ
    ├── PasswordHasher.cs       # Băm / kiểm tra mật khẩu
    ├── EmailSender.cs          # Gửi email mã xác nhận
    ├── PosterImage.cs          # Đọc / lưu ảnh poster, ảnh món
    ├── đăng nhập/              # Đăng nhập, đăng ký, tìm lại mật khẩu
    ├── khách hàng/             # Trang lịch chiếu cho khách, sơ đồ ghế, bắp nước, thông tin tài khoản
    ├── nhân viên/              # Vé, hoàn vé, phim, suất chiếu, phòng + sơ đồ ghế, quản lý bắp nước
    ├── admin/                  # Màn hình admin, quản lý tài khoản, thống kê, cài đặt
    ├── Properties/
    └── Resources/              # Hình ảnh
```

## Cơ sở dữ liệu

| Bảng | Mô tả |
| --- | --- |
| `TaiKhoan` | Tài khoản đăng nhập; `VaiTro` là `admin`, `staff` hoặc `user`; `Pass` lưu mật khẩu đã băm |
| `Movies` | Phim: tên, giá vé (giá ghế thường), thời lượng (phút), ảnh poster |
| `Showtimes` | Suất chiếu: phim, phòng, ngày chiếu, giờ chiếu |
| `Rooms` | Phòng chiếu (script tạo sẵn Phòng 1–3) |
| `RoomMovies` | Phim được gán cho phòng nào |
| `Seats` | Ghế của từng phòng: tên ghế (A1, B5...) và loại (`Thuong`, `VIP`, `Sweetbox`, `KhongDung`) |
| `BookedSeats` | Vé: suất chiếu, ghế, người đặt, giá, thời điểm đặt; vé hoàn có `IsBooked = 0`, số tiền trả lại và thời điểm hoàn |
| `CaiDat` | Cài đặt dạng khóa / giá trị (phụ thu, phí hoàn, thời gian dọn phòng...) |
| `DoUong` | Món bắp nước: tên, mô tả, giá, ảnh, đang bán |
| `DonDoUong` | Đơn bắp nước: người đặt, suất chiếu (nếu đặt kèm vé), tổng tiền, thời điểm đặt |
| `ChiTietDonDoUong` | Món trong đơn: số lượng, đơn giá lúc bán |

## Hạn chế hiện tại

- Mã xác nhận quên mật khẩu chỉ gửi qua email; chưa gửi SMS vì cần dịch vụ SMS trả phí.
- Chưa hủy / hoàn được đơn bắp nước; hoàn vé không tự hoàn đơn bắp nước đặt kèm. Khách hàng chưa có màn hình xem lại lịch sử đơn bắp nước.
- Ứng dụng kết nối thẳng tới SQL Server (không có máy chủ trung gian), nên máy nào chạy ứng dụng cũng cần quyền trên database; phù hợp dùng trong mạng nội bộ của rạp, chưa phù hợp để phát hành cho khách tự cài.

## Lỗi thường gặp

- **"Không kết nối được cơ sở dữ liệu"** hoặc lỗi *network-related or instance-specific error*: kiểm tra dịch vụ SQL Server đã chạy chưa và `Data Source` trong `App.config` đã đúng chưa.
- **"Cannot open database movie"**: chưa chạy `sql/script.sql`, hoặc tài khoản Windows đang dùng không có quyền trên database.
- **"Invalid column name 'Duration'"**, **'Poster'**, **'RefundAmount'**... hoặc **"Invalid object name 'DoUong'"**, **'DonDoUong'**: database được tạo bằng script bản cũ, chạy `sql/cap_nhat_csdl.sql`.
- **"Không gửi được email"**: kiểm tra phần `Smtp*` trong `App.config`; với Gmail phải dùng Mật khẩu ứng dụng, cổng `587`, `SmtpEnableSsl=true`, và máy không chặn kết nối ra cổng 587.
- **Quên mật khẩu mà tài khoản chưa có email:** nhờ admin đặt lại trong **Quản lý tài khoản**, sau đó bổ sung email trong **Thay đổi thông tin**.
