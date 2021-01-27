
namespace Opcodes
{
    /// <summary>
    /// Our opcode class
    /// These bytes should remain read only
    /// 
    /// </summary>
    public static class MessageOpcodeTypes
    {
        public const ushort serverTransfer = 65535; ///0xFFFF
        public const ushort NewConnection = 65534; ///0xFFFE
        public const ushort LongUnreliableMessage = 65532; ///0xFFFC
        public const byte ShortUnreliableMessage = 252; ///0xFC
        public const ushort LongReliableMessage = 65531; ///0xFFFB
        public const byte ShortReliableMessage = 251; ///0xFB
        public const ushort MultiLongReliableMessage = 65530; ///0xFFFA
        public const byte MultiShortReliableMessage = 250; ///0xFA
        public const byte UnknownMessage = 249; ///0xF9
    }

    public static class UnreliableTypes
    {

        public const byte ClientActorUpdate = 64; //0x40 Client Actor update
    }

    public static class GameOpcode
    {

        public const ushort DiscVersion = 0; ///0x0000
        public const ushort Authenticate2 = 1; ///0x0001
        public const ushort CharacterSelect = 44; ///0x002C
        public const ushort Camera1 = 2001; /// 0x07D1
        public const ushort Camera2 = 2037; ///0x07F5
        public const ushort Authenticate = 2308; ///0x0904
        public const ushort GameServers = 1971; ///0x07B3
        public const ushort DelCharacter = 45; ///0x002D
        public const ushort CreateCharacter = 43; ///0x002B
        public const ushort NameTaken = 47; ///0x002F
        public const ushort SELECTED_CHAR = 42; // 0x002A
        public const ushort MemoryDump = 13; //0x000D
        public const ushort Time = 19; //0x0013
        public const ushort IgnoreList = 4101; //0x1005
        public const ushort ActorSpeed = 248; //0x00F8
        public const ushort ClientMessage = 2682; //0x0A7A

    }

    public static class GameVersions
    {

        public const int EQOA_FRONTIERS = 0x25;
        public const int EQOA_VANILLA = 0x12;
        public const int UNKNOWN = 0x00;

    }


    public static class SessionOpcode
    {
        public static readonly sbyte NewSession = 0x21; ///Client requesting new session
        public static readonly sbyte CloseSession = 0x14; ///Client requesting to close session
        public static readonly sbyte ContinueSession = 0x1; ///Client is continuing session

    }

    public static class BundleOpcode
    {
        public const sbyte ProcessAll = 99; ///Client requesting new session
        public const sbyte ProcessReport = 35; ///Client requesting to close session
        public const sbyte ProcessMessages = 32; ///Client is continuing session
        public const sbyte ProcessMessageAndReport = 13; ///Client is continuing session
        public const sbyte NewProcessReport = 3; ///Client is continuing session
        public const sbyte NewProcessMessages = 0; ///Client is continuing session
    }
}
