using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;
using ReturnHome.Server.Network;
using System.ServiceModel.Channels;
using static System.Collections.Specialized.BitVector32;

namespace ReturnHome.Server.Managers
{

    public static class LuaState
    {
        public static readonly Lua State = new();

        static LuaState()
        {
            State.LoadCLRPackage();

            State["AddQuest"] = Quest.AddQuest;
            State["DeleteQuest"] = Quest.DeleteQuest;
            State["TeleportPlayer"] = ServerTeleportPlayer.TeleportPlayer;
            State["StartQuest"] = Quest.StartQuest;
            State["ContinueQuest"] = Quest.ContinueQuest;
            State["CompleteQuest"] = Quest.CompleteQuest;
            State["CheckQuestItem"] = Character.CheckIfQuestItemInInventory;
            State["GenerateChatMessage"] = Opcodes.Chat.ChatMessage.GenerateClientSpecificChat;
            State["GrantXP"] = Character.GrantXP;
            State["GetWorld"] = Utility_Funcs.GetEnumObjectByValue<World>;
            State["GetClass"] = Utility_Funcs.GetEnumObjectByValue<Class>;
            State["GetRace"] = Utility_Funcs.GetEnumObjectByValue<Race>;
            State["GetHumanType"] = Utility_Funcs.GetEnumObjectByValue<HumanType>;
            State["UpdateAnim"] = Entity.UpdateAnim;
            //wont actually return npc object to update, only npcid
            State["GrantItem"] = ItemManager.GrantItem;
            State["TurnInItem"] = ItemManager.UpdateQuantity;
            State["RemoveTunar"] = Entity.RemoveTunar;
            State["AddTunar"] = Entity.AddTunar;

            State["CastSpell"] = ServerCastSpell.CastSpell;
            State["Damage"] = ServerDamage.Damage;
            State["CoolDown"] = ServerSpellCoolDown.SpellCoolDown;
            State["TeleportPlayer"] = ServerTeleportPlayer.TeleportPlayer;
            State["ServerLearnSpell"] = ServerLearnSpell.LearnSpell;
            State["LearnSpell"] = SpellManager.LearnSpell;
            State["GetSpell"] = SpellManager.GetSpellPattern;






            // add Lua bindings here by object type
        }
    }
}
