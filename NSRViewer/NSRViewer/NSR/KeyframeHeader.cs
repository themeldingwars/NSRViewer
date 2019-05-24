using System.ComponentModel;

namespace NSRViewer.NSR
{
    public struct KeyframeHeader
    {
        [DisplayName("Keyframe Order 0")]
        public ushort KeyframeOrder0 { get; set; }

        [DisplayName("Keyframe Order 1")]
        public ushort KeyframeOrder1 { get; set; }

        [DisplayName("Length")]
        public ushort Length { get; set; }

        [DisplayName("UNK0")]
        public ushort Unk0 { get; set; }

        [DisplayName("ID")]
        public byte[] Id { get; set; }

        [DisplayName("Data Type")]
        public byte DataType { get; set; }

        [DisplayName("UNK1")]
        public byte Unk1 { get; set; }

        [DisplayName("Data Count")]
        public byte DataCount { get; set; }

        [DisplayName("Data"), Description("Raw data for the keyframe")]
        public byte[] Data { get; set; }

        [DisplayName("Keyframe Type"), Description("Type of keyframe based on first byte of keyframe")]
        public string KeyframeType { get; set; }

        [DisplayName("Keyframe Position"), Description("Position of the keyframe in the raw file")]
        public long RawPosition { get; set; }
    }
}
