using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace NSRViewer
{
    public partial class Viewer : Form
    {
        public Viewer()
        {
            InitializeComponent();
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                Description = "Select directory containing NSR files..."
            };

            if (Directory.Exists(ReplayDirectoryTextBox.Text))
            {
                folderBrowserDialog.SelectedPath = ReplayDirectoryTextBox.Text;
            }

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                LoadNSRDirectory(folderBrowserDialog.SelectedPath);
            }
        }

        public void LoadNSRDirectory(string nsrDirectory)
        {
            ReplayFileListBox.Items.Clear();
            ReplayDirectoryTextBox.Text = nsrDirectory;
            foreach (string file in Directory.GetFiles(nsrDirectory, "*.nsr", SearchOption.TopDirectoryOnly))
            {
                // verify extension due to 3 char extension processing being different
                if (new FileInfo(file).Extension == ".nsr")
                {
                    ReplayFileListBox.Items.Add(file);
                }
            }

            foreach (string file in Directory.GetFiles(nsrDirectory, "*.nsr_raw", SearchOption.TopDirectoryOnly))
            {
                ReplayFileListBox.Items.Add(file);
            }
        }

        public string GetNSRDirectory()
        {
            return ReplayDirectoryTextBox.Text;
        }

        public ListBox.ObjectCollection GetReplayFileListBoxItems()
        {
            return ReplayFileListBox.Items;
        }

        public void ApplyFilteredReplayFileListItems(List<string> resultList)
        {
            ReplayFileListBox.Items.Clear();
            foreach (string item in resultList)
            {
                ReplayFileListBox.Items.Add(item);
            }

            ResetLabelText();
        }

        public bool LoadNSRPreview(BinaryReader binaryReader, ref NSR.NSR nsrFile)
        {
            // matrix_fury::RequestGhosts->Headers

            // description header
            try
            {
                char[] nsrDescription = binaryReader.ReadChars(4);
            }
            catch (InvalidDataException ex)
            {
                // file is either corrupt or not a valid NSR file
                return false;
            }

            nsrFile.DescriptionHeader.Version = binaryReader.ReadUInt32();
            nsrFile.DescriptionHeader.HeaderLength = binaryReader.ReadUInt32();
            nsrFile.DescriptionHeader.MetaLength = binaryReader.ReadUInt32();
            nsrFile.DescriptionHeader.DescriptionLength = binaryReader.ReadUInt32();
            nsrFile.DescriptionHeader.OffsetData = binaryReader.ReadUInt32();
            nsrFile.DescriptionHeader.Unk0 = binaryReader.ReadUInt32();
            nsrFile.DescriptionHeader.ProtocolVersion = binaryReader.ReadUInt32();
            nsrFile.DescriptionHeader.MicrosecondEpoch = binaryReader.ReadUInt64(); // utc
            nsrFile.DescriptionHeader.Unk1 = binaryReader.ReadUInt32();
            nsrFile.DescriptionHeader.Unk2 = binaryReader.ReadUInt32();
            // description header

            // index header

            // if the header_length is 48 bytes then the file is large and the index header is now split into filename*.nsr.index
            if (nsrFile.DescriptionHeader.HeaderLength != 48)
            {
                char[] nsrIndex = binaryReader.ReadChars(4);

                nsrFile.IndexHeader.Version = binaryReader.ReadUInt32();
                nsrFile.IndexHeader.Unk0 = binaryReader.ReadUInt32();
                nsrFile.IndexHeader.Unk1 = binaryReader.ReadUInt32();
                nsrFile.IndexHeader.Count = binaryReader.ReadUInt32();
                nsrFile.IndexHeader.IndexOffset = binaryReader.ReadUInt32();
                nsrFile.IndexHeader.Offsets = new uint[nsrFile.IndexHeader.Count];

                for (int i = 0; i < nsrFile.IndexHeader.Count; i++)
                {
                    nsrFile.IndexHeader.Offsets[i] = binaryReader.ReadUInt32();
                }
            }
            // index header

            // meta header
            nsrFile.MetaHeader.Version = binaryReader.ReadUInt32();
            nsrFile.MetaHeader.ZoneId = binaryReader.ReadUInt32();
            nsrFile.MetaHeader.Description = ReadToNull(ref binaryReader);
            nsrFile.MetaHeader.RecordingTime = ReadToNull(ref binaryReader);

            nsrFile.MetaHeader.Position = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
            nsrFile.MetaHeader.Rotation = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());

            nsrFile.MetaHeader.CharacterGuid = binaryReader.ReadUInt64();
            nsrFile.MetaHeader.CharacterName = ReadToNull(ref binaryReader);
            nsrFile.MetaHeader.Unk3 = binaryReader.ReadBytes(18);

            nsrFile.MetaHeader.FirefallVersion = ReadToNull(ref binaryReader);

            nsrFile.MetaHeader.MicrosecondEpoch = binaryReader.ReadUInt64();
            nsrFile.MetaHeader.MonthRecording = binaryReader.ReadUInt32();
            nsrFile.MetaHeader.DayRecording = binaryReader.ReadUInt32();
            nsrFile.MetaHeader.YearRecording = binaryReader.ReadUInt32();
            nsrFile.MetaHeader.YearLore = binaryReader.ReadUInt32();
            nsrFile.MetaHeader.SolarTime = binaryReader.ReadSingle(); // utc

            nsrFile.MetaHeader.GameTime = ReadToNull(ref binaryReader);

            nsrFile.MetaHeader.Padding = binaryReader.ReadBytes(128 - (nsrFile.MetaHeader.GameTime.Length + 1));

            nsrFile.MetaHeader.Unk4 = binaryReader.ReadBytes(31);
            // meta header

            return true;
        }

        public bool LoadNSRIndexHeader(BinaryReader binaryReader, ref NSR.NSR nsrFile)
        {
            // binaryReader is to a filename.*.nsr.index stream

            char[] nsrIndex = binaryReader.ReadChars(4);

            nsrFile.IndexHeader.Version = binaryReader.ReadUInt32();
            nsrFile.IndexHeader.Unk0 = binaryReader.ReadUInt32();
            nsrFile.IndexHeader.Unk1 = binaryReader.ReadUInt32();
            nsrFile.IndexHeader.Count = binaryReader.ReadUInt32();
            nsrFile.IndexHeader.IndexOffset = binaryReader.ReadUInt32();
            nsrFile.IndexHeader.Offsets = new uint[nsrFile.IndexHeader.Count];

            for (int i = 0; i < nsrFile.IndexHeader.Count; i++)
            {
                nsrFile.IndexHeader.Offsets[i] = binaryReader.ReadUInt32();
            }

            // TODO: Provide option to update DescriptionHeader.HeaderLength to include IndexHeader length
            // This would ultimately merge the standalone index file into the base nsr file.
            if (false)
            {
                nsrFile.DescriptionHeader.HeaderLength += nsrFile.IndexHeader.IndexOffset + (nsrFile.IndexHeader.Count * 4);
            }

            return true;
        }

        public bool LoadNSR(BinaryReader binaryReader, ref NSR.NSR nsrFile)
        {
            LoadNSRPreview(binaryReader, ref nsrFile);
            nsrFile.RawSize = binaryReader.BaseStream.Length;

            try
            {
                // ghosts - keyframe
                while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    NSR.KeyframeHeader keyframeHeader = new NSR.KeyframeHeader
                    {
                        RawPosition = binaryReader.BaseStream.Position,
                        KeyframeOrder0 = binaryReader.ReadUInt16(),
                        KeyframeOrder1 = binaryReader.ReadUInt16(),
                        Length = binaryReader.ReadUInt16(),
                        Unk0 = binaryReader.ReadUInt16(),
                        Id = binaryReader.ReadBytes(8),
                        DataType = binaryReader.ReadByte(),
                        Unk1 = binaryReader.ReadByte(),
                        DataCount = binaryReader.ReadByte(),
                    };

                    keyframeHeader.Data = new byte[keyframeHeader.Length - 11];
                    keyframeHeader.Data = binaryReader.ReadBytes(keyframeHeader.Length - 11);
                    keyframeHeader.KeyframeType = NSR.KeyframeHeader.MapKeyframeTypes(keyframeHeader.Id[0]);
                    keyframeHeader.KeyframePosition = nsrFile.KeyframeHeaders.Count;

                    nsrFile.KeyframeHeaders.Add(keyframeHeader);
                }
                // ghosts - keyframe
            }
            catch (EndOfStreamException exception)
            {
                string message = "";
                message += "Error reading GHOSTs. Would you like to attempt to still view successfully parsed ones?" + Environment.NewLine;
                message += Environment.NewLine + "---- GHOST ERROR ----" + Environment.NewLine + exception.Message + Environment.NewLine;
                message += exception.StackTrace + Environment.NewLine;

                if (MessageBox.Show(message, "Error readings Ghosts", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    // TODO: Perform specific ghost safe-load error handling if needed
                    // Currently returning what we have is good enough
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private void ResetLabelText()
        {
            ProtocolVersionValLabel.Text = "<NULL>";
            ZoneValLabel.Text = "<NULL>";
            DescriptionValLabel.Text = "<NULL>";
            DateValLabel.Text = "<NULL>";
            CharacterGUIDValLabel.Text = "<NULL>";
            UserValLabel.Text = "<NULL>";
            FirefallVersionValLabel.Text = "<NULL>";
            Date2ValLabel.Text = "<NULL>";
            FileSizeValLabel.Text = "<NULL>";
        }

        private static string ReadToNull(ref BinaryReader binaryReader)
        {
            string retVal = "";
            Int32 character;
            while ((character = binaryReader.Read()) != -1)
            {
                char c = (char)character;
                if (character != '\0')
                {
                    retVal += c;
                }
                else
                {
                    break;
                }
            }
            return retVal;
        }

        private static byte[] Decompress(byte[] gzip)
        {
            using (MemoryStream compressedStream = new MemoryStream(gzip))
            using (GZipStream gZipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (MemoryStream decompressedStream = new MemoryStream())
            {
                gZipStream.CopyTo(decompressedStream);
                return decompressedStream.ToArray();
            }
        }

        private void ReplayFileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetLabelText();

            if (ReplayFileListBox.SelectedIndex <= -1)
            {
                return;
            }

            using (FileStream compressedStreamPreview = new FileStream(ReplayFileListBox.SelectedItem.ToString(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                compressedStreamPreview.Position = compressedStreamPreview.Length - 4;
                byte[] lengthBytes = new byte[4];
                compressedStreamPreview.Read(lengthBytes, 0, 4);
                compressedStreamPreview.Position = 0;

                using (GZipStream decompressedStream = new GZipStream(compressedStreamPreview, CompressionMode.Decompress))
                using (BinaryReader b =  (ReplayFileListBox.SelectedItem.ToString().EndsWith(".nsr_raw") ? new BinaryReader(compressedStreamPreview, Encoding.UTF8) : new BinaryReader(decompressedStream, Encoding.UTF8)))
                {
                    NSR.NSR nsrFile = new NSR.NSR();
                    if (!LoadNSRPreview(b, ref nsrFile))
                    {
                        MessageBox.Show("Error loading Network Stream Replay.", "Network Stream Replay Viewer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    long rawLength = BitConverter.ToUInt32(lengthBytes, 0);
                    long compressedLength = compressedStreamPreview.Length;

                    // Set label text
                    ProtocolVersionValLabel.Text = nsrFile.DescriptionHeader.ProtocolVersion.ToString();
                    ZoneValLabel.Text = nsrFile.MetaHeader.Zone.ToString();
                    DescriptionValLabel.Text = nsrFile.MetaHeader.Description;
                    DateValLabel.Text = nsrFile.MetaHeader.RecordingTime;
                    CharacterGUIDValLabel.Text = nsrFile.MetaHeader.CharacterGuid.ToString();
                    UserValLabel.Text = nsrFile.MetaHeader.CharacterName;
                    FirefallVersionValLabel.Text = nsrFile.MetaHeader.FirefallVersion;
                    Date2ValLabel.Text = nsrFile.MetaHeader.GameTime;
                    FileSizeValLabel.Text = $"{compressedLength.ToString("N0")} bytes | {((rawLength / 1024f) / 1024f).ToString("N2")} MB RAW";
                }
            }
        }

        private void ExportDecompressedFileBtn_Click(object sender, EventArgs e)
        {
            if (ReplayFileListBox.SelectedIndex <= -1)
            {
                return;
            }

            if (ReplayFileListBox.SelectedItem.ToString().EndsWith(".nsr_raw"))
            {
                MessageBox.Show("The selected Replay File already is in a decompressed format.", "Network Stream Replay Viewer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Decompressed NSR Files (*.nsr_raw)|*.nsr_raw",
                FileName = Path.GetFileNameWithoutExtension(ReplayFileListBox.SelectedItem.ToString()) + ".nsr_raw"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            byte[] compressedFile = File.ReadAllBytes(ReplayFileListBox.SelectedItem.ToString());
            byte[] decompressedBytes = Decompress(compressedFile);
            File.WriteAllBytes(saveFileDialog.FileName, decompressedBytes);
            MessageBox.Show("Export Complete.", "Network Stream Replay Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CopyInfoBtn_Click(object sender, EventArgs e)
        {
            if (ReplayFileListBox.SelectedIndex <= -1)
            {
                return;
            }

            string clipboardString = "--- Network Stream Replay Information ---" + Environment.NewLine;
            clipboardString += "File: " + ReplayFileListBox.SelectedItem.ToString() + Environment.NewLine;
            clipboardString += "Protocol Version: " + ProtocolVersionValLabel.Text + Environment.NewLine;
            clipboardString += "Zone: " + ZoneValLabel.Text + Environment.NewLine;
            clipboardString += "Description: " + DescriptionValLabel.Text + Environment.NewLine;
            clipboardString += "Recording Date: " + DateValLabel.Text + Environment.NewLine;
            clipboardString += "Character GUID: " + CharacterGUIDValLabel.Text + Environment.NewLine;
            clipboardString += "Character Name: " + UserValLabel.Text + Environment.NewLine;
            clipboardString += "Firefall Version: " + FirefallVersionValLabel.Text + Environment.NewLine;
            clipboardString += "Game Date: " + Date2ValLabel.Text + Environment.NewLine;
            clipboardString += "File Size: " + FileSizeValLabel.Text + Environment.NewLine;

            Clipboard.SetText(clipboardString);
            MessageBox.Show("Information Copied to Clipboard!", "Network Stream Replay Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ViewGhostsBtn_Click(object sender, EventArgs e)
        {
            if (ReplayFileListBox.SelectedIndex <= -1)
            {
                return;
            }

            // Display data collected from matrix_fury::RequestGhosts
            // Full Load NSR
            byte[] compressedFile = File.ReadAllBytes(ReplayFileListBox.SelectedItem.ToString()); // loads compressed size into memory
            byte[] decompressedBytes = ReplayFileListBox.SelectedItem.ToString().EndsWith(".nsr_raw") ? compressedFile : Decompress(compressedFile); // loads raw size 2x into memory

            // Done using compressed data now
            compressedFile = null;

            using (MemoryStream decompressedStream = new MemoryStream(decompressedBytes))
            using (BinaryReader decompressedReader = new BinaryReader(decompressedStream, Encoding.UTF8))
            {
                NSR.NSR nsrFile = new NSR.NSR();
                if (LoadNSR(decompressedReader, ref nsrFile) == false) // loads raw size 2x into memory
                {
                    MessageBox.Show("Error loading Network Stream Replay.", "Network Stream Replay Viewer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // begin decompressed cleanup, they aren't going to be used again and are storing a lot of memory
                    decompressedReader.Dispose();
                    decompressedStream.Dispose();
                    decompressedBytes = null;

                    GhostInfo ghostInfo = new GhostInfo();
                    ghostInfo.SetInfo(ReplayFileListBox.SelectedItem.ToString(), ref nsrFile);

                    ghostInfo.ShowDialog();

                    // release NSR resources
                    nsrFile = null;
                }
            }
            //MessageBox.Show(Ghosts, "Ghosts", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // This doesn't happen often so should be safe to force now
            GC.Collect(2);
        }

        private void ViewInFirefallBtn_Click(object sender, EventArgs e)
        {
            // NSR Default Action
            // "C:\WINDOWS\system32\cmd.exe" /q /c start "firefall" /D"C:\Firefall\system\bin" "C:\Firefall\system\bin\FirefallClient.exe" --open="C:\Replays\replay.nsr"
            if (ReplayFileListBox.SelectedIndex <= -1)
            {
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Firefall Client (*.exe)|*.exe",
                Title = "Select Firefall Client to view NSR file with..."
            };

            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            FileInfo clientInfo = new FileInfo(ofd.FileName);
            startInfo.WorkingDirectory = clientInfo.DirectoryName;
            startInfo.FileName = clientInfo.Name;
            startInfo.Arguments = $"--open=\"{ReplayFileListBox.SelectedItem}\"";
            System.Diagnostics.Process.Start(startInfo);
            MessageBox.Show("Firefall Launched", "Network Stream Replay Viewer", MessageBoxButtons.OK);
        }

        private void ReplayFileListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
        }

        private void ReplayFileListBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files.Length == 1)
            {
                string fileDirectoryPath = "";
                if (File.Exists(files[0]))
                {
                    fileDirectoryPath = new FileInfo(files[0]).DirectoryName;
                }
                else if (Directory.Exists(files[0]))
                {
                    fileDirectoryPath = files[0];
                }

                if (MessageBox.Show($"Open directory [{fileDirectoryPath}] ?", "Network Stream Replay Viewer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    LoadNSRDirectory(fileDirectoryPath);
                    ReplayFileListBox.Focus();
                }
            }
            else
            {
                MessageBox.Show("Only one file/folder may be used at a time. Please try again and Drag & Drop only one.", "Too many files/folders", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ReplayFileListBox.SelectedIndex > -1)
            {
                Clipboard.SetText(ReplayFileListBox.SelectedItem.ToString());
            }
        }

        private void OpenInFileExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ReplayFileListBox.SelectedIndex > -1)
            {
                System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + ReplayFileListBox.SelectedItem + "\"");
            }
        }

        private void SearchFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ReplayFileListBox.Items.Count <= 0)
            {
                return;
            }

            Searcher searchForm = new Searcher();
            searchForm.ShowDialog(this);
        }
    }
}
