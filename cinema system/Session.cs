namespace cinema_system
{
    // Tài khoản đang đăng nhập, gán khi đăng nhập thành công
    internal static class Session
    {
        public static int? IDTaiKhoan { get; private set; }
        public static string TenDangNhap { get; private set; }
        public static string VaiTro { get; private set; }

        public static void DangNhap(int idTaiKhoan, string tenDangNhap, string vaiTro)
        {
            IDTaiKhoan = idTaiKhoan;
            TenDangNhap = tenDangNhap;
            VaiTro = vaiTro;
        }

        public static void DangXuat()
        {
            IDTaiKhoan = null;
            TenDangNhap = null;
            VaiTro = null;
        }
    }
}
