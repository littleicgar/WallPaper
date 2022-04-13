using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WallPaper
{
    public partial class ShowInfo : Form
    {
        private string imageName;
        private ControlForm pForm;
        public ShowInfo(ControlForm parentForm)
        {
            InitializeComponent();
            pForm = parentForm;
        }

        private void CloseDialog_Click(object sender, EventArgs e)
        {
            if (!InfoArea.ReadOnly) {
                if (MessageBox.Show("是否保存信息？","保存", MessageBoxButtons.YesNo) == DialogResult.OK)
                    pForm.ImageName[Image] = InfoArea.Text;
            }
            InfoArea.ReadOnly = true;
            this.Visible = false;
        }

        public string Image
        {
            set {
                if (!InfoArea.ReadOnly)
                {
                    if (MessageBox.Show("是否保存信息？", "保存", MessageBoxButtons.YesNo) == DialogResult.OK)
                        pForm.ImageName[Image] = InfoArea.Text;
                }
                imageName = value;
                InfoArea.Text = pForm.ImageName[value];
                InfoArea.ReadOnly = true;
            }
            get {
                return imageName;
            }
        }
        public void Relocation() {
            this.Location = new Point(pForm.Location.X + pForm.Width - this.Width, pForm.Height);
        }
        private void ChangeInfo_Click(object sender, EventArgs e)
        {
            InfoArea.ReadOnly = false;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            pForm.ImageName[Image] = InfoArea.Text;
            InfoArea.ReadOnly = true;
        }

        private void DelPic_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确实不在显示该照片？", "删除", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            string dataFile = pForm.RootPath + ControlForm.delImgDatafileName;
            string sFile = pForm.RootPath + ControlForm.Imagepath;
            string dFile = pForm.RootPath + ControlForm.DelImagepath;
            if (!File.Exists(dataFile)) return;
            File.Move(sFile + Image, dFile + Image);

            pForm.ImageName.Remove(Image);

            FileStream fs = new FileStream(dataFile, FileMode.Append);
            StreamWriter wr = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            wr.WriteLine(string.Format("imageName[{0:d}] = [\"{1:s}\",\"{2:s}\",0]", 0, Image, InfoArea.Text.Replace("\r\n", "<br>")));
            wr.Close();
            fs.Close();

            InfoArea.ReadOnly = true;
            pForm.NextImg();
        }

        private void AddPic_Click(object sender, EventArgs e)
        {
            string dFile = pForm.RootPath + ControlForm.DelImagepath;
            string sFile = pForm.RootPath + ControlForm.Imagepath;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择文件";
            dialog.Filter = "图片文件(.jpg;.jpeg;.png;.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            dialog.InitialDirectory = pForm.RootPath + ControlForm.DelImagepath;
            string filename;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = dialog.FileName;
                string fname = filename.Substring(filename.LastIndexOf('\\')+1);
                string newfname = fname;
                string fileDes = "";
                if (!File.Exists(filename)) return;
                //处理文件重复
                int i = 0;
                while(File.Exists(sFile + newfname)) {
                    newfname = fname.Insert(fname.LastIndexOf('.'), string.Format("_{0:d}", i++));
                }
                File.Move(filename, sFile + newfname);
                //如果是恢复文件
                if (filename.Substring(0, filename.LastIndexOf('\\')+1) == dFile){
                    Dictionary<string, string> DelImageName = new Dictionary<string, string>();
                    ImportImgData(DelImageName);
                    if (DelImageName.ContainsKey(fname))
                    {
                        fileDes = DelImageName[fname];
                        DelImageName.Remove(fname);
                        ExortImgData(DelImageName);
                    }
                }
                pForm.ImageName.Add(newfname, fileDes);
                pForm.NextImg(newfname);
            }
        }
        private void ImportImgData(Dictionary<string, string> del)
        {
            string dataFile = pForm.RootPath + ControlForm.delImgDatafileName;
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
                del.Add(strArr[1], strArr[3].Replace("<br>", "\r\n"));
            }
            sr.Close();
            fs.Close();
        }
        private void ExortImgData(Dictionary<string, string> del)
        {
            string dataFile = pForm.RootPath + ControlForm.delImgDatafileName;
            FileStream fs = new FileStream(dataFile, FileMode.Create, FileAccess.Write);
            StreamWriter wr = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            int i = 0;
            string des;
            foreach (string img in del.Keys)
            {
                des = del[img].Replace("\r\n", "<br>");
                wr.WriteLine(string.Format("imageName[{0:d}] = [\"{1:s}\",\"{2:s}\",0]", i++, img, des));
            }
            wr.Close();
            fs.Close();
        }
    }
}
