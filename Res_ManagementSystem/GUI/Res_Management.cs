using iTextSharp.text;
using iTextSharp.text.pdf;
using Res_ManagementSystem.BUS;
using Res_ManagementSystem.DAO;
using Res_ManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Res_ManagementSystem.GUI
{
    public partial class Res_Management : Form
    {
        public Res_Management()
        {
            InitializeComponent();
            tab_Res.SelectedIndexChanged += new EventHandler(tab_Res_SelectedIndexChanged);
        }

        private string _id;
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private void Res_Management_Load(object sender, EventArgs e)
        {

            
            LoadLoaiThucDonIntoCombobox();
            LoadBanIntoCombobox();
            DuaDSBanDaGoiLenCombobox();
            loadThangvaoComboBox();

        }

        private void tab_Res_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DateTime.Now.ToString("HH:mm") == "22:05")
            {
                DataTable _dt = AssignmentBUS.LayDSChiaCa();
                
                DataTable _ds = StaffBUS.LayDSNhanVien();
                for(int i=0;i<_dt.Rows.Count;i++)
                {
                    //float sal = float.Parse(_dt.Rows[i][4].ToString());
                    if (StaffBUS.CapNhatLuongNhanVien(_dt.Rows[i][0].ToString(), float.Parse(_dt.Rows[i][4].ToString())+ float.Parse(_ds.Rows[i][8].ToString())))
                    {
                        MessageBox.Show("Tiền Lương hôm nay đã được thêm cho nhân viên !!");
                    }
                    else
                    {
                        MessageBox.Show("Lỗi chưa thể cộng lương cho nhân viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }    
                }
                dgv_PhanCong.DataSource = _dt;
                ExportToPDF(dgv_PhanCong);
            }

            if (tab_Res.SelectedTab.Text == "Phân Công")
            {
                cmb_ManvPC.DataSource = AssignmentBUS.LayDSChiaCa();
                cmb_ManvPC.DisplayMember = "Mã Nhân Viên";
                cmb_ManvPC.ValueMember = "Mã Nhân Viên";
                cmb_ManvPC.SelectedItem = null;
            }
            else if (tab_Res.SelectedTab.Text == "Check IN")
            {
                CheckinLoad();
            } 
        }

        /*------------------------------------------------Nhân Viên-----------------------------------------------*/
        public void ThemNhanVien()
        {
            CheckinDTO ck = new CheckinDTO();
            ck.MaNV = string.Format("STF{0:00}", (StaffDAO.MaTuTang() - 2));
            ck.Checkin1 = 0;
            ck.Checkin2 = 0;
            ck.Checkin3 = 0;
            ck.Checkout1 = 0;
            ck.Checkout2 = 0;
            ck.Checkout3 = 0;
            ck.GioLam = 0;
            ck.CheckinDungGio = 0;
            StaffDTO nv = new StaffDTO();
           
            nv.StaffID = string.Format("STF{0:00}", (StaffDAO.MaTuTang() - 2));
            nv.UserName = tbx_User.Text;
            nv.PassWord = tbx_Pass.Text;
            nv.DisplayName = tbx_DisplName.Text;
            nv.Type = cmB_Type.Text;
            nv.Salary = 0;
            if (verify())
            {
                if (tbx_Pass.Text == tbx_Repass.Text)
                {
                    if (!StaffBUS.KiemTraTenDNTonTai(nv.UserName, nv.StaffID))
                    {
                        bool kq = StaffBUS.ThemNhanVien(nv);
                        if (kq == true)
                        {
                            //Thêm phân công ngay khi thêm nhân viên mới
                            if (AssignmentBUS.ThemPhanCongTheoMaTuTang(nv))
                            {
                                if(CheckinBUS.ThemMoiNhanVienVaoCheckIn(ck))
                                    MessageBox.Show("Thêm người dùng thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                MessageBox.Show("Thêm thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            nv.StaffID = "";
                            nv.UserName = "";
                            nv.PassWord = "";
                            nv.DisplayName = "";
                            

                        }
                        else
                        {
                            MessageBox.Show("Thêm thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }



                    }
                    else
                    {
                        MessageBox.Show("Tên Đăng Nhập Đã Tồn Tại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Nhập lại Mật Khẩu không khớp!!!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Không được ĐỂ TRỐNG bất cứ thông tin nào !!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (_id == "MNG1" || _id == "MNG2")
                ThemNhanVien();
            else
                MessageBox.Show("Chỉ có Quản Lý mới được sử dụng chức năng này","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        private bool verify()
        {
            if (tbx_User.Text == "" || tbx_Pass.Text == "" || tbx_Repass.Text == "" || tbx_DisplName.Text == ""||cmB_Type.Text=="")
            {
                return false;
            }
            else return true;
        }
        private void btn_DSNV_Click(object sender, EventArgs e)
        {
            HienThiDSNV();
        }
        private void HienThiDSNV()
        {
            if (_id != "MNG1" && _id != "MNG2")
            {
                DataTable dt = StaffBUS.LayDSNhanVien();
                dgv_NhanVien.DataSource = dt;
            }
            else
            {
                DataTable dt = StaffBUS.LayDSNhanVienCoMK();
                dgv_NhanVien.DataSource = dt;
            }
        }
        private void btn_LogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất không?", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                loginForm frm = new loginForm();
                frm.Show();
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (_id == "MNG1" || _id == "MNG2")
            {
                int index = dgv_NhanVien.CurrentRow.Index;
                string manv = dgv_NhanVien.Rows[index].Cells[0].Value.ToString();
                if ((MessageBox.Show("Bạn có chắc muốn xóa ?", "Xóa Nhân Viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    try
                    {
                        if (AssignmentBUS.XoaPhanCong(manv))
                        {
                            if (StaffBUS.XoaNhanVien(manv))
                            {
                                MessageBox.Show("Đã Xóa Nhân Viên", "Xóa Nhân Viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                HienThiDSNV();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nhân viên đang được phân công không thể xóa!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    catch
                    {

                    }
                }
            }
            else
            {
                MessageBox.Show("Chỉ có Quản Lý mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            if(tbx_Search.Text=="")
            {
                MessageBox.Show("Chưa Nhập Tên Cần Tra Cứu ! ", "Thông Báo ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
            else
            {
                tbx_DisplName.Text = "";
                tbx_User.Text = "";
                tbx_Repass.Text = "";
                tbx_Pass.Text = "";
                cmB_Type.Text = "Staff";
                DataTable dt = StaffBUS.TraCuuNhanVienTheoTen(tbx_Search.Text);
                dgv_NhanVien.DataSource = dt;
            }    
        }

        private void dgv_DSNV_Click(object sender, EventArgs e)
        {
            if (_id == "MNG1" || _id == "MNG2")
            {
                int index = dgv_NhanVien.CurrentRow.Index;
                tbx_User.Text = dgv_NhanVien.Rows[index].Cells[1].Value.ToString();
                //tbx_Pass.Text = dgv_NhanVien.Rows[index].Cells[2].Value.ToString();
                tbx_DisplName.Text = dgv_NhanVien.Rows[index].Cells[3].Value.ToString();
                cmB_Type.Text = dgv_NhanVien.Rows[index].Cells[4].Value.ToString();
                tbx_User.ReadOnly = false;
                tbx_Pass.ReadOnly = false;
                tbx_Repass.ReadOnly = false;
                tbx_DisplName.ReadOnly = false;
                cmB_Type.Enabled = false;
                cmB_Type.Enabled = true;
                string MK = StaffBUS.LayMatKhauTuTenDN(dgv_NhanVien.Rows[index].Cells[1].Value.ToString());
                tbx_Pass.Text = MK;
                tbx_Repass.Text = MK;
            }
        }
        private void ChinhSuaTTNhanVien()
        {
            StaffDTO nv = new StaffDTO();
            
            if (verify())
            {
                nv.DisplayName = tbx_DisplName.Text;
                nv.PassWord = tbx_Pass.Text;
                nv.UserName = tbx_User.Text;
                if(StaffBUS.CapNhatNhanVien(nv))
                {
                    MessageBox.Show("Cập Nhật Thành Công !!");
                }
            }
            else
            {
                MessageBox.Show("Không được ĐỂ TRỐNG bất cứ thông tin nào !!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try
                {
                    int index = dgv_NhanVien.CurrentRow.Index;
                    tbx_User.Text = dgv_NhanVien.Rows[index].Cells[1].Value.ToString();
                    //tbx_Pass.Text = dgv_NhanVien.Rows[index].Cells[2].Value.ToString();
                    tbx_DisplName.Text = dgv_NhanVien.Rows[index].Cells[3].Value.ToString();
                    cmB_Type.Text = dgv_NhanVien.Rows[index].Cells[4].Value.ToString();
                    tbx_User.ReadOnly = false;
                    tbx_Pass.ReadOnly = false;
                    tbx_Repass.ReadOnly = false;
                    tbx_DisplName.ReadOnly = false;
                    cmB_Type.Enabled = false;
                    cmB_Type.Enabled = true;
                    string MK = StaffBUS.LayMatKhauTuTenDN(dgv_NhanVien.Rows[index].Cells[1].Value.ToString());
                    tbx_Pass.Text = MK;
                    tbx_Repass.Text = MK;
                }
                catch { }
            }    
                
            
            


        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (_id == "MNG1" || _id == "MNG2")
                ChinhSuaTTNhanVien();
            else
                MessageBox.Show("Chỉ có Quản Lý mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        /*------------------------------------------------Kết Thúc Phần Nhân Viên-----------------------------------------------*/



        /*------------------------------------------------Phân Công-----------------------------------------------*/
        static string Date = "";
        static string Date1 = "";
        private void ThemPC(string Date1)
        {
            Date1 = DateTime.Now.ToString("dd/MM//yy");
            AssignmentDTO pc = new AssignmentDTO();
            int[,] Assignment_Array = DataProvider.Assignment();
            System.Data.DataTable _ds = StaffBUS.LayDSNhanVien();
            XoayPhanCong(Date1, Assignment_Array);
            bool kq = false;
            if (AssignmentDAO.XoaToanBoDSPhanCong()||AssignmentBUS.LayDSChiaCa().Rows.Count==0)
            {
                for (int i = 0; i < _ds.Rows.Count; i++)
                {
                    //pc.SID = string.Format("STF{0:00}", i);
                    pc.SID =_ds.Rows[i][0].ToString();
                    pc.Ca1 = Assignment_Array[(i) % 4, 0];
                    pc.Ca2 = Assignment_Array[(i) % 4, 1];
                    pc.Ca3 = Assignment_Array[(i) % 4, 2];
                    pc.Salary = 0;
                    pc.CaTre = 0; pc.SoGioCham = 0; pc.TienPhat = 0; pc.TienThuong = 0; pc.TinhTrangCK=0;

                    if (AssignmentBUS.ThemPhanCong(pc))
                    {
                        //MessageBox.Show("Phân Công Thành Công", "");
                        kq = true;
                    }

                }
            }
            if (kq == true)
            {
                MessageBox.Show("Phân Công Thành Công", "");
                System.Data.DataTable _dt = AssignmentBUS.LayDSChiaCa();
                dgv_PhanCong.DataSource = _dt;
            }
            else
                MessageBox.Show("Phân Công Thất Bại", "");

        }

        public void XoayPhanCong(string Date1, int[,] arr)
        {
            if (Date1 != Date)
            {
                for (int j = 0; j <= 2; j++)
                    for (int i = 0; i <= 2; i++)
                    {
                        int temp = arr[i, j];
                        arr[i, j] = arr[i + 1, j];
                        arr[i + 1, j] = temp;
                    }
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            ThemPC(Date1);
            Date = Date1;
        }

        private void btn_Delpc_Click(object sender, EventArgs e)
        {
            if (_id == "MNG1" || _id == "MNG2")
            {
                int index = dgv_PhanCong.CurrentRow.Index;
                string manv = dgv_PhanCong.Rows[index].Cells[0].Value.ToString();
                if (_id == "MNG1" || _id == "MNG2")
                {
                    if (AssignmentBUS.XoaPhanCong(manv))
                    {
                        MessageBox.Show("Xóa Thành Công !!", "Xóa Nhân Viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
                MessageBox.Show("Chỉ có Quản Lý mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_DSPC_Click(object sender, EventArgs e)
        {

            DataTable dt = AssignmentBUS.LayDSChiaCa();
            dgv_PhanCong.DataSource = dt;
        }

        private void dgv_PhanCong_Click(object sender, EventArgs e)
        {
            int index = dgv_PhanCong.CurrentRow.Index;
            DataTable dt = StaffBUS.LayDSNhanVien();
            tbx_TennvPc.Text = dt.Rows[index][1].ToString();
            cmb_ManvPC.Text= dgv_PhanCong.Rows[index].Cells[0].Value.ToString();
            tbx_TennvPc.ReadOnly = true;
           
            int ca1 = Convert.ToInt32(dgv_PhanCong.Rows[index].Cells[1].Value.ToString());
            int ca2 = Convert.ToInt32(dgv_PhanCong.Rows[index].Cells[2].Value.ToString());
            int ca3 = Convert.ToInt32(dgv_PhanCong.Rows[index].Cells[3].Value.ToString());
            ckB_Ca1PC.Checked = true;
            ckB_Ca2PC.Checked = true;
            ckB_Ca3.Checked = true;
            if(ca1==0)
            {
                ckB_Ca1PC.Checked = false;
            }
            if (ca2 == 0)
            {
                ckB_Ca2PC.Checked = false;
            }
            if (ca3 == 0)
            {
                ckB_Ca3.Checked = false;
            }
            

        }
        private void ExportToPDF(DataGridView dgv)
        {

            if (dgv.Rows.Count > 0)

            {

                string.Format("LuongNgay{0}.pdf", DateTime.Now.ToString("dd/MM/yy"));
                SaveFileDialog save = new SaveFileDialog();

                save.Filter = "PDF (*.pdf)|*.pdf";

                save.FileName = string.Format("LuongNgay{0}.pdf", DateTime.Now.ToString("dd/MM/yy"));

                bool ErrorMessage = false;

                if (save.ShowDialog() == DialogResult.OK)

                {

                    if (File.Exists(save.FileName))

                    {

                        try

                        {

                            File.Delete(save.FileName);

                        }

                        catch (Exception ex)

                        {

                            ErrorMessage = true;

                            MessageBox.Show("Unable to wride data in disk" + ex.Message);

                        }

                    }

                    if (!ErrorMessage)

                    {

                        try

                        {

                            PdfPTable pTable = new PdfPTable(dgv.Columns.Count);

                            pTable.DefaultCell.Padding = 2;

                            pTable.WidthPercentage = 100;

                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
                            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                            iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 12);

                            foreach (DataGridViewColumn col in dgv.Columns)

                            {

                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, f));

                                pTable.AddCell(pCell);

                            }

                            int RowCount = dgv.Rows.Count;
                            int ColumnCount = dgv.Columns.Count;
                            //add rows
                            //
                            foreach (DataGridViewRow row in dgv.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pTable.AddCell(new Phrase(cell.Value.ToString(), f));
                                }
                            }
                            //
                            string path = "D:\\PDF\\";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            using (FileStream fileStream = new FileStream(path + save.FileName, FileMode.Create))

                            {

                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);

                                PdfWriter.GetInstance(document, fileStream);

                                document.Open();

                                document.Add(pTable);

                                document.Close();

                                fileStream.Close();
                            }

                            MessageBox.Show("Data Export Successfully", "info");

                        }

                        catch (Exception ex)

                        {

                            MessageBox.Show("Error while exporting Data" + ex.Message);

                        }

                    }

                }

            }

            else

            {

                MessageBox.Show("No Record Found", "Info");

            }
        }
        private void button_XemdsCkinPC_Click(object sender, EventArgs e)
        {
            PhanCong.DSCheckin dsCk = new PhanCong.DSCheckin();
            dsCk.ShowDialog();
        }

        // Đổi CA
        private void button_DoicaPC_Click(object sender, EventArgs e)
        {
            if (textBox_ID1PC.Text == "" || textBox_ID2PC.Text == "")
            { MessageBox.Show("Không được để trống !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else 
            {
                DataTable ds1 = AssignmentBUS.LayDSChiaCaTuMaNV(textBox_ID1PC.Text);
                DataTable ds2 = AssignmentBUS.LayDSChiaCaTuMaNV(textBox_ID2PC.Text);
                if (rad_Ca1PC.Checked == true)
                {

                }    
            }    
        }

        /*------------------------------------------------Kết Thúc Phần Phân Công-----------------------------------------------*/


        /*------------------------------------------------Check in-----------------------------------------------*/


        private void CheckinLoad()
        {
            DataTable dt = AssignmentBUS.LayDSChiaCaTuMaNV(_id);
            dgvCheckin.DataSource = dt;
            int _offtime = Convert.ToInt32(dateTimePicker_Checkio.Value.ToString("HH"));
            //if (_offtime >= 17 && _offtime <= 18)
            //{
            //    MessageBox.Show("Hệ Thống Chấm Công đang tạm dừng do đang ngoài giờ làm việc", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Close();
            //}
            if (DateTime.Now.ToString("HH:mm") == "22:05")
            {
                TinhTienLuongNgay();
                Close();
            }
        }

        static int ck_i_hour = 0;
        static int chkinhour = 0;
        static int chkinmin = 0;
        int _caTre = 0;
        private void button_Checkin_Click(object sender, EventArgs e)
        {
            


        }
        
        private void button_Checkout_Click(object sender, EventArgs e)
        {
            
        }

        private void button_Checkin_Click_1(object sender, EventArgs e)
        {
            if (_id != "MNG1" && _id!="MNG2")
            {
                DataTable _ds = Res_ManagementSystem.BUS.AssignmentBUS.LayDSChiaCa();

                //MessageBox.Show(_id,"Test",MessageBoxButtons.OK,MessageBoxIcon.Information);
                for (int i = 0; i < _ds.Rows.Count; i++)
                {
                    if (_id == _ds.Rows[i][0].ToString())
                    {
                        //chkinhour = Convert.ToInt32(dateTimePicker_Checkio.Value.ToString("HH"));
                        //chkinmin = Convert.ToInt32(dateTimePicker_Checkio.Value.ToString("mm"));
                        chkinhour = 7;
                        chkinmin = 0;
                        int ca = BUS.AssignmentBUS.LayCaTheoGio(chkinhour);
                        CheckinDTO ck = new CheckinDTO();
                        ck.MaNV = _id;

                        if (ca != 0)
                        {
                            DataTable _ds1 = Res_ManagementSystem.BUS.AssignmentBUS.NVLamTrongCa(ca.ToString(), _id);
                            if (_ds1.Rows.Count != 0)
                            {
                                int _gioTre;
                                int _phutTre;
                                if (ca == 1)
                                {
                                    _gioTre = chkinhour - 7;
                                    _phutTre = chkinmin - 0;
                                    if (_gioTre == 0 && _phutTre == 0)
                                    {
                                        ck_i_hour += chkinhour;
                                        ck.Checkin1 = ck_i_hour;
                                        if (CheckinBUS.CapNhatCheckinDungGio(ck) && AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Thành Công !", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                    }
                                    else if (_gioTre == 0 && _phutTre <= 15)
                                    {

                                        ck_i_hour = chkinhour;
                                        ck.Checkin1 = ck_i_hour;

                                        if (CheckinBUS.CapNhatCheckinDungGio(ck) && AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Thành Công !", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    else if (_gioTre == 0 && _phutTre > 15)
                                    {

                                        ck_i_hour = chkinhour + 1;
                                        ck.Checkin1 = ck_i_hour;

                                        if (AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Thành Công ! Bạn Bị Trễ " + _gioTre + "giờ" + _phutTre + "phút", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        _gioTre += 1;
                                        _caTre = 1;
                                    }
                                    else
                                    {

                                        ck_i_hour = chkinhour;
                                        ck.Checkin1 = ck_i_hour;
                                        if (AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Thành Công ! Bạn Bị Trễ " + _gioTre + "giờ" + _phutTre + "phút", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        _caTre = 1;
                                    }

                                }
                                else if (ca == 2)
                                {
                                    _gioTre = chkinhour - 11;
                                    _phutTre = chkinmin - 0;
                                    if (_gioTre == 0 && _phutTre == 0)
                                    {
                                        ck_i_hour = chkinhour;
                                        ck.Checkin2 = ck_i_hour;

                                        if (CheckinBUS.CapNhatCheckinDungGio(ck) && AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Ca 2 Thành Công !", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    else if (_gioTre == 0 && _phutTre <= 15)
                                    {

                                        ck_i_hour = chkinhour;
                                        ck.Checkin2 = ck_i_hour;

                                        if (CheckinBUS.CapNhatCheckinDungGio(ck) && AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Ca 2 Thành Công !", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    else if (_gioTre == 0 && _phutTre > 15)
                                    {

                                        ck_i_hour = chkinhour + 1;
                                        ck.Checkin2 = ck_i_hour;
                                        if (AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Thành Công ! Bạn Bị Trễ " + _gioTre + "giờ" + _phutTre + "phút", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        _gioTre += 1;
                                        _caTre = 2;
                                    }
                                    else
                                    {

                                        ck_i_hour = chkinhour;
                                        ck.Checkin2 = ck_i_hour;
                                        if (AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Thành Công ! Bạn Bị Trễ " + _gioTre + "giờ" + _phutTre + "phút", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        _caTre = 2;
                                    }
                                }
                                else
                                {
                                    _gioTre = chkinhour - 18;
                                    _phutTre = chkinmin - 0;
                                    if (_gioTre == 0 && _phutTre == 0)
                                    {
                                        ck_i_hour = chkinhour;
                                        ck.Checkin3 = ck_i_hour;
                                        if (CheckinBUS.CapNhatCheckinDungGio(ck) && AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Ca 3 Thành Công !", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    else if (_gioTre == 0 && _phutTre <= 15)
                                    {
                                        ck_i_hour = chkinhour;
                                        ck.Checkin3 = ck_i_hour;
                                        if (CheckinBUS.CapNhatCheckinDungGio(ck) && AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Ca 3 Thành Công !", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    else if (_gioTre == 0 && _phutTre > 15)
                                    {

                                        ck_i_hour = chkinhour + 1;
                                        ck.Checkin3 = ck_i_hour;
                                        if (AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Thành Công ! Bạn Bị Trễ " + _gioTre + "giờ" + _phutTre + "phút", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        _gioTre += 1;
                                        _caTre = 3;
                                    }
                                    else
                                    {

                                        ck_i_hour = chkinhour;
                                        ck.Checkin3 = ck_i_hour;
                                        if (AssignmentBUS.CapNhatCheckin(ck.MaNV))
                                            MessageBox.Show("Check In Thành Công ! Bạn Bị Trễ " + _gioTre + "giờ" + _phutTre + "phút", "Check In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        _caTre = 3;
                                    }
                                }
                                int giotre = Convert.ToInt32(_ds.Rows[i][5].ToString()) + _gioTre;
                                int catre = Convert.ToInt32(_ds.Rows[i][8].ToString()) * 10 + _caTre;
                                if (AssignmentBUS.ThemGioTreCaTre(_id, giotre, catre))
                                {
                                    if (CheckinBUS.CapNhatCheckin(ck))
                                    {

                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Nhân Viên Không có Ca Làm Này Hôm nay", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hệ Thống Chấm Công đang tạm dừng do đang ngoài giờ làm việc", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Chỉ Nhân Viên mới sử dụng chức năng này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //private CheckinDTO CheckinDTO()
        //{
        //    throw new NotImplementedException();
        //}

        private void button_Checkout_Click_1(object sender, EventArgs e)
        {
            if (_id != "MNG1" && _id != "MNG2")
            {
                //int chkouthour = Convert.ToInt32(dateTimePicker_Checkio.Value.ToString("HH"));
                // int chkoutmin = Convert.ToInt32(dateTimePicker_Checkio.Value.ToString("mm"));

                //DataTable _ds = BUS.AssignmentBUS.NVLamTrongCa((ca+1).ToString(), _id);
                CheckinDTO ck = new CheckinDTO();
                ck.MaNV = _id;

                int chkouthour = 15;
                if (chkouthour == 11)
                {
                    int ca = BUS.AssignmentBUS.LayCaTheoGio(chkouthour + 1);
                    DataTable _ds = BUS.AssignmentBUS.NVLamTrongCa(ca.ToString(), _id);
                    if (_ds.Rows.Count > 0)
                    {
                        //_giolam += 11 - ck_i_hour;
                        ck_i_hour = 11;
                        ck.Checkout1 = 11;

                        MessageBox.Show("Bạn Còn Ca 2 Trong Ngày !!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int _ca = BUS.AssignmentBUS.LayCaTheoGio(chkouthour + 7);
                        DataTable _ds_ = BUS.AssignmentBUS.NVLamTrongCa(_ca.ToString(), _id);
                        if (_ds_.Rows.Count > 0)
                        {
                            //MessageBox.Show("Bạn Còn Ca 3 Trong Ngày !!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //_giolam += 11 - ck_i_hour;
                            ck_i_hour = 18;
                            ck.Checkout1 = 11;

                            MessageBox.Show("Bạn Còn Ca 3 Trong Ngày !!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Hết Ca 1", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    //_giolam += 11 - ck_i_hour;
                            ck.Checkout1 = 11;
                            //     MessageBox.Show("Bạn Còn Ca 3 Trong Ngày !!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                        }
                    }
                }
                else if (chkouthour == 15)
                {
                    int ca = BUS.AssignmentBUS.LayCaTheoGio(chkouthour + 3);
                    DataTable _ds = BUS.AssignmentBUS.NVLamTrongCa(ca.ToString(), _id);
                    if (_ds.Rows.Count > 0)
                    {
                        //_giolam += 15 - ck_i_hour;
                        ck_i_hour = 18;
                        ck.Checkout2 = 15;

                        MessageBox.Show("Bạn Còn Ca 3 Trong Ngày !!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ck.Checkout2 = 15;

                        MessageBox.Show("Hết Ca 2", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //_giolam += 15 - ck_i_hour;


                    }
                }
                else if (chkouthour == 22)
                {
                    ck.Checkout3 = 22;

                    MessageBox.Show("Hết Ca 3", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //_giolam += 22 - ck_i_hour;
                }
                else
                {
                    if ((MessageBox.Show("Đang trong giờ làm ! Bạn có chắc muốn check out??", "CheckOUT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {

                        if (chkouthour > 18 && chkouthour < 22)
                            ck.Checkout3 = chkouthour;
                        else if (chkouthour > 11 && chkouthour < 15)
                            ck.Checkout2 = chkouthour;
                        else
                            ck.Checkout1 = chkouthour;

                        MessageBox.Show("Check Out Xong", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //_giolam += chkouthour - ck_i_hour;
                    }
                }
                CheckinBUS.CapNhatCheckOut(ck);
                DataTable ds = CheckinBUS.LayDSCheckinMotNhanVien(_id);
                DataTable dt = AssignmentBUS.LayDSChiaCaTuMaNV(_id);
                if (int.Parse(ds.Rows[0][1].ToString()) != 0 || int.Parse(ds.Rows[0][3].ToString()) != 0)
                    if (int.Parse(dt.Rows[0][9].ToString()) == 1)
                    {
                        TinhTienLuongNgay();
                        TinhTienThuongNgay();
                        CapNhatBangLuongThuong();
                    }

                dgvCheckin.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Chỉ Nhân Viên mới sử dụng chức năng này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }    
        }
        private void TinhTienLuongNgay()
        {
            //DataTable _dt = AssignmentBUS.LayDSChiaCa();
            DataTable _dt = CheckinBUS.LayDSCheckinMotNhanVien(_id);
            CheckinDTO ck = new CheckinDTO();
            ck.MaNV = _id;
            int giolam1 = 0;int giolam2 = 0;int giolam3 = 0;
            // tinh gio lam ca 1
            giolam1 = int.Parse(_dt.Rows[0][2].ToString()) - int.Parse(_dt.Rows[0][1].ToString());

            // tinh gio lam ca 2
            giolam2 = int.Parse(_dt.Rows[0][4].ToString()) - int.Parse(_dt.Rows[0][3].ToString());

            // tinh gio lam ca 3
            giolam3 = int.Parse(_dt.Rows[0][6].ToString()) - int.Parse(_dt.Rows[0][5].ToString());
            int gioLam = giolam1+giolam2 + giolam3;
            ck.GioLam = gioLam;
            if (CheckinBUS.CapNhatGioLam(ck))//theem gio lam vao data
            {

                float salary = 0, psh_salary = 0;
                if (gioLam >= 8)
                {
                    salary = gioLam * 50000;
                }
                else if ( gioLam< 8)
                {
                    salary = gioLam * 50000 - 100000 * (8 - gioLam);
                    psh_salary = 100000 * (8 - gioLam);

                    if (salary < 0)
                    {
                        salary = 0;
                    }
                }
 
                DataTable _ds = AssignmentBUS.LayDSChiaCaTuMaNV(_id);
                float _sal= float.Parse(_ds.Rows[0][4].ToString());
                float _salary= (float)Math.Round(_sal + salary, 1);
                
                if (BUS.AssignmentBUS.ThemLuongTheoNgay(_salary, psh_salary, _id))//them luong va tien phat
                {

                    MessageBox.Show(salary.ToString(), "Tiền Lương Ngày Hôm Nay", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    

                }
            } 
        }
        private void TinhTienThuongNgay()
        {
            DataTable _ds = AssignmentBUS.LayDSChiaCaTuMaNV(_id);
            if (int.Parse(_ds.Rows[0][6].ToString())!=0)
            { float punish_sal = AssignmentBUS.TongTienPhatTrong1Ca(int.Parse(_ds.Rows[0][6].ToString()));
                DataTable ds = AssignmentBUS.LayLuongNhanVienCungCa(int.Parse(_ds.Rows[0][6].ToString()));
                int soluong = ds.Rows.Count;

                float _exsalary = (float)Math.Round(punish_sal / (soluong), 1);
                if (BUS.AssignmentBUS.ThemTienThuong(_exsalary, int.Parse(_ds.Rows[0][6].ToString())))//them tien thuong 
                {

                } 
            }  
        }
        private void CapNhatBangLuongThuong()
        {
            DataTable dt = AssignmentBUS.LayDSChiaCa();
            DataTable _dt = StaffBUS.LayDSNhanVien();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                float Luong = float.Parse(dt.Rows[i][4].ToString()) + float.Parse(dt.Rows[i][8].ToString());
                float Luongbandau = float.Parse(_dt.Rows[i][3].ToString());
                if(StaffBUS.CapNhatLuongNhanVien(_dt.Rows[i][0].ToString(),Luongbandau+Luong))
                { 

                }    
            }
        }





        /////////////////////////////////////   KẾT Thúc CHeckIN /////////////////////////////
        ///

        /////////////////////////////////////////    Thực Đơn   /////////////////////////////////

        private void LoadLoaiThucDonIntoCombobox()
        {
            List<LoaiThucDonDTO> _dsltd = LoaiThucDonBUS.LayDSLoaiThucDon();
            cmB_loaitd_TD.DataSource = _dsltd;
            cmB_loaitd_TD.DisplayMember = "TenLoai";
            cmB_loaitd_TD.ValueMember = "MaLoai";



            cmb_LoaiThucDon.DataSource = _dsltd;
            cmb_LoaiThucDon.DisplayMember = "TenLoai";
            cmb_LoaiThucDon.ValueMember = "MaLoai";

            cmB_loaitdTD.DataSource = _dsltd;
            cmB_loaitdTD.DisplayMember = "TenLoai";
            cmB_loaitdTD.ValueMember = "MaLoai";


            cmB_loaitdCNGM.DataSource = _dsltd;
            cmB_loaitdCNGM.DisplayMember = "TenLoai";
            cmB_loaitdCNGM.ValueMember = "MaLoai";
        }
        public void ThemThucDon()
        {
            ThucDonDTO td = new ThucDonDTO();
            GiaDTO g = new GiaDTO();
            td.MaTD = ThucDonBUS.MaTuTang();
            td.MaLoai = LoaiThucDonBUS.LayMaLoaiTuTenLoai(cmB_loaitd_TD.Text);
            td.TenTD = tbx_tentd_TD.Text;
            td.DonViTinh = tbx_donvitinhTD.Text;

            g.MaTD = td.MaTD;
            g.NgayADGia = dtime_ngaythemTD.Value;

            try
            {
                g.Gia = double.Parse(tbx_dongiaTD.Text);
                bool kt = ThucDonBUS.KiemTraTrungTenThucDon(td.TenTD);
                if (kt == true)
                {
                    bool kq1 = ThucDonBUS.ThemThucDon(td);
                    bool kq2 = GiaBUS.ThemGia(g);
                    if (kq1 == true && kq2 == true)
                        MessageBox.Show("Thêm thực đơn thành công!", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Thực đơn này đã có!", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Kiểu dữ liệu nhập đơn giá không chính xác! Vui lòng nhập lại đơn giá!");
            }
        }

        private void btn_ThemTD_Click(object sender, EventArgs e)
        {
            if (_id == "MNG1" || _id =="MNG2")
            {
                if (tbx_tentd_TD.Text != "")
                {
                    if (tbx_dongiaTD.Text != "")
                    {
                        if (dtime_ngaythemTD.Text != "")
                        {
                            if (tbx_donvitinhTD.Text != "")
                            {
                                ThemThucDon();
                            }
                            else
                                MessageBox.Show("Chưa nhập đơn vị tính!");
                        }
                        else
                            MessageBox.Show("Chưa nhập ngày áp dụng đơn giá!");
                    }
                    else
                        MessageBox.Show("Chưa nhập đơn giá!");
                }
                else
                    MessageBox.Show("Chưa nhập tên thực đơn!");
            }
            else
            {
                MessageBox.Show("Chỉ có Quản Lý mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        ///
        private void btn_EditTD_Click(object sender, EventArgs e)// Sửa Thực đơn
        {
            if (_id == "MNG1" || _id == "MNG2")
            {
                ThucDonDTO td = new ThucDonDTO();
                GiaDTO g = new GiaDTO();

                int index = dgv_DSTD.CurrentRow.Index;
                if (tbx_tentd_TD.Text != "")
                {

                    if (tbx_donvitinhTD.Text != "")
                    {
                        td.MaTD = int.Parse(dgv_DSTD.Rows[index].Cells[0].Value.ToString());
                        td.MaLoai = LoaiThucDonBUS.LayMaLoaiTuTenLoai(cmB_loaitd_TD.Text);
                        td.TenTD = tbx_tentd_TD.Text;
                        td.DonViTinh = tbx_donvitinhTD.Text;
                        bool kt;
                        kt = ThucDonBUS.KiemTraTenTDCapNhat(tbx_tentd_TD.Text, td.MaTD);
                        if (kt == true)
                        {
                            g.MaTD = td.MaTD;
                            if (dtime_ngaythemTD.Text != "")
                            {
                                g.NgayADGia = DateTime.Parse(dtime_ngaythemTD.Text);

                                try
                                {
                                    double gia = double.Parse(tbx_dongiaTD.Text);
                                    if (gia > 0)
                                    {
                                        g.Gia = gia;
                                        bool kq1 = ThucDonBUS.CapNhatThucDon(td);
                                        bool kq2 = GiaBUS.CapNhatGia(g);
                                        if (kq1 == true && kq2 == true)
                                        {
                                            MessageBox.Show("Cập nhật thực đơn thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            tbx_tentd_TD.Text = "";
                                            tbx_dongiaTD.Text = "";
                                            dtime_ngaythemTD.Text = "";
                                            tbx_donvitinhTD.Text = "";
                                            cmB_loaitdTD_SelectedIndexChanged(sender, e);
                                        }
                                        else
                                            MessageBox.Show("Cập nhật thực đơn thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Đơn giá phải lớn hơn 0!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        tbx_dongiaTD.Text = "";
                                        tbx_dongiaTD.Focus();
                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("Chưa nhập đơn giá hoặc kiểu dữ liệu đơn giá không đúng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    tbx_dongiaTD.Text = "";
                                    tbx_dongiaTD.Focus();
                                }
                            }
                            else
                                MessageBox.Show("Chưa nhập ngày áp dụng giá!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Tên thực đơn bị trùng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tbx_tentd_TD.Text = "";
                            tbx_tentd_TD.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chưa nhập đơn vị tính!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbx_donvitinhTD.Text = "";
                        tbx_donvitinhTD.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Chưa nhập tên thực đơn!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbx_tentd_TD.Text = "";
                    tbx_tentd_TD.Focus();
                }
            }
            else
            {
                MessageBox.Show("Chỉ có Quản Lý mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void btn_Xoa_Click(object sender, EventArgs e)//Xóa Thức Đơn
        {
            if (_id == "MNG1" || _id == "MNG2")
            {
                try
                {
                    int index = dgv_DSTD.CurrentRow.Index;
                    int maTD = int.Parse(dgv_DSTD.Rows[index].Cells[0].Value.ToString());
                    DateTime ngayAD = DateTime.Parse(dtime_ngaythemTD.Text);
                    DialogResult result = MessageBox.Show("Chắn chắn xóa?!!!", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                    if (result == DialogResult.Yes)
                    {
                        bool kq1, kq2;
                        try
                        {
                            kq1 = GiaBUS.XoaGiaTheoMaTDVaNgayAD(maTD, ngayAD);
                            kq2 = ThucDonBUS.XoaThucDonTheoMaTD(maTD);

                            if (kq1 == true && kq2 == true)
                            {
                                MessageBox.Show("Xóa thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tbx_tentd_TD.Text = "";
                                tbx_dongiaTD.Text = "";
                                dtime_ngaythemTD.Text = "";
                                tbx_donvitinhTD.Text = "";

                                if (tbx_tentdTD.Text != "")
                                    btn_TimTD_Click(sender, e);
                                if (cmB_loaitdTD.Text != "")
                                    cmB_loaitdTD_SelectedIndexChanged(sender, e);
                            }
                            else
                                MessageBox.Show("Xóa thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch
                        {
                            MessageBox.Show("Thực đơn đã được gọi món hoặc có trong hóa đơn. Không thể xóa!!!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Chưa chọn thực đơn cần xóa!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Chỉ có Quản Lý mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_TimTD_Click(object sender, EventArgs e)// Tìm Thực Đơn
        {
            tbx_tentd_TD.Text = "";
            cmB_loaitdTD.Text = "";
            dtime_ngaythemTD.Text = "";
            tbx_dongiaTD.Text = "";
            tbx_donvitinhTD.Text = "";
            if (tbx_tentdTD.Text == "")
                MessageBox.Show("Chưa nhập tên thực đơn cần tra cứu!", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DataTable kq = ThucDonBUS.TraCuuThucDonTheoTen(tbx_tentdTD.Text);
                dgv_DSTD.DataSource = kq;
            }
        }

        private void dgv_DSTD_Click(object sender, EventArgs e)
        {

            int index = dgv_DSTD.CurrentRow.Index;
            tbx_tentd_TD.Text = dgv_DSTD.Rows[index].Cells[1].Value.ToString();
            tbx_dongiaTD.Text = dgv_DSTD.Rows[index].Cells[2].Value.ToString();
            dtime_ngaythemTD.Text = dgv_DSTD.Rows[index].Cells[3].Value.ToString();
            tbx_donvitinhTD.Text = dgv_DSTD.Rows[index].Cells[4].Value.ToString();
            cmB_loaitd_TD.Text = dgv_DSTD.Rows[index].Cells[5].Value.ToString();
        }


        public void LoadBanIntoCombobox()
        {
            List<BanDTO> _dsban = BanBUS.LayDSBan();
            List<int> _dsBanDaDat = HoaDonBUS.LayDSBanChuaThanhToan();
            List<int> _dsTam = new List<int>();
            for (int i = 0; i < _dsban.Count; i++)
            {
                bool flag = false;
                for (int j = 0; j < _dsBanDaDat.Count; j++)
                {
                    if (_dsban[i].MaBan == _dsBanDaDat[j])
                    {
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    _dsTam.Add(int.Parse(_dsban[i].MaBan.ToString()));
                }
            }
            cmB_ChonBan.DataSource = _dsTam;
            //cmb_chonbanCN.DataSource = _dsTam;
        }
        //public void DuaDSBanDaGoiLenCombobox()
        //{
        //    cmbDSBanCanLapHD.Items.Clear();
        //    cmbDSBanCapNhat.Items.Clear();
        //    cmbDSBanCanLapHD.Text = "";
        //    cmbDSBanCapNhat.Text = "";
        //    List<int> _dsMaBan = HoaDonBUS.LayDSBanChuaThanhToan();
        //    for (int i = 0; i < _dsMaBan.Count; i++)
        //    {
        //        cmbDSBanCanLapHD.Items.Add(_dsMaBan[i].ToString());
        //        cmbDSBanCapNhat.Items.Add(_dsMaBan[i].ToString());
        //    }
        //}

        private void btn_ThemMon_Click(object sender, EventArgs e)
        {
            if (tbx_DonGia.Text != "")
            {
                int maTD = int.Parse(lb_DSTD.SelectedValue.ToString());
                string tenTD = ThucDonBUS.LayTenThucDonTuMaThucDon(maTD);
                bool tonTai = false;
                int dong = 0;
                for (int i = 0; i < lv_CTgoimon.Items.Count; i++)
                {
                    if (int.Parse(lv_CTgoimon.Items[i].SubItems[0].Text) == maTD)
                    {
                        tonTai = true;
                        dong = i;
                    }
                }
                //string soLuong = "1";
                
                string soLuong = numerud_SoLuong.Text;
                
                if (tonTai == false)
                {
                    string donGia = tbx_DonGia.Text;
                    string thanhTien = (double.Parse(donGia) * double.Parse(soLuong)).ToString() ;
                    ListViewItem item = new ListViewItem();
                    item.Text = maTD.ToString();
                    item.SubItems.Add(tenTD);
                    item.SubItems.Add(donGia);
                    item.SubItems.Add(soLuong);
                    item.SubItems.Add(thanhTien);
                    this.lv_CTgoimon.Items.Add(item);
                    numerud_SoLuong.Text = "1";
                }
                else
                {
                    int sl = int.Parse(lv_CTgoimon.Items[dong].SubItems[3].Text) + int.Parse(soLuong);
                    double thanhTien = double.Parse(tbx_DonGia.Text) * sl;
                    lv_CTgoimon.Items[dong].SubItems[3].Text = sl.ToString();
                    lv_CTgoimon.Items[dong].SubItems[4].Text = thanhTien.ToString();

                }
            }
            else
            {
                MessageBox.Show("Bạn nhập giá không chính xác!");
            }
        }

        private void lb_DSTD_Click(object sender, EventArgs e)
        {
            int maTD = int.Parse(lb_DSTD.SelectedValue.ToString());
            double gia = GiaBUS.LayGiaTheoMaThucDon(maTD);
            lbl_GTK.Text = gia.ToString();
            //tbx_DonGia.Text = Convert.ToString(double.Parse(lbl_GTK.Text));
            tbx_DonGia.Text = gia.ToString();
        }

        private void cmb_LoaiThucDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ThucDonDTO> _dstd = new List<ThucDonDTO>();
            string tenLoai = cmb_LoaiThucDon.Text.ToString();
            int maLoaiTD = LoaiThucDonBUS.LayMaLoaiTuTenLoai(tenLoai);
            _dstd = ThucDonBUS.LayDSThucDonTheoMaLoai(maLoaiTD);
            lb_DSTD.DataSource = _dstd;
            lb_DSTD.DisplayMember = "TenTD";
            lb_DSTD.ValueMember = "MaTD";
            tbx_DonGia.Text = "";
            lbl_GTK.Text = "0";
        }

        private void numerud_SoLuong_ValueChanged(object sender, EventArgs e)
        {
            lbl_GTK.Text = (double.Parse(tbx_DonGia.Text) * int.Parse(numerud_SoLuong.Value.ToString())).ToString();
        }

        private void btn_XoaTD_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có muốn xóa thực đơn này không?", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                if (result == DialogResult.Yes)
                {
                    lv_CTgoimon.FocusedItem.Remove();
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn Thực Đơn cần xóa!");
            }
        }

        private void btn_XoaDSTD_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa hết danh sách không?", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
            if (result == DialogResult.Yes)
            {
                lv_CTgoimon.Items.Clear();
            }
        }

        private void cmB_loaitd_TD_SelectedIndexChanged(object sender, EventArgs e)
        {
            int maLoai = LoaiThucDonBUS.LayMaLoaiTuTenLoai(cmB_loaitd_TD.Text);
            DataTable dsTD = ThucDonBUS.LayDanhSachTDTheoMaLoai(maLoai);

            dgv_DSTD.DataSource = dsTD;
            tbx_tentdTD.Text = "";
        }

        private void cmB_loaitdTD_SelectedIndexChanged(object sender, EventArgs e)
        {
            int maLoai = LoaiThucDonBUS.LayMaLoaiTuTenLoai(cmB_loaitd_TD.Text);
            DataTable dsTD = ThucDonBUS.LayDanhSachTDTheoMaLoai(maLoai);

            dgv_DSTD.DataSource = dsTD;
            tbx_tentdTD.Text = "";
        }

        private void btn_LuuGoiMonGM_Click(object sender, EventArgs e)
        {
            if (lv_CTgoimon.Items.Count > 0)
            {
                if (tbx_SoKhach.Text != "")
                {
                    HoaDonDTO hd = new HoaDonDTO();
                    CTHoaDonDTO cthd = new CTHoaDonDTO();
                    hd.MsBan = int.Parse(cmB_ChonBan.Text);
                    int maHD = HoaDonBUS.LayMaHoaDonCanLap();
                    hd.TongTien = 0;
                    hd.MsNVLap = _id;

                    hd.MsNVTT = _id;
                    int soKhach = int.Parse(tbx_SoKhach.Text);
                    if (soKhach > 0)
                    {
                        hd.SoKhach = soKhach;

                        bool kq = HoaDonBUS.LapHoaDon(hd);
                        if (kq == true)
                        {
                            for (int i = 0; i < lv_CTgoimon.Items.Count; i++)
                            {
                                cthd.SoHD = hd.SoHD;
                                cthd.MaTD = int.Parse(lv_CTgoimon.Items[i].SubItems[0].Text);
                                cthd.DonGia = double.Parse(lv_CTgoimon.Items[i].SubItems[2].Text);
                                cthd.SoLuong = int.Parse(lv_CTgoimon.Items[i].SubItems[3].Text);
                                CTHoaDonBUS.ThemChiTietHoaDon(cthd);
                            }
                            MessageBox.Show("Lưu gọi món thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DuaDSBanDaGoiLenCombobox();
                            LoadBanIntoCombobox();
                            lv_CTgoimon.Items.Clear();
                            tbx_DonGia.Text = "";
                            tbx_SoKhach.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số khách phải lớn hơn 0!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbx_SoKhach.Text = "";
                        tbx_SoKhach.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Chưa nhập số lượng khách!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbx_SoKhach.Text = "";
                    tbx_SoKhach.Focus();
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn thực đơn!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DuaDSBanDaGoiLenCombobox()
        {
            cmB_DSBanCanLHD.Items.Clear();
            cmb_chonbanCN.Items.Clear();
            cmB_DSBanCanLHD.Text = "";
            cmb_chonbanCN.Text = "";
            List<int> _dsMaBan = HoaDonBUS.LayDSBanChuaThanhToan();
            for (int i = 0; i < _dsMaBan.Count; i++)
            {
                cmB_DSBanCanLHD.Items.Add(_dsMaBan[i].ToString());
                cmb_chonbanCN.Items.Add(_dsMaBan[i].ToString());
            }
        }

        private void btn_addCN_Click(object sender, EventArgs e)
        {
            if (cmb_chonbanCN.Text != "")
            {
                if (tbx_dongiaCN.Text != "")
                {
                    int maTD = int.Parse(lb_dstdCNGM.SelectedValue.ToString());
                    string tenTD = ThucDonBUS.LayTenThucDonTuMaThucDon(maTD);
                    bool tonTai = false;
                    int dong = 0;
                    for (int i = 0; i < lv_ctgmCNGM.Items.Count; i++)
                    {
                        if (int.Parse(lv_ctgmCNGM.Items[i].SubItems[0].Text) == maTD)
                        {
                            tonTai = true;
                            dong = i;
                        }
                    }
                    //string soLuong = "1";

                    string soLuong = numeric_solCN.Text;

                    if (tonTai == false)
                    {
                        string donGia = tbx_dongiaCN.Text;
                        double thanhTienCN = double.Parse(donGia) * double.Parse(soLuong);
                        ListViewItem item = new ListViewItem();
                        item.Text = maTD.ToString();
                        item.SubItems.Add(tenTD);
                        item.SubItems.Add(donGia);
                        item.SubItems.Add(soLuong);
                        item.SubItems.Add(thanhTienCN.ToString());
                        this.lv_ctgmCNGM.Items.Add(item);
                        numeric_solCN.Text = "1";
                    }
                    else
                    {
                        int sl = int.Parse(lv_ctgmCNGM.Items[dong].SubItems[3].Text) + int.Parse(soLuong);
                        double thanhtienCN = double.Parse(tbx_dongiaCN.Text) * sl;
                        lv_ctgmCNGM.Items[dong].SubItems[3].Text = sl.ToString();
                        lv_ctgmCNGM.Items[dong].SubItems[4].Text = thanhtienCN.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn nhập giá không chính xác!");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn bàn cần cập nhật!");
            }
        }

        private void cmB_loaitdCNGM_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ThucDonDTO> _dstd = new List<ThucDonDTO>();
            string tenLoai = cmB_loaitdCNGM.Text.ToString();
            int maLoaiTD = LoaiThucDonBUS.LayMaLoaiTuTenLoai(tenLoai);
            _dstd = ThucDonBUS.LayDSThucDonTheoMaLoai(maLoaiTD);
            lb_dstdCNGM.DataSource = _dstd;
            lb_dstdCNGM.DisplayMember = "TenTD";
            lb_dstdCNGM.ValueMember = "MaTD";
            tbx_dongiaCN.Text = "";
            lbl_giatkCN.Text = "0";
        }

        private void lb_dstdCNGM_Click(object sender, EventArgs e)
        {
            int maTD = int.Parse(lb_dstdCNGM.SelectedValue.ToString());
            double gia = GiaBUS.LayGiaTheoMaThucDon(maTD);
            lbl_giatkCN.Text = gia.ToString();
            //tbx_DonGia.Text = Convert.ToString(double.Parse(lbl_GTK.Text));
            tbx_dongiaCN.Text = gia.ToString();
        }

        private void btn_LuuGoiMonCN_Click(object sender, EventArgs e)
        {
            if (lv_ctgmCNGM.Items.Count > 0)
            {
                if (tbx_sokCN.Text != "")
                {
                    HoaDonDTO hd = new HoaDonDTO();
                    CTHoaDonDTO cthd = new CTHoaDonDTO();
                    hd.MsBan = int.Parse(cmb_chonbanCN.Text);
                    hd.SoKhach = int.Parse(tbx_sokCN.Text);
                    hd.SoHD = HoaDonBUS.LaySoHDTuMaBan(int.Parse(cmb_chonbanCN.Text));
                    HoaDonBUS.CapNhatSoKhach(hd.SoKhach, hd.SoHD);
                    bool kq = CTHoaDonBUS.XoaCTHDTheoSoHD(hd.SoHD);

                    for (int i = 0; i < lv_ctgmCNGM.Items.Count; i++)
                    {
                        cthd.SoHD = hd.SoHD;
                        cthd.MaTD = ThucDonBUS.LayMaThucDonTuTenThucDon(lv_ctgmCNGM.Items[i].SubItems[1].Text);
                        cthd.DonGia = double.Parse(lv_ctgmCNGM.Items[i].SubItems[2].Text);
                        cthd.SoLuong = int.Parse(lv_ctgmCNGM.Items[i].SubItems[3].Text);
                        CTHoaDonBUS.ThemChiTietHoaDon(cthd);
                    }
                    if (kq == true)
                    {
                        MessageBox.Show("Cập nhật gọi món thành công!");
                        DuaDSBanDaGoiLenCombobox();
                        LoadBanIntoCombobox();
                        lv_ctgmCNGM.Items.Clear();
                        tbx_dongiaCN.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Nhập số lượng khách!");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn thực đơn!");
            }
        }

        private void numeric_solCN_ValueChanged(object sender, EventArgs e)
        {
            lbl_giatkCN.Text = (double.Parse(tbx_dongiaCN.Text) * int.Parse(numeric_solCN.Value.ToString())).ToString();
        }

        private void btn_CapNhatMoiGM_Click(object sender, EventArgs e)
        {
            if (lv_ctgmCNGM.Items.Count > 0)
            {
                if (tbx_sokCN.Text != "")
                {
                    HoaDonDTO hd = new HoaDonDTO();
                    CTHoaDonDTO cthd = new CTHoaDonDTO();
                    hd.MsBan = int.Parse(cmb_chonbanCN.Text);
                    hd.SoKhach = int.Parse(tbx_sokCN.Text);
                    hd.SoHD = HoaDonBUS.LaySoHDTuMaBan(int.Parse(cmb_chonbanCN.Text));
                    HoaDonBUS.CapNhatSoKhach(hd.SoKhach, hd.SoHD);
                    //bool kq = CTHoaDonBUS.XoaCTHDTheoSoHD(hd.SoHD);

                    for (int i = 0; i < lv_ctgmCNGM.Items.Count; i++)
                    {
                        cthd.SoHD = hd.SoHD;
                        cthd.MaTD = ThucDonBUS.LayMaThucDonTuTenThucDon(lv_ctgmCNGM.Items[i].SubItems[1].Text);
                        cthd.DonGia = double.Parse(lv_ctgmCNGM.Items[i].SubItems[2].Text);
                        cthd.SoLuong = int.Parse(lv_ctgmCNGM.Items[i].SubItems[3].Text);
                        CTHoaDonBUS.ThemChiTietHoaDon(cthd);
                    }
                    MessageBox.Show("Cập nhật gọi món thành công!");
                    DuaDSBanDaGoiLenCombobox();
                    LoadBanIntoCombobox();
                    lv_ctgmCNGM.Items.Clear();
                    tbx_dongiaCN.Text = "";
                }
                else
                {
                    MessageBox.Show("Nhập số lượng khách!");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn thực đơn!");
            }
        }

        private void btn_tinhtienLHD_Click(object sender, EventArgs e)
        {
            if (cmB_DSBanCanLHD.Text == "")
                MessageBox.Show("Chưa chọn bàn cần tính!");
            else
            {
                double tongTien = 0;
                for (int i = 0; i < lv_dsctgmLHD.Items.Count; i++)
                {
                    double donGia = double.Parse(lv_dsctgmLHD.Items[i].SubItems[1].Text);
                    int soLuong = int.Parse(lv_dsctgmLHD.Items[i].SubItems[2].Text);
                    tongTien += donGia * soLuong;
                }
                lbl_tinhtienLHD.Text = tongTien.ToString();
            }
        }

        private void cmB_DSBanCanLHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmB_nguoilhdLHD.Text = "";
            lv_dsctgmLHD.Items.Clear();
            DataTable _ds = new DataTable();
            int maBan = int.Parse(cmB_DSBanCanLHD.Text);
            int maHD = HoaDonBUS.LaySoHDTuMaBan(maBan);
            _ds = CTHoaDonBUS.LayDSCTHDTuMaHD(maHD);
            for (int i = 0; i < _ds.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = _ds.Rows[i][0].ToString();
                item.SubItems.Add(_ds.Rows[i][1].ToString());
                item.SubItems.Add(_ds.Rows[i][2].ToString());
                item.SubItems.Add((double.Parse(_ds.Rows[i][1].ToString())*double.Parse(_ds.Rows[i][2].ToString())).ToString());
                lv_dsctgmLHD.Items.Add(item);
            }

            int gioLap = HoaDonBUS.LayGioLapHDChuaThanhToanTheoMaBan(maBan);
            int ca = AssignmentBUS.LayCaTheoGio(gioLap);
            cmB_nguoilhdLHD.Text=HoaDonBUS.LayMaNVTTTuSoHD(maHD);
            //if(ca!=0)
            //{
            //    //cmB_nguoilhdLHD..Text=
            //}
              
        }
        public void DuaDSHoaDonLenDataGridView()
        {
            DataTable _dshd = HoaDonBUS.LayDSHoaDon();
            //dtgDSHD.DataSource = _dshd;
        }
        private void btn_lhdLHD_Click(object sender, EventArgs e)
        {
            if (lv_dsctgmLHD.Items.Count > 0)
            {
                if (cmB_nguoilhdLHD.Text == "")
                    MessageBox.Show("Chưa chọn nhân viên tiếp tân!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (lbl_tinhtienLHD.Text == "0")
                        MessageBox.Show("Chưa tính tổng tiền!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        HoaDonDTO hd = new HoaDonDTO();
                        //hd.MsNVTT = cmB_nguoilhdLHD.SelectedValue.ToString();
                        hd.MsNVTT = cmB_nguoilhdLHD.Text;
                        hd.MsBan = int.Parse(cmB_DSBanCanLHD.Text);
                        hd.SoHD = HoaDonBUS.LaySoHDTuMaBan(hd.MsBan);
                        hd.TongTien = double.Parse(lbl_tinhtienLHD.Text);
                        bool kq = HoaDonBUS.CapNhatLapHoaDon(hd);
                        if (kq == true)
                        {
                            MessageBox.Show("Lập hóa đơn thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DuaDSHoaDonLenDataGridView();
                            LoadBanIntoCombobox();
                            loadThangvaoComboBox();
                            lv_dsctgmLHD.Items.Clear();
                            DuaDSBanDaGoiLenCombobox();
                            lbl_tinhtienLHD.Text = "0";
                            DialogResult result = MessageBox.Show("Bạn có muốn in hóa đơn này ra không!", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                            if (result == DialogResult.Yes)
                            {

                                GUI.HoaDon.XemHoaDon frm = new GUI.HoaDon.XemHoaDon();
                                frm.SoHD = hd.SoHD;
                                frm.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lập hóa đơn thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn bàn!");
            }
        }

       // Quan ly hoa don
        
        private void btn_TraCuuTN_QLHD_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngay = dti_ngayQLHD.Value;
                DataTable kq = HoaDonBUS.ThongKeHDTheoNgay(ngay);
                dgv_dshdQLHD.DataSource = kq;
            }
            catch
            {
                MessageBox.Show("Mời chọn ngày cần tra cứu!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_Tracuuthang_QLHD_Click(object sender, EventArgs e)
        {
            try
            {
                int thang = int.Parse(cmB_thang_QLHD.Text);
                int nam = int.Parse(tbx_nam_QLHD.Text);
                DataTable dt = HoaDonBUS.ThongKeHDTheoThang(thang, nam);
                dgv_dshdQLHD.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Mời chọn tháng cần tra cứu!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_XoangayQLHD_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngay = dti_ngayQLHD.Value;
                DataTable dt = HoaDonBUS.ThongKeHDTheoNgay(ngay);
                XoaDSHD(dt);
            }
            catch
            {
                MessageBox.Show("Xóa hóa đơn không thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaDSHD(DataTable dt)
        {
            if (_id=="MNG1"||_id=="MNG2")
            {
                DialogResult result = MessageBox.Show("Chắn chắn xóa?!!!", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                if (result == DialogResult.Yes)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int soHD = int.Parse(dt.Rows[i][0].ToString());
                        CTHoaDonBUS.XoaCTHDTheoSoHD(soHD);
                        HoaDonBUS.XoaHDTheoSoHD(soHD);
                    }
                    MessageBox.Show("Xóa hóa đơn thành công!");
                    DuaDSHoaDonLenDataGridView();

                    dgv_dscthdQLHD.DataSource = null;
                    try
                    {
                        int idx2 = dgv_dshdQLHD.CurrentRow.Index;
                        int maHD = int.Parse(dgv_dshdQLHD.Rows[idx2].Cells[0].Value.ToString());
                        DataTable _ds = CTHoaDonBUS.LayDSCTHDTuMaHD(maHD);
                        dgv_dscthdQLHD.DataSource = _ds;
                    }
                    catch { }
                }
            }
            else
            {
                MessageBox.Show("Chỉ có Quản Lý mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_xoathang_QLHD_Click(object sender, EventArgs e)
        {
            try
            {
                int thang = int.Parse(cmB_thang_QLHD.Text);
                int nam = int.Parse(tbx_nam_QLHD.Text);
                DataTable dt = HoaDonBUS.ThongKeHDTheoThang(thang, nam);
                dgv_dshdQLHD.DataSource = dt;
                XoaDSHD(dt);
            }
            catch
            {
                MessageBox.Show("Xóa hóa đơn không thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_inhdQLHD_Click(object sender, EventArgs e)
        {
            try
            {
                HoaDon.XemHoaDon frm = new HoaDon.XemHoaDon();
                int idx = dgv_dshdQLHD.CurrentRow.Index;
                frm.SoHD = int.Parse(dgv_dshdQLHD.Rows[idx].Cells[0].Value.ToString());
                frm.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Chưa chọn hóa đơn cần in!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoahd_QLHD_Click(object sender, EventArgs e)
        {
            if (_id=="MNg1"||_id=="MNG2")
            {
                DialogResult result = MessageBox.Show("Chắn chắn xóa?!!!", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        int idx = dgv_dshdQLHD.CurrentRow.Index;
                        int SoHD = int.Parse(dgv_dshdQLHD.Rows[idx].Cells[0].Value.ToString());
                        CTHoaDonBUS.XoaCTHDTheoSoHD(SoHD);
                        HoaDonBUS.XoaHDTheoSoHD(SoHD);
                        MessageBox.Show("Xóa Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DuaDSHoaDonLenDataGridView();
                        dgv_dscthdQLHD.DataSource = null;
                        try
                        {
                            int idx2 = dgv_dshdQLHD.CurrentRow.Index;
                            int maHD = int.Parse(dgv_dshdQLHD.Rows[idx2].Cells[0].Value.ToString());
                            DataTable _ds = CTHoaDonBUS.LayDSCTHDTuMaHD(maHD);
                            dgv_dscthdQLHD.DataSource = _ds;
                        }
                        catch { }
                    }
                    catch
                    {
                        MessageBox.Show("Không có hóa đơn thanh toán nào trong hệ thống!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Chỉ có Quản Lý mới có thể sử dụng chức năng này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgv_dshdQLHD_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = dgv_dshdQLHD.CurrentRow.Index;
                int maHD = int.Parse(dgv_dshdQLHD.Rows[idx].Cells[0].Value.ToString());
                DataTable _ds = CTHoaDonBUS.LayDSCTHDTuMaHD(maHD);
                dgv_dscthdQLHD.DataSource = _ds;
            }
            catch { }
        }

        private void loadThangvaoComboBox()
        {
            DataTable dt = HoaDonBUS.LayCacThangLapHD();
            cmB_thang_QLHD.DataSource = dt;
            cmB_thang_QLHD.DisplayMember = "ThangLap";
            cmB_thang_QLHD.ValueMember = "ThangLap";

            cmB_thangTK.DataSource = dt;
            cmB_thangTK.DisplayMember = "ThangLap";
            cmB_thangTK.ValueMember = "ThangLap";
        }
        /****************** Thống Kê **********************/
        int flag;
        private void btn_tktheongayTK_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngay = dti_ngayTK.Value;
                DataTable kq = HoaDonBUS.ThongKeHDTheoNgay(ngay);
                dgv_TK.DataSource = kq;
                ThongKe(kq);
                if (kq.Rows.Count > 0)
                    flag = 1;
                else
                    flag = 0;
            }
            catch
            {
                //MessageBox.Show("Mời chọn ngày cần thống kê!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void ThongKe(DataTable kq)
        {
            if (kq.Rows.Count > 0)
            {
                double tongDoanhThu = 0;
                int tongKhachDen = 0;
                for (int i = 0; i < dgv_TK.Rows.Count - 1; i++)
                {
                    tongDoanhThu += double.Parse(dgv_TK.Rows[i].Cells[5].Value.ToString());
                    tongKhachDen += int.Parse(dgv_TK.Rows[i].Cells[3].Value.ToString());
                }
                lbl_dtTK.Text = tongDoanhThu.ToString() + " VNÐ";
                lbl_soKhachTK.Text = tongKhachDen.ToString() + " Khách";

                DataTable _ds = new DataTable();
                _ds.Columns.Add("SoHD", typeof(int));
                _ds.Columns.Add("MaThucDon", typeof(int));
                _ds.Columns.Add("SoLuong", typeof(int));
                _ds.Columns.Add("DonGia", typeof(double));
                _ds.PrimaryKey = new DataColumn[] { _ds.Columns["SoHD"], _ds.Columns["MaThucDon"] };
                for (int i = 0; i < kq.Rows.Count; i++)
                {
                    int SoHD = int.Parse(kq.Rows[i][0].ToString());
                    if (_ds.Rows.Count == 0)
                    {
                        DataTable dtct = CTHoaDonBUS.LayDSCTHD(SoHD);
                        for (int j = 0; j < dtct.Rows.Count; j++)
                        {
                            DataRow ct = _ds.NewRow();
                            ct[0] = int.Parse(dtct.Rows[j][0].ToString());
                            ct[1] = int.Parse(dtct.Rows[j][1].ToString());
                            ct[2] = int.Parse(dtct.Rows[j][2].ToString());
                            ct[3] = double.Parse(dtct.Rows[j][3].ToString());
                            _ds.Rows.Add(ct);
                        }
                    }
                    else
                    {
                        DataTable dtct = CTHoaDonBUS.LayDSCTHD(SoHD);
                        for (int j = 0; j < dtct.Rows.Count; j++)
                        {
                            bool kt = false;
                            int dong = 0;
                            for (int k = 0; k < _ds.Rows.Count; k++)
                            {
                                if (dtct.Rows[j][1].ToString() == _ds.Rows[k][1].ToString())
                                {
                                    dong = k;
                                    kt = true;
                                }
                            }
                            if (kt == true)
                            {
                                _ds.Rows[dong][2] = int.Parse(_ds.Rows[dong][2].ToString()) + int.Parse(dtct.Rows[j][2].ToString());
                            }
                            else
                            {
                                DataRow ct = _ds.NewRow();
                                ct[0] = int.Parse(dtct.Rows[j][0].ToString());
                                ct[1] = int.Parse(dtct.Rows[j][1].ToString());
                                ct[2] = int.Parse(dtct.Rows[j][2].ToString());
                                ct[3] = double.Parse(dtct.Rows[j][3].ToString());
                                _ds.Rows.Add(ct);
                            }
                        }
                    }
                }

                DataTable _dstd = new DataTable();
                _dstd.Columns.Add("SoHD", typeof(int));
                _dstd.Columns.Add("MaThucDon", typeof(int));
                _dstd.Columns.Add("SoLuong", typeof(int));
                _dstd.Columns.Add("DonGia", typeof(double));
                _dstd.PrimaryKey = new DataColumn[] { _dstd.Columns["SoHD"], _dstd.Columns["MaThucDon"] };

                DataTable _dstu = new DataTable();
                _dstu.Columns.Add("SoHD", typeof(int));
                _dstu.Columns.Add("MaThucDon", typeof(int));
                _dstu.Columns.Add("SoLuong", typeof(int));
                _dstu.Columns.Add("DonGia", typeof(double));
                _dstu.PrimaryKey = new DataColumn[] { _dstu.Columns["SoHD"], _dstu.Columns["MaThucDon"] };

                for (int i = 0; i < _ds.Rows.Count; i++)
                {
                    if (ThucDonBUS.KiemTraThucAnNuocUong(int.Parse(_ds.Rows[i][1].ToString())))
                    {
                        DataRow ct = _dstd.NewRow();
                        ct[0] = int.Parse(_ds.Rows[i][0].ToString());
                        ct[1] = int.Parse(_ds.Rows[i][1].ToString());
                        ct[2] = int.Parse(_ds.Rows[i][2].ToString());
                        ct[3] = double.Parse(_ds.Rows[i][3].ToString());
                        _dstd.Rows.Add(ct);
                    }
                    else
                    {
                        DataRow ct = _dstu.NewRow();
                        ct[0] = int.Parse(_ds.Rows[i][0].ToString());
                        ct[1] = int.Parse(_ds.Rows[i][1].ToString());
                        ct[2] = int.Parse(_ds.Rows[i][2].ToString());
                        ct[3] = double.Parse(_ds.Rows[i][3].ToString());
                        _dstu.Rows.Add(ct);
                    }
                }

                if (_dstd.Rows.Count > 0)
                {
                    int MaxTD = int.Parse(_dstd.Rows[0][2].ToString());
                    for (int i = 0; i < _dstd.Rows.Count; i++)
                    {
                        int sl = int.Parse(_dstd.Rows[i][2].ToString());
                        if (MaxTD < sl)
                            MaxTD = int.Parse(_dstd.Rows[i][2].ToString());
                    }
                    int y = 0;
                    for (int i = 0; i < _dstd.Rows.Count; i++)
                    {
                        if (MaxTD == int.Parse(_dstd.Rows[i][2].ToString()))
                            y = i;
                    }

                    int MaTD = int.Parse(_dstd.Rows[y][1].ToString());
                    lbl_soluongbanTK.Text = MaxTD.ToString();
                    lbl_tdbcnTK.Text = ThucDonBUS.LayTenThucDonTuMaThucDon(MaTD);
                    lbl_dvtTDTK.Text = ThucDonBUS.LayDonViTinhTuMaTD(MaTD);
                }

                if (_dstu.Rows.Count > 0)
                {
                    int MaxTU = 0;
                    for (int i = 0; i < _dstu.Rows.Count; i++)
                    {
                        if (MaxTU < int.Parse(_dstu.Rows[i][2].ToString()))
                            MaxTU = int.Parse(_dstu.Rows[i][2].ToString());
                    }
                    int z = 0;
                    for (int i = 0; i < _dstu.Rows.Count; i++)
                    {
                        if (MaxTU == int.Parse(_dstu.Rows[i][2].ToString()))
                            z = i;
                    }

                    int MaTU = int.Parse(_dstu.Rows[z][1].ToString());
                    lbl_slbanTUTK.Text = MaxTU.ToString();
                    lbl_TUbcnTK.Text = ThucDonBUS.LayTenThucDonTuMaThucDon(MaTU);
                    lbt_dvtTUTK.Text = ThucDonBUS.LayDonViTinhTuMaTD(MaTU);
                }

            }
            else
            {
                lbl_tdbcnTK.Text = "Null";
                lbl_TUbcnTK.Text = "Null";
                lbl_dvtTDTK.Text = "Null";
                lbt_dvtTUTK.Text = "Null";
                lbl_soluongbanTK.Text = "0";
                lbl_slbanTUTK.Text = "0";
            }

        }

        private void btn_tkthangTK_Click(object sender, EventArgs e)
        {
            try
            {
                int thang = int.Parse(cmB_thangTK.Text);
                int nam = int.Parse(tbx_namTK.Text);
                DataTable dt = HoaDonBUS.ThongKeHDTheoThang(thang, nam);
                dgv_TK.DataSource = dt;
                ThongKe(dt);
                if (dt.Rows.Count > 0)
                    flag = 2;
                else
                    flag = 0;
            }
            catch
            {
                //MessageBox.Show("Mời chọn tháng cần thống kê!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_IntkngayTK_Click(object sender, EventArgs e)
        {
            if (dgv_TK.Rows.Count < 1 || flag == 0)
            {
                MessageBox.Show("Danh sách rỗng!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (flag == 1)
                {
                    HoaDon.ThongKeTheoNgayForm frm = new HoaDon.ThongKeTheoNgayForm();
                    frm.TongDoanhThu = lbl_dtTK.Text;
                    frm.Ngay = dti_ngayTK.Value;
                    frm.ShowDialog();
                }
                if (flag == 2)
                {
                    //ViewThongKeTheoThangNam frm = new ViewThongKeTheoThangNam();
                    //frm.ThangNam = cbThangTK.Text + "/" + tbNamTK.Text;
                    //frm.TongDoanhThu = lbTongDoanhThu.Text;
                    //frm.ShowDialog();
                }
                if (flag == 3)
                {
                    //ViewThongKeTheoKhoangNgay frm = new ViewThongKeTheoKhoangNgay();
                    //frm.TongDoanhThu = lbTongDoanhThu.Text;
                    //frm.TuNgay = dtiTuNgay.Value;
                    //frm.DenNgay = dtiDenNgay.Value;
                    //frm.ShowDialog();
                }
            }
        }

        private void Res_Management_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Cảnh Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, false);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        ///////////////////////// GỌI MÓN ////////////////////////////////////

    }
   
}
