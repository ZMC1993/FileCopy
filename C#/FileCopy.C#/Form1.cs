using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformDemo
{
    public partial class Form1 : Form
    {
        private static string SourcePath;
        private static string TargetPath;

        public Form1()
        {
            InitializeComponent();
            string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            SourcePath = config.AppSettings.Settings["sourcePath"].Value;
            TargetPath = config.AppSettings.Settings["targetPath"].Value;
            txtSourcePath.Text = SourcePath;
            txtTargetPath.Text = TargetPath;

        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < txtFiles.Lines.Length; i++)
            {
                try
                {
                    copyHandle(txtFiles.Lines[i].ToString());
                }
                catch (Exception ex)
                {
                    ShowMessage(ex.Message);
                    continue;
                }
            }
            
            

        }


        #region 复制操作
        private void copyHandle(string filePath) {
            string target = TargetPath + filePath;
            string source = SourcePath + filePath;
            if (!File.Exists(source)) {
                ShowMessage("源文件：" + source + "不存在！");
                return;
            }
            string targetFilePath = target.Substring(0, target.LastIndexOf('/'));
            if (!Directory.Exists(targetFilePath)) {
                ShowMessage("开始创建路径：" + targetFilePath);
                Directory.CreateDirectory(targetFilePath);
            }

            File.Copy(source, target, true);
            ShowMessage("复制文件:" + source + "成功！");
        }

        #endregion


        #region 实时输出消息
        private delegate void ShowMessageHandler(string msg);
        private void ShowMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowMessageHandler(ShowMessage), new object[] { msg });
            }
            else
            {
                if (msg != null)
                {
                    txtMessage.AppendText(msg + "\r\n");
                }
            }
        }
        #endregion

        private void TxtSourcePath_TextChanged(object sender, EventArgs e)
        {
            SourcePath = txtSourcePath.Text;
        }

        private void TxtTargetPath_TextChanged(object sender, EventArgs e)
        {
            TargetPath = txtTargetPath.Text;
        }
    }
}
