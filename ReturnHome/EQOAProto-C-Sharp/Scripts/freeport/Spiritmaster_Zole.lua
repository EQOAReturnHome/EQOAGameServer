local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Warrior(0) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "102") == "1") then
if (choice:find("Farewell")) then
multiDialogue = { "Spiritmaster Zole: It is best that you not wonder off if your guildmaster has put you to a task. Guildmasters expect focus and punctuality."
 } 
elseif (choice:find("Commander")) then
BindPlayer(thisEntity.ObjectID)
multiDialogue = { "Spiritmaster Zole: You were sent to me for binding, or rather, the binding of your spirit.",
    "Spiritmaster Zole: When a spirit is bound to a location, that spirit will return to the last location it was bound to if it's body is slain.",
    "Spiritmaster Zole: The body will rematerialize there along with all of it's equipment. I will bind your spirit to this location now.",
    "Spiritmaster Zole: Only when the gods deem that your destiny has been fulfilled will you truly die. Until then you will always return.",
    "Spiritmaster Zole: As per Commander Nothard's request, I will bind you as I bind all new warriors.",
    "Spiritmaster Zole: Before I send you back to Commander Nothard you must speak with Coachman Ronks.",
    "Spiritmaster Zole: You can find him west through the doorway, then south through the long hallway, then head to the stables to the southwest .",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 102, quests[102][1].log)
else
    npcDialogue = "Hello."
    diagOptions = { "Sorry to bother, but Commander Nothard sent me.", "Farewell." }
end
  else
        npcDialogue =
"Spiritmaster Zole: I can bind you here if you want."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

