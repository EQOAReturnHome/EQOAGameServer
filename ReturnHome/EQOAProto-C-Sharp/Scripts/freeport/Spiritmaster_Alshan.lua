local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Magician(10) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "100102") == "1") then
if (choice:find("Good")) then
multiDialogue = { "Spiritmaster Alshan: It is best that you not wonder off if your guildmaster has put you to a task. Guildmasters expect focus and punctuality."
 } 
elseif (choice:find("bother")) then
BindPlayer(thisEntity.ObjectID)
multiDialogue = { "Spiritmaster Alshan: You were sent to me for binding, or rather, the binding of your spirit.",
    "Spiritmaster Alshan: When a spirit is bound to a location, that spirit will return to the last location it was bound to if it's body is slain.",
    "Spiritmaster Alshan: The body will rematerialize there along with all of it's equipment. I will bind your spirit to this location now.",
    "Spiritmaster Alshan: Before I send you back to Malsis you must speak with Coachman Ronks. He runs the stables to the south.",
    "Spiritmaster Alshan: You can find him just passed the southern exit of Freeport. Head out the doorway southeast of here, then south along the midroad, then southwest to the stables.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100102, quests[100102][1].log)
else
    npcDialogue = "Hello."
    diagOptions = { "Sorry to bother, but Malsis sent me.", "Good day!" }
end
  else
        npcDialogue =
"Spiritmaster Alshan: Shall I bind your spirit to this location, playerName?"
    end
------
--Enchanter(12) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "120102") == "1") then
if (choice:find("Farewell")) then
multiDialogue = { "Spiritmaster Alshan: It is best that you not wonder off if your guildmaster has put you to a task. Guildmasters expect focus and punctuality."
 } 
elseif (choice:find("bother")) then
BindPlayer(thisEntity.ObjectID)
multiDialogue = { "Spiritmaster Alshan: You were sent to me for binding, or rather, the binding of your spirit.",
    "Spiritmaster Alshan: When a spirit is bound to a location, that spirit will return to the last location it was bound to if it's body is slain.",
    "Spiritmaster Alshan: The body will rematerialize there along with all of it's equipment. I will bind your spirit to this location now.",
    "Spiritmaster Alshan: Before I send you back to Azlynn you must speak with Coachman Ronks. He runs the stables to the south.",
    "Spiritmaster Alshan: You can find him southeast through the doorway, then south through the long hallway, then head to the stables to the southwest.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120102, quests[120102][1].log)
else
    npcDialogue = "Hello."
    diagOptions = { "Sorry to bother, but Azlynn sent me.", "Farewell." }
end
  else
        npcDialogue =
"Spiritmaster Alshan: Shall I bind your spirit to this location, playerName?"
    end
------
--Wizard(13) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "130102") == "1") then
if (choice:find("Farewell")) then
multiDialogue = { "Spiritmaster Alshan: It is best that you not wonder off if your guildmaster has put you to a task. Guildmasters expect focus and punctuality."
 } 
elseif (choice:find("Sivrendesh")) then
BindPlayer(thisEntity.ObjectID)
multiDialogue = { "Spiritmaster Alshan: You were sent to me for binding, or rather, the binding of your spirit.",
    "Spiritmaster Alshan: When a spirit is bound to a location, it will return to the last location it was bound to if it's body is slain.",
    "Spiritmaster Alshan: The body will rematerialize there along with all of it's equipment. I will bind your spirit to this location now.",
    "Spiritmaster Alshan: Before I send you back to Sivrendesh you must speak with Coachman Ronks. He runs the stables to the south.",
    "Spiritmaster Alshan: You can find him just past the southern exit of Freeport. Head out the doorway southeast of here, then south along the midroad, then southwest to the stables.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 130102, quests[130102][1].log)
else
    npcDialogue = "Hello."
    diagOptions = { "Sorry to bother, but Sivrendesh sent me.", "Farewell." }
end
  else
        npcDialogue =
"Spiritmaster Alshan: Shall I bind your spirit to this location, playerName?"
    end
------



SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end

