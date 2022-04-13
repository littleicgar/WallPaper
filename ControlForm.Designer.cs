namespace WallPaper
{
    partial class ControlForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlForm));
            this.WallPaperNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.NotifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RefreshTime = new System.Windows.Forms.ToolStripTextBox();
            this.SequenceReview = new System.Windows.Forms.ToolStripMenuItem();
            this.文件目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoStart = new System.Windows.Forms.ToolStripMenuItem();
            this.Quit = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshPaper = new System.Windows.Forms.Timer(this.components);
            this.ImageTip = new System.Windows.Forms.ToolTip(this.components);
            this.RefreshInfo = new System.Windows.Forms.PictureBox();
            this.Info = new System.Windows.Forms.PictureBox();
            this.Next = new System.Windows.Forms.PictureBox();
            this.Prev = new System.Windows.Forms.PictureBox();
            this.NotifyMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Info)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Next)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Prev)).BeginInit();
            this.SuspendLayout();
            // 
            // WallPaperNotify
            // 
            this.WallPaperNotify.BalloonTipText = "WallPaper";
            this.WallPaperNotify.BalloonTipTitle = "WallPaper";
            this.WallPaperNotify.ContextMenuStrip = this.NotifyMenu;
            this.WallPaperNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("WallPaperNotify.Icon")));
            this.WallPaperNotify.Text = "WallPaper";
            this.WallPaperNotify.Visible = true;
            // 
            // NotifyMenu
            // 
            this.NotifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshTime,
            this.SequenceReview,
            this.文件目录ToolStripMenuItem,
            this.AutoStart,
            this.Quit});
            this.NotifyMenu.Name = "NotifyMenu";
            this.NotifyMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.NotifyMenu.ShowCheckMargin = true;
            this.NotifyMenu.Size = new System.Drawing.Size(147, 117);
            this.NotifyMenu.Text = "WallPaper";
            // 
            // RefreshTime
            // 
            this.RefreshTime.AutoSize = false;
            this.RefreshTime.AutoToolTip = true;
            this.RefreshTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RefreshTime.Name = "RefreshTime";
            this.RefreshTime.Size = new System.Drawing.Size(30, 23);
            this.RefreshTime.Tag = "";
            this.RefreshTime.Text = "60";
            this.RefreshTime.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.RefreshTime.ToolTipText = "更新背景的时间间隔（分钟）";
            this.RefreshTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RefreshTime_KeyDown);
            // 
            // SequenceReview
            // 
            this.SequenceReview.CheckOnClick = true;
            this.SequenceReview.Name = "SequenceReview";
            this.SequenceReview.Size = new System.Drawing.Size(146, 22);
            this.SequenceReview.Text = "顺序浏览";
            this.SequenceReview.Click += new System.EventHandler(this.SequenceReview_Click);
            // 
            // 文件目录ToolStripMenuItem
            // 
            this.文件目录ToolStripMenuItem.Name = "文件目录ToolStripMenuItem";
            this.文件目录ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.文件目录ToolStripMenuItem.Text = "文件目录";
            this.文件目录ToolStripMenuItem.Click += new System.EventHandler(this.文件目录ToolStripMenuItem_Click);
            // 
            // AutoStart
            // 
            this.AutoStart.CheckOnClick = true;
            this.AutoStart.Name = "AutoStart";
            this.AutoStart.Size = new System.Drawing.Size(146, 22);
            this.AutoStart.Text = "开机启动";
            this.AutoStart.Click += new System.EventHandler(this.AutoStart_Click);
            // 
            // Quit
            // 
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(146, 22);
            this.Quit.Text = "退出";
            this.Quit.Click += new System.EventHandler(this.Quit_Click);
            // 
            // RefreshPaper
            // 
            this.RefreshPaper.Interval = 600000;
            this.RefreshPaper.Tick += new System.EventHandler(this.RefreshPaper_Tick);
            // 
            // ImageTip
            // 
            this.ImageTip.AutoPopDelay = 50000;
            this.ImageTip.InitialDelay = 500;
            this.ImageTip.IsBalloon = true;
            this.ImageTip.ReshowDelay = 100;
            this.ImageTip.ShowAlways = true;
            // 
            // RefreshInfo
            // 
            this.RefreshInfo.BackColor = System.Drawing.Color.Transparent;
            this.RefreshInfo.Image = global::WallPaper.Properties.Resources.refresh;
            this.RefreshInfo.Location = new System.Drawing.Point(60, 0);
            this.RefreshInfo.Margin = new System.Windows.Forms.Padding(0);
            this.RefreshInfo.Name = "RefreshInfo";
            this.RefreshInfo.Size = new System.Drawing.Size(20, 20);
            this.RefreshInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RefreshInfo.TabIndex = 3;
            this.RefreshInfo.TabStop = false;
            this.RefreshInfo.Click += new System.EventHandler(this.RefreshInfo_Click);
            // 
            // Info
            // 
            this.Info.BackColor = System.Drawing.Color.Transparent;
            this.Info.Image = global::WallPaper.Properties.Resources.info;
            this.Info.Location = new System.Drawing.Point(40, 0);
            this.Info.Margin = new System.Windows.Forms.Padding(0);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(20, 20);
            this.Info.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Info.TabIndex = 2;
            this.Info.TabStop = false;
            this.Info.Click += new System.EventHandler(this.Info_Click);
            // 
            // Next
            // 
            this.Next.BackColor = System.Drawing.Color.Transparent;
            this.Next.Image = global::WallPaper.Properties.Resources.next;
            this.Next.Location = new System.Drawing.Point(20, 0);
            this.Next.Margin = new System.Windows.Forms.Padding(0);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(20, 20);
            this.Next.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Next.TabIndex = 1;
            this.Next.TabStop = false;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Prev
            // 
            this.Prev.BackColor = System.Drawing.Color.Transparent;
            this.Prev.Image = global::WallPaper.Properties.Resources.prev;
            this.Prev.InitialImage = null;
            this.Prev.Location = new System.Drawing.Point(0, 0);
            this.Prev.Margin = new System.Windows.Forms.Padding(0);
            this.Prev.Name = "Prev";
            this.Prev.Size = new System.Drawing.Size(20, 20);
            this.Prev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Prev.TabIndex = 0;
            this.Prev.TabStop = false;
            this.Prev.Click += new System.EventHandler(this.Prev_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(80, 20);
            this.ControlBox = false;
            this.Controls.Add(this.RefreshInfo);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.Prev);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControlForm";
            this.Opacity = 0.3D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ControlForm";
            this.Activated += new System.EventHandler(this.ControlForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ControlForm_FormClosed);
            this.Load += new System.EventHandler(this.ControlForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ControlForm_Paint);
            this.NotifyMenu.ResumeLayout(false);
            this.NotifyMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Info)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Next)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Prev)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon WallPaperNotify;
        private System.Windows.Forms.Timer RefreshPaper;
        private System.Windows.Forms.PictureBox Prev;
        private System.Windows.Forms.PictureBox Next;
        private System.Windows.Forms.PictureBox Info;
        private System.Windows.Forms.PictureBox RefreshInfo;
        private System.Windows.Forms.ToolTip ImageTip;
        private System.Windows.Forms.ContextMenuStrip NotifyMenu;
        private System.Windows.Forms.ToolStripMenuItem Quit;
        private System.Windows.Forms.ToolStripMenuItem SequenceReview;
        private System.Windows.Forms.ToolStripMenuItem AutoStart;
        private System.Windows.Forms.ToolStripTextBox RefreshTime;
        private System.Windows.Forms.ToolStripMenuItem 文件目录ToolStripMenuItem;
    }
}

