using System;

namespace ReturnHome.Server.EntityObject.Actors
{
    public struct SpawnPoint
    {

        public readonly int _pointID;
        public int _groupID;
        public float _x;
        public float _y;
        public float _z;
        public float _facing;
        public int _world;
        public int _respawnTime;
        public int _enabled;


        public SpawnPoint(int pointID, int groupID, float x, float y, float z, float facing, int world, int respawnTime, int enabled)
        {
            _pointID = pointID;
            _groupID = groupID;
            _x = x;
            _y = y;
            _z = z;
            _facing = facing;
            _world = world;
            _respawnTime = respawnTime;
            _enabled = enabled;
        }
    }
}
