namespace NSRViewer
{
    partial class Searcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Searcher));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ProtocolVersionRadioBtn = new System.Windows.Forms.RadioButton();
            this.ZoneIDRadioBtn = new System.Windows.Forms.RadioButton();
            this.ZoneRadioBtn = new System.Windows.Forms.RadioButton();
            this.DescriptionRadioBtn = new System.Windows.Forms.RadioButton();
            this.RecordingDateRadioBtn = new System.Windows.Forms.RadioButton();
            this.CharacterGUIDRadioBtn = new System.Windows.Forms.RadioButton();
            this.CharacterNameRadioBtn = new System.Windows.Forms.RadioButton();
            this.FirefallVersionRadioBtn = new System.Windows.Forms.RadioButton();
            this.GameDateRadioBtn = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SubstringRadioBtn = new System.Windows.Forms.RadioButton();
            this.ExactTextRadioBtn = new System.Windows.Forms.RadioButton();
            this.SearchStringTextBox = new System.Windows.Forms.TextBox();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.ClearCurrentSearchCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.GameDateRadioBtn);
            this.groupBox1.Controls.Add(this.FirefallVersionRadioBtn);
            this.groupBox1.Controls.Add(this.CharacterNameRadioBtn);
            this.groupBox1.Controls.Add(this.CharacterGUIDRadioBtn);
            this.groupBox1.Controls.Add(this.RecordingDateRadioBtn);
            this.groupBox1.Controls.Add(this.DescriptionRadioBtn);
            this.groupBox1.Controls.Add(this.ZoneRadioBtn);
            this.groupBox1.Controls.Add(this.ZoneIDRadioBtn);
            this.groupBox1.Controls.Add(this.ProtocolVersionRadioBtn);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkGray;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Field";
            // 
            // ProtocolVersionRadioBtn
            // 
            this.ProtocolVersionRadioBtn.Checked = true;
            this.ProtocolVersionRadioBtn.Location = new System.Drawing.Point(6, 22);
            this.ProtocolVersionRadioBtn.Name = "ProtocolVersionRadioBtn";
            this.ProtocolVersionRadioBtn.Size = new System.Drawing.Size(129, 23);
            this.ProtocolVersionRadioBtn.TabIndex = 0;
            this.ProtocolVersionRadioBtn.TabStop = true;
            this.ProtocolVersionRadioBtn.Text = "Protocol Version";
            this.ProtocolVersionRadioBtn.UseVisualStyleBackColor = true;
            // 
            // ZoneIDRadioBtn
            // 
            this.ZoneIDRadioBtn.Location = new System.Drawing.Point(141, 22);
            this.ZoneIDRadioBtn.Name = "ZoneIDRadioBtn";
            this.ZoneIDRadioBtn.Size = new System.Drawing.Size(82, 23);
            this.ZoneIDRadioBtn.TabIndex = 1;
            this.ZoneIDRadioBtn.Text = "Zone ID";
            this.ZoneIDRadioBtn.UseVisualStyleBackColor = true;
            // 
            // ZoneRadioBtn
            // 
            this.ZoneRadioBtn.Location = new System.Drawing.Point(229, 22);
            this.ZoneRadioBtn.Name = "ZoneRadioBtn";
            this.ZoneRadioBtn.Size = new System.Drawing.Size(67, 23);
            this.ZoneRadioBtn.TabIndex = 2;
            this.ZoneRadioBtn.Text = "Zone";
            this.ZoneRadioBtn.UseVisualStyleBackColor = true;
            // 
            // DescriptionRadioBtn
            // 
            this.DescriptionRadioBtn.Location = new System.Drawing.Point(302, 22);
            this.DescriptionRadioBtn.Name = "DescriptionRadioBtn";
            this.DescriptionRadioBtn.Size = new System.Drawing.Size(100, 23);
            this.DescriptionRadioBtn.TabIndex = 3;
            this.DescriptionRadioBtn.Text = "Description";
            this.DescriptionRadioBtn.UseVisualStyleBackColor = true;
            // 
            // RecordingDateRadioBtn
            // 
            this.RecordingDateRadioBtn.Location = new System.Drawing.Point(408, 22);
            this.RecordingDateRadioBtn.Name = "RecordingDateRadioBtn";
            this.RecordingDateRadioBtn.Size = new System.Drawing.Size(116, 23);
            this.RecordingDateRadioBtn.TabIndex = 4;
            this.RecordingDateRadioBtn.Text = "Recording Date";
            this.RecordingDateRadioBtn.UseVisualStyleBackColor = true;
            // 
            // CharacterGUIDRadioBtn
            // 
            this.CharacterGUIDRadioBtn.Location = new System.Drawing.Point(6, 51);
            this.CharacterGUIDRadioBtn.Name = "CharacterGUIDRadioBtn";
            this.CharacterGUIDRadioBtn.Size = new System.Drawing.Size(119, 23);
            this.CharacterGUIDRadioBtn.TabIndex = 5;
            this.CharacterGUIDRadioBtn.Text = "Character GUID";
            this.CharacterGUIDRadioBtn.UseVisualStyleBackColor = true;
            // 
            // CharacterNameRadioBtn
            // 
            this.CharacterNameRadioBtn.Location = new System.Drawing.Point(131, 51);
            this.CharacterNameRadioBtn.Name = "CharacterNameRadioBtn";
            this.CharacterNameRadioBtn.Size = new System.Drawing.Size(126, 23);
            this.CharacterNameRadioBtn.TabIndex = 6;
            this.CharacterNameRadioBtn.Text = "Character Name";
            this.CharacterNameRadioBtn.UseVisualStyleBackColor = true;
            // 
            // FirefallVersionRadioBtn
            // 
            this.FirefallVersionRadioBtn.Location = new System.Drawing.Point(263, 51);
            this.FirefallVersionRadioBtn.Name = "FirefallVersionRadioBtn";
            this.FirefallVersionRadioBtn.Size = new System.Drawing.Size(111, 23);
            this.FirefallVersionRadioBtn.TabIndex = 7;
            this.FirefallVersionRadioBtn.Text = "Firefall Version";
            this.FirefallVersionRadioBtn.UseVisualStyleBackColor = true;
            // 
            // GameDateRadioBtn
            // 
            this.GameDateRadioBtn.Location = new System.Drawing.Point(380, 51);
            this.GameDateRadioBtn.Name = "GameDateRadioBtn";
            this.GameDateRadioBtn.Size = new System.Drawing.Size(98, 23);
            this.GameDateRadioBtn.TabIndex = 8;
            this.GameDateRadioBtn.Text = "Game Date";
            this.GameDateRadioBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ClearCurrentSearchCheckBox);
            this.groupBox2.Controls.Add(this.ExactTextRadioBtn);
            this.groupBox2.Controls.Add(this.SubstringRadioBtn);
            this.groupBox2.ForeColor = System.Drawing.Color.DarkGray;
            this.groupBox2.Location = new System.Drawing.Point(12, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(530, 59);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Options";
            // 
            // SubstringRadioBtn
            // 
            this.SubstringRadioBtn.Checked = true;
            this.SubstringRadioBtn.Location = new System.Drawing.Point(6, 22);
            this.SubstringRadioBtn.Name = "SubstringRadioBtn";
            this.SubstringRadioBtn.Size = new System.Drawing.Size(86, 23);
            this.SubstringRadioBtn.TabIndex = 0;
            this.SubstringRadioBtn.TabStop = true;
            this.SubstringRadioBtn.Text = "Substring";
            this.SubstringRadioBtn.UseVisualStyleBackColor = true;
            // 
            // ExactTextRadioBtn
            // 
            this.ExactTextRadioBtn.Location = new System.Drawing.Point(98, 22);
            this.ExactTextRadioBtn.Name = "ExactTextRadioBtn";
            this.ExactTextRadioBtn.Size = new System.Drawing.Size(86, 23);
            this.ExactTextRadioBtn.TabIndex = 1;
            this.ExactTextRadioBtn.Text = "Exact Text";
            this.ExactTextRadioBtn.UseVisualStyleBackColor = true;
            // 
            // SearchStringTextBox
            // 
            this.SearchStringTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchStringTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.SearchStringTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchStringTextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.SearchStringTextBox.Location = new System.Drawing.Point(12, 170);
            this.SearchStringTextBox.Name = "SearchStringTextBox";
            this.SearchStringTextBox.Size = new System.Drawing.Size(530, 23);
            this.SearchStringTextBox.TabIndex = 2;
            // 
            // SearchBtn
            // 
            this.SearchBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchBtn.Location = new System.Drawing.Point(12, 199);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(530, 23);
            this.SearchBtn.TabIndex = 3;
            this.SearchBtn.Text = "Search";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // ClearCurrentSearchCheckBox
            // 
            this.ClearCurrentSearchCheckBox.Checked = true;
            this.ClearCurrentSearchCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ClearCurrentSearchCheckBox.Location = new System.Drawing.Point(380, 22);
            this.ClearCurrentSearchCheckBox.Name = "ClearCurrentSearchCheckBox";
            this.ClearCurrentSearchCheckBox.Size = new System.Drawing.Size(144, 23);
            this.ClearCurrentSearchCheckBox.TabIndex = 2;
            this.ClearCurrentSearchCheckBox.Text = "Clear Current Search";
            this.ClearCurrentSearchCheckBox.UseVisualStyleBackColor = true;
            // 
            // Searcher
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(554, 231);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.SearchStringTextBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DarkGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(570, 270);
            this.Name = "Searcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Searcher";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton ProtocolVersionRadioBtn;
        private System.Windows.Forms.RadioButton ZoneIDRadioBtn;
        private System.Windows.Forms.RadioButton ZoneRadioBtn;
        private System.Windows.Forms.RadioButton RecordingDateRadioBtn;
        private System.Windows.Forms.RadioButton DescriptionRadioBtn;
        private System.Windows.Forms.RadioButton CharacterNameRadioBtn;
        private System.Windows.Forms.RadioButton CharacterGUIDRadioBtn;
        private System.Windows.Forms.RadioButton GameDateRadioBtn;
        private System.Windows.Forms.RadioButton FirefallVersionRadioBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton ExactTextRadioBtn;
        private System.Windows.Forms.RadioButton SubstringRadioBtn;
        private System.Windows.Forms.TextBox SearchStringTextBox;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.CheckBox ClearCurrentSearchCheckBox;
    }
}