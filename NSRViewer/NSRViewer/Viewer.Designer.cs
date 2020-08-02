namespace NSRViewer
{
    partial class Viewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
            this.ReplayDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.ReplayFileListBox = new System.Windows.Forms.ListBox();
            this.ReplayFileItemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyFilePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenInFileExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReplayContextToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.SearchFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InformationGroupBox = new System.Windows.Forms.GroupBox();
            this.FileSizeValLabel = new System.Windows.Forms.Label();
            this.FileSizeLabel = new System.Windows.Forms.Label();
            this.Date2ValLabel = new System.Windows.Forms.Label();
            this.Date2Label = new System.Windows.Forms.Label();
            this.FirefallVersionValLabel = new System.Windows.Forms.Label();
            this.FirefallVersionLabel = new System.Windows.Forms.Label();
            this.UserValLabel = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.CharacterGUIDValLabel = new System.Windows.Forms.Label();
            this.CharacterGUIDLabel = new System.Windows.Forms.Label();
            this.DateValLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.DescriptionValLabel = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.ZoneValLabel = new System.Windows.Forms.Label();
            this.ZoneLabel = new System.Windows.Forms.Label();
            this.ProtocolVersionValLabel = new System.Windows.Forms.Label();
            this.ProtocolVersionLabel = new System.Windows.Forms.Label();
            this.ExportDecompressedFileBtn = new System.Windows.Forms.Button();
            this.CopyInfoBtn = new System.Windows.Forms.Button();
            this.ViewGhostsBtn = new System.Windows.Forms.Button();
            this.ViewInFirefallBtn = new System.Windows.Forms.Button();
            this.ReplayFileItemContextMenu.SuspendLayout();
            this.InformationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReplayDirectoryTextBox
            // 
            this.ReplayDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplayDirectoryTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ReplayDirectoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReplayDirectoryTextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.ReplayDirectoryTextBox.Location = new System.Drawing.Point(12, 12);
            this.ReplayDirectoryTextBox.Name = "ReplayDirectoryTextBox";
            this.ReplayDirectoryTextBox.ReadOnly = true;
            this.ReplayDirectoryTextBox.Size = new System.Drawing.Size(647, 23);
            this.ReplayDirectoryTextBox.TabIndex = 0;
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseBtn.Location = new System.Drawing.Point(665, 12);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(42, 23);
            this.BrowseBtn.TabIndex = 1;
            this.BrowseBtn.Text = "...";
            this.BrowseBtn.UseVisualStyleBackColor = true;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // ReplayFileListBox
            // 
            this.ReplayFileListBox.AllowDrop = true;
            this.ReplayFileListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplayFileListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ReplayFileListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReplayFileListBox.ContextMenuStrip = this.ReplayFileItemContextMenu;
            this.ReplayFileListBox.ForeColor = System.Drawing.Color.DarkGray;
            this.ReplayFileListBox.FormattingEnabled = true;
            this.ReplayFileListBox.IntegralHeight = false;
            this.ReplayFileListBox.ItemHeight = 15;
            this.ReplayFileListBox.Location = new System.Drawing.Point(12, 41);
            this.ReplayFileListBox.Name = "ReplayFileListBox";
            this.ReplayFileListBox.Size = new System.Drawing.Size(695, 182);
            this.ReplayFileListBox.TabIndex = 2;
            this.ReplayFileListBox.SelectedIndexChanged += new System.EventHandler(this.ReplayFileListBox_SelectedIndexChanged);
            this.ReplayFileListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.ReplayFileListBox_DragDrop);
            this.ReplayFileListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.ReplayFileListBox_DragEnter);
            // 
            // ReplayFileItemContextMenu
            // 
            this.ReplayFileItemContextMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ReplayFileItemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyFilePathToolStripMenuItem,
            this.OpenInFileExplorerToolStripMenuItem,
            this.ReplayContextToolStripSeparator,
            this.SearchFilesToolStripMenuItem});
            this.ReplayFileItemContextMenu.Name = "ReplayFileItemContextMenu";
            this.ReplayFileItemContextMenu.ShowImageMargin = false;
            this.ReplayFileItemContextMenu.Size = new System.Drawing.Size(159, 76);
            // 
            // CopyFilePathToolStripMenuItem
            // 
            this.CopyFilePathToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.CopyFilePathToolStripMenuItem.Name = "CopyFilePathToolStripMenuItem";
            this.CopyFilePathToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.CopyFilePathToolStripMenuItem.Text = "Copy File Path";
            this.CopyFilePathToolStripMenuItem.Click += new System.EventHandler(this.CopyFilePathToolStripMenuItem_Click);
            // 
            // OpenInFileExplorerToolStripMenuItem
            // 
            this.OpenInFileExplorerToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.OpenInFileExplorerToolStripMenuItem.Name = "OpenInFileExplorerToolStripMenuItem";
            this.OpenInFileExplorerToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.OpenInFileExplorerToolStripMenuItem.Text = "Open in File Explorer";
            this.OpenInFileExplorerToolStripMenuItem.Click += new System.EventHandler(this.OpenInFileExplorerToolStripMenuItem_Click);
            // 
            // ReplayContextToolStripSeparator
            // 
            this.ReplayContextToolStripSeparator.Name = "ReplayContextToolStripSeparator";
            this.ReplayContextToolStripSeparator.Size = new System.Drawing.Size(155, 6);
            // 
            // SearchFilesToolStripMenuItem
            // 
            this.SearchFilesToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray;
            this.SearchFilesToolStripMenuItem.Name = "SearchFilesToolStripMenuItem";
            this.SearchFilesToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.SearchFilesToolStripMenuItem.Text = "Search Files";
            this.SearchFilesToolStripMenuItem.Click += new System.EventHandler(this.SearchFilesToolStripMenuItem_Click);
            // 
            // InformationGroupBox
            // 
            this.InformationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InformationGroupBox.Controls.Add(this.FileSizeValLabel);
            this.InformationGroupBox.Controls.Add(this.FileSizeLabel);
            this.InformationGroupBox.Controls.Add(this.Date2ValLabel);
            this.InformationGroupBox.Controls.Add(this.Date2Label);
            this.InformationGroupBox.Controls.Add(this.FirefallVersionValLabel);
            this.InformationGroupBox.Controls.Add(this.FirefallVersionLabel);
            this.InformationGroupBox.Controls.Add(this.UserValLabel);
            this.InformationGroupBox.Controls.Add(this.UserLabel);
            this.InformationGroupBox.Controls.Add(this.CharacterGUIDValLabel);
            this.InformationGroupBox.Controls.Add(this.CharacterGUIDLabel);
            this.InformationGroupBox.Controls.Add(this.DateValLabel);
            this.InformationGroupBox.Controls.Add(this.DateLabel);
            this.InformationGroupBox.Controls.Add(this.DescriptionValLabel);
            this.InformationGroupBox.Controls.Add(this.DescriptionLabel);
            this.InformationGroupBox.Controls.Add(this.ZoneValLabel);
            this.InformationGroupBox.Controls.Add(this.ZoneLabel);
            this.InformationGroupBox.Controls.Add(this.ProtocolVersionValLabel);
            this.InformationGroupBox.Controls.Add(this.ProtocolVersionLabel);
            this.InformationGroupBox.ForeColor = System.Drawing.Color.DarkGray;
            this.InformationGroupBox.Location = new System.Drawing.Point(12, 228);
            this.InformationGroupBox.Name = "InformationGroupBox";
            this.InformationGroupBox.Size = new System.Drawing.Size(695, 233);
            this.InformationGroupBox.TabIndex = 3;
            this.InformationGroupBox.TabStop = false;
            this.InformationGroupBox.Text = "Network Stream Replay Information";
            // 
            // FileSizeValLabel
            // 
            this.FileSizeValLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileSizeValLabel.Location = new System.Drawing.Point(123, 203);
            this.FileSizeValLabel.Name = "FileSizeValLabel";
            this.FileSizeValLabel.Size = new System.Drawing.Size(566, 23);
            this.FileSizeValLabel.TabIndex = 17;
            this.FileSizeValLabel.Text = "<NULL>";
            this.FileSizeValLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FileSizeLabel
            // 
            this.FileSizeLabel.Location = new System.Drawing.Point(6, 203);
            this.FileSizeLabel.Name = "FileSizeLabel";
            this.FileSizeLabel.Size = new System.Drawing.Size(111, 23);
            this.FileSizeLabel.TabIndex = 16;
            this.FileSizeLabel.Text = "File Size:";
            this.FileSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Date2ValLabel
            // 
            this.Date2ValLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Date2ValLabel.Location = new System.Drawing.Point(123, 180);
            this.Date2ValLabel.Name = "Date2ValLabel";
            this.Date2ValLabel.Size = new System.Drawing.Size(566, 23);
            this.Date2ValLabel.TabIndex = 15;
            this.Date2ValLabel.Text = "<NULL>";
            this.Date2ValLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Date2Label
            // 
            this.Date2Label.Location = new System.Drawing.Point(6, 180);
            this.Date2Label.Name = "Date2Label";
            this.Date2Label.Size = new System.Drawing.Size(111, 23);
            this.Date2Label.TabIndex = 14;
            this.Date2Label.Text = "Game Date:";
            this.Date2Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FirefallVersionValLabel
            // 
            this.FirefallVersionValLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FirefallVersionValLabel.Location = new System.Drawing.Point(123, 157);
            this.FirefallVersionValLabel.Name = "FirefallVersionValLabel";
            this.FirefallVersionValLabel.Size = new System.Drawing.Size(566, 23);
            this.FirefallVersionValLabel.TabIndex = 13;
            this.FirefallVersionValLabel.Text = "<NULL>";
            this.FirefallVersionValLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FirefallVersionLabel
            // 
            this.FirefallVersionLabel.Location = new System.Drawing.Point(6, 157);
            this.FirefallVersionLabel.Name = "FirefallVersionLabel";
            this.FirefallVersionLabel.Size = new System.Drawing.Size(111, 23);
            this.FirefallVersionLabel.TabIndex = 12;
            this.FirefallVersionLabel.Text = "Firefall Version:";
            this.FirefallVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserValLabel
            // 
            this.UserValLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserValLabel.Location = new System.Drawing.Point(123, 134);
            this.UserValLabel.Name = "UserValLabel";
            this.UserValLabel.Size = new System.Drawing.Size(566, 23);
            this.UserValLabel.TabIndex = 11;
            this.UserValLabel.Text = "<NULL>";
            this.UserValLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserLabel
            // 
            this.UserLabel.Location = new System.Drawing.Point(6, 134);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(111, 23);
            this.UserLabel.TabIndex = 10;
            this.UserLabel.Text = "Character Name:";
            this.UserLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CharacterGUIDValLabel
            // 
            this.CharacterGUIDValLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CharacterGUIDValLabel.Location = new System.Drawing.Point(123, 111);
            this.CharacterGUIDValLabel.Name = "CharacterGUIDValLabel";
            this.CharacterGUIDValLabel.Size = new System.Drawing.Size(566, 23);
            this.CharacterGUIDValLabel.TabIndex = 9;
            this.CharacterGUIDValLabel.Text = "<NULL>";
            this.CharacterGUIDValLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CharacterGUIDLabel
            // 
            this.CharacterGUIDLabel.Location = new System.Drawing.Point(6, 111);
            this.CharacterGUIDLabel.Name = "CharacterGUIDLabel";
            this.CharacterGUIDLabel.Size = new System.Drawing.Size(111, 23);
            this.CharacterGUIDLabel.TabIndex = 8;
            this.CharacterGUIDLabel.Text = "Character GUID:";
            this.CharacterGUIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DateValLabel
            // 
            this.DateValLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DateValLabel.Location = new System.Drawing.Point(123, 88);
            this.DateValLabel.Name = "DateValLabel";
            this.DateValLabel.Size = new System.Drawing.Size(566, 23);
            this.DateValLabel.TabIndex = 7;
            this.DateValLabel.Text = "<NULL>";
            this.DateValLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DateLabel
            // 
            this.DateLabel.Location = new System.Drawing.Point(6, 88);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(111, 23);
            this.DateLabel.TabIndex = 6;
            this.DateLabel.Text = "Recording Date:";
            this.DateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DescriptionValLabel
            // 
            this.DescriptionValLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionValLabel.Location = new System.Drawing.Point(123, 65);
            this.DescriptionValLabel.Name = "DescriptionValLabel";
            this.DescriptionValLabel.Size = new System.Drawing.Size(566, 23);
            this.DescriptionValLabel.TabIndex = 5;
            this.DescriptionValLabel.Text = "<NULL>";
            this.DescriptionValLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.Location = new System.Drawing.Point(6, 65);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(111, 23);
            this.DescriptionLabel.TabIndex = 4;
            this.DescriptionLabel.Text = "Description:";
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ZoneValLabel
            // 
            this.ZoneValLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoneValLabel.Location = new System.Drawing.Point(123, 42);
            this.ZoneValLabel.Name = "ZoneValLabel";
            this.ZoneValLabel.Size = new System.Drawing.Size(566, 23);
            this.ZoneValLabel.TabIndex = 3;
            this.ZoneValLabel.Text = "<NULL>";
            this.ZoneValLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ZoneLabel
            // 
            this.ZoneLabel.Location = new System.Drawing.Point(6, 42);
            this.ZoneLabel.Name = "ZoneLabel";
            this.ZoneLabel.Size = new System.Drawing.Size(111, 23);
            this.ZoneLabel.TabIndex = 2;
            this.ZoneLabel.Text = "Zone:";
            this.ZoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProtocolVersionValLabel
            // 
            this.ProtocolVersionValLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProtocolVersionValLabel.Location = new System.Drawing.Point(123, 19);
            this.ProtocolVersionValLabel.Name = "ProtocolVersionValLabel";
            this.ProtocolVersionValLabel.Size = new System.Drawing.Size(566, 23);
            this.ProtocolVersionValLabel.TabIndex = 1;
            this.ProtocolVersionValLabel.Text = "<NULL>";
            this.ProtocolVersionValLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProtocolVersionLabel
            // 
            this.ProtocolVersionLabel.Location = new System.Drawing.Point(6, 19);
            this.ProtocolVersionLabel.Name = "ProtocolVersionLabel";
            this.ProtocolVersionLabel.Size = new System.Drawing.Size(111, 23);
            this.ProtocolVersionLabel.TabIndex = 0;
            this.ProtocolVersionLabel.Text = "Protocol Version:";
            this.ProtocolVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExportDecompressedFileBtn
            // 
            this.ExportDecompressedFileBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExportDecompressedFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportDecompressedFileBtn.Location = new System.Drawing.Point(12, 467);
            this.ExportDecompressedFileBtn.Name = "ExportDecompressedFileBtn";
            this.ExportDecompressedFileBtn.Size = new System.Drawing.Size(175, 25);
            this.ExportDecompressedFileBtn.TabIndex = 4;
            this.ExportDecompressedFileBtn.Text = "Export Decompressed File";
            this.ExportDecompressedFileBtn.UseVisualStyleBackColor = true;
            this.ExportDecompressedFileBtn.Click += new System.EventHandler(this.ExportDecompressedFileBtn_Click);
            // 
            // CopyInfoBtn
            // 
            this.CopyInfoBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyInfoBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyInfoBtn.Location = new System.Drawing.Point(399, 467);
            this.CopyInfoBtn.Name = "CopyInfoBtn";
            this.CopyInfoBtn.Size = new System.Drawing.Size(175, 25);
            this.CopyInfoBtn.TabIndex = 5;
            this.CopyInfoBtn.Text = "Copy Info to Clipboard";
            this.CopyInfoBtn.UseVisualStyleBackColor = true;
            this.CopyInfoBtn.Click += new System.EventHandler(this.CopyInfoBtn_Click);
            // 
            // ViewGhostsBtn
            // 
            this.ViewGhostsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ViewGhostsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewGhostsBtn.Location = new System.Drawing.Point(193, 467);
            this.ViewGhostsBtn.Name = "ViewGhostsBtn";
            this.ViewGhostsBtn.Size = new System.Drawing.Size(200, 25);
            this.ViewGhostsBtn.TabIndex = 6;
            this.ViewGhostsBtn.Text = "GHOSTS";
            this.ViewGhostsBtn.UseVisualStyleBackColor = true;
            this.ViewGhostsBtn.Click += new System.EventHandler(this.ViewGhostsBtn_Click);
            // 
            // ViewInFirefallBtn
            // 
            this.ViewInFirefallBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewInFirefallBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewInFirefallBtn.Location = new System.Drawing.Point(580, 467);
            this.ViewInFirefallBtn.Name = "ViewInFirefallBtn";
            this.ViewInFirefallBtn.Size = new System.Drawing.Size(127, 25);
            this.ViewInFirefallBtn.TabIndex = 7;
            this.ViewInFirefallBtn.Text = "View in Firefall";
            this.ViewInFirefallBtn.UseVisualStyleBackColor = true;
            this.ViewInFirefallBtn.Click += new System.EventHandler(this.ViewInFirefallBtn_Click);
            // 
            // Viewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(718, 501);
            this.Controls.Add(this.ViewInFirefallBtn);
            this.Controls.Add(this.ViewGhostsBtn);
            this.Controls.Add(this.CopyInfoBtn);
            this.Controls.Add(this.ExportDecompressedFileBtn);
            this.Controls.Add(this.InformationGroupBox);
            this.Controls.Add(this.ReplayFileListBox);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(this.ReplayDirectoryTextBox);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DarkGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(734, 430);
            this.Name = "Viewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Network Stream Replay Viewer";
            this.ReplayFileItemContextMenu.ResumeLayout(false);
            this.InformationGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ReplayDirectoryTextBox;
        private System.Windows.Forms.Button BrowseBtn;
        private System.Windows.Forms.ListBox ReplayFileListBox;
        private System.Windows.Forms.GroupBox InformationGroupBox;
        private System.Windows.Forms.Label DescriptionValLabel;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Label ZoneValLabel;
        private System.Windows.Forms.Label ZoneLabel;
        private System.Windows.Forms.Label ProtocolVersionValLabel;
        private System.Windows.Forms.Label ProtocolVersionLabel;
        private System.Windows.Forms.Label CharacterGUIDValLabel;
        private System.Windows.Forms.Label CharacterGUIDLabel;
        private System.Windows.Forms.Label DateValLabel;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.Label UserValLabel;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Label Date2ValLabel;
        private System.Windows.Forms.Label Date2Label;
        private System.Windows.Forms.Label FirefallVersionValLabel;
        private System.Windows.Forms.Label FirefallVersionLabel;
        private System.Windows.Forms.Button ExportDecompressedFileBtn;
        private System.Windows.Forms.Button CopyInfoBtn;
        private System.Windows.Forms.Button ViewGhostsBtn;
        private System.Windows.Forms.Button ViewInFirefallBtn;
        private System.Windows.Forms.Label FileSizeValLabel;
        private System.Windows.Forms.Label FileSizeLabel;
        private System.Windows.Forms.ContextMenuStrip ReplayFileItemContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CopyFilePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenInFileExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ReplayContextToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem SearchFilesToolStripMenuItem;
    }
}

