using System.Collections.Generic;

namespace NSRViewer.NSR
{
    public class NSR
    {
        public long RawSize;
        public DescriptionHeader DescriptionHeader;
        public IndexHeader IndexHeader;
        public MetaHeader MetaHeader;
        public List<KeyframeHeader> KeyframeHeaders = new List<KeyframeHeader>();
    }
}