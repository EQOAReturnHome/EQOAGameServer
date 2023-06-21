local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Magician(10) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "100120") == "3") then
if (choice:find("deliver")) then
multiDialogue = { "Page Joseph Robert: I'll have you know I am delivering items of the upmost importance. I wouldn't hand them over to a grubby street rat such that you are. You will have to pry them from my cold, dead fingers.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100120, quests[100120][3].log)
else
    npcDialogue = "Approaching a man at this time of day? Please be quick, I have tasks at hand."
    diagOptions = { "May I help deliver those for you? You look awfully tired." }
end
  else
        npcDialogue =
"Page Joseph Robert: I am in service of the honorable Sir Hanst. He has much official business that needs proper delivery. Now if you don't mind, I'll be on my way."
    end
------
--Bard(5) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "50120") == "3") then
if (choice:find("deliver")) then
multiDialogue = { "Page Joseph Robert: I would normally never dream of handing over such important documents, but to be honest, I am quite exhausted today. I can see in your eyes that you are a person of honor and integrity.",
    "Page Joseph Robert: Please see that these are delivered promptly. I am going to find a nice place to rest.",
"You have received the missives from Qeynos.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50120, quests[50120][3].log)
GrantItem(mySession, items.MISSIVES_FROM_QEYNOS, 1)
else
    npcDialogue = "Approaching a man at this time of day? Please be quick, I have tasks at hand."
    diagOptions = { "May I help deliver those for you? You look awfully tired." }
end
  else
        npcDialogue =
"Page Joseph Robert: I am in service of the honorable Sir Hanst. He has much official business that needs proper delivery. Now if you don't mind, I'll be on my way."
    end
------
--Cleric(9) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "90120") == "3") then
if (choice:find("deliver")) then
multiDialogue = { "Page Joseph Robert: I'll have you know I am delivering items of the upmost importance. I wouldn't hand them over to a grubby street rat such that you are. You will have to pry them from my cold, dead fingers.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90120, quests[90120][3].log)
else
    npcDialogue = "Approaching a man at this time of day? Please be quick, I have tasks at hand."
    diagOptions = { "May I help deliver those for you? You look awfully tired." }
end
  else
        npcDialogue =
"Page Joseph Robert: I am in service of the honorable Sir Hanst. He has much official business that needs proper delivery. Now if you don't mind, I'll be on my way."
    end
------
--Rogue(6) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "60120") == "3") then
if (choice:find("deliver")) then
multiDialogue = { "Page Joseph Robert: I'll have you know I am delivering items of the upmost importance. I wouldn't hand them over to a grubby street rat such that you are. You will have to pry them from my cold, dead fingers.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60120, quests[60120][3].log)
else
    npcDialogue = "Approaching a man at this time of day? Please be quick, I have tasks at hand."
    diagOptions = { "May I help deliver those for you? You look awfully tired." }
end
  else
        npcDialogue =
"Page Joseph Robert: I am in service of the honorable Sir Hanst. He has much official business that needs proper delivery. Now if you don't mind, I'll be on my way."
    end
------
--Warrior(0) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "120") == "3") then
if (choice:find("deliver")) then
multiDialogue = { "Page Joseph Robert: I'll have you know I am delivering items of the upmost importance. I wouldn't hand them over to a grubby street rat such that you are. You will have to pry them from my cold, dead fingers.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120, quests[120][3].log)
else
    npcDialogue = "Approaching a man at this time of day? Please be quick, I have tasks at hand."
    diagOptions = { "May I help deliver those for you? You look awfully tired." }
end
  else
        npcDialogue =
"Page Joseph Robert: I am in service of the honorable Sir Hanst. He has much official business that needs proper delivery. Now if you don't mind, I'll be on my way."
    end
------




SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end


