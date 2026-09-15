using System.Configuration;

namespace cinema_system
{
    internal static class Db
    {
        // Chuỗi kết nối dùng chung cho toàn bộ ứng dụng, cấu hình trong App.config (connectionStrings/movie)
        public static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["movie"].ConnectionString;
    }
}
