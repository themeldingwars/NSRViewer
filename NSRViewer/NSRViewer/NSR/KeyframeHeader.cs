using System.Collections.Generic;
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

        public static string MapKeyframeTypes(uint typeId)
        {
            return KeyframeTypes.ContainsKey(typeId) ? KeyframeTypes[typeId] : $"<DEFAULT> [{typeId}]";
        }

        private static readonly Dictionary<uint, string> KeyframeTypes = new Dictionary<uint, string>
        {
            {0, "Generic"},
            {2, "Character_BaseController"},
            {3, "Character_NPCController"},
            {4, "Character_MissionAndMarkerController"},
            {5, "Character_CombatController"},
            {6, "Character_LocalEffectsController"},
            {7, "Character_SpectatorController"},
            {8, "Character_ObserverView"},
            {9, "Character_EquipmentView"},
            {10, "Character_AIObserverView"},
            {11, "Character_Combat View"},
            {12, "Character_MovementView"},
            {13, "Character_TinyObjectView"},
            {14, "Character_DynamicProjectileView"},
            {16, "Melding_ObserverView"},
            {18, "MeldingBubble_ObserverView"},
            {20, "AreaVisualData_ObserverView"},
            {21, "AreaVisualData_ParticleEffectsView"},
            {22, "AreaVisualData_MapMarkerView"},
            {23, "AreaVisualData_TinyObjectView"},
            {24, "AreaVisualData_LootObjectView"},
            {25, "AreaVisualData_ForceShieldView"},
            {27, "Vehicle_BaseController"},
            {28, "Vehicle_CombatController"},
            {29, "Vehicle_ObserverView"},
            {30, "Vehicle_CombatView"},
            {31, "Vehicle_MovementView"},
            {33, "Anchor_AIObserverView"},
            {35, "Deployable_ObserverView"},
            {36, "Deployable_NPCObserverView"},
            {37, "Deployable_HardpointView"},
            {39, "Turret_BaseController"},
            {40, "Turret_ObserverView"},
            {45, "Outpost_ObserverView"},
            {48, "ResourceNode_ObserverView"},
            {51, "CarryObject_ObserverView"},
            {53, "LootStoreExtension_LootObjectView"},
        };
    }
}
