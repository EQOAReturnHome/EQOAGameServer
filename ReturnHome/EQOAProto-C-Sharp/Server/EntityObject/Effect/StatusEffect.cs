
using System;

namespace ReturnHome.Server.EntityObject.Effect
{
    public class StatusEffect
    {

        public int id;
        public string name;
        public uint icon;
        public long lastTick;
        public uint duration;
        public uint tier;
        public uint casterID;
        public Status status;

        //default constructor
        public StatusEffect()
        {

        }


        //Status effect contructor for players(need name and icon for 43 unreliable message)
        public StatusEffect(int id, string name, uint icon, uint duration, uint tier, uint casterID)
        {
            status = new(icon, name);
            this.name = name;
            this.id = id;
            this.lastTick = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            this.duration = duration;
            this.tier = tier;
            this.casterID = casterID;
        }

        //Status effect constructor for non-players
        public StatusEffect(int id, uint tick, uint duration, uint tier)
        {
            this.id = id;
            this.lastTick = tick;
            this.duration = duration;
            this.tier = tier;
        }

        public StatusEffect(string name, uint icon)
        {
            this.name = name;
            this.icon = icon;
        }


    }
}
