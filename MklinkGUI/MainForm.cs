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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MklinkGUI
{
    public partial class MainForm : Form
    {
        public string Version = "0.0.1";
        private AboutForm aboutForm;        // AboutForm 实例
        bool IsEnglish;
        Language CurrentLanguage;
        public dynamic stringsCollection = new System.Dynamic.ExpandoObject(); // 动态语言资源

        public void StringsChange(Language language)
        {
            switch (language)
            {
                case Language.English:
                    stringsCollection.LinkPath = "Link Path";
                    stringsCollection.LinkType = "Link Type";
                    stringsCollection.TargetPath = "Target Path";
                    stringsCollection.BrowseLink = "Browse...";
                    stringsCollection.BrowseTarget = "Browse...";
                    stringsCollection.CreateLink = "Create Link";
                    stringsCollection.RemoveLink = "Remove Link";
                    stringsCollection.About = "About...";
                    stringsCollection.ChangeLanguage = "Change Language";
                    stringsCollection.SymbolicLink = "Symbolic Link";
                    stringsCollection.HardLink = "Hard Link";
                    stringsCollection.DirectoryJunction = "Directory Junction";
                    stringsCollection.ErrorNotFilltheBlank = "Please specify both target and link paths.";
                    stringsCollection.ErrorNotLinkType_RemoveLink = "Please specify a valid link path to remove.";
                    stringsCollection.Error = "Error";
                    stringsCollection.Success = "Success";
                    stringsCollection.AdminPermission = "This operation requires administrator privileges. Would you like to restart the application as an administrator?";
                    stringsCollection.PermissionRequired = "Permission Required";
                    stringsCollection.NotValidLinkType = "Please specify a valid link type.";
                    stringsCollection.NotDirectoryJunction = "The specified path is not a directory junction.";
                    stringsCollection.DirectoryJunctionRemoved = "Directory junction removed successfully.";
                    stringsCollection.SpecificPathNotValid = "The specified path is not a valid link or does not exist.";
                    stringsCollection.LinkRemovedSuccessfully = "Link removed successfully.";
                    stringsCollection.FailedToRemoveLink = "Failed to remove link: ";
                    stringsCollection.FailedToExecuteCommand = "Failed to execute command: ";
                    stringsCollection.SelectFolder = "Select a folder.";
                    stringsCollection.Output = "Output";
                    break;
                case Language.Chinese:
                    stringsCollection.LinkPath = "链接路径";
                    stringsCollection.LinkType = "链接类型";
                    stringsCollection.TargetPath = "目标路径";
                    stringsCollection.BrowseLink = "浏览...";
                    stringsCollection.BrowseTarget = "浏览...";
                    stringsCollection.CreateLink = "创建链接";
                    stringsCollection.RemoveLink = "删除链接";
                    stringsCollection.About = "关于…";
                    stringsCollection.ChangeLanguage = "切换语言";
                    stringsCollection.SymbolicLink = "符号链接";
                    stringsCollection.HardLink = "硬链接";
                    stringsCollection.DirectoryJunction = "目录联接";
                    stringsCollection.ErrorNotFilltheBlank = "请指定目标和链接路径。";
                    stringsCollection.ErrorNotLinkType_RemoveLink = "请指定一个有效的链接路径来删除。";
                    stringsCollection.Error = "错误";
                    stringsCollection.Success = "成功";
                    stringsCollection.AdminPermission = "此操作需要管理员权限。是否要以管理员身份重新启动应用程序？";
                    stringsCollection.PermissionRequired = "需要权限";
                    stringsCollection.NotValidLinkType = "请指定一个有效的链接类型。";
                    stringsCollection.NotDirectoryJunction = "指定的路径不是目录联接。";
                    stringsCollection.DirectoryJunctionRemoved = "目录联接已成功删除。";
                    stringsCollection.SpecificPathNotValid = "指定的路径不是有效的链接或不存在。";
                    stringsCollection.LinkRemovedSuccessfully = "链接已成功删除。";
                    stringsCollection.FailedToRemoveLink = "删除链接失败：";
                    stringsCollection.FailedToExecuteCommand = "执行命令失败：";
                    stringsCollection.SelectFolder = "选择一个文件夹。";
                    stringsCollection.Output = "输出";
                    break;
            }
            labelLinkPath.Text = stringsCollection.LinkPath;
            labelLinkType.Text = stringsCollection.LinkType;
            labelTargetPath.Text = stringsCollection.TargetPath;
            btnBrowseLink.Text = stringsCollection.BrowseLink;
            btnBrowseTarget.Text = stringsCollection.BrowseTarget;
            btnCreateLink.Text = stringsCollection.CreateLink;
            btnRemoveLink.Text = stringsCollection.RemoveLink;
            btnAbout.Text = stringsCollection.About;
            btnChangeLanguage.Text = stringsCollection.ChangeLanguage;
            cbLinkType.Items.Clear();
            cbLinkType.Items.AddRange(new string[] { stringsCollection.SymbolicLink, stringsCollection.HardLink, stringsCollection.DirectoryJunction });
        }

        // 保存临时状态
        public static void SaveTemporaryState(string targetPath, string linkPath, string linkType)
        {
            Registry.CurrentUser.CreateSubKey(@"Software\MklinkGUI\TempState");
            var key = Registry.CurrentUser.OpenSubKey(@"Software\MklinkGUI\TempState", true);
            key.SetValue("TargetPath", targetPath);
            key.SetValue("LinkPath", linkPath);
            key.SetValue("LinkType", linkType);
        }

        // 读取临时状态
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
            if ( language == Language.English )
            {
                IsEnglish = true;
            }
            else
            {
                IsEnglish = false;
            }
            CurrentLanguage = language;
            StringsChange(language);
        }

        private Language GetLanguageFromCulture(CultureInfo culture)
        {
            switch (culture.Name)
            {
                case "en-US": // 英语 (美国)
                case "en-GB": // 英语 (英国)
                    CurrentLanguage = Language.English;
                    return Language.English;
                case "zh-CN": // 中文 (简体)
                    CurrentLanguage = Language.Chinese;
                    return Language.Chinese;
                default:
                    CurrentLanguage = Language.English;
                    return Language.English; // 默认英语
            }
        }

        private void btnCreateLink_Click(object sender, EventArgs e)
        {
            string targetPath = txtTargetPath.Text;
            string linkPath = txtLinkPath.Text;
            string linkType = cbLinkType.Text;

            if (string.IsNullOrEmpty(targetPath) || string.IsNullOrEmpty(linkPath))
            {
                MessageBox.Show(stringsCollection.ErrorNotFilltheBlank, stringsCollection.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(linkType))
            {
                MessageBox.Show(stringsCollection.NotValidLinkType, stringsCollection.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 检查是否需要管理员权限
            bool requiresAdmin = linkType == stringsCollection.SymbolicLink || linkType == stringsCollection.DirectoryJunction;

            if (requiresAdmin && !AdminHelper.IsRunAsAdmin())
            {
                // 提示用户以管理员身份重新启动
                DialogResult result = MessageBox.Show(stringsCollection.AdminPermission, stringsCollection.PermissionRequired, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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

            if (linkType == stringsCollection.SymbolicLink)
            {
                args += " /D";
            }
            else if (linkType == stringsCollection.HardLink)
            {
                args += " /H";
            }
            else if (linkType == stringsCollection.DirectoryJunction)
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
                MessageBox.Show(stringsCollection.ErrorNotLinkType_RemoveLink, stringsCollection.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 获取链接类型（假设你有一个控件来选择链接类型，比如一个ComboBox）
                string linkType = cbLinkType.Text;

                if (string.IsNullOrEmpty(linkType))
                {
                    MessageBox.Show(stringsCollection.NotValidLinkType, stringsCollection.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (linkType == stringsCollection.DirectoryJunction)
                {
                    // 目录联接，使用 Directory.Delete
                    if (Directory.Exists(linkPath))
                    {
                        Directory.Delete(linkPath, true); // true 表示递归删除
                        MessageBox.Show(stringsCollection.DirectoryJunctionRemoved, stringsCollection.Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(stringsCollection.NotDirectoryJunction, stringsCollection.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // 硬链接和符号链接，使用 File.Delete
                    if (File.Exists(linkPath) || Directory.Exists(linkPath))
                    {
                        File.Delete(linkPath); // 对于文件硬链接或符号链接，使用 File.Delete
                        MessageBox.Show(stringsCollection.LinkRemovedSuccessfully, stringsCollection.Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(stringsCollection.SpecificPathNotValid, stringsCollection.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{stringsCollection.FailedToRemoveLink} {ex.Message}", stringsCollection.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnBrowseTarget_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog TargetDir = new FolderBrowserDialog())
            {
                TargetDir.Description = stringsCollection.SelectFolder;
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
                LinkDir.Description = stringsCollection.SelectFolder;
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

                MessageBox.Show(result, stringsCollection.Output, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{stringsCollection.FailedToExecuteCommand} {ex.Message}", stringsCollection.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // labelLinkType 点击事件处理程序（如果需要）
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            // labelTargetPath 点击事件处理程序（如果需要）
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm(this);
            // 设置语言
            aboutForm.SetLanguage(CurrentLanguage);
            aboutForm.ShowDialog();
        }

        private void btnChangeLanguage_Click(object sender, EventArgs e)
        {
            if ( IsEnglish )
            {
                SetLanguage(Language.Chinese);
            }
            else
            {
                SetLanguage(Language.English);
            }
        }
    }
}
