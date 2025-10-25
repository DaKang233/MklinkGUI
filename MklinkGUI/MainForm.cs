using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MklinkGUI
{
    public partial class MainForm : Form
    {
        public string Version = "0.0.1";
        private AboutForm aboutForm;        // AboutForm 实例

        // 可选：保存临时状态（需在MainForm中调用）
        public static void SaveTemporaryState(string targetPath, string linkPath, string linkType)
        {
            Registry.CurrentUser.CreateSubKey(@"Software\MklinkGUI\TempState");
            var key = Registry.CurrentUser.OpenSubKey(@"Software\MklinkGUI\TempState", true);
            key.SetValue("TargetPath", targetPath);
            key.SetValue("LinkPath", linkPath);
            key.SetValue("LinkType", linkType);
        }

        // 可选：读取临时状态（需在MainForm中调用）
        public static void LoadTemporaryState(MainForm form)
        {
            var keyPath = @"Software\MklinkGUI\TempState";
            var key = Registry.CurrentUser.OpenSubKey(keyPath);

            if (key != null)
            {
                // 1. 核心步骤：读取注册表（已成功）
                form.txtTargetPath.Text = key.GetValue("TargetPath")?.ToString() ?? "";
                form.txtLinkPath.Text = key.GetValue("LinkPath")?.ToString() ?? "";
                form.cbLinkType.Text = key.GetValue("LinkType")?.ToString() ?? "";

                // 2. 优化后的清理步骤：用try-catch包裹，避免抛错
                try
                {
                    key.Close(); // 先关闭已打开的key
                    Registry.CurrentUser.DeleteSubKeyTree(keyPath); // 再删除
                }
                catch (UnauthorizedAccessException)
                {
                    // 即使删除失败，也不抛错，仅静默记录（可选）
                    // 比如写日志："临时注册表项删除失败，权限不足"
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
            aboutForm = new AboutForm(this);    // 实例化 AboutForm
        }

        private Language GetCurrentCulture()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            // MessageBox.Show("Current culture: " + currentCulture.Name); // 添加此行
            Language appLanguage = GetLanguageFromCulture(currentCulture);
            return appLanguage;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetLanguage(GetCurrentCulture());
            LoadTemporaryState(this);
        }

        public enum Language
        {
            English,
            Chinese,
        }

        private void SetLanguage(Language language)
        {
            switch (language)
            {
                default:
                case Language.English:
                    labelLinkPath.Text = "Link Path";
                    labelLinkType.Text = "Link Type";
                    labelTargetPath.Text = "Target Path";
                    btnBrowseLink.Text = "Browse...";
                    btnBrowseTarget.Text = "Browse...";
                    btnCreateLink.Text = "Create Link";
                    btnRemoveLink.Text = "Remove Link";
                    btnAbout.Text = "About...";
                    break;
                case Language.Chinese:
                    labelLinkPath.Text = "链接路径";
                    labelLinkType.Text = "链接类型";
                    labelTargetPath.Text = "目标路径";
                    btnBrowseLink.Text = "浏览...";
                    btnBrowseTarget.Text = "浏览...";
                    btnCreateLink.Text = "创建链接";
                    btnRemoveLink.Text = "删除链接";
                    btnAbout.Text = "关于…";
                    break;
            }
        }

        private Language GetLanguageFromCulture(CultureInfo culture)
        {
            switch (culture.Name)
            {
                case "en-US": // 英语 (美国)
                case "en-GB": // 英语 (英国)
                    return Language.English;
                case "zh-CN": // 中文 (简体)
                    return Language.Chinese;
                default:
                    return Language.English; // 默认英语
            }
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            SetLanguage(Language.English);
        }

        private void btnChinese_Click(object sender, EventArgs e)
        {
            SetLanguage(Language.Chinese);
        }

        private void btnCreateLink_Click(object sender, EventArgs e)
        {
            string targetPath = txtTargetPath.Text;
            string linkPath = txtLinkPath.Text;
            string linkType = cbLinkType.Text;

            if (string.IsNullOrEmpty(targetPath) || string.IsNullOrEmpty(linkPath))
            {
                MessageBox.Show("Please specify both target and link paths.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(linkType))
            {
                MessageBox.Show("Please specify a valid link type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 检查是否需要管理员权限
            bool requiresAdmin = linkType == "Symbolic Link" || linkType == "Directory Junction";

            if (requiresAdmin && !AdminHelper.IsRunAsAdmin())
            {
                // 提示用户以管理员身份重新启动
                DialogResult result = MessageBox.Show("This operation requires administrator privileges. Would you like to restart the application as an administrator?", "Permission Required", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // 以管理员身份重新启动程序
                    SaveTemporaryState(targetPath,linkPath,linkType);
                    AdminHelper.RestartAsAdmin();
                }

                return;
            }

            // 如果权限足够，继续执行创建链接操作
            CreateLink(linkType, linkPath, targetPath);
        }

        private void CreateLink(string linkType, string linkPath, string targetPath)
        {
            string command = "cmd.exe";
            string args = "/c mklink";

            if (linkType == "Symbolic Link")
            {
                args += " /D";
            }
            else if (linkType == "Hard Link")
            {
                args += " /H";
            }
            else if (linkType == "Directory Junction")
            {
                args += " /J";
            }

            args += $" \"{linkPath}\" \"{targetPath}\"";

            ExecuteCommand(command, args);
        }
        private void btnRemoveLink_Click(object sender, EventArgs e)
        {
            string linkPath = txtLinkPath.Text;

            if (string.IsNullOrEmpty(linkPath))
            {
                MessageBox.Show("Please specify a valid link path to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 获取链接类型（假设你有一个控件来选择链接类型，比如一个ComboBox）
                string linkType = cbLinkType.Text;

                if (string.IsNullOrEmpty(linkType))
                {
                    MessageBox.Show("Please specify a valid link type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (linkType == "Directory Junction")
                {
                    // 目录联接，使用 Directory.Delete
                    if (Directory.Exists(linkPath))
                    {
                        Directory.Delete(linkPath, true); // true 表示递归删除
                        MessageBox.Show("Directory junction removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The specified path is not a directory junction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // 硬链接和符号链接，使用 File.Delete
                    if (File.Exists(linkPath) || Directory.Exists(linkPath))
                    {
                        File.Delete(linkPath); // 对于文件硬链接或符号链接，使用 File.Delete
                        MessageBox.Show("Link removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The specified path is not a valid link or does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to remove link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnBrowseTarget_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog TargetDir = new FolderBrowserDialog())
            {
                TargetDir.Description = "选择一个文件夹。";
                TargetDir.ShowNewFolderButton = true;
                if (TargetDir.ShowDialog() == DialogResult.OK)
                {
                    txtTargetPath.Text = TargetDir.SelectedPath;
                }
            }
        }

        private void btnBrowseLink_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog LinkDir = new FolderBrowserDialog())
            {
                LinkDir.Description = "选择一个文件夹。";
                LinkDir.ShowNewFolderButton = true;
                if (LinkDir.ShowDialog() == DialogResult.OK)
                {
                    txtLinkPath.Text = LinkDir.SelectedPath;
                }
            }
        }

        private void ExecuteCommand(string command, string args)
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo(command, args)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process proc = new Process
                {
                    StartInfo = procStartInfo
                };

                proc.Start();
                string result = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                MessageBox.Show(result, "Output", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to execute command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm(this);
            // 设置语言
            aboutForm.SetLanguage(GetCurrentCulture());
            aboutForm.ShowDialog();
        }
    }
}
