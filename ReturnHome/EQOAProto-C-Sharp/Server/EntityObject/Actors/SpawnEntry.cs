using System;

namespace ReturnHome.Server.EntityObject.Actors
{
    public struct SpawnEntry
    {
        public readonly int _entryID { get; }
        public readonly int _npcID { get; }
        public readonly int _spawnGroupID { get; }
        public readonly int _chance { get; }

        public SpawnEntry(int entryID, int spawnGroupID, int npcID, int chance)
        {
            _entryID = entryID;
            _spawnGroupID = spawnGroupID;
            _npcID = npcID;
            _chance = chance;
        }
    }
}
