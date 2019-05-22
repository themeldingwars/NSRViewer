namespace NSRViewer
{
    partial class Ghost_Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ghost_Info));
            this.GhostInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.KeyframesListBox = new System.Windows.Forms.ListView();
            this.GhostPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.KeyframesValLabel = new System.Windows.Forms.Label();
            this.KeyframesLabel = new System.Windows.Forms.Label();
            this.RawSizeValLabel = new System.Windows.Forms.Label();
            this.RawSizeLabel = new System.Windows.Forms.Label();
            this.GhostInfoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // GhostInfoGroupBox
            // 
            this.GhostInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GhostInfoGroupBox.Controls.Add(this.KeyframesListBox);
            this.GhostInfoGroupBox.Controls.Add(this.GhostPropertyGrid);
            this.GhostInfoGroupBox.Controls.Add(this.KeyframesValLabel);
            this.GhostInfoGroupBox.Controls.Add(this.KeyframesLabel);
            this.GhostInfoGroupBox.Controls.Add(this.RawSizeValLabel);
            this.GhostInfoGroupBox.Controls.Add(this.RawSizeLabel);
            this.GhostInfoGroupBox.ForeColor = System.Drawing.Color.DarkGray;
            this.GhostInfoGroupBox.Location = new System.Drawing.Point(12, 12);
            this.GhostInfoGroupBox.Name = "GhostInfoGroupBox";
            this.GhostInfoGroupBox.Size = new System.Drawing.Size(750, 357);
            this.GhostInfoGroupBox.TabIndex = 0;
            this.GhostInfoGroupBox.TabStop = false;
            this.GhostInfoGroupBox.Text = "<File>";
            // 
            // KeyframesListBox
            // 
            this.KeyframesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.KeyframesListBox.AutoArrange = false;
            this.KeyframesListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.KeyframesListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KeyframesListBox.ForeColor = System.Drawing.Color.DarkGray;
            this.KeyframesListBox.FullRowSelect = true;
            this.KeyframesListBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.KeyframesListBox.HideSelection = false;
            this.KeyframesListBox.LabelWrap = false;
            this.KeyframesListBox.Location = new System.Drawing.Point(6, 68);
            this.KeyframesListBox.MultiSelect = false;
            this.KeyframesListBox.Name = "KeyframesListBox";
            this.KeyframesListBox.ShowGroups = false;
            this.KeyframesListBox.Size = new System.Drawing.Size(351, 283);
            this.KeyframesListBox.TabIndex = 4;
            this.KeyframesListBox.UseCompatibleStateImageBehavior = false;
            this.KeyframesListBox.View = System.Windows.Forms.View.Details;
            this.KeyframesListBox.VirtualMode = true;
            this.KeyframesListBox.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.KeyframesListBox_RetrieveVirtualItem);
            this.KeyframesListBox.SelectedIndexChanged += new System.EventHandler(this.KeyframesListBox_SelectedIndexChanged);
            // 
            // GhostPropertyGrid
            // 
            this.GhostPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GhostPropertyGrid.CanShowVisualStyleGlyphs = false;
            this.GhostPropertyGrid.CategoryForeColor = System.Drawing.Color.DarkGray;
            this.GhostPropertyGrid.CategorySplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.GhostPropertyGrid.CommandsVisibleIfAvailable = false;
            this.GhostPropertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.GhostPropertyGrid.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.GhostPropertyGrid.HelpBorderColor = System.Drawing.Color.Gray;
            this.GhostPropertyGrid.HelpForeColor = System.Drawing.Color.DarkGray;
            this.GhostPropertyGrid.HelpVisible = false;
            this.GhostPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.GhostPropertyGrid.Location = new System.Drawing.Point(356, 68);
            this.GhostPropertyGrid.Name = "GhostPropertyGrid";
            this.GhostPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.GhostPropertyGrid.Size = new System.Drawing.Size(388, 283);
            this.GhostPropertyGrid.TabIndex = 5;
            this.GhostPropertyGrid.ToolbarVisible = false;
            this.GhostPropertyGrid.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.GhostPropertyGrid.ViewBorderColor = System.Drawing.Color.Gray;
            this.GhostPropertyGrid.ViewForeColor = System.Drawing.Color.DarkGray;
            // 
            // KeyframesValLabel
            // 
            this.KeyframesValLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KeyframesValLabel.Location = new System.Drawing.Point(123, 42);
            this.KeyframesValLabel.Name = "KeyframesValLabel";
            this.KeyframesValLabel.Size = new System.Drawing.Size(621, 23);
            this.KeyframesValLabel.TabIndex = 3;
            this.KeyframesValLabel.Text = "<NULL>";
            this.KeyframesValLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KeyframesLabel
            // 
            this.KeyframesLabel.Location = new System.Drawing.Point(6, 42);
            this.KeyframesLabel.Name = "KeyframesLabel";
            this.KeyframesLabel.Size = new System.Drawing.Size(111, 23);
            this.KeyframesLabel.TabIndex = 2;
            this.KeyframesLabel.Text = "Keyframes:";
            this.KeyframesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RawSizeValLabel
            // 
            this.RawSizeValLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RawSizeValLabel.Location = new System.Drawing.Point(123, 19);
            this.RawSizeValLabel.Name = "RawSizeValLabel";
            this.RawSizeValLabel.Size = new System.Drawing.Size(621, 23);
            this.RawSizeValLabel.TabIndex = 1;
            this.RawSizeValLabel.Text = "<NULL>";
            this.RawSizeValLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RawSizeLabel
            // 
            this.RawSizeLabel.Location = new System.Drawing.Point(6, 19);
            this.RawSizeLabel.Name = "RawSizeLabel";
            this.RawSizeLabel.Size = new System.Drawing.Size(111, 23);
            this.RawSizeLabel.TabIndex = 0;
            this.RawSizeLabel.Text = "Raw Size:";
            this.RawSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Ghost_Info
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(774, 381);
            this.Controls.Add(this.GhostInfoGroupBox);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DarkGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 420);
            this.Name = "Ghost_Info";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ghost Info";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ghost_Info_FormClosing);
            this.Load += new System.EventHandler(this.Ghost_Info_Load);
            this.GhostInfoGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GhostInfoGroupBox;
        private System.Windows.Forms.Label RawSizeLabel;
        private System.Windows.Forms.Label RawSizeValLabel;
        private System.Windows.Forms.Label KeyframesValLabel;
        private System.Windows.Forms.Label KeyframesLabel;
        private System.Windows.Forms.PropertyGrid GhostPropertyGrid;
        private System.Windows.Forms.ListView KeyframesListBox;
    }
}