using System;

namespace NSRViewer.NSR
{
    public struct DescriptionHeader
    {
        public uint Version;
        public uint HeaderLength;
        public uint MetaLength;
        public uint DescriptionLength;
        public uint OffsetData;
        public uint Unk0;
        public uint ProtocolVersion;
        public UInt64 MicrosecondEpoch;
        public uint Unk1;
        public uint Unk2;
    }
}
