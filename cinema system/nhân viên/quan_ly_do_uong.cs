using rạp_chiếu_phim.khách_hàng;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace cinema_system.nhân_viên
{
    // Quản lý danh sách bắp nước (tên, mô tả, giá, ảnh, đang bán) và bán tại quầy
    public partial class quan_ly_do_uong : UserControl
    {
        private int selectedId = -1;
        private byte[] hinhAnh;

        public quan_ly_do_uong()
        {
            InitializeComponent();
            dgvDoUong.DataBindingComplete += (s, e) =>
            {
                if (dgvDoUong.Columns["IDDoUong"] == null)
                    return;
                dgvDoUong.Columns["IDDoUong"].HeaderText = "ID";
                dgvDoUong.Columns["TenDoUong"].HeaderText = "Tên món";
                dgvDoUong.Columns["MoTa"].HeaderText = "Mô tả";
                dgvDoUong.Columns["Gia"].HeaderText = "Giá";
                dgvDoUong.Columns["Gia"].DefaultCellStyle.FormatProvider = new CultureInfo("vi-VN");
                dgvDoUong.Columns["Gia"].DefaultCellStyle.Format = "c0";
                dgvDoUong.Columns["DangBan"].HeaderText = "Đang bán";
                dgvDoUong.Columns["DaBan"].HeaderText = "Đã bán";
                dgvDoUong.ClearSelection();
            };
            LoadDoUong();
        }

        private void LoadDoUong()
        {
            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT d.IDDoUong, d.TenDoUong, d.MoTa, d.Gia, d.DangBan,
                             ISNULL((SELECT SUM(ct.SoLuong) FROM ChiTietDonDoUong ct WHERE ct.IDDoUong = d.IDDoUong), 0) AS DaBan
                      FROM DoUong d ORDER BY d.IDDoUong", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDoUong.DataSource = dt;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên món.");
                txtTen.Focus();
                return false;
            }
            if (numGia.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá bán.");
                numGia.Focus();
                return false;
            }
            return true;
        }

        private void AddParameters(SqlCommand cmd)
        {
            string moTa = txtMoTa.Text.Trim();
            cmd.Parameters.AddWithValue("@ten", txtTen.Text.Trim());
            cmd.Parameters.AddWithValue("@mota", moTa == "" ? (object)DBNull.Value : moTa);
            cmd.Parameters.AddWithValue("@gia", numGia.Value);
            cmd.Parameters.AddWithValue("@dangban", chkDangBan.Checked);
            cmd.Parameters.Add("@hinh", SqlDbType.VarBinary, -1).Value = (object)hinhAnh ?? DBNull.Value;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO DoUong (TenDoUong, MoTa, Gia, DangBan, HinhAnh) VALUES (@ten, @mota, @gia, @dangban, @hinh)", conn);
                AddParameters(cmd);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thêm món thành công!");
            LoadDoUong();
            ClearForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedId < 0)
            {
                MessageBox.Show("Hãy chọn món cần sửa.");
                return;
            }
            if (!ValidateInput())
                return;

            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                // Đơn cũ đã lưu đơn giá lúc bán nên đổi giá không ảnh hưởng
                SqlCommand cmd = new SqlCommand(
                    "UPDATE DoUong SET TenDoUong=@ten, MoTa=@mota, Gia=@gia, DangBan=@dangban, HinhAnh=@hinh WHERE IDDoUong=@id", conn);
                AddParameters(cmd);
                cmd.Parameters.AddWithValue("@id", selectedId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Sửa món thành công!");
            LoadDoUong();
            ClearForm();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedId < 0)
            {
                MessageBox.Show("Hãy chọn món cần xóa.");
                return;
            }
            if (MessageBox.Show("Xóa món \"" + txtTen.Text.Trim() + "\"?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM DoUong WHERE IDDoUong=@id", conn);
                    cmd.Parameters.AddWithValue("@id", selectedId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex) when (ex.Number == 547) // vi phạm khóa ngoại
            {
                MessageBox.Show("Món này đã có trong đơn hàng nên không thể xóa.\nHãy bỏ chọn \"Đang bán\" rồi bấm Sửa để ngừng bán.");
                return;
            }

            MessageBox.Show("Xóa món thành công!");
            LoadDoUong();
            ClearForm();
        }

        private void dgvDoUong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvDoUong.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["IDDoUong"].Value);
            txtTen.Text = row.Cells["TenDoUong"].Value?.ToString();
            txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();
            numGia.Value = Math.Min(numGia.Maximum, Convert.ToDecimal(row.Cells["Gia"].Value));
            chkDangBan.Checked = Convert.ToBoolean(row.Cells["DangBan"].Value);

            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT HinhAnh FROM DoUong WHERE IDDoUong=@id", conn);
                cmd.Parameters.AddWithValue("@id", selectedId);
                hinhAnh = cmd.ExecuteScalar() as byte[];
            }
            ShowHinh();
        }

        private void ShowHinh()
        {
            Image old = picHinh.Image;
            picHinh.Image = PosterImage.Load(hinhAnh, null);
            old?.Dispose();
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    hinhAnh = PosterImage.FromFile(ofd.FileName);
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("File đã chọn không phải là ảnh hợp lệ.");
                    return;
                }
                ShowHinh();
            }
        }

        private void btnBoAnh_Click(object sender, EventArgs e)
        {
            hinhAnh = null;
            ShowHinh();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDoUong();
        }

        private void btnBanTaiQuay_Click(object sender, EventArgs e)
        {
            using (drink f = new drink(null))
            {
                f.ShowDialog(FindForm());
            }
            LoadDoUong();
        }

        private void ClearForm()
        {
            selectedId = -1;
            txtTen.Clear();
            txtMoTa.Clear();
            numGia.Value = 0;
            chkDangBan.Checked = true;
            hinhAnh = null;
            ShowHinh();
            dgvDoUong.ClearSelection();
        }
    }
}
