using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MklinkGUI
{
    public partial class AboutForm : Form
    {
        private MainForm mainForm;

        public AboutForm(MainForm form)
        {
            InitializeComponent();
            this.mainForm = form ?? throw new ArgumentNullException(nameof(form)); // 确保 form 不为 null
        }
        // 使用 this.mainForm 来访问 MainForm 中的属性
        public void SetLanguage(MainForm.Language language)
        {
            switch (language)
            {
                case MainForm.Language.English:
                    this.Text = "About"; // 示例文本
                    btnClose.Text = "Close";
                    lblSoftwareName.Text = "Mklink GUI";
                    lblVersion.Text = "Version: " + mainForm.Version;
                    lblDeveloper.Text = "Developer: 大康233 (DaKang233)";
                    lblDescription.Text = "A simple tool to create and manage symbolic links, hard links, and directory junctions.";
                    lblContact.Text = "Contact: contact@dakang233.com";
                    lblContact.Links.Add(0, lblContact.Text.Length, "mailto:contact@dakang233.com");
                    break;

                case MainForm.Language.Chinese:
                    this.Text = "关于"; // 示例文本
                    btnClose.Text = "关闭";
                    lblSoftwareName.Text = "Mklink GUI";
                    lblVersion.Text = "版本: " + mainForm.Version;
                    lblDeveloper.Text = "开发者: 大康233 (DaKang233)";
                    lblDescription.Text = "一个简单的工具，用于创建和管理符号链接、硬链接和目录联接。";
                    lblContact.Text = "联系方式: contact@dakang233.com";
                    lblContact.Links.Add(0, lblContact.Text.Length, "mailto:contact@dakang233.com");
                    break;
            }
        }
        
        private void lblContact_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 当用户点击链接时，启动默认邮件客户端
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
