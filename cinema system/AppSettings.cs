using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;

namespace cinema_system
{
    // Các thiết lập nghiệp vụ admin chỉnh được, lưu trong bảng CaiDat (Khoa / GiaTri).
    // Thiếu dòng nào (hoặc database cũ chưa có bảng) thì dùng giá trị mặc định bên dưới.
    internal class AppSettings
    {
        public decimal PhuThuVIP = 30000;       // cộng thêm vào giá ghế thường
        public decimal PhuThuSweetbox = 80000;
        public decimal GiaVeMacDinh = 70000;    // phim chưa nhập giá vé
        public int ThoiLuongMacDinh = 120;      // phút, phim chưa nhập thời lượng
        public int ThoiGianDonPhong = 15;       // phút nghỉ tối thiểu giữa 2 suất trong cùng phòng
        public decimal PhiHoanVe = 10;          // % giá vé giữ lại khi hoàn
        public int HoanVeTruocPhut = 30;        // chỉ hoàn được trước giờ chiếu ít nhất bấy nhiêu phút

        public static AppSettings Load(SqlConnection conn)
        {
            AppSettings s = new AppSettings();
            Dictionary<string, string> values = new Dictionary<string, string>();

            try
            {
                using (SqlDataReader r = new SqlCommand("SELECT Khoa, GiaTri FROM CaiDat", conn).ExecuteReader())
                {
                    while (r.Read())
                        values[r.GetString(0)] = r.GetString(1);
                }
            }
            catch (SqlException ex) when (ex.Number == 208) // chưa có bảng CaiDat
            {
                return s;
            }

            s.PhuThuVIP = GetDecimal(values, "PhuThuVIP", s.PhuThuVIP);
            s.PhuThuSweetbox = GetDecimal(values, "PhuThuSweetbox", s.PhuThuSweetbox);
            s.GiaVeMacDinh = GetDecimal(values, "GiaVeMacDinh", s.GiaVeMacDinh);
            s.ThoiLuongMacDinh = (int)GetDecimal(values, "ThoiLuongMacDinh", s.ThoiLuongMacDinh);
            s.ThoiGianDonPhong = (int)GetDecimal(values, "ThoiGianDonPhong", s.ThoiGianDonPhong);
            s.PhiHoanVe = GetDecimal(values, "PhiHoanVe", s.PhiHoanVe);
            s.HoanVeTruocPhut = (int)GetDecimal(values, "HoanVeTruocPhut", s.HoanVeTruocPhut);
            return s;
        }

        public static AppSettings Load()
        {
            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                return Load(conn);
            }
        }

        public void Save(SqlConnection conn)
        {
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                SaveValue(conn, tran, "PhuThuVIP", PhuThuVIP);
                SaveValue(conn, tran, "PhuThuSweetbox", PhuThuSweetbox);
                SaveValue(conn, tran, "GiaVeMacDinh", GiaVeMacDinh);
                SaveValue(conn, tran, "ThoiLuongMacDinh", ThoiLuongMacDinh);
                SaveValue(conn, tran, "ThoiGianDonPhong", ThoiGianDonPhong);
                SaveValue(conn, tran, "PhiHoanVe", PhiHoanVe);
                SaveValue(conn, tran, "HoanVeTruocPhut", HoanVeTruocPhut);
                tran.Commit();
            }
        }

        private static decimal GetDecimal(Dictionary<string, string> values, string key, decimal defaultValue)
        {
            return values.TryGetValue(key, out string text)
                   && decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value)
                ? value
                : defaultValue;
        }

        private static void SaveValue(SqlConnection conn, SqlTransaction tran, string key, decimal value)
        {
            SqlCommand cmd = new SqlCommand(
                @"UPDATE CaiDat SET GiaTri = @value WHERE Khoa = @key;
                  IF @@ROWCOUNT = 0 INSERT INTO CaiDat (Khoa, GiaTri) VALUES (@key, @value);", conn, tran);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.Parameters.AddWithValue("@value", value.ToString(CultureInfo.InvariantCulture));
            cmd.ExecuteNonQuery();
        }
    }
}
