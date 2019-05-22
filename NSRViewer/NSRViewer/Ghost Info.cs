using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRViewer
{
    public partial class Ghost_Info : Form
    {
        List<NSR.KeyframeHeader> CachedKeyframeList = new List<NSR.KeyframeHeader>();
        Label LoadingPacifier = new Label();
        NSR TempNSRFile = new NSR();

        public Ghost_Info()
        {
            InitializeComponent();

            LoadingPacifier = new Label();
            LoadingPacifier.Text = "Loading...";
            LoadingPacifier.BackColor = Color.FromArgb(255, 30, 30, 30);
            LoadingPacifier.ForeColor = Color.FromArgb(255, 112, 112, 112);
            LoadingPacifier.Dock = DockStyle.Fill;
            LoadingPacifier.TextAlign = ContentAlignment.MiddleCenter;
            LoadingPacifier.Font = new Font("Segoe UI", 36);
            LoadingPacifier.Visible = true;
            this.Controls.Add(LoadingPacifier);
            ColumnHeader header = new ColumnHeader();
            header.Text = "";
            header.Name = "Name";
            header.Width = KeyframesListBox.Width - 19;
            KeyframesListBox.Columns.Add(header);
            LoadingPacifier.BringToFront();
        }

        public void SetInfo(string FileName, ref NSR NSRFile)
        {
            LoadingPacifier.Visible = true;
            this.Refresh();            

            KeyframesListBox.Items.Clear();
            CachedKeyframeList = new List<NSR.KeyframeHeader>();

            GhostInfoGroupBox.Text = FileName;
            RawSizeValLabel.Text = ((NSRFile.raw_size / 1024f) / 1024f).ToString("N2") + " MB";
            KeyframesValLabel.Text = NSRFile.Keyframe_Headers.Count.ToString("N0");

            TempNSRFile = NSRFile;
        }

        private void Ghost_Info_Load(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Refresh();

            DateTime StartBenchmark = DateTime.Now;
            CachedKeyframeList.AddRange(TempNSRFile.Keyframe_Headers);
            TempNSRFile = null;
            KeyframesListBox.VirtualMode = true;
            KeyframesListBox.VirtualListSize = CachedKeyframeList.Capacity;
            DateTime EndBenchmark = DateTime.Now;
            double Benchmark = EndBenchmark.Subtract(StartBenchmark).TotalSeconds;

            // Warn when it has taken more than 10 seconds to load ghosts, application should not be frozen for longer than that
            if (Benchmark > 10.0)
            {
                MessageBox.Show("GHOSTS took " + Benchmark.ToString("N2") + " seconds to load.");
            }

            LoadingPacifier.Visible = false;
        }

        private void KeyframesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyframesListBox.SelectedIndices.Count != 0)
            {
                GhostPropertyGrid.SelectedObject = (NSR.KeyframeHeader)CachedKeyframeList[KeyframesListBox.SelectedIndices[0]];
            }
        }

        private void KeyframesListBox_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem LVI = new ListViewItem();
            LVI.Text = e.ItemIndex + " - " + CachedKeyframeList[e.ItemIndex].keyframe_type;
            e.Item = LVI;
        }

        private void Ghost_Info_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                LoadingPacifier.Text = "Closing...";
                LoadingPacifier.Visible = true;
                this.Refresh();
                // Clear then dispose the listview to prevent a long freeze otherwise
                CachedKeyframeList.Clear();
                KeyframesListBox.Items.Clear();
                KeyframesListBox.Dispose();
                LoadingPacifier.Dispose();
            }
        }
    }
}
