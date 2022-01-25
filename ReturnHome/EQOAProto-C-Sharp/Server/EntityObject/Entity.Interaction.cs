
using System;
using System.Numerics;
using ReturnHome.Opcodes;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {

        private uint _target;
        private uint _targetCounter = 1;
        private Entity _ourTarget;

        public uint Target
        {
            get { return _target; }
            set
            {
                if (true)
                {
                    _target = value;
                    ObjectUpdateTarget();

                    //Keep a reference to our current target on hand
                    EntityManager.QueryForEntity(_target, out _ourTarget);

                    if (isPlayer && ObjectID != 0)
                    {
                        //Get target information about the object
                        TargetInformation(_target);
                    }
                }
            }
        }

        public void TargetInformation(uint Target)
        {
            Entity ent;
            Memory<byte> message;
            //If false, ignore? Might need some kind of escalation, why would we target something not known about?
            //If true, prep message
            if (EntityManager.QueryForEntity(_target, out ent))
            {
                //This shouldn't happen, but to be safe? Eventually could be an expired object that was originally target?
                if (ent == null)
                    return;

                int offset = 0;

                //This message type seems to always be this length from live packet captures?
                message = new byte[0x0109];

                Span<byte> temp = message.Span;
                temp.Write((ushort)GameOpcode.TargetInformation, ref offset);
                temp.Write((byte)3, ref offset); // 0/1 = red face 2/3 = neutral face 4/5 = blue face //Perform Calculations to check for 
                temp.Write((byte)2, ref offset); // 0 = red con 1 = yellow con 2 = white con 3 = Dark Blue con 4 = Light Blue Con 5 = Green con 6 = Yellowish/white con? 7 = no con at all? But can still target? 14 = faded yellow con? 15 = faded orange con? 60 = yellowish/green con?

                offset = 124;
                temp.Write(_target, ref offset);

                offset = 261;
                temp.Write(_targetCounter++, ref offset);
                SessionQueueMessages.PackMessage(((Character)this).characterSession, message, MessageOpcodeTypes.ShortReliableMessage);
            }
        }

        public bool IsWithinRange()
        {
            //What should be the distance check against interacting with NPC's?
            //Should this distance check be farther or shorter for attacking in combat, too? Such as auto attack
            if (Vector3.Distance(Position, _ourTarget.Position) <= 10.0f)
                return true;
            return false;

        }
    }
}
