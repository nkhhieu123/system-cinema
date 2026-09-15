using System.Configuration;
using System.Data.SqlClient;

namespace cinema_system
{
    internal static class Db
    {
        // Chuỗi kết nối dùng chung cho toàn bộ ứng dụng, cấu hình trong App.config (connectionStrings/movie)
        public static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["movie"].ConnectionString;

        // Tên đăng nhập, email và số điện thoại đều dùng để đăng nhập nên không được trùng với tài khoản khác.
        // Trả về câu thông báo nếu bị trùng, null nếu hợp lệ. Giá trị rỗng thì bỏ qua;
        // excludeId là tài khoản đang sửa (-1 khi thêm mới).
        public static string FindAccountDuplicate(SqlConnection conn, string tenDangNhap, string email, string sdt, int excludeId)
        {
            if (AccountValueExists(conn, "TenDangNhap", tenDangNhap, excludeId))
                return "Tên đăng nhập đã tồn tại!";
            if (AccountValueExists(conn, "Email", email, excludeId))
                return "Email đã được tài khoản khác sử dụng!";
            if (AccountValueExists(conn, "SDT", sdt, excludeId))
                return "Số điện thoại đã được tài khoản khác sử dụng!";
            return null;
        }

        private static bool AccountValueExists(SqlConnection conn, string column, string value, int excludeId)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) FROM TaiKhoan WHERE " + column + " = @value AND IDTaiKhoan <> @id", conn);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@id", excludeId);
            return (int)cmd.ExecuteScalar() > 0;
        }
    }
}
