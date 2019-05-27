using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NSRViewer
{
    public partial class GhostInfo : Form
    {
        private List<NSR.KeyframeHeader> _cachedKeyframeList = new List<NSR.KeyframeHeader>();
        private List<NSR.KeyframeHeader> _cachedVisibleKeyframeList = new List<NSR.KeyframeHeader>();
        private readonly Label _loadingPacifier;
        private NSR.NSR _tempNSRFile = new NSR.NSR();
        private List<string> _loadedKeyframeTypes = new List<string>();

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

        public void SetInfo(string fileName, ref NSR.NSR nsrFile)
        {
            _loadingPacifier.Visible = true;
            Refresh();

            KeyframesListBox.Items.Clear();
            _cachedKeyframeList = new List<NSR.KeyframeHeader>();
            _cachedVisibleKeyframeList = new List<NSR.KeyframeHeader>();

            GhostInfoGroupBox.Text = fileName;
            RawSizeValLabel.Text = ((nsrFile.RawSize / 1024f) / 1024f).ToString("N2") + " MB";
            KeyframesValLabel.Text = nsrFile.KeyframeHeaders.Count.ToString("N0");

            _tempNSRFile = nsrFile;
        }

        private void Ghost_Info_Load(object sender, EventArgs e)
        {
            Visible = true;
            Refresh();

            DateTime startBenchmark = DateTime.Now;
            // Cache all the keyframes for future reference, but only actually display based on the visible cache list
            _cachedKeyframeList.AddRange(_tempNSRFile.KeyframeHeaders);
            _cachedVisibleKeyframeList.AddRange(_tempNSRFile.KeyframeHeaders);
            _tempNSRFile = null;
            KeyframesListBox.VirtualMode = true;
            KeyframesListBox.VirtualListSize = _cachedVisibleKeyframeList.Count;

            foreach (NSR.KeyframeHeader keyframeHeader in _cachedKeyframeList)
            {
                if (!_loadedKeyframeTypes.Contains(keyframeHeader.KeyframeType))
                    _loadedKeyframeTypes.Add(keyframeHeader.KeyframeType);
            }
            FillFilterSuggestions();

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
                GhostPropertyGrid.SelectedObject = _cachedVisibleKeyframeList[KeyframesListBox.SelectedIndices[0]];
            }
            else
            {
                GhostPropertyGrid.SelectedObject = null;
            }
        }

        private void KeyframesListBox_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            NSR.KeyframeHeader tempKeyframeHeader = _cachedVisibleKeyframeList[e.ItemIndex];
            ListViewItem listViewItem = new ListViewItem
            {
                Text = tempKeyframeHeader.KeyframePosition + " - " + tempKeyframeHeader.KeyframeType
            };
            
            if (FilterComboBox.Text != "")
                listViewItem.ForeColor = Color.Turquoise;
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
            _cachedVisibleKeyframeList.Clear();
            KeyframesListBox.Items.Clear();
            KeyframesListBox.Dispose();
            _loadingPacifier.Dispose();
        }

        private void RemoveUnmatchedKeyframes()
        {
            // This process can take a while on lists above 100K items
            // TODO: Consider adding benchmark and option to break out of filter early instead of freezing
            KeyframesListBox.BeginUpdate();
            _cachedVisibleKeyframeList.Clear();
            foreach (NSR.KeyframeHeader keyframeHeader in _cachedKeyframeList)
            {
                if (keyframeHeader.KeyframeType.Contains(FilterComboBox.Text))
                    _cachedVisibleKeyframeList.Add(keyframeHeader);
            }
            KeyframesListBox.VirtualListSize = _cachedVisibleKeyframeList.Count;            
            KeyframesListBox.EndUpdate();
        }

        private void FillFilterSuggestions()
        {
            FilterComboBox.Items.Clear();
            FilterComboBox.Items.AddRange(_loadedKeyframeTypes.ToArray());
            FilterComboBox.Sorted = true;
        }

        private void FilterComboBox_TextChanged(object sender, EventArgs e)
        {
            RemoveUnmatchedKeyframes();
            // Remove keyframe selection as it might be getting deleted
            KeyframesListBox.SelectedIndices.Clear();
            GhostPropertyGrid.SelectedObject = null;
            if (FilterComboBox.Text != "")
                FilteredKeyframesValLabel.Text = _cachedVisibleKeyframeList.Count.ToString("N0");
            else
                FilteredKeyframesValLabel.Text = "<NULL>";
        }
    }
}
