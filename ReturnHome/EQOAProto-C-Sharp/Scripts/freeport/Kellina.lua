local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Magician(10) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "100103") == "0") then
if (choice:find("Actually")) then
multiDialogue = { "Kellina: Oohh, now that isn't that disappointing. Well I suppose I should instruct you. But first you must prove your worth.",
    "Kellina: Equip whatever mighty weapon you may have and slay ants. Bring me 2 cracked ant pincers as proof of your valiant deed.",
    "Kellina: After I receive the pincers I will reward you with a scroll of smoldering aura, a magician's specialty.",
    "Kellina: Now be off. I've wasted enough of my time with you.",
    "You have received a quest!"
}
StartQuest(mySession, 100103, quests[100103][0].log)
else
    npcDialogue = "I don't have time for chit chat, dear."
    diagOptions = { "Actually, Malsis sent me." }
end
elseif (GetPlayerFlags(mySession, "100103") == "1") then
if (CheckQuestItem(mySession, items.CRACKED_ANT_PINCER, 2))
 then
if (choice:find("nevermind")) then
npcDialogue = "Kellina: It's important that you bring me what I have asked from you. What was it now...Ah yes, I need two cracked ant pincers."
elseif (choice:find("apologize")) then
multiDialogue = { "Kellina: I suppose I can disregard your folly. You are new after all. And you did complete the task I assigned you.",
    "Kellina: I am a woman of my word. Take this scroll and study it well. The spell is paltry compared to my power, but it's a start.",
    "You have finished a quest!",
"You have given away a cracked ant pincer.",
"You have given away a cracked ant pincer.",
"You have received a Smoldering Aura Scroll.",
"Kellina: Unfortunately I must assign you another task, but I haven't the energy to deal with a novice right now. Come back later."
}
CompleteQuest(mySession, 100103, quests[100103][1].xp, 100104 )
TurnInItem(mySession, items.CRACKED_ANT_PINCER, 2)
GrantItem(mySession, items.SMOLDERING_AURA, 1)
else
    npcDialogue = "I don't remember calling for you."
    diagOptions = { "I apologize Lady, but I have the pincers.", "Yes, but...I...nevermind, sorry." }
end
else
npcDialogue = "Kellina: It's important that you bring me what I have asked from you. What was it now...Ah yes, I need two cracked ant pincers."
end
elseif (GetPlayerFlags(mySession, "100104") == "0") then
if (level >=4) then
if (choice:find("apprenticeship")) then
multiDialogue = { "Kellina: Apprenticeship? You must at least wear the blue robe of our caste first if you wish to call yourself my apprentice.",
    "Kellina: Luckily for you I'm feeling somewhat generous today. I'll enchant the robe for you if you bring me the components."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Go on, fetch them quickly."
    diagOptions = { "But...I don't know what they are?" }

elseif (choice:find("don\'t")) then
multiDialogue = { "Kellina: Solusek's eye, do I even have to think for you now? Nevermind. What I require is a plain robe, and a silk cord.",
    "Kellina: Plain robes can be purchased from Merchant Yulia just outside this building.",
    "Kellina: Merchant Yesam has silk cords for sale just inside the eastern city gate nearest the docks.",
    "Kellina: I'll also need a ruined bat wing. I'm sure the guards wouldn't mind if you collected the wing from the bat pests outside.",
    "Kellina: Now shoo. And don't come back until you have everything in proper order.",
    "You have received a quest!"
}
StartQuest(mySession, 100104, quests[100104][0].log)
else
    npcDialogue = "I don't have time for chit chat, dear."
    diagOptions = { "I wish to continue my apprenticeship." }
end
else
npcDialogue ="Kellina: I have to take care of something, however you should check back with me soon."
end
elseif (GetPlayerFlags(mySession, "100104") == "1") then
if (CheckQuestItem(mySession, items.PLAIN_ROBE, 1)
and CheckQuestItem(mySession, items.SILK_CORD, 1)
and CheckQuestItem(mySession, items.RUINED_BAT_WING, 1))
 then
multiDialogue = { "Kellina: Thank you, the enchantment on this robe shall serve to strengthen your defenses against elemental cold.",
    "Kellina: You may now be strong enough to help with a task given to me, get some rest first though and come back when you're ready.",
"You have given away a plain robe.",
"You have given away a silk cord.",
"You have given away a ruined bat wing.",
"You have received a Blue Robe.",
    "You have finished a quest!",
"Kellina: You will find as you use your weapons and armor in battle, they will begin to wear down and lose their effectiveness.",
"Kellina: If you visit a blacksmith they will repair your weapons and armor for a price.",
"Kellina: You can find Blacksmith Coalbrick at the Bazaar on the north side of the city."
}
CompleteQuest(mySession, 100104, quests[100104][1].xp, 100105 )
TurnInItem(mySession, items.PLAIN_ROBE, 1)
TurnInItem(mySession, items.SILK_CORD, 1)
TurnInItem(mySession, items.RUINED_BAT_WING, 1)
GrantItem(mySession, items.BLUE_ROBE, 1)
else
npcDialogue = "Kellina: You'll need to prove yourself by gathering these items. I need 1 plain robe, 1 silk cord, and 1 ruined bat wing."
end
elseif (GetPlayerFlags(mySession, "100105") == "0") then
if (level >=5) then
if (choice:find("ready")) then
multiDialogue = { "Kellina: I truly hope you are my dear, for the task I have for you is necessarily fraught with peril.",
    "Kellina: Our Academy depends on caravan shipments from the west for a great deal of our research supplies.",
    "Kellina: Just recently, a band of highwaymen have been hijacking caravans into Freeport in both the deserts and grasslands."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Will you use the knowledge we have taught you thus far to recover the stolen goods and bring these criminals to justice?"
    diagOptions = { "I will.", "Sorry, I can't help you with this." }
elseif (choice:find("can\'t")) then
multiDialogue = { "Kellina: Do you fancy yourself your own master? You had best reconsider getting this done or we will have to reconsider your enrollment here."
 } 
elseif (choice:find("will")) then
multiDialogue = { "Kellina: Great knowledge will be the reward for your bravery. However this is no challenge to take lightly or alone.",
    "Kellina: For this you should seek the company of other adventurers in Freeport. The Smiling Serpent tavern is a good place to start.",
    "Kellina: I am sure that we are not the only guild being made to suffer by these criminals, and that there are others who will help.",
    "Kellina: Go now my apprentice, and return when you've recovered the stolen goods.",
    "You have received a quest!"
}
StartQuest(mySession, 100105, quests[100105][0].log)
else
    npcDialogue = "I don't have time for chit chat, dear."
    diagOptions = { "I am ready for my next task." }
end
else
npcDialogue ="Kellina: I have something for you, but you aren't yet ready. Go slay a few critters and check back with me."
end
elseif (GetPlayerFlags(mySession, "100105") == "1") then
if (CheckQuestItem(mySession, items.STOLEN_GOODS, 1))
 then
if (choice:find("quite")) then
npcDialogue = "Kellina: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
elseif (choice:find("have")) then
multiDialogue = { "Kellina: I see...! To be honest, I wasn't sure if I would ever see you again, but here you are proving yourself more useful...",
    "Kellina: What was your name again? Ah yes, playerName. The Academy, along with the other guilds might owe you a debt. I'm not sure what we would have done without these supplies.",
    "Kellina: For this, I am proud to have you as a true Magician of The Academy of Arcane Science. I award you with this Motivate Scroll.",
    "Kellina: I will certainly have more tasks for you, but I need some time to look into a few things. Come see me again later, playerName.",
"You have given away the stolen goods.",
"You have received a Motivate Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 100105, quests[100105][1].xp, 100107 )
TurnInItem(mySession, items.STOLEN_GOODS, 1)
GrantItem(mySession, items.MOTIVATE, 1)
else
    npcDialogue = "Have you yet retrieved the stolen goods?"
    diagOptions = { "I have. Here they are...", "Not quite yet." }
end
else
npcDialogue = "Kellina: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
end
elseif (GetPlayerFlags(mySession, "100107") == "0") then
if (level >=7) then
if (choice:find("Nothing")) then
multiDialogue = { "Kellina: playerName, you'll need to learn to speak up for yourself, or you won't get far in this city."
 } 
elseif (choice:find("assignment")) then
multiDialogue = { "Kellina: Well then, you've returned not a moment too soon. I have a task that needs attention but I am preoccupied at the moment.",
    "Kellina: I've just received word of a dangerous creature spotted near freeport. The Chichan is a poisonous eel that preys upon unwitting travelers.",
    "Kellina: The thing is, the eels are not native of this region. Someone must have brought it to freeport intentionally.",
    "Kellina: I'll need a piece of the eel to investigate further. Bring to me it's venom sac. You will find it north of Freeport, along the river. Hurry along now, playerName.",
    "You have received a quest!"
}
StartQuest(mySession, 100107, quests[100107][0].log)
else
    npcDialogue = "I am quite busy, what could you possibly need?"
    diagOptions = { "I am ready for my next assignment.", "Nothing, really." }
end
else
npcDialogue ="Kellina: I need a little more time to finish a spell I am working on. Come back a little later."
end
elseif (GetPlayerFlags(mySession, "100107") == "1") then
if (CheckQuestItem(mySession, items.EEL_VENOM_SAC, 1))
 then
if (choice:find("sorry")) then
npcDialogue = "Kellina: Stay focused. Bring me a Chichan Eel venom sac. You will find it north of Freeport, along the river."
elseif (choice:find("right")) then
multiDialogue = { "Kellina: Well done. You might be a bright spot in this otherwise dreary place.",
    "Kellina: I must investigate this venom sac immediately.",
    "Kellina: As for your reward, take this Infusion Scroll and these Blackened Leggings.",
    "Kellina: Thank you for your services, playerName. Come see me later, I will have more for you.",
"You have given away an eel venom sac.",
"You have received an Infusion Scroll.",
"You have received the Blackened Leggings.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 100107, quests[100107][1].xp, 100110 )
TurnInItem(mySession, items.EEL_VENOM_SAC, 1)
GrantItem(mySession, items.INFUSION, 1)
GrantItem(mySession, items.BLACKENED_LEGGINGS, 1)
else
    npcDialogue = "Do you have the venom sac?"
    diagOptions = { "It is right here.", "Oh sorry, not yet." }
end
else
npcDialogue = "Kellina: Stay focused. Bring me a Chichan Eel venom sac. You will find it north of Freeport, along the river."
end
elseif (GetPlayerFlags(mySession, "100110") == "0") then
if (level >=10) then
if (choice:find("Maybe")) then
multiDialogue = { "Kellina: Declining to help your teacher will not look good on your records, I assure you!"
 } 
elseif (choice:find("Kellina")) then
multiDialogue = { "Kellina: I have so many requests for assistance now I can barely keep up. I must now have you help me with this next task.",
    "Kellina: Guard Sareken is in need of assistance. Please find him at the north entrance of Freeport.",
    "Kellina: I may call upon you later, so please check back when you can.",
    "You have received a quest!"
}
StartQuest(mySession, 100110, quests[100110][0].log)
else
    npcDialogue = "playerName, please tell me you are here to help..."
    diagOptions = { "I am, Kellina.", "Maybe later..." }
end
else
npcDialogue ="Kellina: You'll need to develop your skills a little further before you can handle what I have in store for you."
end
elseif (GetPlayerFlags(mySession, "100113") == "0") then
if (level >=13) then
if (choice:find("nevermind")) then
multiDialogue = { "Kellina: playerName, you'll need to learn to speak up for yourself, or you won't get far in this city."
 } 
elseif (choice:find("returned")) then
multiDialogue = { "Kellina: Yes of course, I can use you to... Oh nevermind that.",
    "Kellina: It is good you have returned. A colleague of mine has returned to Freeport. To be honest, I cant stand him. He's had a bit of a mishap, and needs our...your help.",
    "Kellina: See to him at once. I must warn you, he can be a bit...demanding. See to it that his wishes are fulfilled.",
    "Kellina: You can find Ilenar in the Shining Shield Guild Hall. Head out through the midroad, and to the southeast a little, head through the Smiling Serpent Inn.",
    "Kellina: To the east of the inn is the Shining Shield Mercenaries. Watch your step there playerName...",
    "You have received a quest!"
}
StartQuest(mySession, 100113, quests[100113][0].log)
else
    npcDialogue = "What do you want? I really can't..."
    diagOptions = { "I have returned to learn more.", "Oh nevermind..." }
end
else
npcDialogue ="Kellina: Things aren't going so well. I'll have a report for you a little later..."
end
elseif (GetPlayerFlags(mySession, "100115") == "5") then
multiDialogue = { "Kellina: I've received word that the venerable Ilenar Crelwin is pleased with your work as of late. You have proved yourself a worthy agent, playerName.",
    "Kellina: For all of your accomplishments, it is time you wore something befitting of your rank. I therefore award you with a Summoners Garb.",
    "Kellina: I also award you with this Endure Fire Scroll. May these things protect you, in dangerous places.",
    "Kellina: I believe it is time for you to explore the world a bit more. But do check back later, I may yet have another quest to fit your skills.",
"You have received an Endure Fire Scroll.",
"You have received a Summoners Garb.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 100115, quests[100115][5].xp, 100120 )
GrantItem(mySession, items.ENDURE_FIRE, 1)
GrantItem(mySession, items.SUMMONERS_GARB, 1)
elseif (GetPlayerFlags(mySession, "100120") == "0") then
if (level >=20) then
if (choice:find("whatever")) then
multiDialogue = { "Kellina: I know that things have been rough for you lately playerName, but you seem to be handling it well.",
    "Kellina: It is time for you to make a choice that may shape your destiny. But before that, Ilenar Crelwin has a final test for you. You should see to him at once.",
    "You have received a quest!"
}
StartQuest(mySession, 100120, quests[100120][0].log)
else
    npcDialogue = "You have returned...relatively unscathed I see. I am glad you are here, now that I have something you might enjoy..."
    diagOptions = { "I can handle whatever you can throw at me." }
end
else
npcDialogue ="Kellina: I have something very important coming up for you. Please check back with me later."
end
elseif (GetPlayerFlags(mySession, "100120") == "9") then
multiDialogue = { "Kellina: I have received word from Ilenar. He is very pleased.",
    "Kellina: As payment for your services, you've earned a set of new abilities and weapons. The first is a Frozen Mark Scroll, an ability that enhances your pet. It comes with a Magical Staff.",
    "Kellina: I also reward you with a Lava Stone Scroll, which summons a charged item with a potent damage spell. It comes with a Mystical Tome.",
"You have received a Frozen Mark Scroll.",
"You have received a Magical Staff.",
"You have received a Lava Stone Scroll.",
"You have received a Mystical Tome.",
    "You have finished a quest!",
"Kellina: Incredibly, you have learned all that I can teach you. You must go out into the world and seek out new magic. Don't forget what we have accomplished here. Let the elements guide your way. Farewell playerName."
}
CompleteQuest(mySession, 100120, quests[100120][9].xp, 100121 )
GrantItem(mySession, items.FROZEN_MARK, 1)
GrantItem(mySession, items.MAGICAL_STAFF, 1)
GrantItem(mySession, items.LAVA_STONE, 1)
GrantItem(mySession, items.MYSTICAL_TOME, 1)
  else
        npcDialogue =
"Kellina: I am quite busy with my students right now. Are you sure you're in the right place playerName?"
    end
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

