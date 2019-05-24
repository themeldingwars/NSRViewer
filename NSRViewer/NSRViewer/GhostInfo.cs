using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NSRViewer
{
    public partial class GhostInfo : Form
    {
        private List<NSR.KeyframeHeader> _cachedKeyframeList = new List<NSR.KeyframeHeader>();
        private readonly Label _loadingPacifier;
        private NSR _tempNSRFile = new NSR();

        public GhostInfo()
        {
            InitializeComponent();

            _loadingPacifier = new Label
            {
                Text = "Loading...",
                BackColor = Color.FromArgb(255, 30, 30, 30),
                ForeColor = Color.FromArgb(255, 112, 112, 112),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 36),
                Visible = true
            };
            Controls.Add(_loadingPacifier);

            ColumnHeader header = new ColumnHeader
            {
                Text = "",
                Name = "Name",
                Width = KeyframesListBox.Width - 19
            };
            KeyframesListBox.Columns.Add(header);
            _loadingPacifier.BringToFront();
        }

        public void SetInfo(string fileName, ref NSR nsrFile)
        {
            _loadingPacifier.Visible = true;
            Refresh();

            KeyframesListBox.Items.Clear();
            _cachedKeyframeList = new List<NSR.KeyframeHeader>();

            GhostInfoGroupBox.Text = fileName;
            RawSizeValLabel.Text = ((nsrFile.raw_size / 1024f) / 1024f).ToString("N2") + " MB";
            KeyframesValLabel.Text = nsrFile.Keyframe_Headers.Count.ToString("N0");

            _tempNSRFile = nsrFile;
        }

        private void Ghost_Info_Load(object sender, EventArgs e)
        {
            Visible = true;
            Refresh();

            DateTime startBenchmark = DateTime.Now;
            _cachedKeyframeList.AddRange(_tempNSRFile.Keyframe_Headers);
            _tempNSRFile = null;
            KeyframesListBox.VirtualMode = true;
            KeyframesListBox.VirtualListSize = _cachedKeyframeList.Capacity;
            DateTime endBenchmark = DateTime.Now;
            double benchmark = endBenchmark.Subtract(startBenchmark).TotalSeconds;

            // Warn when it has taken more than 10 seconds to load ghosts, application should not be frozen for longer than that
            if (benchmark > 10.0)
            {
                MessageBox.Show("GHOSTS took " + benchmark.ToString("N2") + " seconds to load.");
            }

            _loadingPacifier.Visible = false;
        }

        private void KeyframesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyframesListBox.SelectedIndices.Count != 0)
            {
                GhostPropertyGrid.SelectedObject = _cachedKeyframeList[KeyframesListBox.SelectedIndices[0]];
            }
        }

        private void KeyframesListBox_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem listViewItem = new ListViewItem
            {
                Text = e.ItemIndex + " - " + _cachedKeyframeList[e.ItemIndex].keyframe_type
            };
            e.Item = listViewItem;
        }

        private void Ghost_Info_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }

            _loadingPacifier.Text = "Closing...";
            _loadingPacifier.Visible = true;
            Refresh();
            // Clear then dispose the listview to prevent a long freeze otherwise
            _cachedKeyframeList.Clear();
            KeyframesListBox.Items.Clear();
            KeyframesListBox.Dispose();
            _loadingPacifier.Dispose();
        }
    }
}
