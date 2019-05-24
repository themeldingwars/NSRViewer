namespace NSRViewer.NSR
{
    public struct IndexHeader
    {
        public uint Version;
        public uint Unk0;
        public uint Unk1;
        public uint Count;
        public uint IndexOffset;
        public uint[] Offsets;
    }
}
