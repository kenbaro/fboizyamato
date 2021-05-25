using Res_ManagementSystem.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Res_ManagementSystem.GUI.PhanCong
{
    public partial class DSCheckin : Form
    {
        public DSCheckin()
        {
            InitializeComponent();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {

        }

        private void DSCheckin_Load(object sender, EventArgs e)
        {
            dataGridView_dschkin.DataSource = CheckinBUS.LayDSCheckin();
           
        }
    }
}
