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
    public partial class Searcher : Form
    {
        int SearchField = 0;
        int SearchType = 0;

        public Searcher()
        {
            InitializeComponent();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            Viewer OwningForm = (Viewer)Owner;
            if (ClearCurrentSearchCheckBox.Checked)
                OwningForm.LoadNSRDirectory(OwningForm.GetNSRDirectory());
            CheckSearchField();
            CheckSearchType();
            ListBox.ObjectCollection FileList = OwningForm.GetReplayFileListBoxItems();
            string SearchString = SearchStringTextBox.Text;
            List<string> ResultsList = new List<string>();
            string ErrorMessage = "";

            foreach (string file in FileList)
            {
                using (FileStream compressed_stream_preview = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (GZipStream decompressed_stream = new GZipStream(compressed_stream_preview, CompressionMode.Decompress))
                    {
                        NSR NSRFile = new NSR();
                        using (BinaryReader b = new BinaryReader(decompressed_stream, Encoding.UTF8))
                        {
                            if (OwningForm.LoadNSRPreview(b, ref NSRFile) == false)
                            {
                                if (ErrorMessage == "")
                                    ErrorMessage = "The following file(s) were unable to be read:" + Environment.NewLine;
                                ErrorMessage += file + Environment.NewLine;
                            }
                            else
                            {
                                switch (SearchField)
                                {
                                    case 0:
                                        if (SearchType == 0)
                                        {
                                            if (NSRFile.Description_Header.protocol_version.ToString().IndexOf(SearchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                                ResultsList.Add(file);
                                        }
                                        else
                                        {
                                            if (NSRFile.Description_Header.protocol_version.ToString().Equals(SearchString))
                                                ResultsList.Add(file);
                                        }
                                        break;
                                    case 1:
                                        if (SearchType == 0)
                                        {
                                            if (NSRFile.Meta_Header.zone_id.ToString().IndexOf(SearchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                                ResultsList.Add(file);
                                        }
                                        else
                                        {
                                            if (NSRFile.Meta_Header.zone_id.ToString().Equals(SearchString))
                                                ResultsList.Add(file);
                                        }
                                        break;
                                    case 2:
                                        if (SearchType == 0)
                                        {
                                            if (NSRFile.Meta_Header.zone.ToString().IndexOf(SearchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                                ResultsList.Add(file);
                                        }
                                        else
                                        {
                                            if (NSRFile.Meta_Header.zone.ToString().Equals(SearchString))
                                                ResultsList.Add(file);
                                        }
                                        break;
                                    case 3:
                                        if (SearchType == 0)
                                        {
                                            if (NSRFile.Meta_Header.description.IndexOf(SearchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                                ResultsList.Add(file);
                                        }
                                        else
                                        {
                                            if (NSRFile.Meta_Header.description.Equals(SearchString))
                                                ResultsList.Add(file);
                                        }
                                        break;
                                    case 4:
                                        if (SearchType == 0)
                                        {
                                            if (NSRFile.Meta_Header.recording_time.IndexOf(SearchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                                ResultsList.Add(file);
                                        }
                                        else
                                        {
                                            if (NSRFile.Meta_Header.recording_time.Equals(SearchString))
                                                ResultsList.Add(file);
                                        }
                                        break;
                                    case 5:
                                        if (SearchType == 0)
                                        {
                                            if (NSRFile.Meta_Header.character_guid.ToString().IndexOf(SearchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                                ResultsList.Add(file);
                                        }
                                        else
                                        {
                                            if (NSRFile.Meta_Header.character_guid.ToString().Equals(SearchString))
                                                ResultsList.Add(file);
                                        }
                                        break;
                                    case 6:
                                        if (SearchType == 0)
                                        {
                                            if (NSRFile.Meta_Header.character_name.IndexOf(SearchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                                ResultsList.Add(file);
                                        }
                                        else
                                        {
                                            if (NSRFile.Meta_Header.character_name.Equals(SearchString))
                                                ResultsList.Add(file);
                                        }
                                        break;
                                    case 7:
                                        if (SearchType == 0)
                                        {
                                            if (NSRFile.Meta_Header.firefall_version.IndexOf(SearchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                                ResultsList.Add(file);
                                        }
                                        else
                                        {
                                            if (NSRFile.Meta_Header.firefall_version.Equals(SearchString))
                                                ResultsList.Add(file);
                                        }
                                        break;
                                    case 8:
                                        if (SearchType == 0)
                                        {
                                            if (NSRFile.Meta_Header.game_time.IndexOf(SearchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                                ResultsList.Add(file);
                                        }
                                        else
                                        {
                                            if (NSRFile.Meta_Header.game_time.Equals(SearchString))
                                                ResultsList.Add(file);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            if (ErrorMessage != "")
                MessageBox.Show(ErrorMessage);

            if (ResultsList.Count > 0)
            {
                OwningForm.ApplyFilteredReplayFileListItems(ResultsList);
                MessageBox.Show("Search Complete." + Environment.NewLine + ResultsList.Count + " Matches Found.");
            }
            else
            {
                MessageBox.Show("No matches found. Search filter will not be applied.");
            }

            Close();
        }

        private void CheckSearchField()
        {
            if (ProtocolVersionRadioBtn.Checked)
                SearchField = 0;
            if (ZoneIDRadioBtn.Checked)
                SearchField = 1;
            if (ZoneRadioBtn.Checked)
                SearchField = 2;
            if (DescriptionRadioBtn.Checked)
                SearchField = 3;
            if (RecordingDateRadioBtn.Checked)
                SearchField = 4;
            if (CharacterGUIDRadioBtn.Checked)
                SearchField = 5;
            if (CharacterNameRadioBtn.Checked)
                SearchField = 6;
            if (FirefallVersionRadioBtn.Checked)
                SearchField = 7;
            if (GameDateRadioBtn.Checked)
                SearchField = 8;
        }

        private void CheckSearchType()
        {
            if (SubstringRadioBtn.Checked)
                SearchType = 0;
            if (ExactTextRadioBtn.Checked)
                SearchType = 1;
        }
    }
}
