local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Cleric(9) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "90102") == "1") then
if (choice:find("Farewell")) then
multiDialogue = { "Spiritmaster Keika: It is best that you not wonder off if your guildmaster has put you to a task. Guildmasters expect focus and punctuality."
 } 
elseif (choice:find("Denouncer")) then
BindPlayer(thisEntity.ObjectID)
multiDialogue = { "Spiritmaster Keika: Ahh, you must be a new member of the Shining Shield Mercenaries. All new recruits come to me for binding.",
    "Spiritmaster Keika: I am a Spiritmaster. I have been trained to bind a person's Soul to a certain location if they so wish it.",
    "Spiritmaster Keika: When you are slain, your spirit will return to where it is bound. There your body and possessions will rematerialize.",
    "Spiritmaster Keika: Only when the gods deem that your destiny has been fulfilled will you truly die. Until then you will always return.",
    "Spiritmaster Keika: As per Denouncer Alshea's request, I will bind you as I bind all new clerics.",
    "Spiritmaster Keika: Before I send you back to Denouncer Alshea you must speak with Coachman Ronks.",
    "Spiritmaster Keika: You can find him just west of here past the docks.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90102, quests[90102][1].log)
else
    npcDialogue = "Hello."
    diagOptions = { "Sorry to bother, but Denouncer Alshea sent me.", "Farewell." }
end
  else
        npcDialogue =
"Spiritmaster Keika: I suppose you'd like your spirit bound to this location?"
    end
-----
--Rogue(6) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "60102") == "1") then
if (choice:find("Farewell")) then
multiDialogue = { "Spiritmaster Keika: It is best that you not wonder off if your guildmaster has put you to a task. Guildmasters expect focus and punctuality."
 } 
elseif (choice:find("Necorik")) then
BindPlayer(thisEntity.ObjectID)
multiDialogue = { "Spiritmaster Keika: Ahh, you must be a new member of the Shining Shield Mercenaries. All new recruits come to me for binding.",
    "Spiritmaster Keika: I am a Spiritmaster. I have been trained to bind a person's Soul to a certain location if they so wish it.",
    "Spiritmaster Keika: When you are slain, your spirit will return to where it is bound. There your body and possessions will rematerialize.",
    "Spiritmaster Keika: Only when the gods deem that your destiny has been fulfilled will you truly die. Until then you will always return.",
    "Spiritmaster Keika: As per Necorik the Ghost's request, I will bind you as I bind all new rogues.",
    "Spiritmaster Keika: Before I send you back to Necorik the Ghost you must speak with Coachman Ronks.",
    "Spiritmaster Keika: You can find him just west of here past the docks.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60102, quests[60102][1].log)
else
    npcDialogue = "Hello."
    diagOptions = { "Sorry to bother, but Necorik the Ghost sent me.", "Farewell." }
end
  else
        npcDialogue =
"Spiritmaster Keika: I suppose you'd like your spirit bound to this location?"
    end
------
--Necromancer(11) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "110102") == "1") then
if (choice:find("Goodbye")) then
multiDialogue = { "Spiritmaster Keika: It is best that you not wonder off if your guildmaster has put you to a task. Guildmasters expect focus and punctuality."
 } 
elseif (choice:find("Corious")) then
BindPlayer(thisEntity.ObjectID)
multiDialogue = { "Spiritmaster Keika: Ahh, you must be a new member of the House Slaerin. All new recruits come to me for binding.",
    "Spiritmaster Keika: I am a Spiritmaster. I have been trained to bind a person's Soul to a certain location if they so wish it.",
    "Spiritmaster Keika: When you are slain, your spirit will return to where it is bound. There your body and possessions will rematerialize.",
    "Spiritmaster Keika: Only when the gods deem that your destiny has been fulfilled will you truly die. Until then you will always return.",
    "Spiritmaster Keika: As per Corious Slaerin's request, I will bind you as I bind all new necromancers.",
    "Spiritmaster Keika: Before I send you back to Corious Slaerin, you must speak with Coachman Ronks.",
    "Spiritmaster Keika: You can find him just west of here past the docks..",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110102, quests[110102][1].log)
else
    npcDialogue = "Hello."
    diagOptions = { "Corious Slaerin sent me.", "Goodbye." }
end
  else
        npcDialogue =
"Spiritmaster Keika: I suppose you'd like your spirit bound to this location?"
    end
------


SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

