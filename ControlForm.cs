using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WallPaper
{
    public partial class ControlForm : Form
    {
        internal Dictionary <string,string> ImageName = new Dictionary<string, string>();
        private string _rootpath;
        internal const string Imagepath = "IMG\\";
        internal const string DelImagepath = "Old\\";
        internal const string datafileName = "Resource\\imageinfo.js";
        internal const string delImgDatafileName = "Resource\\DelImageInfoBackup.js";
        private Stack<string> imghistory = new Stack<string>();
        private ShowInfo infoDialog;
        private string currentImg;
        private bool seqReview;
        public ControlForm()
        {
            InitializeComponent();
            try
            {
                if (Environment.OSVersion.Version.Major < 6)
                {
                    SendToBack();
                    IntPtr hWndNewParent = User32.FindWindow("Progman", null);
                    User32.SetParent(base.Handle, hWndNewParent);
                }
                else
                {
                    User32.SetWindowPos(base.Handle, 1, 0, 0, 0, 0, User32.SE_SHUTDOWN_PRIVILEGE);
                }
            }
            catch (ApplicationException exx)
            {
                MessageBox.Show(this, exx.Message, "Pin to Desktop");
            }
            RefreshPaper.Interval = Properties.Settings.Default.RefreshTime;
            RefreshTime.Text = (RefreshPaper.Interval / 60000).ToString();
            seqReview = Properties.Settings.Default.SeqReview;
            SequenceReview.Checked = seqReview;
            RootPath = Directory.Exists(Properties.Settings.Default.DataPath)? Properties.Settings.Default.DataPath:AppDomain.CurrentDomain.BaseDirectory + "Data";
        }
        private void ControlForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExortImgData();
            Properties.Settings.Default.RefreshTime = RefreshPaper.Interval;
            Properties.Settings.Default.SeqReview = seqReview;
            Properties.Settings.Default.DataPath = RootPath;
            Properties.Settings.Default.Save();
        }
        private void ControlForm_Load(object sender, EventArgs e)
        {
            this.Width = 80;
            this.Height = 20;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, 0);
            ImportImgData();
            NextImg();
            RefreshPaper.Start();
        }

        private void ControlForm_Activated(object sender, EventArgs e)
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                User32.SetWindowPos(base.Handle, 1, 0, 0, 0, 0, User32.SE_SHUTDOWN_PRIVILEGE);
            }
        }
        public string RootPath {
            get {
                return _rootpath;
            }
            set {
                if (!Directory.Exists(value)) {
                    throw new Exception("指定的背景数据存放目录不存在！");
                }
                if (value.Substring(value.Length - 1) != "\\")
                    _rootpath = value + "\\";
                else
                    _rootpath = value;
            }
        } 
        private void ControlForm_Paint(object sender, PaintEventArgs e)
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                User32.SetWindowPos(base.Handle, 1, 0, 0, 0, 0, User32.SE_SHUTDOWN_PRIVILEGE);
            }
        }
        private void ImportImgData() {
            ImageName.Clear();
            string dataFile = RootPath + datafileName;
            if (!File.Exists(dataFile)) return;
            FileStream fs = new FileStream(dataFile, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            string line;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                line = line.Substring(line.IndexOf('='));
                line = line.Substring(1, line.Length - 2);
                string[] strArr = line.Split('\"');
                ImageName.Add(strArr[1], strArr[3].Replace("<br>","\r\n"));
            }
            sr.Close();
            fs.Close();
        }
        private void ExortImgData()
        {
            string dataFile = RootPath + datafileName;
            FileStream fs = new FileStream(dataFile, FileMode.Create, FileAccess.Write);
            StreamWriter wr = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            int i = 0;
            string des;
            foreach(string img in ImageName.Keys) 
            {
                des = ImageName[img].Replace("\r\n", "<br>");
                wr.WriteLine(string.Format("imageName[{0:d}] = [\"{1:s}\",\"{2:s}\",0]", i++, img, des));
            }
            wr.Close();
            fs.Close();
        }
        private void Prev_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left)
                PrevImg();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left)
                NextImg();
        }

        private void Info_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button != MouseButtons.Left)
                return;
            if (infoDialog == null)
            {
                infoDialog = new ShowInfo(this);
                infoDialog.Image = currentImg;
            }
            infoDialog.Show();
            infoDialog.Relocation();
        }

        private void RefreshInfo_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button != MouseButtons.Left)
                return;
            DirectoryInfo dir = new DirectoryInfo(RootPath+Imagepath);
            FileInfo[] fil = dir.GetFiles();
            string fname;
            foreach (FileInfo f in fil)
            {
                fname = f.Name;
                if (!ImageName.ContainsKey(fname)) {
                    if (".jpg,.jpeg,.png,.bmp".Contains(fname.Substring(fname.LastIndexOf('.')))) {
                        ImageName.Add(fname, "");
                    }
                }
            }
            foreach (string img in ImageName.Keys.ToList()) {
                if (!File.Exists(RootPath + Imagepath + img))
                    ImageName.Remove(img);
            }
        }
        public void PrevImg()
        {
            if (imghistory.Count == 0)
                return;
            string prevName = imghistory.Pop();
            if (prevName != null)
                ShowImage(prevName, true);
        }
        public void NextImg(string imgname = "") {
            string imgfile = imgname;
            if (imgfile == "")
            {
                if (seqReview)
                {
                    bool b_next = false;
                    foreach (string keyname in ImageName.Keys){
                        if (b_next)
                        {
                            imgfile = keyname;
                            break;
                        }
                        if (keyname == currentImg)
                            b_next = true;
                    }
                    if (imgfile == "")
                    {
                        //从开始
                        imgfile = ImageName.Keys.First();
                    }
                }
                else
                {
                    Random rd = new Random();
                    int t = rd.Next(ImageName.Count);
                    imgfile = ImageName.Keys.ToArray()[t];
                }
            }
            ShowImage(imgfile);
        }
        private void RefreshPaper_Tick(object sender, EventArgs e)
        {
            NextImg();
        }
        //nResult: 0 失败; 其他值 成功
        private int ShowImage(string filename, bool isprev = false)
        {
            string[] tmp = filename.Split('.');
            string scrfile = RootPath + Imagepath + filename;
            string tempFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\bmpTrans.bmp";
            if (tmp[tmp.Length - 1].ToLower() != "bmp")
            {
                try
                {
                    Bitmap bmp = new Bitmap(scrfile);
                    bmp.Save(tempFile, System.Drawing.Imaging.ImageFormat.Bmp);
                    bmp.Dispose();
                    scrfile = tempFile;
                }
                catch (Exception e){
                    MessageBox.Show("保存文件失败，请检查文件夹是否只读，或者硬盘已满！"+"\r\n"+ tempFile +"\r\n"+"源文件名称："+filename+"\r\nException message:"+e.Message);
                }
            }
            if (File.Exists(scrfile))
            {
                //nResult: 0 失败; 其他值 成功
                int nResult = User32.SystemParametersInfo(20, 1, scrfile, 0x1 | 0x2);
                if (nResult != 0)
                {
                    if(currentImg!=null&&!isprev)
                        imghistory.Push(currentImg);
                    currentImg = filename;
                    ImageTip.SetToolTip(Info, "<<"+filename.Substring(0,filename.LastIndexOf('.'))+">>"+ImageName[filename]);
                    if (infoDialog != null)
                        infoDialog.Image = currentImg;
                    return nResult;
                }
            }
            return 0;
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AutoStart_Click(object sender, EventArgs e)
        {
            AutoRun.SetMeStart(AutoStart.Checked);
        }

        private void SequenceReview_Click(object sender, EventArgs e)
        {
            //SequenceReview.Checked = !SequenceReview.Checked;
            seqReview = SequenceReview.Checked;
        }

        private void RefreshTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                int timeTemp;
                try
                {
                    timeTemp = int.Parse(RefreshTime.Text);
                    //分钟
                    RefreshPaper.Interval = timeTemp *60000 ;
                    this.NotifyMenu.Visible = false;
                }
                catch
                {
                    MessageBox.Show("桌面刷新时间需要输入数字");
                }
            }
        }
        private void 文件目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog pathDialog = new FolderBrowserDialog();
            pathDialog.SelectedPath = RootPath;
            pathDialog.Description = "选择背景图片及数据所在目录";
            if (pathDialog.ShowDialog() == DialogResult.OK) {
                if (MessageBox.Show("是否复制现有目录到新位置", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    CopyDir(RootPath, pathDialog.SelectedPath);
                string temp = RootPath;
                RootPath = pathDialog.SelectedPath;
                try
                {
                    ImportImgData();
                    NextImg();
                }
                catch {
                    MessageBox.Show("选择目录数据存在问题，无法更换新目录，使用原目录");
                    RootPath = temp;
                    ImportImgData();
                    NextImg();
                }
            }
        }

        /// <summary>
        /// 复制文件夹及包含的文件
        /// </summary>
        /// <param name="sourceDirName">原始路径</param>
        /// <param name="destDirName">目标路径</param>
        /// <returns>true：复制成功；false：复制失败</returns>
        public bool CopyDir(string sourceDirName, string destDirName)
        {
            try
            {
                if (sourceDirName.Substring(sourceDirName.Length - 1) != "\\")
                    sourceDirName = sourceDirName + "\\";
                if (destDirName.Substring(destDirName.Length - 1) != "\\")
                    destDirName = destDirName + "\\";
                if (Directory.Exists(sourceDirName))
                {
                    if (!Directory.Exists(destDirName))
                        Directory.CreateDirectory(destDirName);
                    foreach (string item in Directory.GetFiles(sourceDirName))
                    {
                        File.Copy(item, destDirName + Path.GetFileName(item), true);
                    }
                    foreach (string item in Directory.GetDirectories(sourceDirName))
                    {
                        sourceDirName = item;
                        CopyDir(sourceDirName, destDirName + item.Substring(item.LastIndexOf("\\") + 1));
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
