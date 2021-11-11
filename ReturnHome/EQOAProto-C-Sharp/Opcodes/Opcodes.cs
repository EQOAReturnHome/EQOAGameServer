namespace ReturnHome.Opcodes
{
    /// <summary>
    /// Our opcode class
    /// These bytes should remain read only
    /// 
    /// </summary>
    public static class MessageOpcodeTypes
    {
        public const ushort serverTransfer = 0xFFFF;
        public const ushort NewConnection = 0xFFFE;
        public const ushort LongUnreliableMessage = 0xFFFC;
        public const byte ShortUnreliableMessage = 0xFC;
        public const ushort LongReliableMessage = 0xFFFB;
        public const byte ShortReliableMessage = 0xFB;
        public const ushort MultiLongReliableMessage = 0xFFFA;
        public const byte MultiShortReliableMessage = 0xFA;
        public const byte PingMessage = 0xF9;
        public const byte UnknownMessage = 0xF8;
    }

    public static class UnreliableTypes
    {

        public const byte ClientActorUpdate = 64; //0x40 Client Actor update
    }

    public enum GameOpcode : ushort
    {
        DiscVersion = 0x00, //0
        Authenticate2 = 0x01, ///1
        CharacterSelect = 0x2C, ///44
        Camera1 = 0x07D1, ///2001
        Camera2 = 0x07F5, ///2037
        Authenticate = 0x0904, ///2308
        GameServers = 0x07B3, ///1971
        DelCharacter = 0x2D, ///45
        CreateCharacter = 0x2B, ///43
        NameTaken = 0x2F, ///47
        SELECTED_CHAR = 0x2A, // 42
        MemoryDump = 0x0D, //13
        Time = 0x13, //19
        RandomName = 0x12, //18
        IgnoreList = 0x1005, //4101
        ActorSpeed = 0xF8, //248
        ClientMessage = 0x0A7A, //2682 - White text message
        DisconnectClient = 0x09B0, //2480
        ColoredChat = 0x0A7B, //2683 - Color text message

        //From client
        ClientSayChat = 0x000B, //11 - Normal say message from client
        ClientShout = 0x0C01, //3073 - Shout message from client
        ChangeChatMode = 0x000E //14 - Client requests change to "permanant" chat mode
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
        public const byte ProcessAll = 99; ///Client requesting new session
        public const byte ProcessReport = 35; ///Client requesting to close session
        public const byte ProcessMessages = 32; ///Client is continuing session
        public const byte ProcessMessageAndReport = 19; ///Client is continuing session
        public const byte NewProcessReport = 3; ///Client is continuing session
        public const byte NewProcessMessages = 0; ///Client is continuing session
    }
}
