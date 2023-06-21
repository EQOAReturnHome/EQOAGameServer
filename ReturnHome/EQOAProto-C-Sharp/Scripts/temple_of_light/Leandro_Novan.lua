local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Magician(10) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "100120") == "6") then
if (choice:find("Geldwins")) then
multiDialogue = { "Leandro Novan: Ah yes, a letter from Sir Hanst. Hmm...A most unusual request, to be sure. The Grimoire doesn't usually leave the temple. Well then, we will have to speak to Praetor Gunner about this. Follow me please...",
"You have given away a Forged Letter."
}
ContinueQuest(mySession, 100120, quests[100120][6].log)
TurnInItem(mySession, items.FORGED_LETTER, 1)
else
    npcDialogue = "Strangers at the temple are not taken lightly. State your business."
    diagOptions = { "Sir Hanst is in need of Geldwins Grimoire. I shall deliver it." }
end
  else
        npcDialogue =
"Leandro Novan: Strangers at the temple are not taken lightly. While we welcome worship at this holy temple, there are many in the world who would desecrate this sacred space."
    end
------
--Bard(5) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "50120") == "7") then
if (choice:find("Geldwins")) then
multiDialogue = { "Leandro Novan: Ah yes, a letter from Sir Hanst. Hmm...A most unusual request, to be sure. The Grimoire doesn't usually leave the temple. Well then, we will have to speak to Praetor Gunner about this. Follow me please...",
"You have given away a Forged Letter."
}
ContinueQuest(mySession, 50120, quests[50120][7].log)
TurnInItem(mySession, items.FORGED_LETTER, 1)
else
    npcDialogue = "Strangers at the temple are not taken lightly. State your business."
    diagOptions = { "Sir Hanst is in need of Geldwins Grimoire. I shall deliver it." }
end
  else
        npcDialogue =
"Leandro Novan: Strangers at the temple are not taken lightly. While we welcome worship at this holy temple, there are many in the world who would desecrate this sacred space."
    end
------
--Cleric(9) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "90120") == "6") then
if (choice:find("Geldwins")) then
multiDialogue = { "Leandro Novan: Ah yes, a letter from Sir Hanst. Hmm...A most unusual request, to be sure. The Grimoire doesn't usually leave the temple. Well then, we will have to speak to Praetor Gunner about this. Follow me please...",
"You have given away a Forged Letter."
}
ContinueQuest(mySession, 90120, quests[90120][6].log)
TurnInItem(mySession, items.FORGED_LETTER, 1)
else
    npcDialogue = "Strangers at the temple are not taken lightly. State your business."
    diagOptions = { "Sir Hanst is in need of Geldwins Grimoire. I shall deliver it." }
end
  else
        npcDialogue =
"Leandro Novan: Strangers at the temple are not taken lightly. While we welcome worship at this holy temple, there are many in the world who would desecrate this sacred space."
    end
------
--Rogue(6) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "60120") == "6") then
if (choice:find("Geldwins")) then
multiDialogue = { "Leandro Novan: Ah yes, a letter from Sir Hanst. Hmm...A most unusual request, to be sure. The Grimoire doesn't usually leave the temple. Well then, we will have to speak to Praetor Gunner about this. Follow me please...",
"You have given away a Forged Letter."
}
ContinueQuest(mySession, 60120, quests[60120][6].log)
TurnInItem(mySession, items.FORGED_LETTER, 1)
else
    npcDialogue = "Strangers at the temple are not taken lightly. State your business."
    diagOptions = { "Sir Hanst is in need of Geldwins Grimoire. I shall deliver it." }
end
  else
        npcDialogue =
"Leandro Novan: Strangers at the temple are not taken lightly. While we welcome worship at this holy temple, there are many in the world who would desecrate this sacred space."
    end
------
--Warrior(0) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "120") == "6") then
if (choice:find("Geldwins")) then
multiDialogue = { "Leandro Novan: Ah yes, a letter from Sir Hanst. Hmm...A most unusual request, to be sure. The Grimoire doesn't usually leave the temple. Well then, we will have to speak to Praetor Gunner about this. Follow me please...",
"You have given away a Forged Letter."
}
ContinueQuest(mySession, 120, quests[120][6].log)
TurnInItem(mySession, items.FORGED_LETTER, 1)
else
    npcDialogue = "Strangers at the temple are not taken lightly. State your business."
    diagOptions = { "Sir Hanst is in need of Geldwins Grimoire. I shall deliver it." }
end
  else
        npcDialogue =
"Leandro Novan: Strangers at the temple are not taken lightly. While we welcome worship at this holy temple, there are many in the world who would desecrate this sacred space."
    end
------



SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end



