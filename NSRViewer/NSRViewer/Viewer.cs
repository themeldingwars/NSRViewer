using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowNewFolderButton = false;
            FBD.Description = "Select directory containing NSR files...";
            if (Directory.Exists(ReplayDirectoryTextBox.Text))
                FBD.SelectedPath = ReplayDirectoryTextBox.Text;

            if (FBD.ShowDialog() == DialogResult.OK)
            {
                LoadNSRDirectory(FBD.SelectedPath);
            }
        }

        public void LoadNSRDirectory(string NSRDirectory)
        {
            ReplayFileListBox.Items.Clear();
            ReplayDirectoryTextBox.Text = NSRDirectory;
            foreach (string file in Directory.GetFiles(NSRDirectory, "*.nsr", SearchOption.TopDirectoryOnly))
            {
                // verify extension due to 3 char extension processing being different
                if (new FileInfo(file).Extension == ".nsr")
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

        public void ApplyFilteredReplayFileListItems(List<string> ResultList)
        {
            ReplayFileListBox.Items.Clear();
            foreach (string item in ResultList)
            {
                ReplayFileListBox.Items.Add(item);
            }

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

        public bool LoadNSRPreview(BinaryReader binaryReader, ref NSR NSRFile)
        {
            // matrix_fury::RequestGhosts->Headers

            // description header
            char[] NSRD = binaryReader.ReadChars(4);
            NSRFile.Description_Header.version = binaryReader.ReadUInt32();
            NSRFile.Description_Header.header_length = binaryReader.ReadUInt32();
            NSRFile.Description_Header.meta_length = binaryReader.ReadUInt32();
            NSRFile.Description_Header.description_length = binaryReader.ReadUInt32();
            NSRFile.Description_Header.offset_data = binaryReader.ReadUInt32();
            NSRFile.Description_Header.unk0 = binaryReader.ReadUInt32();
            NSRFile.Description_Header.protocol_version = binaryReader.ReadUInt32();
            NSRFile.Description_Header.microsecond_epoch = binaryReader.ReadUInt64(); // utc
            NSRFile.Description_Header.unk1 = binaryReader.ReadUInt32();
            NSRFile.Description_Header.unk2 = binaryReader.ReadUInt32();
            // description header

            // index header

            // if the header_length is 48 bytes then the file is large and the index header is now split into filename*.nsr.index
            if (NSRFile.Description_Header.header_length != 48)
            {
                char[] NSRI = binaryReader.ReadChars(4);

                NSRFile.Index_Header.version = binaryReader.ReadUInt32();
                NSRFile.Index_Header.unk0 = binaryReader.ReadUInt32();
                NSRFile.Index_Header.unk1 = binaryReader.ReadUInt32();
                NSRFile.Index_Header.count = binaryReader.ReadUInt32();
                NSRFile.Index_Header.index_offset = binaryReader.ReadUInt32();
                NSRFile.Index_Header.offsets = new uint[NSRFile.Index_Header.count];
                for (int i = 0; i < NSRFile.Index_Header.count; i++)
                    NSRFile.Index_Header.offsets[i] = binaryReader.ReadUInt32();
            }
            // index header

            // meta header
            NSRFile.Meta_Header.version = binaryReader.ReadUInt32();
            NSRFile.Meta_Header.zone_id = binaryReader.ReadUInt32();
            NSRFile.Meta_Header.description = ReadToNull(ref binaryReader);
            NSRFile.Meta_Header.recording_time = ReadToNull(ref binaryReader);

            NSRFile.Meta_Header.position = new System.Numerics.Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
            NSRFile.Meta_Header.rotation = new System.Numerics.Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());


            NSRFile.Meta_Header.character_guid = binaryReader.ReadUInt64();
            NSRFile.Meta_Header.character_name = ReadToNull(ref binaryReader);
            NSRFile.Meta_Header.unk3 = binaryReader.ReadBytes(18);

            NSRFile.Meta_Header.firefall_version = ReadToNull(ref binaryReader);

            NSRFile.Meta_Header.microsecond_epoch = binaryReader.ReadUInt64();
            NSRFile.Meta_Header.month_recording = binaryReader.ReadUInt32();
            NSRFile.Meta_Header.day_recording = binaryReader.ReadUInt32();
            NSRFile.Meta_Header.year_recording = binaryReader.ReadUInt32();
            NSRFile.Meta_Header.year_lore = binaryReader.ReadUInt32();
            NSRFile.Meta_Header.solar_time = binaryReader.ReadSingle(); // utc

            NSRFile.Meta_Header.game_time = ReadToNull(ref binaryReader);

            NSRFile.Meta_Header.padding = binaryReader.ReadBytes(128 - (NSRFile.Meta_Header.game_time.Length + 1));

            NSRFile.Meta_Header.unk4 = binaryReader.ReadBytes(31);
            // meta header
            
            return true;
        }

        public bool LoadNSR(BinaryReader binaryReader, ref NSR NSRFile)
        {
            LoadNSRPreview(binaryReader, ref NSRFile);
            NSRFile.raw_size = binaryReader.BaseStream.Length;

            try
            {
                // ghosts - keyframe
                while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    NSR.KeyframeHeader KeyframeHeader = new NSR.KeyframeHeader();

                    KeyframeHeader.raw_position = binaryReader.BaseStream.Position;

                    KeyframeHeader.keyframe_order_0 = binaryReader.ReadUInt16();
                    KeyframeHeader.keyframe_order_1 = binaryReader.ReadUInt16();
                    KeyframeHeader.length = binaryReader.ReadUInt16();
                    KeyframeHeader.unk0 = binaryReader.ReadUInt16();
                    KeyframeHeader.id = new byte[8];
                    KeyframeHeader.id = binaryReader.ReadBytes(8);

                    KeyframeHeader.data_type = binaryReader.ReadByte();
                    KeyframeHeader.unk1 = binaryReader.ReadByte();
                    KeyframeHeader.data_count = binaryReader.ReadByte();

                    KeyframeHeader.data = new byte[KeyframeHeader.length - 11];
                    switch (KeyframeHeader.id[0])
                    {
                        case 0:
                            KeyframeHeader.keyframe_type = "Generic";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 2:
                            KeyframeHeader.keyframe_type = "Character_BaseController";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 3:
                            KeyframeHeader.keyframe_type = "Character_NPCController";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 4:
                            KeyframeHeader.keyframe_type = "Character_MissionAndMarkerController";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 5:
                            KeyframeHeader.keyframe_type = "Character_CombatController";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 6:
                            KeyframeHeader.keyframe_type = "Character_LocalEffectsController";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 7:
                            KeyframeHeader.keyframe_type = "Character_SpectatorController";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 8:
                            KeyframeHeader.keyframe_type = "Character_ObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 9:
                            KeyframeHeader.keyframe_type = "Character_EquipmentView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 10:
                            KeyframeHeader.keyframe_type = "Character_AIObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 11:
                            KeyframeHeader.keyframe_type = "Character_Combat View";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 12:
                            KeyframeHeader.keyframe_type = "Character_MovementView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 13:
                            KeyframeHeader.keyframe_type = "Character_TinyObjectView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 14:
                            KeyframeHeader.keyframe_type = "Character_DynamicProjectileView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 16:
                            KeyframeHeader.keyframe_type = "Melding_ObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 18:
                            KeyframeHeader.keyframe_type = "MeldingBubble_ObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 20:
                            KeyframeHeader.keyframe_type = "AreaVisualData_ObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 21:
                            KeyframeHeader.keyframe_type = "AreaVisualData_ParticleEffectsView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 22:
                            KeyframeHeader.keyframe_type = "AreaVisualData_MapMarkerView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 23:
                            KeyframeHeader.keyframe_type = "AreaVisualData_TinyObjectView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 24:
                            KeyframeHeader.keyframe_type = "AreaVisualData_LootObjectView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 25:
                            KeyframeHeader.keyframe_type = "AreaVisualData_ForceShieldView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 27:
                            KeyframeHeader.keyframe_type = "Vehicle_BaseController";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 28:
                            KeyframeHeader.keyframe_type = "Vehicle_CombatController";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 29:
                            KeyframeHeader.keyframe_type = "Vehicle_ObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 30:
                            KeyframeHeader.keyframe_type = "Vehicle_CombatView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 31:
                            KeyframeHeader.keyframe_type = "Vehicle_MovementView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 33:
                            KeyframeHeader.keyframe_type = "Anchor_AIObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 35:
                            KeyframeHeader.keyframe_type = "Deployable_ObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 36:
                            KeyframeHeader.keyframe_type = "Deployable_NPCObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 37:
                            KeyframeHeader.keyframe_type = "Deployable_HardpointView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 39:
                            KeyframeHeader.keyframe_type = "Turret_BaseController";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        case 40:
                            KeyframeHeader.keyframe_type = "Turret_ObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 45:
                            KeyframeHeader.keyframe_type = "Outpost_ObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 48:
                            KeyframeHeader.keyframe_type = "ResourceNode_ObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 51:
                            KeyframeHeader.keyframe_type = "CarryObject_ObserverView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;

                        case 53:
                            KeyframeHeader.keyframe_type = "LootStoreExtension_LootObjectView";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                        default:
                            KeyframeHeader.keyframe_type = $"<DEFAULT> [{KeyframeHeader.id[0]}]";
                            KeyframeHeader.data = binaryReader.ReadBytes(KeyframeHeader.length - 11);
                            break;
                    }
                    NSRFile.Keyframe_Headers.Add(KeyframeHeader);
                }
                // ghosts - keyframe
            }
            catch (EndOfStreamException e)
            {
                string Message = "";
                Message += "Error reading GHOSTs. Would you like to attempt to still view successfully parsed ones?" + Environment.NewLine;
                Message += Environment.NewLine + "---- GHOST ERROR ----" + Environment.NewLine + e.Message + Environment.NewLine;
                Message += e.StackTrace + Environment.NewLine;
                if (MessageBox.Show(Message, "Error readings Ghosts", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    // perform specific ghost safe-load error handling if needed
                    // currently returning what we have is good enough
                }
                else
                {
                    return false;
                }
            }
            
            return true;
        }

        string ReadToNull(ref BinaryReader b)
        {
            string RetVal = "";
            Int32 character;
            while ((character = b.Read()) != -1)
            {
                char c = (char)character;
                if (character != '\0')
                    RetVal += c;
                else
                    break;
            }
            return RetVal;
        }

        static byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip),
            CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        private void ReplayFileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReplayFileListBox.SelectedIndex > -1)
            {
                using (FileStream compressed_stream_preview = new FileStream(ReplayFileListBox.SelectedItem.ToString(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    compressed_stream_preview.Position = compressed_stream_preview.Length - 4;
                    byte[] length_bytes = new byte[4];
                    compressed_stream_preview.Read(length_bytes, 0, 4);
                    compressed_stream_preview.Position = 0;
                    long raw_length = BitConverter.ToUInt32(length_bytes, 0);
                    long comp_length = compressed_stream_preview.Length;
                    using (GZipStream decompressed_stream = new GZipStream(compressed_stream_preview, CompressionMode.Decompress))
                    {
                        NSR NSRFile = new NSR();
                        using (BinaryReader b = new BinaryReader(decompressed_stream, Encoding.UTF8))
                        {
                            if (LoadNSRPreview(b, ref NSRFile) == false)
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
                                MessageBox.Show("Error loading Network Stream Replay.");
                            }
                            else
                            {
                                // Set label text
                                ProtocolVersionValLabel.Text = NSRFile.Description_Header.protocol_version.ToString();
                                ZoneValLabel.Text = NSRFile.Meta_Header.zone.ToString();
                                DescriptionValLabel.Text = NSRFile.Meta_Header.description;
                                DateValLabel.Text = NSRFile.Meta_Header.recording_time;
                                CharacterGUIDValLabel.Text = NSRFile.Meta_Header.character_guid.ToString();
                                UserValLabel.Text = NSRFile.Meta_Header.character_name;
                                FirefallVersionValLabel.Text = NSRFile.Meta_Header.firefall_version;
                                Date2ValLabel.Text = NSRFile.Meta_Header.game_time;
                                FileSizeValLabel.Text = $"{comp_length.ToString("N0")} bytes | {((raw_length / 1024f) / 1024f).ToString("N2")} MB RAW";
                            }
                        }
                    }
                }
            }
            else
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
        }

        private void ExportDecompressedFileBtn_Click(object sender, EventArgs e)
        {
            if (ReplayFileListBox.SelectedIndex > -1)
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = "Decompressed NSR Files (*.nsr_raw)|*.nsr_raw";
                SFD.FileName = Path.GetFileNameWithoutExtension(ReplayFileListBox.SelectedItem.ToString()) + ".nsr_raw";
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    byte[] compressed_file = File.ReadAllBytes(ReplayFileListBox.SelectedItem.ToString());
                    byte[] decompressed_bytes = Decompress(compressed_file);
                    File.WriteAllBytes(SFD.FileName, decompressed_bytes);
                    MessageBox.Show("Export Complete.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void CopyInfoBtn_Click(object sender, EventArgs e)
        {
            if (ReplayFileListBox.SelectedIndex > -1)
            {
                string ClipboardString = "--- Network Stream Replay Information ---" + Environment.NewLine;
                ClipboardString += "File: " + ReplayFileListBox.SelectedItem.ToString() + Environment.NewLine;
                ClipboardString += "Protocol Version: " + ProtocolVersionValLabel.Text + Environment.NewLine;
                ClipboardString += "Zone: " + ZoneValLabel.Text + Environment.NewLine;
                ClipboardString += "Description: " + DescriptionValLabel.Text + Environment.NewLine;
                ClipboardString += "Recording Date: " + DateValLabel.Text + Environment.NewLine;
                ClipboardString += "Character GUID: " + CharacterGUIDValLabel.Text + Environment.NewLine;
                ClipboardString += "Character Name: " + UserValLabel.Text + Environment.NewLine;
                ClipboardString += "Firefall Version: " + FirefallVersionValLabel.Text + Environment.NewLine;
                ClipboardString += "Game Date: " + Date2ValLabel.Text + Environment.NewLine;
                ClipboardString += "File Size: " + FileSizeValLabel.Text + Environment.NewLine;

                Clipboard.SetText(ClipboardString);
                MessageBox.Show("Information Copied to Clipboard!", "Information Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ViewGhostsBtn_Click(object sender, EventArgs e)
        {
            if (ReplayFileListBox.SelectedIndex > -1)
            {
                // Display data collected from matrix_fury::RequestGhosts

                // Full Load NSR
                byte[] compressed_file = File.ReadAllBytes(ReplayFileListBox.SelectedItem.ToString()); // loads compressed size into memory
                byte[] decompressed_bytes = Decompress(compressed_file); // loads raw size 2x into memory

                // Done using compressed data now
                compressed_file = null;

                using (MemoryStream decompressed_stream = new MemoryStream(decompressed_bytes))
                {
                    NSR NSRFile = new NSR();
                    using (BinaryReader decompressed_reader = new BinaryReader(decompressed_stream, Encoding.UTF8))
                    {
                        if (LoadNSR(decompressed_reader, ref NSRFile) == false) // loads raw size 2x into memory
                        {
                            MessageBox.Show("Error loading Network Stream Replay.");
                        }
                        else
                        {
                            // begin decompressed cleanup, they aren't going to be used again and are storing a lot of memory
                            decompressed_reader.Dispose();
                            decompressed_stream.Dispose();
                            decompressed_bytes = null;

                            Ghost_Info GhostInfo = new Ghost_Info();
                            GhostInfo.SetInfo(ReplayFileListBox.SelectedItem.ToString(), ref NSRFile);

                            GhostInfo.ShowDialog();

                            // release NSR resources
                            NSRFile = null;
                        }
                    }
                    //MessageBox.Show(Ghosts, "Ghosts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // This doesn't happen often so should be safe to force now
                GC.Collect(2);
            }
        }

        private void ViewInFirefallBtn_Click(object sender, EventArgs e)
        {
            // NSR Default Action
            // "C:\WINDOWS\system32\cmd.exe" /q /c start "firefall" /D"C:\Firefall\system\bin" "C:\Firefall\system\bin\FirefallClient.exe" --open="C:\Replays\replay.nsr"
            if (ReplayFileListBox.SelectedIndex > -1)
            {
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.Filter = "Firefall Client (*.exe)|*.exe";
                OFD.Title = "Select Firefall Client to view NSR file with...";
                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                    FileInfo ClientInfo = new FileInfo(OFD.FileName);
                    StartInfo.WorkingDirectory = ClientInfo.DirectoryName;
                    StartInfo.FileName = ClientInfo.Name;
                    StartInfo.Arguments = $"--open=\"{ReplayFileListBox.SelectedItem.ToString()}\"";
                    System.Diagnostics.Process.Start(StartInfo);
                    MessageBox.Show("Firefall Launched");
                }
            }
        }

        private void ReplayFileListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ReplayFileListBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] Files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (Files.Length == 1)
            {
                string FileDirectoryPath = "";
                if (File.Exists(Files[0]))
                    FileDirectoryPath = new FileInfo(Files[0]).DirectoryName;
                else if (Directory.Exists(Files[0]))
                    FileDirectoryPath = Files[0];
                if (MessageBox.Show($"Open directory [{FileDirectoryPath}] ?", "Open NSR Directory", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    LoadNSRDirectory(FileDirectoryPath);
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
                Clipboard.SetText(ReplayFileListBox.SelectedItem.ToString());
        }

        private void OpenInFileExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ReplayFileListBox.SelectedIndex > -1)
                System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + ReplayFileListBox.SelectedItem.ToString() + "\"");
        }

        private void SearchFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ReplayFileListBox.Items.Count > 0)
            {
                Searcher Search_Form = new Searcher();
                Search_Form.ShowDialog(this);
            }
        }
    }

    public class NSR
    {
        public long raw_size;

        public struct DescriptionHeader
        {
            public uint version;
            public uint header_length;
            public uint meta_length;
            public uint description_length;
            public uint offset_data;
            public uint unk0;
            public uint protocol_version;
            public UInt64 microsecond_epoch;
            public uint unk1;
            public uint unk2;
        }
        public DescriptionHeader Description_Header;

        public struct IndexHeader
        {
            public uint version;
            public uint unk0;
            public uint unk1;
            public uint count;
            public uint index_offset;
            public uint[] offsets;
        }
        public IndexHeader Index_Header;

        public struct MetaHeader
        {
            public uint version;
            public uint zone_id;
            public string zone
            {
                get {
                    if (ZONES.ContainsKey(zone_id))
                        return ZONES[zone_id].ToString() + " (" + zone_id.ToString() + ")";
                    else
                        return zone_id.ToString();
                }
            }
            public string description;
            public string recording_time;
            //public uint unk1; // pos
            //public float[] unk2; // rot

            public System.Numerics.Vector3 position;
            public System.Numerics.Vector4 rotation;

            public UInt64 character_guid;
            public string character_name;
            public byte[] unk3;
            public string firefall_version;
            public UInt64 microsecond_epoch;
            public uint month_recording;
            public uint day_recording;
            public uint year_recording;
            public uint year_lore;
            public float solar_time;
            public string game_time;
            public byte[] padding;
            public byte[] unk4;
        }
        public MetaHeader Meta_Header;

        public struct KeyframeHeader
        {
            [DisplayName("Keyframe Order 0")]
            public ushort keyframe_order_0 { get; set; }

            [DisplayName("Keyframe Order 1")]
            public ushort keyframe_order_1 { get; set; }

            [DisplayName("Length")]
            public ushort length { get; set; }

            [DisplayName("UNK0")]
            public ushort unk0 { get; set; }

            [DisplayName("ID")]
            public byte[] id { get; set; }

            [DisplayName("Data Type")]
            public byte data_type { get; set; }

            [DisplayName("UNK1")]
            public byte unk1 { get; set; }

            [DisplayName("Data Count")]
            public byte data_count { get; set; }

            [DisplayName("Data"), Description("Raw data for the keyframe")]
            public byte[] data { get; set; }

            [DisplayName("Keyframe Type"), Description("Type of keyframe based on first byte of keyframe")]
            public string keyframe_type { get; set; }

            [DisplayName("Keyframe Position"), Description("Position of the keyframe in the raw file")]
            public long raw_position { get; set; }
        }
        public List<KeyframeHeader> Keyframe_Headers;

        static public Dictionary<uint, string> ZONES = new Dictionary<uint, string>()
        {
            { 1, "Thumper" },
            { 2, "Joshs Super Mountains" },
            { 9, "Designerland" },
            { 12, "Nothing" },
            { 13, "Morphic Steppes" },
            { 17, "Pinnacle Ridge" },
            { 33, "Comboton" },
            { 39, "Bryan's Test Zone" },
            { 40, "BuildingParadise" },
            { 42, "Producers Paradise" },
            { 43, "Butte Capture" },
            { 44, "Trenches" },
            { 45, "Lemmings Convoy" },
            { 47, "GlacialCloudTundra" },
            { 48, "CrystalBeach" },
            { 49, "Bamboo Forest" },
            { 50, "Infected_Glacier" },
            { 56, "Audio chaos 4" },
            { 63, "MoonShardCanyon" },
            { 64, "Geode Mountains" },
            { 68, "Geyser Swamp" },
            { 69, "Crescent Strain" },
            { 72, "RedCanyonDesert" },
            { 76, "TEST_ZONE" },
            { 83, "CubicAnamoly" },
            { 84, "Feature Test Zone" },
            { 85, "CelticHighlands" },
            { 88, "SpeedTreeTest" },
            { 94, "Arctic Remnant" },
            { 95, "Andtopia" },
            { 96, "Mikes Remnant" },
            { 104, "Bamboo Forest Test" },
            { 108, "Melding Test" },
            { 109, "Spine1x4" },
            { 110, "Spine2x3" },
            { 111, "Spine2x2" },
            { 113, "Spine4x4" },
            { 115, "AudioTest01" },
            { 117, "Performance Test Area" },
            { 118, "Milestone Remnant" },
            { 119, "BaseAssaultMap" },
            { 121, "ResourceRavine" },
            { 122, "MS6 Gallery" },
            { 127, "Oilrig" },
            { 128, "Cradle" },
            { 136, "Ravine" },
            { 138, "Necropolis" },
            { 139, "Dried Ocean Rift" },
            { 140, "SaltPillarOcean" },
            { 142, "CoralForest" },
            { 143, "DiamondHeadTest" },
            { 145, "Micro_Remnant" },
            { 146, "SketchPropStressTest" },
            { 147, "Wales District" },
            { 152, "CityTest" },
            { 153, "BehaviorTest" },
            { 154, "JH_CityTest" },
            { 156, "Building Test" },
            { 157, "TwoTeamsOneBowl" },
            { 159, "BountyHunter" },
            { 161, "Milestone Battle Blockout" },
            { 162, "Diamond Head [Diamondhead]" },
            { 163, "JHMilestoneTest" },
            { 164, "Isalo Ridge" },
            { 165, "Designerland8x8" },
            { 167, "M28 Doodad Gallery" },
            { 168, "The Colonels Proving Grounds" },
            { 170, "Blockzone" },
            { 172, "Crater" },
            { 173, "CityBlockup" },
            { 174, "Animations Paradise" },
            { 175, "CradleA" },
            { 176, "Dave's 4v4 Spine" },
            { 177, "CradleB" },
            { 178, "Building Block Outs" },
            { 179, "RavineB" },
            { 180, "Ed's 4x4 Spine" },
            { 181, "4x4 Spine 3" },
            { 182, "4x4 Spine 4" },
            { 183, "4x4 Spine 5" },
            { 184, "4x4 Spine 6" },
            { 185, "Opposing Ramps" },
            { 186, "M27 Character Gallery" },
            { 187, "Asmorix's Lair" },
            { 191, "M28 Character Gallery" },
            { 193, "Detail Doodad Test" },
            { 196, "Shangri-La" },
            { 197, "4 Peaks" },
            { 198, "4 Peaks" },
            { 199, "SoAmericanJungle" },
            { 200, "Troika" },
            { 201, "Moonscape" },
            { 203, "Vale of Whispers" },
            { 204, "Sargasso Sea" },
            { 205, "Helix" },
            { 206, "Gopherhill" },
            { 207, "Infantry1" },
            { 208, "Blackwater Swamp" },
            { 210, "WeaponsTest" },
            { 212, "Greenpath" },
            { 214, "WeaponsTest2" },
            { 215, "Copyland" },
            { 216, "Maw" },
            { 219, "Isalo Ridge PVP" },
            { 221, "M29 SpeedTree Test" },
            { 222, "Rum Island" },
            { 223, "Character Gallery" },
            { 224, "SubAlpine Grassland" },
            { 225, "Dune Desert" },
            { 227, "Carbon Mountain" },
            { 228, "LevelDesignTestArea" },
            { 233, "Isle Lewis" },
            { 236, "Water Test Zone" },
            { 239, "Mike Speed Trial" },
            { 242, "New Olick" },
            { 243, "JD_base" },
            { 244, "Melding Repulsor tests" },
            { 245, "Kmcdonald_STweek2" },
            { 246, "Wall of Giants" },
            { 247, "Mike Speed Trial 2" },
            { 248, "Uphill Spine Battle" },
            { 249, "JD_Wonderland" },
            { 250, "JH 5 Point 2" },
            { 251, "Scotts Proving Grounds" },
            { 253, "OldAndrewSpeedTest" },
            { 254, "The Pizzle" },
            { 255, "Mark's Wunderland" },
            { 258, "Mike's 5 cap spine battle" },
            { 259, "Scotts Proving Grounds 2" },
            { 260, "PVP Dried Ocean" },
            { 261, "PVP Salt Pillar" },
            { 263, "JH 5 Point With Water" },
            { 264, "JD_Waterland" },
            { 265, "JHanson Water Demo" },
            { 267, "JHanson Water Demo 2" },
            { 268, "SouthWestMesa DEMO" },
            { 270, "JH 5 Point 3" },
            { 271, "Fog Demo" },
            { 274, "JH Trigger Test2" },
            { 275, "One capture point map" },
            { 276, "Fubar" },
            { 278, "1 Points Capture Frag Fest" },
            { 279, "SY_test" },
            { 280, "JH Trigger Test" },
            { 282, "JMAC TEST" },
            { 283, "Veil" },
            { 284, "EncounterTestRedux" },
            { 285, "EncounterTestTwo" },
            { 286, "EncounterTestThree" },
            { 288, "Redwood Demo" },
            { 289, "JH Trigger Test4" },
            { 291, "PvP Moonshard OLD" },
            { 292, "ThreeBaseTest" },
            { 293, "JH_BuildingTestZone" },
            { 294, "El Dorado Canyon" },
            { 295, "PVP Moonshard" },
            { 296, "Spawn trigger test" },
            { 297, "JH Thumper Test" },
            { 298, "Outpost Test" },
            { 299, "Status Trigger Test" },
            { 302, "Outpost triggers test" },
            { 303, "JH Modular Cap Test" },
            { 304, "King of the Hill" },
            { 306, "WallOfGiants" },
            { 307, "3PointTriggerTest" },
            { 310, "PVP 3FactionMesa" },
            { 311, "Southwest Mesa" },
            { 313, "JH_Blackwater_4Point" },
            { 314, "Lazonia Palace" },
            { 315, "Mike SoAmerican experimenting" },
            { 316, "JH 4 Point Test" },
            { 317, "JH_OnePointTest" },
            { 318, "Wall of Giants Survival" },
            { 319, "Accord Industrial Demo" },
            { 322, "EdTriggerTest1" },
            { 323, "Tangleroot Flats Demo Zone" },
            { 326, "Dried Ocean Rift New" },
            { 327, "Scale Test Zone" },
            { 328, "JH Outpost Test" },
            { 329, "Glacial Runoff" },
            { 330, "Mike Diamondhead experimenting" },
            { 331, "Outpost Sizes Tests" },
            { 332, "ThumperTest" },
            { 333, "Isalo Ridge Demo Zone" },
            { 334, "DenseForestTest" },
            { 335, "Jefftown" },
            { 336, "JH_StatusTestZone" },
            { 337, "Paradise Cove" },
            { 338, "Blackwater Marsh" },
            { 340, "Repulsor Test" },
            { 341, "JH_HotPocketTest" },
            { 342, "bm_gctundra_unaltered" },
            { 344, "Thousand Steppes Demo Zone" },
            { 345, "Slaughterhouse PvP" },
            { 346, "NPC Test Zone" },
            { 347, "Accord Remnant" },
            { 349, "Blackwater Mesa Demo Zone" },
            { 350, "Isalo Swamp Demo Zone" },
            { 351, "Nested Set Test" },
            { 352, "Four Outpost Test" },
            { 353, "Modular Test Bed" },
            { 355, "AverageDensityTest" },
            { 356, "Shattered Cairns" },
            { 358, "JD_EncounterTestBed" },
            { 359, "Shooting Range" },
            { 360, "4 Zone Modular Test" },
            { 365, "The Cape" },
            { 366, "NewFURYClient" },
            { 367, "Grass DEMO" },
            { 369, "Hanson's Canyon" },
            { 370, "Vortex" },
            { 373, "Rob's Test Zone" },
            { 375, "RR2" },
            { 377, "Firefall Arena" },
            { 378, "Tripoli Pass (Old)" },
            { 379, "PowerCell" },
            { 380, "Outpost Construction Site" },
            { 381, "Dream Team Land" },
            { 384, "Chosen Demos" },
            { 385, "Mike Shangrila experimenting" },
            { 387, "COMMUNION REMNANT" },
            { 388, "VehicleTest" },
            { 390, "Bryan's Outpost Test Zone" },
            { 391, "JungleTest" },
            { 392, "VehicleTestLand" },
            { 394, "Unnamed Designer Zone" },
            { 395, "Vehicle_CTF" },
            { 396, "Bike Jump Test" },
            { 398, "Frostbite Chasm" },
            { 399, "Sand Dunes" },
            { 400, "RG_HQAssaultTest" },
            { 401, "The Flotilla" },
            { 404, "Bryan's PvE Test" },
            { 405, "RG_ReconTest" },
            { 406, "TedTestBed" },
            { 408, "Encounter Debug Zone" },
            { 409, "Mike's Test Track" },
            { 410, "2 Team Powercell" },
            { 411, "InteriorTest" },
            { 412, "1 Tower Powercell" },
            { 413, "Thumper Test Zone" },
            { 414, "Showcase001" },
            { 415, "Effects Zone" },
            { 417, "Gibraltar Falls" },
            { 420, "Feature Test Zone Deux" },
            { 423, "Kiel SaltPillarIsland" },
            { 424, "Kiel Jungle" },
            { 425, "Kiel Dragon" },
            { 426, "Kiel SlaughterFall" },
            { 427, "Kiel Shangri_la" },
            { 428, "Narrows" },
            { 430, "Bryan's Size Tests" },
            { 431, "Mike Wales District experimenting" },
            { 448, "New Eden" },
            { 449, "Tower Assault" },
            { 451, "Tripoli Pass" },
            { 452, "Thumper Island" },
            { 457, "SF_MeldingJungle" },
            { 458, "Powercell (PvP)" },
            { 459, "5-point Collapsing Melding (PvP)" },
            { 461, "The Last Stand (PvE)" },
            { 462, "Sweep-N-Clear (PvE)" },
            { 464, "The Abyss (PvE)" },
            { 467, "Attack/Defend (PvP)" },
            { 469, "New Eden (PvE)" },
            { 470, "Calderas (PvP)" },
            { 471, "Tower Assault (PvP)" },
            { 474, "Ancient Lair" },
            { 475, "Mission Test Zone" },
            { 476, "Mikes Test" },
            { 477, "Stephen Test Zone" },
            { 478, "AbyssEncounterTestZone" },
            { 479, "Repulsor test" },
            { 480, "Abyss" },
            { 481, "Oracle" },
            { 482, "Sweep and Clear start" },
            { 483, "Sweep and Clear Test" },
            { 484, "Pather Test Zone" },
            { 485, "Resource Crater" },
            { 487, "Create Test Zone" },
            { 488, "Creature Team Test Zone" },
            { 489, "AI Drilltown" },
            { 490, "AI Zone 2" },
            { 492, "PAX Side Mission Area" },
            { 493, "PAX thumper area" },
            { 495, "New Valencia Test" },
            { 497, "Drill Test" },
            { 498, "AI Drilltown 2" },
            { 499, "Fake Drill Town" },
            { 500, "Dredge Eastern Chunk" },
            { 501, "TEST_brigand_pvp_zone" },
            { 502, "Final Screen Test Zone" },
            { 504, "TEST_brigand_pvp_enter" },
            { 505, "_Episode 2" },
            { 506, "Dynamic Mission Prototype Zone" },
            { 509, "Sunken Harbor -- OLD" },
            { 510, "Encounter Tutorial Zone" },
            { 511, "Mike Encounter Experimenting" },
            { 513, "DW Testbed" },
            { 514, "Arena Queue Master" },
            { 515, "TEST_brigand_pvp2" },
            { 516, "Feral Garden" },
            { 517, "Andrew Test Zone" },
            { 518, "Feral_garden_Test" },
            { 519, "Nordeste Falls" },
            { 520, "OrbitalComTowerMPmap" },
            { 521, "Prison Instance" },
            { 522, "Eric Anchor Test Zone" },
            { 524, "Sunken Harbor: Arena" },
            { 525, "Bryan's Mission Test Zone" },
            { 526, "Anthony town" },
            { 527, "Sunken Harbor: TDM [Sunken Harbor]" },
            { 528, "Orbital Comm Tower" },
            { 529, "Nicolas DM Test" },
            { 530, "NewMPmap" },
            { 531, "Crossroads Training Zone" },
            { 534, "JonOlickTestZone" },
            { 546, "Rudizone" },
            { 547, "Blackwater Harvest" },
            { 548, "Olickia" },
            { 549, "New Eden Master Chunk" },
            { 550, "FXnerdville"},
            { 551, "NewMPmap02" },
            { 552, "Albert's Paradise" },
            { 553, "Scotts Unnamed Zone" },
            { 554, "Open World Test Zone" },
            { 555, "New Barcelona" },
            { 556, "Blackwater Extraction Site: Harvester [Harvester]" },
            { 557, "Chub Town" },
            { 558, "Arc Light Fragment Prototyple" },
            { 559, "Orbital Comm Tower TDM" },
            { 560, "Nikdelphia" },
            { 561, "OCT Tri Cull Test" },
            { 562, "Ryderzone" },
            { 563, "Reuse Me" },
            { 564, "Blackwater Extraction Site: Zombie Battle" },
            { 565, "Axerland" },
            { 566, "Whitebox" },
            { 567, "Objective Test Zone" },
            { 568, "Sunken Harbor: TDM (2v2)" },
            { 570, "OCT Flythrough" },
            { 571, "Yet Another AI Test Zone" },
            { 572, "Arboretum: TDM" },
            { 573, "Sunken Harbor: NPE" },
            { 574, "Copacabana Characters" },
            { 575, "Resource Run Test" },
            { 576, "Moisture Farm: TDM [Moisture Farm]" },
            { 577, "Copacabana Peacetime Test" },
            { 578, "Copa Peacetime NO NPCS" },
            { 579, "RCannaday" },
            { 580, "GreenLand" },
            { 581, "Coral Forest Creature Test" },
            { 582, "Copacabana Invasion Test Zone" },
            { 583, "Flight Path test" },
            { 584, "OCT: Sabotage [Orbital]" },
            { 585, "John Su Land" },
            { 586, "Lamp/Light test zone" },
            { 587, "Wales District Test" },
            { 588, "CTF World" },
            { 589, "Interior Harvester" },
            { 590, "Eric's Fjords" },
            { 591, "Towerland" },
            { 592, "New Biscuitland" },
            { 593, "Light Zone" },
            { 594, "dB Land" },
            { 595, "Speedway" },
            { 596, "Thumper Test Bed" },
            { 597, "Bryan's Isolated Test Zone" },
            { 598, "CollapsingBubble" },
            { 600, "bubbleEdit" },
            { 601, "Encounter Test Zone" },
            { 602, "Chosen War Test Bed" },
            { 604, "Meteorfield" },
            { 605, "The Rig:TDM [Oil Rig]" },
            { 607, "Test Zone" },
            { 608, "Event Test Zone" },
            { 609, "AI_Tower_test" },
            { 610, "Reuse me" },
            { 611, "Nothing Too" },
            { 616, "Arboretum Skirmish TDM" },
            { 617, "Copacabana Beta" },
            { 618, "DEP Moisture Farm: Skirmish" },
            { 619, "The Dam" },
            { 620, "WaterfallTestZone" },
            { 621, "KupratisLand" },
            { 622, "Moisture Farm: Team Skirmish" },
            { 623, "Rudiland" },
            { 624, "Resource Test Bed" },
            { 625, "Hangar Map Test" },
            { 629, "Bay of Pork" },
            { 634, "Sub-zone Land" },
            { 635, "The Track" },
            { 636, "MNTest" },
            { 637, "JH Mission Test Zone" },
            { 638, "pezlandia" },
            { 641, "DeathTown" },
            { 645, "Spawn Test Zone" },
            { 646, "Biscuit Cup" },
            { 647, "JH_Nothing" },
            { 648, "MajikMarkerLand" },
            { 649, "JMAC Encounter Test" },
            { 650, "UryZone" },
            { 651, "JH_DebugZone" },
            { 652, "BnZone" },
            { 653, "dB Reverb" },
            { 654, "JH_DynamicTestZone" },
            { 656, "Isles of Bsu" },
            { 657, "dB Test Music" },
            { 658, "Shanty Town:TDM [Shanty Town]" },
            { 659, "Chosen Invasion FX Test" },
            { 660, "Matt_lighting" },
            { 663, "JH_CTFTest" },
            { 665, "sy_ctf_test" },
            { 667, "RH_TestZone" },
            { 669, "1 Thumper Test-zone" },
            { 670, "Sunken Harbor Invasion Test" },
            { 671, "Million Steppes" },
            { 673, "Ltg_Ury" },
            { 676, "Sabotest" },
            { 679, "bpielstick_test" },
            { 681, "Milner_Zone" },
            { 682, "Thump Dump" },
            { 683, "bp_pvp_ctf4" },
            { 684, "Tornado Test Area" },
            { 685, "TestZone-BCottle" },
            { 686, "cpowell_test" },
            { 687, "bp_pvp_ctf2" },
            { 688, "Northern Shores" },
            { 691, "Bubble Test Zone" },
            { 692, "Outer Province Town" },
            { 694, "Swedeland" },
            { 695, "Navzonia" },
            { 696, "Rexburg" },
            { 698, "SIN Card Test" },
            { 704, "BARO'S TRANS  PREVIEW" },
            { 706, "Navzonia02" },
            { 707, "bp_pvp_domination" },
            { 708, "LuauLarryLand" },
            { 709, "Ambient Heaven" },
            { 710, "Trans Hub" },
            { 712, "Bomb Push Test" },
            { 713, "Nictator" },
            { 714, "bp_pvp_ctf3" },
            { 716, "Yameru's Caves" },
            { 717, "Cameron's Combat_test_aera_low_lvl" },
            { 718, "Astrek Prototype Stadium JD" },
            { 720, "Costa Mesa" },
            { 721, "Santa Cruz" },
            { 722, "Watchtower Chosen War Test" },
            { 723, "Watchtower Encounter Test" },
            { 727, "bp_staging" },
            { 728, "Botanical Gardens Test Zone" },
            { 729, "MeldingZone" },
            { 730, "RedwoodForest" },
            { 731, "CoralForest" },
            { 732, "Southwest-Mesa" },
            { 733, "CoralCreatures" },
            { 734, "Large_Nothing" },
            { 735, "mini adventures test" },
            { 736, "SH: LUA TDM" },
            { 737, "Darnarcus_test_map" },
            { 740, "Nic AI Watchtower" },
            { 743, "Newzoneland" },
            { 744, "TunaTest" },
            { 745, "Rexburg2" },
            { 746, "Cameron's Encounter Playground" },
            { 748, "Incursion Test Zone" },
            { 749, "waterfallpvp" },
            { 750, "MichalTest" },
            { 751, "cavepvp" },
            { 752, "Heckweek" },
            { 754, "JR_TestZone" },
            { 755, "Michael M Audio Zone" },
            { 757, "Test_ZONE_ARC" },
            { 759, "Green Hill Zone" },
            { 760, "Astrek Prototype Jetball Arena" },
            { 761, "test" },
            { 764, "dB The Encounter Zone" },
            { 766, "Yamamoto 1" },
            { 767, "GreenScreen" },
            { 769, "bp_pvp_br2" },
            { 770, "Yamamoto 2" },
            { 771, "Chosen Warfront Test Zone" },
            { 722, "Yamamoto 3" },
            { 773, "Newzoneland" },
            { 775, "bp_pvp_br3" },
            { 776, "bp_pvp_br4" },
            { 777, "Southwest Mesa" },
            { 778, "Baneclaw Test Zone" },
            { 780, "Yamamoto 6" },
            { 781, "New Player Experience: Battle Lab" },
            { 782, "Redlands" },
            { 783, "Yamamoto 7" },
            { 784, "Yamamoto 8" },
            { 785, "bp_pvp_br5" },
            { 786, "Hallway" },
            { 787, "DH_02" },
            { 788, "bp_pvp_br-rebuild" },
            { 789, "Adventure Time" },
            { 790, "bp_pvp_br-rebuild2" },
            { 791, "Joshs Mini Adventure" },
            { 795, "JTempLand" },
            { 796, "Roman's Moon Base" },
            { 797, "DH_mini_POI" },
            { 799, "Powerball2" },
            { 803, "Melding Cave [Mission 15 - Agrievan]" },
            { 805, "Epicenter Melding Tornado Pocket [Epicenter - Melding Tornado Pocket]" },
            { 807, "Trevor" },
            { 808, "Chopshopistan" },
            { 809, "MD_mini adventures" },
            { 810, "copa_06" },
            { 811, "bp_pvp_br-rebuild3" },
            { 812, "bp_pvp_br6" },
            { 813, "CydTest" },
            { 816, "Miel Land" },
            { 817, "Bryan Town" },
            { 818, "Fjord Flight Paths" },
            { 820, "ShantyPreview" },
            { 821, "Shanty test" },
            { 822, "Flight Path: Simple Space" },
            { 824, "Mentone Beach" },
            { 825, "Bryan's First PvE Test" },
            { 826, "Tanukiland" },
            { 827, "CydTestToo" },
            { 828, "Bryan's Combat Arena" },
            { 830, "Transhub_Optimize_Test" },
            { 831, "Transhub_No_tunnel" },
            { 832, "New Olick 2" },
            { 833, "Campaign_Act01_Ch01_M08_BlackwaterAnomaly [Mission 20 - Razor's Edge (Blackwater Anomaly)]" },
            { 834, "McMayhem Vineyard" },
            { 835, "DH_Mud_test_01" },
            { 836, "DO_NOT_USE" },
            { 837, "Blackwater_Anomaly_Test_DESIGN" },
            { 838, "Defense" },
            { 839, "DO_NOT_USE" },
            { 840, "Wind tweak zone" },
            { 841, "Stop Selecting Newer Eden" },
            { 842, "Southern New Eden" },
            { 843, "Antarctica" },
            { 844, "Omnidyne-M Prototype Stadium" },
            { 845, "LizzyLand" },
            { 846, "Empty Sunken Harbor" },
            { 847, "Saras_Small_Texas" },
            { 848, "SargassoSea FX Test" },
            { 849, "BrandoLand" },
            { 851, "Tavarus" },
            { 852, "Copa Chunka" },
            { 853, "Bestiary" },
            { 858, "Panic In Detroit" },
            { 861, "Campaign_Act01_Ch01_M05_ProvingGround [Research Station]" },
            { 862, "bp_pvp_openworld1" },
            { 863, "Operation01_Cliff'sEdge [Cliff's Edge]" },
            { 864, "Campaign_Act01_Ch01_M03_DirtyDeeds [Mission 16 - Unearthed]" },
            { 865, "Abyss - Melding Tornado Pocket" },
            { 866, "Lirpa's Lonely Island" },
            { 868, "Cinerarium" },
            { 867, "Eye of the Storm Melding Pocket" },
            { 869, "Eric ARES mission zone" },
            { 870, "bp_pvp_armybase1" },
            { 872, "Lirpa 2" },
            { 873, "Well" },
            { 874, "Open World PvP Test" },
            { 877, "Cross Roads PvP" },
            { 878, "First Impressions: Copacabana" },
            { 879, "Sunken Harbor Test" },
            { 880, "Test Site" },
            { 881, "AmnDragon's Brainstorm Extravaganza" },
            { 882, "DH_poi_test" },
            { 885, "Dredge" },
            { 886, "Smokestack Open World PvP" },
            { 887, "open world pvp building test" },
            { 888, "Old World Sin" },
            { 889, "ACanaday Test" },
            { 891, "DH TEST BAKING" },
            { 892, "DH_TEST_BAKING_02" },
            { 893, "Nizuul's Paradise" },
            { 894, "TH_Whitebox" },
            { 895, "Nizuul's Lair" },
            { 896, "Breitner test" },
            { 897, "Base Test Zone" },
            { 902, "Christian's Fun Land 2" },
            { 1002, "Character Create" },
            { 1003, "Campaign_Act01_Ch01_M01_CrashDown [Mission 03 - Crash Down]" },
            { 1004, "Copa South 2 Chunkas" },
            { 1005, "DeathZoneLand" },
            { 1006, "BigChinLand" },
            { 1007, "Campaign_Act01_Ch01_M06_PowerGrab [Mission 18 - Vagrant Dawn]" },
            { 1008, "Campaign_Act01_Ch01_M07_RiskyBusiness [Mission 14 - Icebreaker]" },
            { 1009, "dB Crystite Meteor Shower" },
            { 1010, "Stage5_TestRoom" },
            { 1011, "Siegeland" },
            { 1013, "BigNSmallCageLand" },
            { 1014, "CageTestZone" },
            { 1015, "Thump Dump - Mission4" },
            { 1016, "Islandtest" },
            { 1017, "Edward_Bowman_test_map" },
            { 1018, "DO NOT USE - Marketing - Antarctica" },
            { 1019, "Adrian's test spawn" },
            { 1021, "Wasteland" },
            { 1022, "Boss - Wormasaurus" },
            { 1023, "ArcTest" },
            { 1024, "First 5 Experience - Intro to Firefall" },
            { 1025, "NPE precursor dropship test zone" },
            { 1026, "Steph's Newland" },
            { 1027, "BattlecruiserTestZone" },
            { 1028, "Devils Tusk Intro" },
            { 1029, "MasterlatviaWorld" },
            { 1030, "Sertao" },
            { 1031, "Newzoneland" },
            { 1032, "SouthwestMesaDuneTest" },
            { 1033, "Boss - Wormasaurus No Encounter" },
            { 1040, "Chosen Battlezone" },
            { 1041, "Scotts Arena" },
            { 1042, "Campaign - Power Grab No Encounter" },
            { 1043, "Mob Combat Zone" },
            { 1046, "Sertao West Chunks Only" },
            { 1047, "McTestZone" },
            { 1048, "Sertao ARES Mine" },
            { 1050, "Epicenter Baneclaw Lair" },
            { 1051, "Baneclaw Lair" },
            { 1052, "Sertao AREA Hydroelectric Facility" },
            { 1053, "Accord ARES Modular Set" },
            { 1054, "Devils Tusk Warfront" },
            { 1055, "Sertao ARES Whitebox Zone" },
            { 1056, "Greenscreen" },
            { 1057, "Old Buildings" },
            { 1058, "Tremmel_Test" },
            { 1060, "Tremmel_Test_8" },
            { 1061, "Green Screen for Video" },
            { 1062, "Blackwater_BogTest" },
            { 1064, "Vu_test" },
            { 1065, "Race Track" },
            { 1066, "Melding Defense" },
            { 1067, "Campaign_Act01_Ch02_M01_Herald" },
            { 1068, "Craterland" },
            { 1069, "Operation_000 [Operation 01 - Miru]" },
            { 1071, "Weapons Testing Lab" },
            { 1072, "JMarks Replay Testing" },
            { 1074, "JS_Test" },
            { 1075, "CharacterSelect" },
            { 1076, "DigitalGreenscreen" },
            { 1079, "Dwamer_Test_Zone" },
            { 1080, "Fredericksberg" },
            { 1081, "Harvester Encounter Test" },
            { 1082, "Supply Depot Encounter Test" },
            { 1085, "KrisTestArea" },
            { 1086, "GregTown" },
            { 1087, "Vid Chosen Destruction 01" },
            { 1088, "Operation_004" },
            { 1089, "Operation_003 [Operation 03 - The ARES Team]" },
            { 1090, "Vid Accord Beauty 1" },
            { 1091, "Jared_testzone" },
            { 1092, "dB EndlessCombat" },
            { 1093, "Operation_002 [Operation 02 - High Tide]" },
            { 1094, "Vid Green Land" },
            { 1095, "AresTestZone" },
            { 1096, "Vid MGV Proving Grounds" },
            { 1097, "Vid MGV Proving Grounds PT2" },
            { 1098, "CoreMission_14.0_Lure_TEST" },
            { 1099, "CoreMission_8.0_Taken [Mission 09 - Taken]" },
            { 1100, "CoreMission_1.0_EverythingIsShadow [Mission 01 - Everything Is Shadow]" },
            { 1101, "CoreMission_6.0_TomClancysMissionSix [Mission 07 - Trespass]" },
            { 1102, "CoreMission_3.0_Razorwind [Mission 04 - Razorwind]" },
            { 1103, "CoreMission_18.0_AirChosen" },
            { 1104, "CoreMission_2.0_Bathsheba [Mission 02 - Bathsheba]" },
            { 1105, "CoreMission_Test" },
            { 1106, "CoreMission_9.0_KingOfTheDump [Mission 10 - Off The Grid]" },
            { 1107, "Core Combat Test Zone" },
            { 1108, "CoreMissionDevelopmentZone_TDMission01" },
            { 1109, "Replicator Fight Test Zone" },
            { 1110, "CoreMissionDevelopmentZone_TDMission02" },
            { 1111, "CoreMissionDevelopmentZone_TDMission03" },
            { 1112, "CoreMissionDevelopmentZone_TDMission04" },
            { 1113, "CoreMission_5.0_SafeHouse [Mission 06 - Safe House]" },
            { 1114, "CoreMission_10.0_Taken2 [Mission 11 - Consequence]" },
            { 1115, "CoreMission_17.0_Ossuary" },
            { 1116, "CoreMission_21.0_ClaimJumper" },
            { 1117, "CoreMission_4.0_NoExit [Mission 05 - No Exit]" },
            { 1118, "PvP Test Zone (Klee)" },
            { 1119, "Newzoneland" },
            { 1120, "Newzoneland" },
            { 1121, "ToddTestTestland" },
            { 1122, "CoreMission_32.0_TheInevitable" },
            { 1123, "CoreMission_15.0_Liberator" },
            { 1124, "CoreMission_12.0_InsideMan" },
            { 1125, "Battlelab_01 [Battlelab 01]" },
            { 1128, "CoreMission_14.0_Lure" },
            { 1129, "DontNeed-deleteme" },
            { 1130, "CoreMission_13.0_ProofOfLife" },
            { 1134, "Mission 08 - Catch Of The Day" },
            { 1147, "Refinery: TDM" },
            { 1151, "Mission 17 - SOS" },
            { 1154, "Mission 13 - Accelerate" },
            { 1155, "Mission 12 - Prison Break" },
            { 1162, "BattleLab - The Danger Room" },
            { 1163, "Holdout: Jericho" },
            { 1171, "Mission 19 - Gatecrasher" },
            { 1172, "New Eden Trial" },
            { 1173, "Raid 01 - Defense of Dredge" },
            { 1181, "M22_Mission_22_Homecoming" }
        };

        public NSR()
        {
            Keyframe_Headers = new List<KeyframeHeader>();
        }
    }
}
