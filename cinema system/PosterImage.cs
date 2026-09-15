using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace cinema_system
{
    // Ảnh poster phim được lưu thẳng trong database (cột Movies.Poster) nên máy nào dùng chung DB cũng thấy.
    // Dữ liệu cũ chỉ có đường dẫn file (Movies.PosterPath) thì vẫn đọc từ file nếu còn tồn tại.
    internal static class PosterImage
    {
        private const int MaxWidth = 600;
        private const int MaxHeight = 900;

        // Đọc file ảnh người dùng chọn, thu nhỏ lại và trả về dữ liệu JPEG để lưu vào DB
        public static byte[] FromFile(string path)
        {
            using (Image source = Image.FromFile(path))
            {
                double scale = Math.Min(1.0, Math.Min((double)MaxWidth / source.Width, (double)MaxHeight / source.Height));
                int width = Math.Max(1, (int)(source.Width * scale));
                int height = Math.Max(1, (int)(source.Height * scale));

                using (Bitmap bmp = new Bitmap(width, height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White); // ảnh PNG trong suốt -> nền trắng
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(source, 0, 0, width, height);
                    }

                    using (MemoryStream ms = new MemoryStream())
                    {
                        bmp.Save(ms, ImageFormat.Jpeg);
                        return ms.ToArray();
                    }
                }
            }
        }

        // Tạo ảnh để hiển thị; không có ảnh hoặc ảnh lỗi thì trả về null
        public static Image Load(object posterData, object posterPath)
        {
            if (posterData is byte[] data && data.Length > 0)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(data))
                    using (Image img = Image.FromStream(ms))
                    {
                        return new Bitmap(img);
                    }
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }

            string path = posterPath as string;
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                try
                {
                    using (Image img = Image.FromFile(path))
                    {
                        return new Bitmap(img);
                    }
                }
                catch (OutOfMemoryException) // file không phải ảnh
                {
                    return null;
                }
            }

            return null;
        }
    }
}
