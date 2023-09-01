local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Magician(10) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "100113") == "2") then
if (CheckQuestItem(mySession, items.FINE_CHOCOLATES, 1))
 then
if (choice:find("just")) then
npcDialogue = "Delwin Stitchfinger: Hmm..."
elseif (choice:find("tending")) then
multiDialogue = { "Delwin Stitchfinger: Oh I see...yes this may take me a heartbeat to fix. The only problem is...",
    "Delwin Stitchfinger: I'm having a hankerin' somethin' fierce for some chocolate. I can't really focus without it..."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Would you be good enough to fetch me some chocolate?"
    diagOptions = { "This must be terrible for you. Sure...", "Sorry, I shouldn't be around chocolate..." }
elseif (choice:find("shouldn\'t")) then
multiDialogue = { "Delwin Stitchfinger: I know what you mean. It is a lavish delicacy, but one that I can't live without."
 } 
elseif (choice:find("terrible")) then
multiDialogue = { "Delwin Stitchfinger: Wonderful! Please go see Grocer Fritz and purchase some fine chocolate. I'll see about starting on this robe...",
"You have given away a damaged robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100113, quests[100113][2].log)
TurnInItem(mySession, items.DAMAGED_ROBE, 1)
else
    npcDialogue = "Well hello there, have you something I can mend?"
    diagOptions = { "I have a robe that needs tending...", "I am just here for the view." }
end
else
npcDialogue = "Delwin Stitchfinger: Hmm..."
end
elseif (GetPlayerFlags(mySession, "100113") == "3") then
if (CheckQuestItem(mySession, items.FINE_CHOCOLATES, 1))
 then
multiDialogue = { "Delwin Stitchfinger: Oh...Hello! Uh...err, is that the chocolate? I'll just take it, here...*NOM, NOM*",
    "Delwin Stitchfinger: Oh yes, Heavenly. Thank you. I am ok now. I believe I can now focus on this fancy robe . Please give me a little time and check back.",
    "You have finished a quest!",
"You have given away the fine chocolates.",
    "You have received a quest!"
}
ContinueQuest(mySession, 100113, quests[100113][3].log)
TurnInItem(mySession, items.FINE_CHOCOLATES, 1)
else
npcDialogue = "Delwin Stitchfinger: Hmm... Go see Grocer Fritz, purchase some fine chocolate and return to me."
end
elseif (GetPlayerFlags(mySession, "100113") == "4") then
multiDialogue = { "Delwin Stitchfinger: Well then, I'm all finished here. Goodness, looks like I've smeared a wee bit of chocolate on the robe. Not to worry, a quick wipe down and...",
    "Delwin Stitchfinger: ...Should be good as new. Oh dear, where are my glasses? Anyway, you best be off to return that fancy robe to it's master.",
"You have received a chocolate stained robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100113, quests[100113][4].log)
GrantItem(mySession, items.CHOCOLATE_STAINED_ROBE, 1)
elseif (GetPlayerFlags(mySession, "100113") == "6") then
if (choice:find("nevermind")) then
multiDialogue = { "Delwin Stitchfinger: Hmm... You sure do seem nervous. Something you want to chat about?"
 } 
elseif (choice:find("chocolates")) then
multiDialogue = { "Delwin Stitchfinger: Oh my stars, what a delectable selection. They look irresistible. Tell Ilenar that I was happy to be of service.",
    "Delwin Stitchfinger: I simply must try one... lets see...*NOM NOM NOM*. Oh so good, I must have another...*NOM NOM* I have never had a....*COUGH* *GASP* *GAG*",
    "You have finished a quest!",
    "You have received a quest!",
"You have given away a special box of chocolates."
}
ContinueQuest(mySession, 100113, quests[100113][6].log)
TurnInItem(mySession, items.SPECIAL_BOX_OF_CHOCOLATES, 1)
else
    npcDialogue = "Well hello there playerName, have you something I can mend?"
    diagOptions = { "Ilenar would like to thank you with these chocolates...", "Uh...err, nevermind." }
end
  else
        npcDialogue =
"Delwin Stitchfinger: Hello! We like to take care of fancy linens and robes for the elite and prestigious here. There might not be much for you, I suspect."
    end
------
--Bard(5) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "50113") == "2") then
if (CheckQuestItem(mySession, items.FINE_CHOCOLATES, 1))
 then
if (choice:find("just")) then
npcDialogue = "Delwin Stitchfinger: Hmm..."
elseif (choice:find("tending")) then
multiDialogue = { "Delwin Stitchfinger: Oh I see...yes this may take me a heartbeat to fix. The only problem is...",
    "Delwin Stitchfinger: I'm having a hankerin' somethin' fierce for some chocolate. I can't really focus without it..."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Would you be good enough to fetch me some chocolate?"
    diagOptions = { "This must be terrible for you. Sure...", "Sorry, I shouldn't be around chocolate..." }
elseif (choice:find("shouldn\'t")) then
multiDialogue = { "Delwin Stitchfinger: I know what you mean. It is a lavish delicacy, but one that I can't live without."
 } 
elseif (choice:find("terrible")) then
multiDialogue = { "Delwin Stitchfinger: Wonderful! Please go see Grocer Fritz and purchase some fine chocolate. I'll see about starting on this robe...",
"You have given away a damaged robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50113, quests[50113][2].log)
TurnInItem(mySession, items.DAMAGED_ROBE, 1)
else
    npcDialogue = "Greetins, what brings ye to these parts?"
    diagOptions = { "I have a robe that needs tending...", "I am just here for the view." }
end
else
npcDialogue = "Delwin Stitchfinger: Hmm..."
end
elseif (GetPlayerFlags(mySession, "50113") == "3") then
if (CheckQuestItem(mySession, items.FINE_CHOCOLATES, 1))
 then
multiDialogue = { "Delwin Stitchfinger: Oh...Hello! Uh...err, is that the chocolate? I'll just take it, here...*NOM, NOM*",
    "Delwin Stitchfinger: Oh yes, Heavenly. Thank you. I am ok now. I believe I can now focus on this fancy robe . Please give me a little time and check back.",
    "You have finished a quest!",
"You have given away the fine chocolates.",
    "You have received a quest!"
}
ContinueQuest(mySession, 50113, quests[50113][3].log)
TurnInItem(mySession, items.FINE_CHOCOLATES, 1)
else
npcDialogue = "Delwin Stitchfinger: Hmm... Go see Grocer Fritz, purchase some fine chocolate and return to me."
end
elseif (GetPlayerFlags(mySession, "50113") == "4") then
multiDialogue = { "Delwin Stitchfinger: Well then, I'm all finished here. Goodness, looks like I've smeared a wee bit of chocolate on the robe. Not to worry, a quick wipe down and...",
    "Delwin Stitchfinger: ...Should be good as new. Oh dear, where are my glasses? Anyway, you best be off to return that fancy robe to it's master.",
"You have received a chocolate stained robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50113, quests[50113][4].log)
GrantItem(mySession, items.CHOCOLATE_STAINED_ROBE, 1)
elseif (GetPlayerFlags(mySession, "50113") == "7") then
if (choice:find("nevermind")) then
multiDialogue = { "Delwin Stitchfinger: Hmm... You sure do seem nervous. Something you want to chat about?"
 } 
elseif (choice:find("chocolates")) then
multiDialogue = { "Delwin Stitchfinger: Oh my stars, what a delectable selection. They look irresistible. Tell Ilenar that I was happy to be of service.",
    "Delwin Stitchfinger: I simply must try one... lets see...*NOM NOM NOM*. Oh so good, I must have another...*NOM NOM* I have never had a....*COUGH* *GASP* *GAG*",
    "You have finished a quest!",
    "You have received a quest!",
"You have given away a special box of chocolates."
}
ContinueQuest(mySession, 50113, quests[50113][7].log)
TurnInItem(mySession, items.SPECIAL_BOX_OF_CHOCOLATES, 1)
else
    npcDialogue = "Well hello there playerName, have you something I can mend?"
    diagOptions = { "Ilenar would like to thank you with these chocolates...", "Uh...err, nevermind." }
end
  else
        npcDialogue =
"Delwin Stitchfinger: Hello! We like to take care of fancy linens and robes for the elite and prestigious here. There might not be much for you, I suspect."
    end
------
--Cleric(9) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "90113") == "2") then
if (CheckQuestItem(mySession, items.FINE_CHOCOLATES, 1))
 then
if (choice:find("just")) then
npcDialogue = "Delwin Stitchfinger: Hmm..."
elseif (choice:find("tending")) then
multiDialogue = { "Delwin Stitchfinger: Oh I see...yes this may take me a heartbeat to fix. The only problem is...",
    "Delwin Stitchfinger: I'm having a hankerin' somethin' fierce for some chocolate. I can't really focus without it..."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Would you be good enough to fetch me some chocolate?"
    diagOptions = { "This must be terrible for you. Sure...", "Sorry, I shouldn't be around chocolate..." }
elseif (choice:find("shouldn\'t")) then
multiDialogue = { "Delwin Stitchfinger: I know what you mean. It is a lavish delicacy, but one that I can't live without."
 } 
elseif (choice:find("terrible")) then
multiDialogue = { "Delwin Stitchfinger: Wonderful! Please go see Grocer Fritz and purchase some fine chocolate. I'll see about starting on this robe...",
"You have given away a damaged robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90113, quests[90113][2].log)
TurnInItem(mySession, items.DAMAGED_ROBE, 1)
else
    npcDialogue = "Well hello there, have you something I can mend?"
    diagOptions = { "I have a robe that needs tending...", "I am just here for the view." }
end
else
npcDialogue = "Delwin Stitchfinger: Hmm..."
end
elseif (GetPlayerFlags(mySession, "90113") == "3") then
if (CheckQuestItem(mySession, items.FINE_CHOCOLATES, 1))
 then
multiDialogue = { "Delwin Stitchfinger: Oh...Hello! Uh...err, is that the chocolate? I'll just take it, here...*NOM, NOM*",
    "Delwin Stitchfinger: Oh yes, Heavenly. Thank you. I am ok now. I believe I can now focus on this fancy robe . Please give me a little time and check back.",
    "You have finished a quest!",
"You have given away the fine chocolates.",
    "You have received a quest!"
}
ContinueQuest(mySession, 90113, quests[90113][3].log)
TurnInItem(mySession, items.FINE_CHOCOLATES, 1)
else
npcDialogue = "Delwin Stitchfinger: Hmm... Go see Grocer Fritz, purchase some fine chocolate and return to me."
end
elseif (GetPlayerFlags(mySession, "90113") == "4") then
multiDialogue = { "Delwin Stitchfinger: Well then, I'm all finished here. Goodness, looks like I've smeared a wee bit of chocolate on the robe. Not to worry, a quick wipe down and...",
    "Delwin Stitchfinger: ...Should be good as new. Oh dear, where are my glasses? Anyway, you best be off to return that fancy robe to it's master.",
"You have received a chocolate stained robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90113, quests[90113][4].log)
GrantItem(mySession, items.CHOCOLATE_STAINED_ROBE, 1)
elseif (GetPlayerFlags(mySession, "90113") == "6") then
if (choice:find("nevermind")) then
multiDialogue = { "Delwin Stitchfinger: Hmm... You sure do seem nervous. Something you want to chat about?"
 } 
elseif (choice:find("chocolates")) then
multiDialogue = { "Delwin Stitchfinger: Oh my stars, what a delectable selection. They look irresistible. Tell Ilenar that I was happy to be of service.",
    "Delwin Stitchfinger: I simply must try one... lets see...*NOM NOM NOM*. Oh so good, I must have another...*NOM NOM* I have never had a....*COUGH* *GASP* *GAG*",
    "You have finished a quest!",
    "You have received a quest!",
"You have given away a special box of chocolates."
}
ContinueQuest(mySession, 90113, quests[90113][6].log)
TurnInItem(mySession, items.SPECIAL_BOX_OF_CHOCOLATES, 1)
else
    npcDialogue = "Well hello there playerName, have you something I can mend?"
    diagOptions = { "Ilenar would like to thank you with these chocolates...", "Uh...err, nevermind." }
end
  else
        npcDialogue =
"Delwin Stitchfinger: Hello! We like to take care of fancy linens and robes for the elite and prestigious here. There might not be much for you, I suspect."
    end
------
--Rogue(6) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "60113") == "2") then
if (CheckQuestItem(mySession, items.FINE_CHOCOLATES, 1))
 then
if (choice:find("just")) then
npcDialogue = "Delwin Stitchfinger: Hmm..."
elseif (choice:find("tending")) then
multiDialogue = { "Delwin Stitchfinger: Oh I see...yes this may take me a heartbeat to fix. The only problem is...",
    "Delwin Stitchfinger: I'm having a hankerin' somethin' fierce for some chocolate. I can't really focus without it..."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Would you be good enough to fetch me some chocolate?"
    diagOptions = { "This must be terrible for you. Sure...", "Sorry, I shouldn't be around chocolate..." }
elseif (choice:find("shouldn\'t")) then
multiDialogue = { "Delwin Stitchfinger: I know what you mean. It is a lavish delicacy, but one that I can't live without."
 } 
elseif (choice:find("terrible")) then
multiDialogue = { "Delwin Stitchfinger: Wonderful! Please go see Grocer Fritz and purchase some fine chocolate. I'll see about starting on this robe...",
"You have given away a damaged robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60113, quests[60113][2].log)
TurnInItem(mySession, items.DAMAGED_ROBE, 1)
else
    npcDialogue = "Well hello there, have you something I can mend?"
    diagOptions = { "I have a robe that needs tending...", "I am just here for the view." }
end
else
npcDialogue = "Delwin Stitchfinger: Hmm..."
end
elseif (GetPlayerFlags(mySession, "60113") == "3") then
if (CheckQuestItem(mySession, items.FINE_CHOCOLATES, 1))
 then
multiDialogue = { "Delwin Stitchfinger: Oh...Hello! Uh...err, is that the chocolate? I'll just take it, here...*NOM, NOM*",
    "Delwin Stitchfinger: Oh yes, Heavenly. Thank you. I am ok now. I believe I can now focus on this fancy robe . Please give me a little time and check back.",
    "You have finished a quest!",
"You have given away the fine chocolates.",
    "You have received a quest!"
}
ContinueQuest(mySession, 60113, quests[60113][3].log)
TurnInItem(mySession, items.FINE_CHOCOLATES, 1)
else
npcDialogue = "Delwin Stitchfinger: Hmm... Go see Grocer Fritz, purchase some fine chocolate and return to me."
end
elseif (GetPlayerFlags(mySession, "60113") == "4") then
multiDialogue = { "Delwin Stitchfinger: Well then, I'm all finished here. Goodness, looks like I've smeared a wee bit of chocolate on the robe. Not to worry, a quick wipe down and...",
    "Delwin Stitchfinger: ...Should be good as new. Oh dear, where are my glasses? Anyway, you best be off to return that fancy robe to it's master.",
"You have received a chocolate stained robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60113, quests[60113][4].log)
GrantItem(mySession, items.CHOCOLATE_STAINED_ROBE, 1)
elseif (GetPlayerFlags(mySession, "60113") == "6") then
if (choice:find("nevermind")) then
multiDialogue = { "Delwin Stitchfinger: Hmm... You sure do seem nervous. Something you want to chat about?"
 } 
elseif (choice:find("chocolates")) then
multiDialogue = { "Delwin Stitchfinger: Oh my stars, what a delectable selection. They look irresistible. Tell Ilenar that I was happy to be of service.",
    "Delwin Stitchfinger: I simply must try one... lets see...*NOM NOM NOM*. Oh so good, I must have another...*NOM NOM* I have never had a....*COUGH* *GASP* *GAG*",
    "You have finished a quest!",
    "You have received a quest!",
"You have given away a special box of chocolates."
}
ContinueQuest(mySession, 60113, quests[60113][6].log)
TurnInItem(mySession, items.SPECIAL_BOX_OF_CHOCOLATES, 1)
else
    npcDialogue = "Well hello there playerName, have you something I can mend?"
    diagOptions = { "Ilenar would like to thank you with these chocolates...", "Uh...err, nevermind." }
end
  else
        npcDialogue =
"Delwin Stitchfinger: Hello! We like to take care of fancy linens and robes for the elite and prestigious here. There might not be much for you, I suspect."
    end
------
--Warrior(0) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "113") == "2") then
if (CheckQuestItem(mySession, items.FINE_CHOCOLATES, 1))
 then
if (choice:find("just")) then
npcDialogue = "Delwin Stitchfinger: Hmm..."
elseif (choice:find("tending")) then
multiDialogue = { "Delwin Stitchfinger: Oh I see...yes this may take me a heartbeat to fix. The only problem is...",
    "Delwin Stitchfinger: I'm having a hankerin' somethin' fierce for some chocolate. I can't really focus without it..."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Would you be good enough to fetch me some chocolate?"
    diagOptions = { "This must be terrible for you. Sure...", "Sorry, I shouldn't be around chocolate..." }
elseif (choice:find("shouldn\'t")) then
multiDialogue = { "Delwin Stitchfinger: I know what you mean. It is a lavish delicacy, but one that I can't live without."
 } 
elseif (choice:find("terrible")) then
multiDialogue = { "Delwin Stitchfinger: Wonderful! Please go see Grocer Fritz and purchase some fine chocolate. I'll see about starting on this robe...",
"You have given away a damaged robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 113, quests[113][2].log)
TurnInItem(mySession, items.DAMAGED_ROBE, 1)
else
    npcDialogue = "Well hello there, have you something I can mend?"
    diagOptions = { "I have a robe that needs tending...", "I am just here for the view." }
end
else
npcDialogue = "Delwin Stitchfinger: Hmm..."
end
elseif (GetPlayerFlags(mySession, "113") == "3") then
if (CheckQuestItem(mySession, items.FINE_CHOCOLATES, 1))
 then
multiDialogue = { "Delwin Stitchfinger: Oh...Hello! Uh...err, is that the chocolate? I'll just take it, here...*NOM, NOM*",
    "Delwin Stitchfinger: Oh yes, Heavenly. Thank you. I am ok now. I believe I can now focus on this fancy robe . Please give me a little time and check back.",
    "You have finished a quest!",
"You have given away the fine chocolates.",
    "You have received a quest!"
}
ContinueQuest(mySession, 113, quests[113][3].log)
TurnInItem(mySession, items.FINE_CHOCOLATES, 1)
else
npcDialogue = "Delwin Stitchfinger: Hmm... Go see Grocer Fritz, purchase some fine chocolate and return to me."
end
elseif (GetPlayerFlags(mySession, "113") == "4") then
multiDialogue = { "Delwin Stitchfinger: Well then, I'm all finished here. Goodness, looks like I've smeared a wee bit of chocolate on the robe. Not to worry, a quick wipe down and...",
    "Delwin Stitchfinger: ...Should be good as new. Oh dear, where are my glasses? Anyway, you best be off to return that fancy robe to it's master.",
"You have received a chocolate stained robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 113, quests[113][4].log)
GrantItem(mySession, items.CHOCOLATE_STAINED_ROBE, 1)
elseif (GetPlayerFlags(mySession, "113") == "6") then
if (choice:find("nevermind")) then
multiDialogue = { "Delwin Stitchfinger: Hmm... You sure do seem nervous. Something you want to chat about?"
 } 
elseif (choice:find("chocolates")) then
multiDialogue = { "Delwin Stitchfinger: Oh my stars, what a delectable selection. They look irresistible. Tell Ilenar that I was happy to be of service.",
    "Delwin Stitchfinger: I simply must try one... lets see...*NOM NOM NOM*. Oh so good, I must have another...*NOM NOM* I have never had a....*COUGH* *GASP* *GAG*",
    "You have finished a quest!",
    "You have received a quest!",
"You have given away a special box of chocolates."
}
ContinueQuest(mySession, 113, quests[113][6].log)
TurnInItem(mySession, items.SPECIAL_BOX_OF_CHOCOLATES, 1)
else
    npcDialogue = "Well hello there playerName, have you something I can mend?"
    diagOptions = { "Ilenar would like to thank you with these chocolates...", "Uh...err, nevermind." }
end
  else
        npcDialogue =
"Delwin Stitchfinger: Hello! We like to take care of fancy linens and robes for the elite and prestigious here. There might not be much for you, I suspect."
    end
------




SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end


