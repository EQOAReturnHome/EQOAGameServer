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
-----
--Rogue(6) Human(0) Eastern(1)
elseif (GetPlayerFlags(mySession, "60102") == "1") then
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
------
--Necromancer(11) Human(0) Eastern(1)
elseif (GetPlayerFlags(mySession, "110102") == "1") then
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
------
--Shadowknight(3) Human(0) Eastern(1)
elseif (GetPlayerFlags(mySession, "30102") == "1") then
if (choice:find("Nevermind")) then
multiDialogue = { "Spiritmaster Keika: It is best that you not wonder off if your guildmaster has put you to a task. Guildmasters expect focus and punctuality."
 } 
elseif (choice:find("Crimsonhand")) then
BindPlayer(thisEntity.ObjectID)
multiDialogue = { "Spiritmaster Keika: A new shadowknight to take the trials are you? Well I hope you do better than the last one, hearing his screams was unpleasant.",
    "Spiritmaster Keika: I am a Spiritmaster, trained in the sacred art of binding ones soul to a specific location.",
    "Spiritmaster Keika: When you are slain, your spirit will return to where it is bound. There your body and possessions will rematerialize.",
    "Spiritmaster Keika: Only when ones destiny by the gods is fulfilled will they truly die. I shall now bind your spirit here for the time being.",
    "Spiritmaster Keika: As per Malethai Crimsonhand's request, I will bind you as I bind all new shadowknights.",
    "Spiritmaster Keika: Before I send you back to your master you must speak with Coachman Ronks.",
    "Spiritmaster Keika: You can find him just west of here past the docks.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 30102, quests[30102][1].log)
else
    npcDialogue = "Hello."
    diagOptions = { "Malethai Crimsonhand sent me.", "Nevermind." }
end
elseif (choice:find("bind")) then
    npcDialogue = "Your soul will now return here, playerName."
    BindPlayer(thisEntity.ObjectID)
elseif (choice:find("Not")) then
    npcDialogue = "Please come back if you change your mind."
else
    npcDialogue = "Would you like me to bind your spirit to this location, child?"
    diagOptions = {"Yes, please bind my soul.", "Not at this time."}
    end
------
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end

