using System;
using System.Collections.Concurrent;
using ReturnHome.Server.EntityObject.Player;

namespace ReturnHome.Server.EntityObject
{
    public static class DefaultCharacter
    {
        public static ConcurrentDictionary<(Race, Class, HumanType, Sex), Character> DefaultCharacterDict = new();
    }
}
