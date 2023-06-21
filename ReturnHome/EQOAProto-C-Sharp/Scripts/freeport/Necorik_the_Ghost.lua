local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Rogue(6) Human(0) Eastern(1)
if
            (class == "Rogue" and race == "Human" and humanType == "Eastern" and
            GetPlayerFlags(mySession, "60101") == "noFlags")
then
        SetPlayerFlags(mySession, "60101", "0")
end
if (GetPlayerFlags(mySession, "60101") == "0") then
if (choice:find("Mercenaries")) then
multiDialogue = { "Necorik the Ghost: Right. Well, don't think you can just walk in here and learn our skills and use our supplies without paying for it.",
    "Necorik the Ghost: You must complete a number of tasks before you will be considered a member.",
    "Necorik the Ghost: Your first task is to acquire burglar's gloves from Merchant Olkan. Your fee for this will be waived.",
    "Necorik the Ghost: When you have the burglar's gloves, return to me and I'll send you on your second task...If you can manage.",
    "You have received a quest!"
}
StartQuest(mySession, 60101, quests[60101][0].log)
else
    npcDialogue = "What brings you to this hall of darkness?"
    diagOptions = { "I wish to be a rogue of The Shining Shield Mercenaries." }
end
elseif (GetPlayerFlags(mySession, "60101") == "1") then
if (CheckQuestItem(mySession, items.BURGLARS_GLOVES, 1))
 then
if (choice:find("eventually")) then
npcDialogue = "Necorik the Ghost: I'll need you to purchase the burglar's gloves from Merchant Olkan, then return to me."
elseif (choice:find("course")) then
multiDialogue = { "Necorik the Ghost: So...maybe you aren't a dimwit after all. But you'll need more skills to make it as a rogue around here.",
    "Necorik the Ghost: If you still wish to learn, return to me shortly. I have other things to attend to right now.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 60101, quests[60101][1].xp, 60102 )
else
    npcDialogue = "I take it you have the gloves I sent you for?"
    diagOptions = { "Of course I do.", "Not yet. I'll get around to it eventually..." }
end
else
npcDialogue = "Necorik the Ghost: I'll need you to purchase the burglar's gloves from Merchant Olkan, then return to me."
end
elseif (GetPlayerFlags(mySession, "60102") == "0") then
if (choice:find("ready")) then
multiDialogue = { "Necorik the Ghost: Rogues practice stealth and thievery and deal in poisons and the art of assassination. Though they have difficulty going at it alone, a skilled rogue is lethal in group situations.",
    "Necorik the Ghost: A rogue relies on the ability to get behind a target. There they can backstab to score powerful critical hits, which are sometimes more lethal than the damage caused by other fighters.",
    "Necorik the Ghost: Rogues must be careful not to draw the enemies attention however, because they cannot absorb as much damage as the likes of a warrior.",
"Necorik the Ghost: Now for your next task. I need you to speak to Spiritmaster Keika.",
"Necorik the Ghost: You can find her near the south east corner of the city. Go out the doorway of The Shining Shield, through the east city exit, head south along the wall, and take a right around the corner.",
"Necorik the Ghost: Return only when you complete any tasks she gives you.",
    "You have received a quest!"
}
StartQuest(mySession, 60102, quests[60102][0].log)
else
    npcDialogue = "This had better be good."
    diagOptions = { "I'm ready for more." }
end
elseif (GetPlayerFlags(mySession, "60102") == "3") then
if (choice:find("Maybe")) then
multiDialogue = { "Necorik the Ghost: You'll find that we guildmasters don't like to be kept waiting. I suggest you tend to the task at hand. Consult your quest log if you have lost track of your tasks."
 } 
elseif (choice:find("done")) then
multiDialogue = { "Necorik the Ghost: That is good. Be sure to have yourself bound often. It is quite inconvenient to be defeated far from your last binding.",
    "Necorik the Ghost: Now that you've completed that, I have another task for you. Go see Athera, she will assist you.",
    "Necorik the Ghost: You can find Athera upstairs, in the room to the left.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 60102, quests[60102][3].xp, 60103 )
else
    npcDialogue = "Are you finished with my orders?"
    diagOptions = { "Yes, it is done.", "Hmm, not sure. Maybe not." }
end
  else
        npcDialogue =
"Necorik the Ghost: Well, don't think you can just walk in here and learn our skills and use our supplies without paying for it."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

