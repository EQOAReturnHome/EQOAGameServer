local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Rogue(6) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "60103") == "0") then
if (choice:find("Necorik")) then
multiDialogue = { "Athera: Ah, of course he did. Well I suppose I must instruct you. But first you must prove your worth.",
    "Athera: Equip whatever mighty weapon you may have and slay snakes. Bring me 2 ruined snake scales as proof of your accomplishments.",
    "Athera: After I receive the scales, I will reward you with a Sneak Scroll, a rogue's specialty.",
    "Athera: Make haste, and choose your enemies wisely.",
    "You have received a quest!"
}
StartQuest(mySession, 60103, quests[60103][0].log)
else
    npcDialogue = "Be quick, I don't really have time for this."
    diagOptions = { "Necorik the Ghost sent me to you." }
end
elseif (GetPlayerFlags(mySession, "60103") == "1") then
if (CheckQuestItem(mySession, items.RUINED_SNAKE_SCALE, 2))
 then
if (choice:find("Yeah")) then
npcDialogue = "Athera: It's important that you bring me what I have asked from you. I need two ruined snake scales."
elseif (choice:find("scales")) then
multiDialogue = { "Athera: I see that you do well in following instruction. Perhaps there is hope for you.",
    "Athera: As I said, you will be rewarded. Take this scroll and study it well. The skill is trivial compared to my abilities, but it's a good start for you, playerName.",
    "You have finished a quest!",
"You have given away a ruined snake scale.",
"You have given away a ruined snake scale.",
"You have received a Sneak Scroll.",
"Athera: Come back to me in a bit. You didn't think you were my only recruit, did you? I'll have more work for you, I'm sure."
}
CompleteQuest(mySession, 60103, quests[60103][1].xp, 60104 )
TurnInItem(mySession, items.RUINED_SNAKE_SCALE, 2)
GrantItem(mySession, items.SNEAK, 1)
else
    npcDialogue = "Do you have what I asked for?"
    diagOptions = { "Yes, I have the scales.", "Yeah, but I want to keep them." }
end
else
npcDialogue = "Athera: It's important that you bring me what I have asked from you. I need two ruined snake scales."
end
elseif (GetPlayerFlags(mySession, "60104") == "0") then
if (level >=4) then
if (choice:find("continue")) then
multiDialogue = { "Athera: You must at least wield a Tiger Dirk first if you wish to call yourself a rogue.",
    "Athera: Fortunately, I have one ready for you, but you must first aid in gathering the supplies to build the next one.",
    "Athera: These things must be acquired with your own blood, sweat and tears, I'm certainly not giving it to you for free.",
"Athera: The supplies I require are an iron ore, a ivory, and an beetle leg segment.",
"Athera: Purchase iron ore from Merchant Shohan near the blacksmith at the north side of Freeport.",
"Athera: Merchant Yesam also has ivory's for sale. I will need one.",
"Athera: I'll also need an beetle leg segment. I'm sure the guards wouldn't mind if you collected the segment from the pests outside.",
"Athera: Now off with you, and don't come back until you have everything in proper order.",
    "You have received a quest!"
}
StartQuest(mySession, 60104, quests[60104][0].log)
else
    npcDialogue = "Oh...you again."
    diagOptions = { "I need to continue my training." }
end
else
npcDialogue ="Athera: I have to take care of something, however you should check back with me soon."
end
elseif (GetPlayerFlags(mySession, "60104") == "1") then
if (CheckQuestItem(mySession, items.IRON_ORE, 1)
and CheckQuestItem(mySession, items.IVORY, 1)
and CheckQuestItem(mySession, items.BEETLE_LEG_SEGMENT, 1))
 then
multiDialogue = { "Athera: Looks like you are one of the eager ones. I guess you have earned this dirk. Wield it well.",
"You have given away an iron ore.",
"You have given away an ivory.",
"You have given away a beetle leg segment.",
"You have received a Tiger Dirk.",
    "You have finished a quest!",
"Athera: You will find as you use your weapons and armor in battle, they will begin to wear down and lose their effectiveness.",
"Athera: If you visit a blacksmith they will repair your weapons and armor for a price.",
"Athera: You can find Blacksmith Coalbrick at the Bazaar on the north side of the city.",
"Athera: I suppose you may just be strong enough to help with a task given to me. Get some rest, and return when you're ready."
}
CompleteQuest(mySession, 60104, quests[60104][1].xp, 60105 )
TurnInItem(mySession, items.IRON_ORE, 1)
TurnInItem(mySession, items.IVORY, 1)
TurnInItem(mySession, items.BEETLE_LEG_SEGMENT, 1)
GrantItem(mySession, items.TIGER_DIRK, 1)
else
npcDialogue = "Athera: You'll need to prove yourself by gathering these items. I need 1 iron ore, 1 ivory, and 1 beetle leg segment."
end
elseif (GetPlayerFlags(mySession, "60105") == "0") then
if (level >=5) then
if (choice:find("am")) then
multiDialogue = { "Athera: The Shining Shield Mercenaries depends on caravan shipments from the west for a great deal of supplies to operate.",
    "Athera: Just recently, a band of highwaymen have been hijacking caravans into Freeport in both the deserts and grasslands.",
    "Athera: Most importantly, I have a long standing contract with the Iron Coffer to protect the caravan routes."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Will you use the skills you have learned so far to hunt down and kill those highwaymen, and return the stolen goods to me?"
    diagOptions = { "I will, Athera.", "Sorry, I can't help you with this." }
elseif (choice:find("can\'t")) then
multiDialogue = { "Athera: Do you really think you can just prance around and pick and choose an activity? You really don't want to get on my bad side. I can have you stripped of everything you have, in a heartbeat. Now, try again."
 } 
elseif (choice:find("Athera")) then
multiDialogue = { "Athera: The Shining Shield Mercenaries will be indebted to you.",
    "Athera: For this you should seek the company of other adventurers in Freeport. The Smiling Serpent tavern is a good place to start.",
    "Athera: I am sure that we are not the only guild being made to suffer by these criminals, and that there are others who will help.",
    "Athera: Go now playerName, and return when you've recovered the stolen goods.",
    "You have received a quest!"
}
StartQuest(mySession, 60105, quests[60105][0].log)
else
    npcDialogue = "I have more work for you. Are you ready?"
    diagOptions = { "I am." }
end
else
npcDialogue ="Athera: I have something for you, but you aren't yet ready. Go slay a few critters and check back with me."
end
elseif (GetPlayerFlags(mySession, "60105") == "1") then
if (CheckQuestItem(mySession, items.STOLEN_GOODS, 1))
 then
if (choice:find("quite")) then
npcDialogue = "Athera: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
elseif (choice:find("have")) then
multiDialogue = { "Athera: Unbelievable... To be honest, I wasn't sure if I would ever see you again, but here you are proving yourself more useful...",
    "Athera: I suppose... I owe you a debt. I'm not sure what I would have done without these supplies.",
    "Athera: For this, I am proud to have you as a true Rogue of The Shining Shield Mercenaries. I award you with this Quick Blade Scroll.",
    "Athera: I will certainly have more work for you, but I need some time to look into a few things. Come see me again later, playerName.",
"You have given away the stolen goods.",
"You have received a Quick Blade Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 60105, quests[60105][1].xp, 60107 )
TurnInItem(mySession, items.STOLEN_GOODS, 1)
GrantItem(mySession, items.QUICK_BLADE, 1)
else
    npcDialogue = "Have you yet retrieved the stolen goods?"
    diagOptions = { "I have. Here they are...", "Not quite yet." }
end
else
npcDialogue = "Athera: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
end
elseif (GetPlayerFlags(mySession, "60107") == "0") then
if (level >=7) then
if (choice:find("really")) then
multiDialogue = { "Athera: Oh, well perhaps it's best you just move along before I show you the business end of my blade. Seriously, Don't talk to me."
 } 
elseif (choice:find("Lets")) then
multiDialogue = { "Athera: Well then, you've returned not a moment too soon. I have a task that needs attention but I am preoccupied at the moment.",
    "Athera: I've just received word of a dangerous creature spotted near freeport. The Chichan is a poisonous eel that preys upon unwitting travelers.",
    "Athera: The thing is, the eels are not native of this region. Someone must have brought it to freeport intentionally.",
    "Athera: I'll need a piece of the eel to investigate further. Bring to me it's venom sac. You will find it north of Freeport, along the river. Hurry along now, playerName.",
    "You have received a quest!"
}
StartQuest(mySession, 60107, quests[60107][0].log)
else
    npcDialogue = "I have more work for you. Are you ready?"
    diagOptions = { "Lets do this.", "Not really. I'm kinda tired." }
end
else
npcDialogue ="Athera: I need a little more time to finish a spell I am working on. Come back a little later."
end
elseif (GetPlayerFlags(mySession, "60107") == "1") then
if (CheckQuestItem(mySession, items.EEL_VENOM_SAC, 1))
 then
if (choice:find("Perhaps")) then
npcDialogue = "Athera: Stay focused. Bring me a Chichan Eel venom sac. You will find it north of Freeport, along the river."
elseif (choice:find("right")) then
multiDialogue = { "Athera: Well done. I hate to say it, but I admit that you are pretty skilled.",
    "Athera: I must investigate this venom sac immediately.",
    "Athera: As for your reward, take this Acrobatics Scroll and these Shining Protectors.",
    "Athera: You have served us well enough for today. Come see me later, playerName. I will have more for you.",
"You have given away an eel venom sac.",
"You have received an Acrobatics Scroll.",
"You have received Shining Protectors.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 60107, quests[60107][1].xp, 60110 )
TurnInItem(mySession, items.EEL_VENOM_SAC, 1)
GrantItem(mySession, items.ACROBATICS, 1)
GrantItem(mySession, items.SHINING_PROTECTORS, 1)
else
    npcDialogue = "Do you have the venom sac?"
    diagOptions = { "It is right here.", "Perhaps not. I'll get to it later." }
end
else
npcDialogue = "Athera: Stay focused. Bring me a Chichan Eel venom sac. You will find it north of Freeport, along the river."
end
elseif (GetPlayerFlags(mySession, "60110") == "0") then
if (level >=10) then
if (choice:find("Maybe")) then
multiDialogue = { "Athera: Just what is it you do in your free time that causes you to be so empty minded when attending your master? Take a hard look at yourself or this is going to sour between us quite hastily."
 } 
elseif (choice:find("Athera")) then
multiDialogue = { "Athera: I must now have you help me with this next task.",
    "Athera: Guard Sareken is in need of assistance. Please find him at the north entrance of Freeport.",
    "Athera: I wouldn't normally bother with this, but I happen to own him a favor. He looked the other way once when I was conducting some...lets say, \"unsavory\" business.",
    "You have received a quest!"
}
StartQuest(mySession, 60110, quests[60110][0].log)
else
    npcDialogue = "playerName, please tell me you are here to help..."
    diagOptions = { "I am, Athera.", "Maybe later..." }
end
else
npcDialogue ="Athera: You'll need to develop your skills a little further before you can handle what I have in store for you."
end
elseif (GetPlayerFlags(mySession, "60113") == "0") then
if (level >=13) then
if (choice:find("perhaps")) then
multiDialogue = { "Athera: Just what is it you do in your free time that causes you to be so empty minded when attending your master? Take a hard look at yourself or this is going to sour between us quite hastily."
 } 
elseif (choice:find("Athera")) then
multiDialogue = { "Athera: The wizard Ilenar Crelwin has come from a long distance on business. He is a colleague of mine, but to be honest, I cant stand him. He's had a bit of a mishap, and needs our...your help.",
    "Athera: Go speak with him at once. I must warn you, he can be a bit...demanding. See to it that his wishes are fulfilled.",
    "Athera: You can find Ilenar in the other corner of this room.",
    "You have received a quest!"
}
StartQuest(mySession, 60113, quests[60113][0].log)
else
    npcDialogue = "You have arrived at the perfect moment, playerName."
    diagOptions = { "How may I serve you, Athera?", "Oh, but perhaps I'm busy right now..." }
end
else
npcDialogue ="Athera: Things aren't going so well. I'll have a report for you a little later..."
end
elseif (GetPlayerFlags(mySession, "60115") == "5") then
multiDialogue = { "Athera: I've received word that the venerable Ilenar Crelwin is pleased with your work as of late. You have proved yourself a worthy agent, playerName.",
    "Athera: For all of your accomplishments, it is time you wore something befitting of your rank. I therefore award you with these Shadowpad Boots.",
    "Athera: I also award you with this Vaulter's Balance Scroll. Hopefully, these things will protect you, in dangerous places.",
    "Athera: I believe it is time for you to explore the world a bit more. But do check back later, I may yet have another quest to fit your skills.",
"You have received a Vaulter's Balance Scroll.",
"You have received Shadowpad Boots.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 60115, quests[60115][5].xp, 60120 )
GrantItem(mySession, items.VAULTERS_BALANCE, 1)
GrantItem(mySession, items.SHADOWPAD_BOOTS, 1)
elseif (GetPlayerFlags(mySession, "60120") == "0") then
if (level >=20) then
if (choice:find("whatever")) then
multiDialogue = { "Athera: Look at you...like a cat, always landing on your feet. It is time for you to make a choice that may shape your destiny. But before that, Ilenar Crelwin has a final test for you. You should see to him at once.",
    "You have received a quest!"
}
StartQuest(mySession, 60120, quests[60120][0].log)
else
    npcDialogue = "You have returned...relatively unscathed I see. I am glad you are here, now that I have something you might enjoy..."
    diagOptions = { "I can handle whatever you can throw at me." }
end
else
npcDialogue ="Athera: I have something very important coming up for you. Please check back with me later."
end
elseif (GetPlayerFlags(mySession, "60120") == "9") then
multiDialogue = { "Athera: I have received word from Ilenar. He is very pleased.",
    "Athera: As payment for your services, you've earned a set of rewards. The first is a Avoidance Scroll, an ability that greatly reduces an enemies desire to attack you. It comes with a Magic Dagger.",
    "Athera: I also reward you with this Minor Wound Scroll, a backstab-like attack that can be done from any angle. It comes with an Magic Rapier.",
"You have received an Avoidance Scroll.",
"You have received a Magic Dagger.",
"You have received a Minor Wound Scroll.",
"You have received a Magic Rapier.",
    "You have finished a quest!",
"Athera: I never thought I would say this, but you have learned all that I can teach you. It's time for you to go out and find what you truly desire. The world is loaded with mystery, danger, and riches.",
"Athera: Besides, I can't have you getting in my way around here. You're still rough around the edges, but don't ever let them see it. Farewell, playerName."
}
CompleteQuest(mySession, 60120, quests[60120][9].xp, 60121 )
GrantItem(mySession, items.AVOIDANCE, 1)
GrantItem(mySession, items.MAGIC_DAGGER, 1)
GrantItem(mySession, items.MINOR_WOUND, 1)
GrantItem(mySession, items.MAGIC_RAPIER, 1)
  else
        npcDialogue =
"Athera: I am much too tied up in my own affairs to consider anything you have to say. Unless you want to pay me an absurd amount of tunar. No? I didn't think so."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end

