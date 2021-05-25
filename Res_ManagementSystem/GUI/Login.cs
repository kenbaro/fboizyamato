//using QLNHFORM;
using Res_ManagementSystem.BUS;
using Res_ManagementSystem.GUI;
using System;
using System.Data;
using System.Windows.Forms;

namespace Res_ManagementSystem
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
     
        private void Login()
        {
            if (txbUser.Text == "" || txbPass.Text == "")
            {
                MessageBox.Show("UserName or PassWord Mustn't Be Empty", "Login ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radManager.Checked == false && radStaff.Checked == false)
            {
                MessageBox.Show("Please Choose Manager or Staff", "Login ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataTable _ds = BUS.StaffBUS.LayDSNhanVienCoMK();
                bool flag = false;
                for (int i = 0; i < _ds.Rows.Count; i++)
                {
                    if (txbUser.Text == _ds.Rows[i]["Username"].ToString() && txbPass.Text == _ds.Rows[i]["Password"].ToString())
                    {
                        if (radManager.Checked == true && _ds.Rows[i]["Type"].ToString() == "Manager")
                        {
                            
                            Res_Management mnFrm = new Res_Management();
                            mnFrm.Show();
                            mnFrm.ID= StaffBUS.TraCuuManvTheoTenDN(txbUser.Text);
                            flag = true;
                            //txbUser.Text = "";
                            //txbPass.Text = "";
                            //radManager.Checked = false;
                            this.Hide();

                        }
                        else if (radStaff.Checked == true && _ds.Rows[i]["Type"].ToString() == "Staff")
                        {
                            Res_Management mnFrm = new Res_Management();
                            mnFrm.Show();
                            mnFrm.ID = StaffBUS.TraCuuManvTheoTenDN(txbUser.Text);
                            flag = true;
                            //txbUser.Text = "";
                            //txbPass.Text = "";
                            //radManager.Checked = false;
                            this.Hide();
                            
                        }
                        else
                        {
                            MessageBox.Show("Hãy Chọn Đúng Quyền Của Mình", "Lỗi Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            radStaff.Checked = false;
                            radManager.Checked = false;
                            flag = true;
                        }
                    }
                }
                if (flag == false)
                {
                    MessageBox.Show("Tài Khoản Hoặc Mật Khẩu Không Đúng", "Lỗi Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txbPass.Text = "";
                    txbPass.Focus();
                }
            }
        }
        private void logButton_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void loginForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += loginFrom_KeyDown;
        }
        private void loginFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }
    }
}
