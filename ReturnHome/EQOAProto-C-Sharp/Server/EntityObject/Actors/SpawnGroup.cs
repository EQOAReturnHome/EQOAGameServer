using System;

namespace ReturnHome.Server.EntityObject.Actors
{
    public struct SpawnGroup
    {
        public readonly int _groupID;
        public readonly int _zoneID;
        public readonly int _spawn_limit;
        public readonly float _min_x;
        public readonly float _max_x;
        public readonly float _min_y;
        public readonly float _max_y;

        public SpawnGroup(int groupID, int zoneID, int spawn_limit, float min_x, float max_x,
            float min_y, float max_y)
        {
            _groupID = groupID;
            _zoneID = zoneID;
            _spawn_limit = spawn_limit;
            _min_x = min_x;
            _max_x = max_x;
            _min_y = min_y;
            _max_y = max_y;
        }
    }
}
