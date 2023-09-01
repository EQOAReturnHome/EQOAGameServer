local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Magician(10) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "100120") == "7") then
if (choice:find("Grimoire")) then
multiDialogue = { "Praetor Gunner: Hmm...",
    "Praetor Gunner: Well then, I shall have to test you. I can tell when someone is lying. Look me straight in the eyes and answer me..."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Where does Sir Hanst Breach reside?"
    diagOptions = { "Qeynos", "Highpass Hold" }
elseif (choice:find("Highpass")) then
multiDialogue = { "Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
 } 
elseif (choice:find("Qeynos")) then
    npcDialogue = "Very well then, Who does Sir Hanst serve?"
    diagOptions = { "Antonius Bayle II", "Advisor Amichevole" }
elseif (choice:find("Amichevole")) then
multiDialogue = { "Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
 } 
elseif (choice:find("Antonius")) then
multiDialogue = { "Praetor Gunner: Alright. I believe you playerName. Something about your eyes. Quite disarming I would say. Here is Geldwin's Grimoire.",
    "Praetor Gunner: I need not remind you of how dangerous this book could be if it fell into the wrong hands. Protect it with your life. Send Sir Hanst my best.",
"You have received a Geldwins Grimoire.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100120, quests[100120][7].log)
GrantItem(mySession, items.GELDWINS_GRIMOIRE, 1)
else
    npcDialogue = "Am I expected to believe that Sir Hanst would ask this temple to relinquish such a dangerous item?"
    diagOptions = { "Yes, Praetor. Sir Hanst must obtain the Grimoire, with urgency." }
end
  else
        npcDialogue =
"Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
    end
------
--Bard(5) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "50120") == "8") then
if (choice:find("Grimoire")) then
multiDialogue = { "Praetor Gunner: Hmm...",
    "Praetor Gunner: Well then, I shall have to test you. I can tell when someone is lying. Look me straight in the eyes and answer me..."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Where does Sir Hanst Breach reside?"
    diagOptions = { "Qeynos", "Highpass Hold" }
elseif (choice:find("Highpass")) then
multiDialogue = { "Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
 } 
elseif (choice:find("Qeynos")) then
    npcDialogue = "Very well then, Who does Sir Hanst serve?"
    diagOptions = { "Antonius Bayle II", "Advisor Amichevole" }
elseif (choice:find("Amichevole")) then
multiDialogue = { "Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
 } 
elseif (choice:find("Antonius")) then
multiDialogue = { "Praetor Gunner: Alright. I believe you playerName. Something about your eyes. Quite disarming I would say. Here is Geldwin's Grimoire.",
    "Praetor Gunner: I need not remind you of how dangerous this book could be if it fell into the wrong hands. Protect it with your life. Send Sir Hanst my best.",
"You have received Geldwins Grimoire.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50120, quests[50120][8].log)
GrantItem(mySession, items.GELDWINS_GRIMOIRE, 1)
else
    npcDialogue = "Am I expected to believe that Sir Hanst would ask this temple to relinquish such a dangerous item?"
    diagOptions = { "Yes, Praetor. Sir Hanst must obtain the Grimoire, with urgency." }
end
  else
        npcDialogue =
"Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
    end
------
--Cleric(9) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "90120") == "7") then
if (choice:find("Grimoire")) then
multiDialogue = { "Praetor Gunner: Hmm...",
    "Praetor Gunner: Well then, I shall have to test you. I can tell when someone is lying. Look me straight in the eyes and answer me..."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Where does Sir Hanst Breach reside?"
    diagOptions = { "Qeynos", "Highpass Hold" }
elseif (choice:find("Highpass")) then
multiDialogue = { "Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
 } 
elseif (choice:find("Qeynos")) then
    npcDialogue = "Very well then, Who does Sir Hanst serve?"
    diagOptions = { "Antonius Bayle II", "Advisor Amichevole" }
elseif (choice:find("Amichevole")) then
multiDialogue = { "Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
 } 
elseif (choice:find("Antonius")) then
multiDialogue = { "Praetor Gunner: Alright. I believe you playerName. Something about your eyes. Quite disarming I would say. Here is Geldwin's Grimoire.",
    "Praetor Gunner: I need not remind you of how dangerous this book could be if it fell into the wrong hands. Protect it with your life. Send Sir Hanst my best.",
"You have received a Geldwins Grimoire.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90120, quests[90120][7].log)
GrantItem(mySession, items.GELDWINS_GRIMOIRE, 1)
else
    npcDialogue = "Am I expected to believe that Sir Hanst would ask this temple to relinquish such a dangerous item?"
    diagOptions = { "Yes, Praetor. Sir Hanst must obtain the Grimoire, with urgency." }
end
  else
        npcDialogue =
"Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
    end
------
--Rogue(6) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "60120") == "7") then
if (choice:find("Grimoire")) then
multiDialogue = { "Praetor Gunner: Hmm...",
    "Praetor Gunner: Well then, I shall have to test you. I can tell when someone is lying. Look me straight in the eyes and answer me..."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Where does Sir Hanst Breach reside?"
    diagOptions = { "Qeynos", "Highpass Hold" }
elseif (choice:find("Highpass")) then
multiDialogue = { "Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
 } 
elseif (choice:find("Qeynos")) then
    npcDialogue = "Very well then, Who does Sir Hanst serve?"
    diagOptions = { "Antonius Bayle II", "Advisor Amichevole" }
elseif (choice:find("Amichevole")) then
multiDialogue = { "Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
 } 
elseif (choice:find("Antonius")) then
multiDialogue = { "Praetor Gunner: Alright. I believe you playerName. Something about your eyes. Quite disarming I would say. Here is Geldwin's Grimoire.",
    "Praetor Gunner: I need not remind you of how dangerous this book could be if it fell into the wrong hands. Protect it with your life. Send Sir Hanst my best.",
"You have received a Geldwins Grimoire.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60120, quests[60120][7].log)
GrantItem(mySession, items.GELDWINS_GRIMOIRE, 1)
else
    npcDialogue = "Am I expected to believe that Sir Hanst would ask this temple to relinquish such a dangerous item?"
    diagOptions = { "Yes, Praetor. Sir Hanst must obtain the Grimoire, with urgency." }
end
  else
        npcDialogue =
"Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
    end
------
--Warrior(0) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "120") == "7") then
if (choice:find("Grimoire")) then
multiDialogue = { "Praetor Gunner: Hmm...",
    "Praetor Gunner: Well then, I shall have to test you. I can tell when someone is lying. Look me straight in the eyes and answer me..."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Where does Sir Hanst Breach reside?"
    diagOptions = { "Qeynos", "Highpass Hold" }
elseif (choice:find("Highpass")) then
multiDialogue = { "Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
 } 
elseif (choice:find("Qeynos")) then
    npcDialogue = "Very well then, Who does Sir Hanst serve?"
    diagOptions = { "Antonius Bayle II", "Advisor Amichevole" }
elseif (choice:find("Amichevole")) then
multiDialogue = { "Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
 } 
elseif (choice:find("Antonius")) then
multiDialogue = { "Praetor Gunner: Alright. I believe you playerName. Something about your eyes. Quite disarming I would say. Here is Geldwin's Grimoire.",
    "Praetor Gunner: I need not remind you of how dangerous this book could be if it fell into the wrong hands. Protect it with your life. Send Sir Hanst my best.",
"You have received a Geldwins Grimoire.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120, quests[120][7].log)
GrantItem(mySession, items.GELDWINS_GRIMOIRE, 1)
else
    npcDialogue = "Am I expected to believe that Sir Hanst would ask this temple to relinquish such a dangerous item?"
    diagOptions = { "Yes, Praetor. Sir Hanst must obtain the Grimoire, with urgency." }
end
  else
        npcDialogue =
"Praetor Gunner: Something is not right here. I don't know who you are, but maybe you should take a moment and regain your thoughts before I have you escorted out."
    end
------



SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end


