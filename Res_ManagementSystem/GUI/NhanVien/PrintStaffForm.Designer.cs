
namespace Res_ManagementSystem.GUI.NhanVien
{
    partial class PrintStaffForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Print = new System.Windows.Forms.Button();
            this.label_print = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rad_All = new System.Windows.Forms.RadioButton();
            this.btn_Check = new System.Windows.Forms.Button();
            this.rad_Staff = new System.Windows.Forms.RadioButton();
            this.rad_Manager = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbx_Print = new System.Windows.Forms.ComboBox();
            this.rad_No = new System.Windows.Forms.RadioButton();
            this.rad_Yes = new System.Windows.Forms.RadioButton();
            this.label_range = new System.Windows.Forms.Label();
            this.label_type = new System.Windows.Forms.Label();
            this.dgvStaff = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStaff)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Print);
            this.panel1.Controls.Add(this.label_print);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dgvStaff);
            this.panel1.Location = new System.Drawing.Point(33, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1193, 574);
            this.panel1.TabIndex = 0;
            // 
            // btn_Print
            // 
            this.btn_Print.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_Print.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Print.Location = new System.Drawing.Point(948, 501);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(135, 42);
            this.btn_Print.TabIndex = 2;
            this.btn_Print.Text = "Print";
            this.btn_Print.UseVisualStyleBackColor = false;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // label_print
            // 
            this.label_print.AutoSize = true;
            this.label_print.Font = new System.Drawing.Font("MV Boli", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_print.Location = new System.Drawing.Point(279, 205);
            this.label_print.Name = "label_print";
            this.label_print.Size = new System.Drawing.Size(285, 44);
            this.label_print.TabIndex = 1;
            this.label_print.Text = "Print Staff List";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rad_All);
            this.panel2.Controls.Add(this.btn_Check);
            this.panel2.Controls.Add(this.rad_Staff);
            this.panel2.Controls.Add(this.rad_Manager);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label_type);
            this.panel2.Location = new System.Drawing.Point(27, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1163, 169);
            this.panel2.TabIndex = 2;
            // 
            // rad_All
            // 
            this.rad_All.AutoSize = true;
            this.rad_All.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad_All.Location = new System.Drawing.Point(251, 84);
            this.rad_All.Name = "rad_All";
            this.rad_All.Size = new System.Drawing.Size(52, 24);
            this.rad_All.TabIndex = 5;
            this.rad_All.TabStop = true;
            this.rad_All.Text = "All";
            this.rad_All.UseVisualStyleBackColor = true;
            // 
            // btn_Check
            // 
            this.btn_Check.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btn_Check.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Check.ForeColor = System.Drawing.Color.Black;
            this.btn_Check.Location = new System.Drawing.Point(904, 45);
            this.btn_Check.Name = "btn_Check";
            this.btn_Check.Size = new System.Drawing.Size(143, 48);
            this.btn_Check.TabIndex = 1;
            this.btn_Check.Text = "Check";
            this.btn_Check.UseVisualStyleBackColor = false;
            this.btn_Check.Click += new System.EventHandler(this.btn_Check_Click);
            // 
            // rad_Staff
            // 
            this.rad_Staff.AutoSize = true;
            this.rad_Staff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad_Staff.Location = new System.Drawing.Point(139, 84);
            this.rad_Staff.Name = "rad_Staff";
            this.rad_Staff.Size = new System.Drawing.Size(70, 24);
            this.rad_Staff.TabIndex = 4;
            this.rad_Staff.TabStop = true;
            this.rad_Staff.Text = "Staff";
            this.rad_Staff.UseVisualStyleBackColor = true;
            // 
            // rad_Manager
            // 
            this.rad_Manager.AutoSize = true;
            this.rad_Manager.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad_Manager.Location = new System.Drawing.Point(24, 84);
            this.rad_Manager.Name = "rad_Manager";
            this.rad_Manager.Size = new System.Drawing.Size(102, 24);
            this.rad_Manager.TabIndex = 3;
            this.rad_Manager.TabStop = true;
            this.rad_Manager.Text = "Manager";
            this.rad_Manager.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbx_Print);
            this.panel3.Controls.Add(this.rad_No);
            this.panel3.Controls.Add(this.rad_Yes);
            this.panel3.Controls.Add(this.label_range);
            this.panel3.Location = new System.Drawing.Point(367, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(442, 163);
            this.panel3.TabIndex = 2;
            // 
            // cbx_Print
            // 
            this.cbx_Print.FormattingEnabled = true;
            this.cbx_Print.Location = new System.Drawing.Point(242, 81);
            this.cbx_Print.Name = "cbx_Print";
            this.cbx_Print.Size = new System.Drawing.Size(180, 24);
            this.cbx_Print.TabIndex = 3;
            // 
            // rad_No
            // 
            this.rad_No.AutoSize = true;
            this.rad_No.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad_No.Location = new System.Drawing.Point(125, 81);
            this.rad_No.Name = "rad_No";
            this.rad_No.Size = new System.Drawing.Size(54, 26);
            this.rad_No.TabIndex = 2;
            this.rad_No.TabStop = true;
            this.rad_No.Text = "No";
            this.rad_No.UseVisualStyleBackColor = true;
            // 
            // rad_Yes
            // 
            this.rad_Yes.AutoSize = true;
            this.rad_Yes.Font = new System.Drawing.Font("Microsoft Tai Le", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad_Yes.Location = new System.Drawing.Point(28, 81);
            this.rad_Yes.Name = "rad_Yes";
            this.rad_Yes.Size = new System.Drawing.Size(57, 26);
            this.rad_Yes.TabIndex = 1;
            this.rad_Yes.TabStop = true;
            this.rad_Yes.Text = "Yes";
            this.rad_Yes.UseVisualStyleBackColor = true;
            // 
            // label_range
            // 
            this.label_range.AutoSize = true;
            this.label_range.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_range.Location = new System.Drawing.Point(120, 33);
            this.label_range.Name = "label_range";
            this.label_range.Size = new System.Drawing.Size(180, 26);
            this.label_range.TabIndex = 0;
            this.label_range.Text = "Use Salary Range:";
            // 
            // label_type
            // 
            this.label_type.AutoSize = true;
            this.label_type.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_type.Location = new System.Drawing.Point(135, 34);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(76, 26);
            this.label_type.TabIndex = 1;
            this.label_type.Text = "Quyền";
            // 
            // dgvStaff
            // 
            this.dgvStaff.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStaff.Location = new System.Drawing.Point(27, 253);
            this.dgvStaff.Name = "dgvStaff";
            this.dgvStaff.RowHeadersWidth = 51;
            this.dgvStaff.RowTemplate.Height = 24;
            this.dgvStaff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStaff.Size = new System.Drawing.Size(849, 307);
            this.dgvStaff.TabIndex = 0;
            // 
            // PrintStaffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1261, 610);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "PrintStaffForm";
            this.Text = "PrintStaffForm";
            this.Load += new System.EventHandler(this.PrintStaffForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStaff)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvStaff;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_type;
        private System.Windows.Forms.Label label_print;
        private System.Windows.Forms.Button btn_Check;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.RadioButton rad_All;
        private System.Windows.Forms.RadioButton rad_Staff;
        private System.Windows.Forms.RadioButton rad_Manager;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rad_No;
        private System.Windows.Forms.RadioButton rad_Yes;
        private System.Windows.Forms.Label label_range;
        private System.Windows.Forms.ComboBox cbx_Print;
    }
}