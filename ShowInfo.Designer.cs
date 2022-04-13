namespace WallPaper
{
    partial class ShowInfo
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
            this.components = new System.ComponentModel.Container();
            this.InfoArea = new System.Windows.Forms.TextBox();
            this.DelPic = new System.Windows.Forms.PictureBox();
            this.ChangeInfo = new System.Windows.Forms.PictureBox();
            this.SaveInfo = new System.Windows.Forms.PictureBox();
            this.CloseDialog = new System.Windows.Forms.PictureBox();
            this.AddPic = new System.Windows.Forms.PictureBox();
            this.ControlTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DelPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChangeInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseDialog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddPic)).BeginInit();
            this.SuspendLayout();
            // 
            // InfoArea
            // 
            this.InfoArea.BackColor = System.Drawing.SystemColors.Info;
            this.InfoArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InfoArea.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.InfoArea.Location = new System.Drawing.Point(5, 5);
            this.InfoArea.Margin = new System.Windows.Forms.Padding(5);
            this.InfoArea.Multiline = true;
            this.InfoArea.Name = "InfoArea";
            this.InfoArea.ReadOnly = true;
            this.InfoArea.Size = new System.Drawing.Size(370, 190);
            this.InfoArea.TabIndex = 0;
            // 
            // DelPic
            // 
            this.DelPic.Image = global::WallPaper.Properties.Resources.deleteImg;
            this.DelPic.Location = new System.Drawing.Point(380, 25);
            this.DelPic.Margin = new System.Windows.Forms.Padding(0);
            this.DelPic.Name = "DelPic";
            this.DelPic.Size = new System.Drawing.Size(20, 20);
            this.DelPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DelPic.TabIndex = 1;
            this.DelPic.TabStop = false;
            this.DelPic.Click += new System.EventHandler(this.DelPic_Click);
            // 
            // ChangeInfo
            // 
            this.ChangeInfo.Image = global::WallPaper.Properties.Resources.changeInfo;
            this.ChangeInfo.Location = new System.Drawing.Point(380, 75);
            this.ChangeInfo.Margin = new System.Windows.Forms.Padding(0);
            this.ChangeInfo.Name = "ChangeInfo";
            this.ChangeInfo.Size = new System.Drawing.Size(20, 20);
            this.ChangeInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ChangeInfo.TabIndex = 2;
            this.ChangeInfo.TabStop = false;
            this.ChangeInfo.Click += new System.EventHandler(this.ChangeInfo_Click);
            // 
            // SaveInfo
            // 
            this.SaveInfo.Image = global::WallPaper.Properties.Resources.saveInfo;
            this.SaveInfo.Location = new System.Drawing.Point(380, 100);
            this.SaveInfo.Margin = new System.Windows.Forms.Padding(0);
            this.SaveInfo.Name = "SaveInfo";
            this.SaveInfo.Size = new System.Drawing.Size(20, 20);
            this.SaveInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SaveInfo.TabIndex = 2;
            this.SaveInfo.TabStop = false;
            this.SaveInfo.Click += new System.EventHandler(this.SaveInfo_Click);
            // 
            // CloseDialog
            // 
            this.CloseDialog.Image = global::WallPaper.Properties.Resources.closeTip;
            this.CloseDialog.Location = new System.Drawing.Point(380, 0);
            this.CloseDialog.Margin = new System.Windows.Forms.Padding(0);
            this.CloseDialog.Name = "CloseDialog";
            this.CloseDialog.Size = new System.Drawing.Size(20, 20);
            this.CloseDialog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CloseDialog.TabIndex = 1;
            this.CloseDialog.TabStop = false;
            this.CloseDialog.Click += new System.EventHandler(this.CloseDialog_Click);
            // 
            // AddPic
            // 
            this.AddPic.Image = global::WallPaper.Properties.Resources.addImg;
            this.AddPic.Location = new System.Drawing.Point(380, 50);
            this.AddPic.Margin = new System.Windows.Forms.Padding(0);
            this.AddPic.Name = "AddPic";
            this.AddPic.Size = new System.Drawing.Size(20, 20);
            this.AddPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AddPic.TabIndex = 1;
            this.AddPic.TabStop = false;
            this.AddPic.Click += new System.EventHandler(this.AddPic_Click);
            // 
            // ShowInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.ControlBox = false;
            this.Controls.Add(this.AddPic);
            this.Controls.Add(this.DelPic);
            this.Controls.Add(this.ChangeInfo);
            this.Controls.Add(this.SaveInfo);
            this.Controls.Add(this.CloseDialog);
            this.Controls.Add(this.InfoArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowInfo";
            this.Opacity = 0.7D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ShowInfo";
            ((System.ComponentModel.ISupportInitialize)(this.DelPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChangeInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseDialog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InfoArea;
        private System.Windows.Forms.PictureBox CloseDialog;
        private System.Windows.Forms.PictureBox SaveInfo;
        private System.Windows.Forms.PictureBox DelPic;
        private System.Windows.Forms.PictureBox ChangeInfo;
        private System.Windows.Forms.PictureBox AddPic;
        private System.Windows.Forms.ToolTip ControlTip;
    }
}