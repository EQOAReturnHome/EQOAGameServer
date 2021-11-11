using System;
using QuadTrees.QTreePointF;
using System.Drawing;
using System.Numerics;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity : IPointFQuadStorable
    {
        public int World;
        public float x;
        public float y;
        public float z;

        //Should be set to time once player is identified as moving, if player stops set back to 0
        private long movement_started;

        //Set position to waypoint after initial position is set, should waypoint be assigned by the client update? Seems logical
        public Vector3 waypoint;
        public Vector3 position;


        public byte Facing;
        public float FacingF;
        public float Speed;
        private float movement_speed = 2.0f;
        public byte Turning = 0;

        public byte Animation;
        public int Target;
        public byte Movement = 1;

        public float VelocityX = 0.0f;
        public float VelocityY = 0.0f;
        public float VelocityZ = 0.0f;
        public float WayPointVelocityX = 0.0f;
        public float WayPointVelocityY = 0.0f;
        public float WayPointVelocityZ = 0.0f;

        public byte EastToWest = 0;
        public byte LateralMovement = 0;
        public byte NorthToSouth = 0;
        public byte SpinDown = 0;

        //For QuadTree
        private PointF _point;

        public PointF Point
        {
            get { return _point; }
        }

        //Calculates our speed? I suspect this will be kinda wrong depending on network latency and such...
        public void CalculateSpeed()
        {
            /*
            _newestPosition = new Vector2(x, z);
            Speed = Vector2.Distance(Position, _newestPosition);
            Console.WriteLine($"Speed is {Speed}");
            Position = _newestPosition;
            */
        }

        public void CalculateDirection()
        {

        }

        public void SetPosition()
        {
            position = new Vector3(x, y, z);
        }

        public void UpdateWayPoint(float X, float Y, float Z)
        {
            movement_started = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            x = X;
            y = Y;
            z = Z;
            waypoint = new Vector3(x, y, z);
            _point = new PointF(x, z);
        }

        public void UpdateFacing(byte facing, byte turning)
        {
            Facing = (byte)(facing + 128);
            FacingF = CoordinateConversions.ConvertFacing(Facing);
            Turning = turning;
        }

        public void UpdateAnimation(byte animation)
        {
            Animation = animation;
        }

        public void UpdateVelocity(float velocityX, float velocityY, float velocityZ)
        {
            WayPointVelocityX = velocityX;
            WayPointVelocityY = velocityY;
            WayPointVelocityZ = velocityZ;
        }

        public void UpdateTarget(int target)
        {
            Target = target;
        }

        public Vector3 getPosition()
        {
            if (movement_started == 0)
            {
                return position;
            }

            float delay = (DateTimeOffset.Now.ToUnixTimeMilliseconds() - movement_started) / 1000.0f;

            Vector3 d = Vector3.Subtract(waypoint, position);

            float dd = Vector3.Distance(waypoint, position);

            if (dd > 0)
            {
                d.X = Speed * d.X / dd * delay;
                d.Y = Speed * d.Y / dd * delay;
                d.Z = Speed * d.Z / dd * delay;
            }

            position = Vector3.Add(position, d);

            return position;
        }
    }
}
