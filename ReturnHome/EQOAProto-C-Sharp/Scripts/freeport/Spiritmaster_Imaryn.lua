local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Bard(5) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "50102") == "1") then
if (choice:find("nothing")) then
multiDialogue = { "Spiritmaster Imaryn: It is best that you not wonder off if your guildmaster has put you to a task. Guildmasters expect focus and punctuality."
 } 
elseif (choice:find("Corufost")) then
BindPlayer(thisEntity.ObjectID)
multiDialogue = { "Spiritmaster Imaryn: Ah, From Corufost. He sends all aspiring bards to me to have their spirits bound to this area.",
    "Spiritmaster Imaryn: If your body is slain, your spirit will flee to where it was last bound. A new body will materialize with all of your equipment.",
    "Spiritmaster Imaryn: I will bind you now.",
    "Spiritmaster Imaryn: Before I send you back to William Corufost you must speak with Coachman Ronks. He manages the stables near the docks.",
    "Spiritmaster Imaryn: Go back through the West gate and go through the archway to the Southeast. The stables should be just to the Southwest.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50102, quests[50102][1].log)
else
    npcDialogue = "Hiya! What can I do for you?"
    diagOptions = { "William Corufost sent me.", "Oh nothing, sorry." }
end
  else
        npcDialogue =
"Spiritmaster Imaryn: Would you like me to bind your spirit to this location, playerName?"
    end
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

