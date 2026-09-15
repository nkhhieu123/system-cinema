using cinema_system.đăng_nhập;
using System;
using System.Windows.Forms;

namespace cinema_system
{
    internal static class Program
    {
        // Số form chính đang mở; khi về 0 thì thoát ứng dụng
        private static int openForms;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Hiện lỗi (vd: không kết nối được SQL Server) thay vì làm crash chương trình
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
                MessageBox.Show(e.Exception.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ShowForm(new Đăng_nhập());
            Application.Run();
        }

        /// <summary>
        /// Hiện một form chính; khi form chính cuối cùng bị đóng thì ứng dụng thoát.
        /// </summary>
        public static void ShowForm(Form form)
        {
            openForms++;
            form.FormClosed += (s, e) =>
            {
                if (--openForms == 0)
                    Application.ExitThread();
            };
            form.Show();
        }

        /// <summary>
        /// Chuyển sang form khác và đóng form hiện tại (không để lại form ẩn chạy ngầm).
        /// </summary>
        public static void SwitchForm(Form current, Form next)
        {
            ShowForm(next);
            current.Close();
        }
    }
}
