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
        private Vector3 _position;


        private byte _facing;
        private float _facingF;
        public float Speed;
        private float movement_speed = 2.0f;
        private byte _turning = 0;

        private byte _animation;
        private int _target;
        public byte Movement = 1;

        public float VelocityX = 0.0f;
        public float VelocityY = 0.0f;
        public float VelocityZ = 0.0f;
        public float WayPointVelocityX = 0.0f;
        public float WayPointVelocityY = 0.0f;
        public float WayPointVelocityZ = 0.0f;

        private byte _eastToWest = 0;
        private byte _lateralMovement = 0;
        private byte _northToSouth = 0;
        private byte _spinDown = 0;
        private byte _firstPerson;

        //For QuadTree
        private PointF _point;

        public PointF Point
        {
            get { return _point; }
        }

        #region Property zone

        public byte Facing
        {
            get { return _facing; }
            set
            {
                if(true)
                {
                    _facing = value;
                    ObjectUpdateFacing();
                }    
            }
        }

        public float FacingF
        {
            get { return _facingF; }
            set
            {
                if(true)
                {
                    _facingF = value;
                    ObjectUpdateFacingF();
                }
            }
        }

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                if(true)
                {
                    _position = value;
                    ObjectUpdatePosition();
                }
            }
        }
        public byte EastToWest
        {
            get { return _eastToWest; }
            set
            {
                if(true)
                {
                    _eastToWest = value;
                    ObjectUpdateEastWest();
                }
            }
        }

        public byte LateralMovement
        {
            get { return _lateralMovement; }
            set
            {
                if(true)
                {
                    _lateralMovement = value;
                    ObjectUpdateLateralMovement();
                }
            }
        }

        public byte NorthToSouth
        {
            get { return _northToSouth; }
            set
            {
                if(true)
                {
                    _northToSouth = value;
                    ObjectUpdateNorthSouth();
                }
            }
        }

        public byte Turning
        {
            get { return _turning; }
            set
            {
                if(true)
                {
                    _turning = value;
                    ObjectUpdateTurning();
                }
            }
        }

        public byte SpinDown
        {
            get { return _spinDown; }
            set
            {
                if(true)
                {
                    _spinDown = value;
                    ObjectUpdateSpinDown();
                }
            }
        }

        public byte Animation
        {
            get { return _animation; }
            set
            {
                if(true)
                {
                    _animation = value;
                    ObjectUpdateAnimation();
                }
            }
        }

        public int Target
        {
            get { return _target; }
            set
            {
                if(true)
                {
                    _target = value;
                    ObjectUpdateTarget();
                }
            }
        }

        public byte FirstPerson
        {
            get { return _firstPerson; }
            set
            {
                if(true)
                {
                    _firstPerson = value;
                    ObjectUpdateFirstPerson();
                }
            }
        }

        #endregion

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
            Position = new Vector3(x, y, z);
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
                return Position;
            }

            float delay = (DateTimeOffset.Now.ToUnixTimeMilliseconds() - movement_started) / 1000.0f;

            Vector3 d = Vector3.Subtract(waypoint, Position);

            float dd = Vector3.Distance(waypoint, Position);

            if (dd > 0)
            {
                d.X = Speed * d.X / dd * delay;
                d.Y = Speed * d.Y / dd * delay;
                d.Z = Speed * d.Z / dd * delay;
            }

            Position = Vector3.Add(Position, d);

            return Position;
        }
    }
}
