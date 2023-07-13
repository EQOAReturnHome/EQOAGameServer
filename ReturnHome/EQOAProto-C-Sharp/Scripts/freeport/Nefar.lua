local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Wizard(13) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "130103") == "0") then
if (choice:find("Sivrendesh")) then
multiDialogue = { "Nefar: Ah yes, fresh talent. It is my pleasure to see what you are made of. But I suppose you'll need some training first.",
    "Nefar: Equip whatever pathetic weapon you may have and slay snakes. Bring me 2 ruined snake scales as proof of your deed.",
    "Nefar: After I receive the scales, I will reward you with the Arcane Bindings Scroll, a wizard's specialty.",
    "Nefar: Run along now. I'll be watching you with idle curiosity.",
    "You have received a quest!"
}
StartQuest(mySession, 130103, quests[130103][0].log)
else
    npcDialogue = "Hello there young one."
    diagOptions = { "Sivrendesh sent me to you." }
end
elseif (GetPlayerFlags(mySession, "130103") == "1") then
if (CheckQuestItem(mySession, items.RUINED_SNAKE_SCALES, 2))
 then
if (choice:find("nevermind")) then
npcDialogue = "Nefar: It's important that you bring me what I have asked from you. I need two ruined snake scales."
elseif (choice:find("ruined")) then
multiDialogue = { "Nefar: I find your work to be quite satisfactory. Maybe I will instruct you a bit longer.",
    "Nefar: As I said, you will be rewarded. Take this scroll and study it well. The skill is trivial compared to my magic, but it's a good start for you, playerName.",
    "You have finished a quest!",
"You have given away ruined snake scales.",
"You have given away ruined snake scales.",
"You have received the Arcane Bindings Scroll.",
"Nefar: I'm just putting the finishing touches on a new lightning spell. Check back with me in few moments little one."
}
CompleteQuest(mySession, 130103, quests[130103][1].xp, 130104 )
TurnInItem(mySession, items.RUINED_SNAKE_SCALES, 2)
GrantItem(mySession, items.ARCANE_BINDINGS, 1)
else
    npcDialogue = "Ah, my little friend has returned."
    diagOptions = { "I have the ruined snake scales.", "Oh, excuse me, nevermind." }
end
else
npcDialogue = "Nefar: It's important that you bring me what I have asked from you. I need two ruined snake scales."
end
elseif (GetPlayerFlags(mySession, "130104") == "0") then
if (level >=4) then
if (choice:find("knowledge")) then
multiDialogue = { "Nefar: In order to continue your studies, you must at least wear a fancy Red Robe if you wish to call yourself an wizard.",
    "Nefar: Fortunately, I have one ready for you, but you must first aid in gathering the supplies to craft the next one.",
"Nefar: What I require from you is an plain robe, a silk cord, and ant leg segments.",
"Nefar: You will need to purchase the plain robe from Merchant Yulia.",
"Nefar: Merchant Yesam also has silk cords for sale. I will need one. He is near the blacksmith at the north side of Freeport.",
"Nefar: I'll also need a ant leg segments. Perhaps the guards wouldn't mind if you collected the segments from the ants outside.",
"Nefar: Now then, don't come back until you have everything in proper order. We wouldn't want you to be embarrassed in front of the other wizards, young one.",
    "You have received a quest!"
}
StartQuest(mySession, 130104, quests[130104][0].log)
else
    npcDialogue = "My little magic user has returned."
    diagOptions = { "Yes, I seek more knowledge." }
end
else
npcDialogue ="Nefar: I have to take care of something, but please check back with me soon."
end
elseif (GetPlayerFlags(mySession, "130104") == "1") then
if (CheckQuestItem(mySession, items.PLAIN_ROBE, 1)
and CheckQuestItem(mySession, items.SILK_CORD, 1)
and CheckQuestItem(mySession, items.ANT_LEG_SEGMENTS, 1))
 then
multiDialogue = { "Nefar: I am quite pleased with your work. Such diligence, and enthusiasm. The last student couldn't quite handle it...",
    "Nefar: And here is that fancy Red Robe you have most certainly earned. I'm sure it will suit you well.",
    "Nefar: What was your name again? Oh yes... playerName. I'll keep you in mind for my future needs.",
"You have given away a plain robe.",
"You have given away a silk cord.",
"You have given away the ant leg segments.",
"You have received a Red Robe.",
    "You have finished a quest!",
"Nefar: You will find as you use your weapons and armor in battle, they will begin to wear down and lose their effectiveness.",
"Nefar: If you visit a blacksmith they will repair your weapons and armor for a price.",
"Nefar: You can find Blacksmith Coalbrick at the Bazaar on the north side of the city.",
"Nefar: I'll have a new task for you shortly. Check back when you are properly equipped."
}
CompleteQuest(mySession, 130104, quests[130104][1].xp, 130105 )
TurnInItem(mySession, items.PLAIN_ROBE, 1)
TurnInItem(mySession, items.SILK_CORD, 1)
TurnInItem(mySession, items.ANT_LEG_SEGMENTS, 1)
GrantItem(mySession, items.RED_ROBE, 1)
else
npcDialogue = "Nefar: You'll need to prove yourself by gathering these items. I need 1 plain robe, 1 silk cord, and 1 ant leg segments."
end
elseif (GetPlayerFlags(mySession, "130105") == "0") then
if (level >=5) then
if (choice:find("ready")) then
multiDialogue = { "Nefar: This next task is so deadly, I shutter to send a poor flower such as yourself to your doom. But a task is a task...",
    "Nefar: The Academy of Arcane Science depends on caravan shipments from the west for a great deal of supplies to operate.",
    "Nefar: Just recently, a band of highwaymen have been hijacking caravans into Freeport in both the deserts and grasslands.",
    "Nefar: The roads are plagued with highwaymen. This cannot be allowed to happen."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Will you use the skills we have taught you thus far to recover the stolen goods and bring these criminals to justice?"
    diagOptions = { "I will, master.", "I don't know if I can do this." }
elseif (choice:find("don\'t")) then
multiDialogue = { "Nefar: You must not let fear dictate your path. Instead, take control by facing it head on. This is the only way you can ever become a true master of magic."
 } 
elseif (choice:find("master")) then
multiDialogue = { "Nefar: We would be in your debt. However this is no challenge to take lightly or alone.",
    "Nefar: For this task you should seek the company of other adventurers in Freeport. The Smiling Serpent tavern is a good place to start.",
    "Nefar: I am sure that we are not the only guild being made to suffer by these criminals, and that there are others who will help.",
    "Nefar: Go now playerName, and return when you've recovered the stolen goods.",
    "You have received a quest!"
}
StartQuest(mySession, 130105, quests[130105][0].log)
else
    npcDialogue = "My little magic user has returned."
    diagOptions = { "I am ready for my next task" }
end
else
npcDialogue ="Nefar: I have something for you, but you aren't yet ready. Go slay a few critters and check back with me."
end
elseif (GetPlayerFlags(mySession, "130105") == "1") then
if (CheckQuestItem(mySession, items.STOLEN_GOODS, 1))
 then
if (choice:find("quite")) then
npcDialogue = "Nefar: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
elseif (choice:find("have")) then
multiDialogue = { "Nefar: I am...quite surprised that you made it. To be honest, I wasn't sure if I would ever see you again but here you are proving yourself ever more valuable...",
    "Nefar: It is quite thrilling watching you grow in power.",
    "Nefar: For this, I am proud to have you as a true Wizard of The Academy of Arcane Science. I award you with this Shock of Frost Scroll.",
    "Nefar: I'll be watching you from a distance for now, but I believe Sivrendesh may need your assistance soon.",
"You have given away the stolen goods.",
"You have received a Shock of Frost Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 130105, quests[130105][1].xp, 130107 )
TurnInItem(mySession, items.STOLEN_GOODS, 1)
GrantItem(mySession, items.SHOCK_OF_FROST, 1)
else
    npcDialogue = "Have you yet retrieved the stolen goods?"
    diagOptions = { "I have. Here they are...", "Not quite yet." }
end
else
npcDialogue = "Nefar: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
end
  else
        npcDialogue =
"Nefar: Hello playerName. I'm not sure why you would come back here to visit me in this corner of the academy, but I am thrilled to be sure. I do enjoy conversation, especially discussion of arcane wizardry."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

