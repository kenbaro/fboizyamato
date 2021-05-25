
namespace Res_ManagementSystem.GUI.PhanCong
{
    partial class DSCheckin
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
            this.label_DSCheckin = new System.Windows.Forms.Label();
            this.dataGridView_dschkin = new System.Windows.Forms.DataGridView();
            this.btn_Print = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_dschkin)).BeginInit();
            this.SuspendLayout();
            // 
            // label_DSCheckin
            // 
            this.label_DSCheckin.AutoSize = true;
            this.label_DSCheckin.Font = new System.Drawing.Font("Lucida Calligraphy", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DSCheckin.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_DSCheckin.Location = new System.Drawing.Point(143, 39);
            this.label_DSCheckin.Name = "label_DSCheckin";
            this.label_DSCheckin.Size = new System.Drawing.Size(499, 36);
            this.label_DSCheckin.TabIndex = 0;
            this.label_DSCheckin.Text = "Danh Sách CheckIn Hôm Nay:";
            // 
            // dataGridView_dschkin
            // 
            this.dataGridView_dschkin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_dschkin.Location = new System.Drawing.Point(44, 97);
            this.dataGridView_dschkin.Name = "dataGridView_dschkin";
            this.dataGridView_dschkin.RowHeadersWidth = 51;
            this.dataGridView_dschkin.RowTemplate.Height = 24;
            this.dataGridView_dschkin.Size = new System.Drawing.Size(708, 272);
            this.dataGridView_dschkin.TabIndex = 1;
            // 
            // btn_Print
            // 
            this.btn_Print.Location = new System.Drawing.Point(256, 389);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(251, 36);
            this.btn_Print.TabIndex = 2;
            this.btn_Print.Text = "Print";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // DSCheckin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Print);
            this.Controls.Add(this.dataGridView_dschkin);
            this.Controls.Add(this.label_DSCheckin);
            this.Name = "DSCheckin";
            this.Text = "DSCheckin";
            this.Load += new System.EventHandler(this.DSCheckin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_dschkin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_DSCheckin;
        private System.Windows.Forms.DataGridView dataGridView_dschkin;
        private System.Windows.Forms.Button btn_Print;
    }
}