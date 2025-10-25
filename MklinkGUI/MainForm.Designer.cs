using System;
using System.Globalization;

namespace MklinkGUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cbLinkType = new System.Windows.Forms.ComboBox();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.txtLinkPath = new System.Windows.Forms.TextBox();
            this.btnCreateLink = new System.Windows.Forms.Button();
            this.btnRemoveLink = new System.Windows.Forms.Button();
            this.btnBrowseTarget = new System.Windows.Forms.Button();
            this.btnBrowseLink = new System.Windows.Forms.Button();
            this.labelLinkType = new System.Windows.Forms.Label();
            this.labelTargetPath = new System.Windows.Forms.Label();
            this.labelLinkPath = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbLinkType
            // 
            this.cbLinkType.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cbLinkType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLinkType.FormattingEnabled = true;
            this.cbLinkType.Items.AddRange(new object[] {
            "Directory Junction",
            "Hard Link",
            "Symbolic Link"});
            this.cbLinkType.Location = new System.Drawing.Point(6, 24);
            this.cbLinkType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbLinkType.MinimumSize = new System.Drawing.Size(169, 0);
            this.cbLinkType.Name = "cbLinkType";
            this.cbLinkType.Size = new System.Drawing.Size(169, 25);
            this.cbLinkType.TabIndex = 0;
            // 
            // txtTargetPath
            // 
            this.txtTargetPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTargetPath.Location = new System.Drawing.Point(6, 82);
            this.txtTargetPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.Size = new System.Drawing.Size(429, 23);
            this.txtTargetPath.TabIndex = 1;
            // 
            // txtLinkPath
            // 
            this.txtLinkPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLinkPath.Location = new System.Drawing.Point(6, 131);
            this.txtLinkPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLinkPath.Name = "txtLinkPath";
            this.txtLinkPath.Size = new System.Drawing.Size(429, 23);
            this.txtLinkPath.TabIndex = 2;
            // 
            // btnCreateLink
            // 
            this.btnCreateLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateLink.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateLink.Location = new System.Drawing.Point(183, 24);
            this.btnCreateLink.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCreateLink.Name = "btnCreateLink";
            this.btnCreateLink.Size = new System.Drawing.Size(122, 25);
            this.btnCreateLink.TabIndex = 3;
            this.btnCreateLink.Text = "Create Link";
            this.btnCreateLink.UseVisualStyleBackColor = true;
            this.btnCreateLink.Click += new System.EventHandler(this.btnCreateLink_Click);
            // 
            // btnRemoveLink
            // 
            this.btnRemoveLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveLink.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemoveLink.Location = new System.Drawing.Point(313, 24);
            this.btnRemoveLink.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemoveLink.Name = "btnRemoveLink";
            this.btnRemoveLink.Size = new System.Drawing.Size(122, 25);
            this.btnRemoveLink.TabIndex = 4;
            this.btnRemoveLink.Text = "Remove Link";
            this.btnRemoveLink.UseVisualStyleBackColor = true;
            this.btnRemoveLink.Click += new System.EventHandler(this.btnRemoveLink_Click);
            // 
            // btnBrowseTarget
            // 
            this.btnBrowseTarget.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBrowseTarget.Location = new System.Drawing.Point(443, 82);
            this.btnBrowseTarget.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowseTarget.Name = "btnBrowseTarget";
            this.btnBrowseTarget.Size = new System.Drawing.Size(122, 23);
            this.btnBrowseTarget.TabIndex = 5;
            this.btnBrowseTarget.Text = "Browse...";
            this.btnBrowseTarget.UseVisualStyleBackColor = true;
            this.btnBrowseTarget.Click += new System.EventHandler(this.btnBrowseTarget_Click);
            // 
            // btnBrowseLink
            // 
            this.btnBrowseLink.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBrowseLink.Location = new System.Drawing.Point(443, 131);
            this.btnBrowseLink.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowseLink.Name = "btnBrowseLink";
            this.btnBrowseLink.Size = new System.Drawing.Size(122, 23);
            this.btnBrowseLink.TabIndex = 6;
            this.btnBrowseLink.Text = "Browse...";
            this.btnBrowseLink.UseVisualStyleBackColor = true;
            this.btnBrowseLink.Click += new System.EventHandler(this.btnBrowseLink_Click);
            // 
            // labelLinkType
            // 
            this.labelLinkType.AutoSize = true;
            this.labelLinkType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLinkType.Location = new System.Drawing.Point(3, 3);
            this.labelLinkType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLinkType.Name = "labelLinkType";
            this.labelLinkType.Size = new System.Drawing.Size(66, 17);
            this.labelLinkType.TabIndex = 7;
            this.labelLinkType.Text = "Link Type:";
            this.labelLinkType.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelTargetPath
            // 
            this.labelTargetPath.AutoSize = true;
            this.labelTargetPath.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelTargetPath.Location = new System.Drawing.Point(3, 61);
            this.labelTargetPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTargetPath.Name = "labelTargetPath";
            this.labelTargetPath.Size = new System.Drawing.Size(78, 17);
            this.labelTargetPath.TabIndex = 8;
            this.labelTargetPath.Text = "Target Path:";
            this.labelTargetPath.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // labelLinkPath
            // 
            this.labelLinkPath.AutoSize = true;
            this.labelLinkPath.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelLinkPath.Location = new System.Drawing.Point(3, 110);
            this.labelLinkPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLinkPath.Name = "labelLinkPath";
            this.labelLinkPath.Size = new System.Drawing.Size(63, 17);
            this.labelLinkPath.TabIndex = 9;
            this.labelLinkPath.Text = "Link Path:";
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnAbout.Location = new System.Drawing.Point(443, 24);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(122, 25);
            this.btnAbout.TabIndex = 10;
            this.btnAbout.Text = "About...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(571, 159);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.labelLinkPath);
            this.Controls.Add(this.labelTargetPath);
            this.Controls.Add(this.labelLinkType);
            this.Controls.Add(this.btnBrowseLink);
            this.Controls.Add(this.btnBrowseTarget);
            this.Controls.Add(this.btnRemoveLink);
            this.Controls.Add(this.btnCreateLink);
            this.Controls.Add(this.txtLinkPath);
            this.Controls.Add(this.txtTargetPath);
            this.Controls.Add(this.cbLinkType);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(587, 198);
            this.Name = "MainForm";
            this.Text = "MkLink GUI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLinkType;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.TextBox txtLinkPath;
        private System.Windows.Forms.Button btnCreateLink;
        private System.Windows.Forms.Button btnRemoveLink;
        private System.Windows.Forms.Button btnBrowseTarget;
        private System.Windows.Forms.Button btnBrowseLink;
        private System.Windows.Forms.Label labelLinkType;
        private System.Windows.Forms.Label labelTargetPath;
        private System.Windows.Forms.Label labelLinkPath;
        private System.Windows.Forms.Button btnAbout;
    }
}

