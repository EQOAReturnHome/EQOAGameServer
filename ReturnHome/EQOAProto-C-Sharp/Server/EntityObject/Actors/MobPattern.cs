using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ReturnHome.Server.EntityObject.Actors
{
    public unsafe struct MobPattern
    {
        private const int MAX_SIZE = 24;
        private readonly int _size;
        public readonly int _mobPatternID;
        public fixed byte _mobPatternName[MAX_SIZE * 2];
        public readonly int _minLevel;
        public readonly int _maxLevel;
        public readonly int _lootTableID;
        public readonly int _factionID;
        public readonly int _aggroRadius;
        public readonly int _see_invis;
        public readonly int _mobClass;
        public readonly int _mobSize;
        public readonly int _modelID;
        public readonly int _hair_color;
        public readonly int _hair_length;
        public readonly int _hair_style;
        public readonly int _face;

        public MobPattern(int mobPatternID, string mobPatternName, int minLevel, int maxLevel, int lootTableID, int factionID, int aggroRadius, int see_invis, int mobClass,
            int mobSize, int modelID, int hair_color, int hair_length, int hair_style, int face)
        {
            int size = mobPatternName.Length;
            _mobPatternID = mobPatternID;

            _size = size <= MAX_SIZE ? size : MAX_SIZE;

            fixed (byte* pName = &_mobPatternName[0])
            {
                MemoryMarshal.AsBytes(mobPatternName.AsSpan(0, _size)).CopyTo(new Span<byte>(pName, MAX_SIZE * 2));
            }

            _minLevel = minLevel;
            _maxLevel = maxLevel;
            _lootTableID = lootTableID;
            _factionID = factionID;
            _aggroRadius = aggroRadius;
            _see_invis = see_invis;
            _mobClass = mobClass;
            _mobSize = mobSize;
            _modelID = modelID;
            _hair_color = hair_color;
            _hair_length = hair_length;
            _hair_style = hair_style;
            _face = face;
        }

        public string GetMobName()
        {
            ReadOnlySpan<byte> temp;

            fixed (byte* pName = &_mobPatternName[0])
            {
                temp = new ReadOnlySpan<byte>(pName, _size * 2);
            }

            return Encoding.Unicode.GetString(temp);
        }

    }
}
