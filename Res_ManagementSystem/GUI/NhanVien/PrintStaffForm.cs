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
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace Res_ManagementSystem.GUI.NhanVien
{
    public partial class PrintStaffForm : Form
    {
        public PrintStaffForm()
        {
            InitializeComponent();
        }

        // thêm các lựa chọn vào combobox
        private void AddSelectionIntoCombobox()
        {
            cbx_Print.Items.Add("Fewer Than 1M");
            cbx_Print.Items.Add("From 1M To 2M");
            cbx_Print.Items.Add("More Than 2M");
        }
        private void PrintStaffForm_Load(object sender, EventArgs e)
        {
            System.Data.DataTable _ds = StaffBUS.LayDSNhanVien();
            dgvStaff.DataSource = _ds;
            AddSelectionIntoCombobox();
        }






        // print Staff

        public void Export_Data_To_Word(DataGridView DGV, string filename)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop

                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;

                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";

                    }
                }
                //table format
                oRange.Text = oTemp;


                object Separator = WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                }

                //table style 
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //header text
                foreach (Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Range headerRange = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                    headerRange.Text = "Staff List :";
                    headerRange.Font.Size = 16;
                    headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                }
                //save image
                //for (r = 0; r <= RowCount - 1; r++)
                //{
                //    try
                //    {
                //        byte[] imgbyte = (byte[])d.Rows[r].Cells[7].Value;
                //        MemoryStream ms = new MemoryStream(imgbyte);


                //        // Image finalPic = (Image)(((Bitmap)dataGridView2.Rows[r].Cells[7].Value).Clone());
                //        Image finalPic = (Image)(new Bitmap(Image.FromStream(ms), new Size(100, 100)));
                //        Clipboard.SetDataObject(finalPic);
                //        oDoc.Application.Selection.Tables[1].Cell(r + 2, 8).Range.Paste();
                //        oDoc.Application.Selection.Tables[1].Cell(r + 2, 8).Range.InsertParagraph();
                //    }
                //    catch (Exception ex)
                //    {
                //        // MessageBox.Show(ex.Message, "ExportToWord", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }

                //}

                //save the file
                oDoc.SaveAs2(filename);


            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            // in danh sách nhân viên

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = "Staff_List.docx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                Export_Data_To_Word(dgvStaff, sfd.FileName);
            }
        }

        private void btn_Check_Click(object sender, EventArgs e)
        {
            if(rad_All.Checked==true)
            {
                if (rad_No.Checked == true)
                    dgvStaff.DataSource = StaffBUS.LayDSToanBoNhanVien();
                else
                {
                   
                }
            }    
            else if (rad_Manager.Checked==true)
                if (rad_No.Checked == true)
                    dgvStaff.DataSource = StaffBUS.LayDSQuanLy();
            else
                     if (rad_No.Checked == true)
                    dgvStaff.DataSource = StaffBUS.LayDSNhanVien();
        }
    }
}
