using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace NSRViewer
{
    public partial class Searcher : Form
    {
        private int _searchField;
        private int _searchType;

        public Searcher()
        {
            InitializeComponent();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            Viewer owningForm = (Viewer)Owner;
            if (ClearCurrentSearchCheckBox.Checked)
            {
                owningForm.LoadNSRDirectory(owningForm.GetNSRDirectory());
            }
            CheckSearchField();
            CheckSearchType();
            ListBox.ObjectCollection fileList = owningForm.GetReplayFileListBoxItems();
            string searchString = SearchStringTextBox.Text;
            List<string> resultsList = new List<string>();
            string errorMessage = "";

            foreach (string file in fileList)
            {
                using (FileStream compressedStreamPreview = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (GZipStream decompressedStream = new GZipStream(compressedStreamPreview, CompressionMode.Decompress))
                using (BinaryReader binaryReader = new BinaryReader(decompressedStream, Encoding.UTF8))
                {
                    NSR nsrFile = new NSR();
                    if (owningForm.LoadNSRPreview(binaryReader, ref nsrFile) == false)
                    {
                        if (errorMessage == "")
                        {
                            errorMessage = "The following file(s) were unable to be read:" + Environment.NewLine;
                        }
                        errorMessage += file + Environment.NewLine;
                    }
                    else
                    {
                        switch (_searchField)
                        {
                            case 0:
                                if (_searchType == 0)
                                {
                                    if (nsrFile.Description_Header.protocol_version.ToString().IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                        resultsList.Add(file);
                                }
                                else
                                {
                                    if (nsrFile.Description_Header.protocol_version.ToString().Equals(searchString))
                                        resultsList.Add(file);
                                }
                                break;
                            case 1:
                                if (_searchType == 0)
                                {
                                    if (nsrFile.Meta_Header.zone_id.ToString().IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                        resultsList.Add(file);
                                }
                                else
                                {
                                    if (nsrFile.Meta_Header.zone_id.ToString().Equals(searchString))
                                        resultsList.Add(file);
                                }
                                break;
                            case 2:
                                if (_searchType == 0)
                                {
                                    if (nsrFile.Meta_Header.zone.ToString().IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                        resultsList.Add(file);
                                }
                                else
                                {
                                    if (nsrFile.Meta_Header.zone.ToString().Equals(searchString))
                                        resultsList.Add(file);
                                }
                                break;
                            case 3:
                                if (_searchType == 0)
                                {
                                    if (nsrFile.Meta_Header.description.IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                        resultsList.Add(file);
                                }
                                else
                                {
                                    if (nsrFile.Meta_Header.description.Equals(searchString))
                                        resultsList.Add(file);
                                }
                                break;
                            case 4:
                                if (_searchType == 0)
                                {
                                    if (nsrFile.Meta_Header.recording_time.IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                        resultsList.Add(file);
                                }
                                else
                                {
                                    if (nsrFile.Meta_Header.recording_time.Equals(searchString))
                                        resultsList.Add(file);
                                }
                                break;
                            case 5:
                                if (_searchType == 0)
                                {
                                    if (nsrFile.Meta_Header.character_guid.ToString().IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                        resultsList.Add(file);
                                }
                                else
                                {
                                    if (nsrFile.Meta_Header.character_guid.ToString().Equals(searchString))
                                        resultsList.Add(file);
                                }
                                break;
                            case 6:
                                if (_searchType == 0)
                                {
                                    if (nsrFile.Meta_Header.character_name.IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                        resultsList.Add(file);
                                }
                                else
                                {
                                    if (nsrFile.Meta_Header.character_name.Equals(searchString))
                                        resultsList.Add(file);
                                }
                                break;
                            case 7:
                                if (_searchType == 0)
                                {
                                    if (nsrFile.Meta_Header.firefall_version.IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                        resultsList.Add(file);
                                }
                                else
                                {
                                    if (nsrFile.Meta_Header.firefall_version.Equals(searchString))
                                        resultsList.Add(file);
                                }
                                break;
                            case 8:
                                if (_searchType == 0)
                                {
                                    if (nsrFile.Meta_Header.game_time.IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                                        resultsList.Add(file);
                                }
                                else
                                {
                                    if (nsrFile.Meta_Header.game_time.Equals(searchString))
                                        resultsList.Add(file);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            if (errorMessage != "")
            {
                MessageBox.Show(errorMessage);
            }

            if (resultsList.Count > 0)
            {
                owningForm.ApplyFilteredReplayFileListItems(resultsList);
                MessageBox.Show("Search Complete." + Environment.NewLine + resultsList.Count + " Matches Found.");
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
                _searchField = 0;
            if (ZoneIDRadioBtn.Checked)
                _searchField = 1;
            if (ZoneRadioBtn.Checked)
                _searchField = 2;
            if (DescriptionRadioBtn.Checked)
                _searchField = 3;
            if (RecordingDateRadioBtn.Checked)
                _searchField = 4;
            if (CharacterGUIDRadioBtn.Checked)
                _searchField = 5;
            if (CharacterNameRadioBtn.Checked)
                _searchField = 6;
            if (FirefallVersionRadioBtn.Checked)
                _searchField = 7;
            if (GameDateRadioBtn.Checked)
                _searchField = 8;
        }

        private void CheckSearchType()
        {
            if (SubstringRadioBtn.Checked)
                _searchType = 0;
            if (ExactTextRadioBtn.Checked)
                _searchType = 1;
        }
    }
}
