local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Enchanter(12) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "120113") == "1") then
if (CheckQuestItem(mySession, items.BAG_OF_COINS, 1))
 then
if (choice:find("looking")) then
npcDialogue = "Agent Wilkenson: Take a look around... I'm not sure you are where you mean to be."
elseif (choice:find("correct")) then
multiDialogue = { "Agent Wilkenson: He mentioned that he would be sending someone for this next operation.",
    "Agent Wilkenson: I'll need you to take this note to Duminven. Look for him at Saerk's Tower.",
    "Agent Wilkenson: Exit the north gate and follow the road northwest to reach Kithicor Forest. Once there, look for Saerk's Tower. Stick to the Highpass Trade Route to reach the forest.",
    "Agent Wilkenson: Climb the hill east of the second gaurdtower past the bridge to reach Saerk's Tower. Find Duminven and do what ever he tells you.",
"You have given away a bag of coins.",
"You have received a note from Wilkinson.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120113, quests[120113][1].log)
TurnInItem(mySession, items.BAG_OF_COINS, 1)
GrantItem(mySession, items.NOTE_FROM_WILKINSON, 1)
else
    npcDialogue = "You must be the one sent from Azlynn."
    diagOptions = { "That is correct.", "Oh sorry, I am looking for the pub." }
end
else
npcDialogue = "Agent Wilkenson: Take a look around... I'm not sure you are where you mean to be."
end
elseif (GetPlayerFlags(mySession, "120120") == "1") then
if (choice:find("think")) then
multiDialogue = { "Agent Wilkenson: I'll need you to stand back. A little further. Now, turn around, keep walking, and don't come near me again."
 } 
elseif (choice:find("Azlynn")) then
multiDialogue = { "Agent Wilkenson: Go speak to Telina the Dark Witch.",
    "Agent Wilkenson: She may have information on the location of the real mark. You can find her in the Guard tower just east of here.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120120, quests[120120][1].log)
else
    npcDialogue = "Something I can help you with?"
    diagOptions = { "Azlynn said you have a lead on the real mark.", "I think not." }
end
  else
        npcDialogue =
"Agent Wilkenson: I'll need you to stand back. A little further. Now, turn around, keep walking, and don't come near me again."
    end
------
--Necromancer(11) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "110113") == "1") then
if (CheckQuestItem(mySession, items.BAG_OF_COINS, 1))
 then
if (choice:find("looking")) then
npcDialogue = "Agent Wilkenson: Take a look around... I'm not sure you are where you mean to be."
elseif (choice:find("correct")) then
multiDialogue = { "Agent Wilkenson: He mentioned that he would be sending someone for this next operation.",
    "Agent Wilkenson: I'll need you to take this note to Duminven. Look for him at Saerk's Tower. It will be a small trek to get there.",
    "Agent Wilkenson: Exit the north gate and follow the road northwest to reach Kithicor Forest. Once there, look for Saerk's Tower. Stick to the Highpass Trade Route to reach the forest.",
    "Agent Wilkenson: Climb the hill east of the second gaurdtower past the bridge to reach Saerk's Tower. Find Duminven and do what ever he tells you.",
"You have given away a bag of coins.",
"You have received a note from Wilkinson.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110113, quests[110113][1].log)
TurnInItem(mySession, items.BAG_OF_COINS, 1)
GrantItem(mySession, items.NOTE_FROM_WILKINSON, 1)
else
    npcDialogue = "You must be the one sent from House Slaerin."
    diagOptions = { "That is correct.", "Oh sorry, I am looking for the..uh..the church." }
end
else
npcDialogue = "Agent Wilkenson: Take a look around... I'm not sure you are where you mean to be."
end
elseif (GetPlayerFlags(mySession, "110120") == "1") then
if (choice:find("think")) then
multiDialogue = { "Agent Wilkenson: I'll need you to stand back. A little further. Now, turn around, keep walking, and don't come near me again."
 } 
elseif (choice:find("Corious")) then
multiDialogue = { "Agent Wilkenson: Go speak to Telina the Dark Witch.",
    "Agent Wilkenson: She may have information on the location of the real mark. You can find her northeast on the coast by a rock.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110120, quests[110120][1].log)
else
    npcDialogue = "Something I can help you with?"
    diagOptions = { "Corious Slaerin said you have a lead on the real mark.", "I think not." }
end
  else
        npcDialogue =
"Agent Wilkenson: I'll need you to stand back. A little further. Now, turn around, keep walking, and don't come near me again."
    end
------


SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

