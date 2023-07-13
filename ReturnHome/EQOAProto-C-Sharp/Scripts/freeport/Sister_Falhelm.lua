local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Cleric(9) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "90103") == "0") then
if (choice:find("Denouncer")) then
multiDialogue = { "Sister Falhelm: I see. Well I suppose I should instruct you. But first you must prove your worth.",
    "Sister Falhelm: Equip whatever mighty weapon you may have and slay spiderlings. Bring me 2 tarantula leg tips as proof of your courageous deed.",
    "Sister Falhelm: After I receive the legs, I will reward you with a Minor Blessing Scroll, a cleric's specialty.",
    "Sister Falhelm: Make haste, and choose your enemies wisely.",
    "You have received a quest!"
}
StartQuest(mySession, 90103, quests[90103][0].log)
else
    npcDialogue = "You don't look like one of my acolytes."
    diagOptions = { "Denouncer Alshea sent me to you." }
end
elseif (GetPlayerFlags(mySession, "90103") == "1") then
if (CheckQuestItem(mySession, items.TARANTULA_LEG_TIPS, 2))
 then
if (choice:find("nevermind")) then
npcDialogue = "Sister Falhelm: It's important that you bring me what I have asked from you. I need two tarantula leg tips."
elseif (choice:find("have")) then
multiDialogue = { "Sister Falhelm: I see that you do well in following instruction. Perhaps there is hope for you.",
    "Sister Falhelm: As I said, you will be rewarded. Take this scroll and study it well. The spell is trivial compared to my power, but it's a start.",
    "You have finished a quest!",
"You have given away a tarantula leg tips.",
"You have given away a tarantula leg tips.",
"You have received a Minor Blessing Scroll.",
"Sister Falhelm: I'd like you to take a moment to meditate and clear your mind. I will have another task for you shortly."
}
CompleteQuest(mySession, 90103, quests[90103][1].xp, 90104 )
TurnInItem(mySession, items.TARANTULA_LEG_TIPS, 2)
GrantItem(mySession, items.MINOR_BLESSING, 1)
else
    npcDialogue = "Do you have what I asked for?"
    diagOptions = { "Yes, I have the tips.", "Yes, but nevermind, sorry." }
end
else
npcDialogue = "Sister Falhelm: It's important that you bring me what I have asked from you. I need two tarantula leg tips."
end
elseif (GetPlayerFlags(mySession, "90104") == "0") then
if (level >=4) then
if (choice:find("apprenticeship")) then
multiDialogue = { "Sister Falhelm: Apprenticeship? You must at least wield an Acolyte's Mace first if you wish to call yourself my apprentice.",
    "Sister Falhelm: Fortunately, I have one ready for you, but you must first aid in gathering the supplies to build the next one.",
"Sister Falhelm: The supplies I require are an iron ore, a leather strip, and an ant leg segment.",
"Sister Falhelm: Purchase iron ore from Merchant  Shohan near the blacksmith at the north side of Freeport.",
"Sister Falhelm: Merchant  Shohan also has leather strip's for sale. I will need one.",
"Sister Falhelm: I'll also need an ant leg segment. I'm sure the guards wouldn't mind if you collected the leg from the pests outside.",
"Sister Falhelm: Now stay focused, and don't come back until you have everything in proper order.",
    "You have received a quest!"
}
StartQuest(mySession, 90104, quests[90104][0].log)
else
    npcDialogue = "So, you have cleared your mind?"
    diagOptions = { "Yes. I wish to continue my apprenticeship." }
end
else
npcDialogue ="Sister Falhelm: I have to take care of something, however you should check back with me soon."
end
elseif (GetPlayerFlags(mySession, "90104") == "1") then
if (CheckQuestItem(mySession, items.IRON_ORE, 1)
and CheckQuestItem(mySession, items.LEATHER_STRIP, 1)
and CheckQuestItem(mySession, items.ANT_LEG_SEGMENT, 1))
 then
multiDialogue = { "Sister Falhelm: Thank you, may you use this mace to protect yourself, and those you consider your own.",
    "Sister Falhelm: You may just be strong enough to help with a task given to me. Get some rest, and return when you're ready.",
"You have given away an iron ore.",
"You have given away a leather strip.",
"You have given away an ant leg segment.",
"You have received an Acolyte's Mace.",
    "You have finished a quest!",
"Sister Falhelm: You will find as you use your weapons and armor in battle, they will begin to wear down and lose their effectiveness.",
"Sister Falhelm: If you visit a blacksmith they will repair your weapons and armor for a price.",
"Sister Falhelm: You can find Blacksmith Coalbrick at the Bazaar on the north side of the city."
}
CompleteQuest(mySession, 90104, quests[90104][1].xp, 90105 )
TurnInItem(mySession, items.IRON_ORE, 1)
TurnInItem(mySession, items.LEATHER_STRIP, 1)
TurnInItem(mySession, items.ANT_LEG_SEGMENT, 1)
GrantItem(mySession, items.ACOLYTES_MACE, 1)
else
npcDialogue = "Sister Falhelm: You'll need to prove yourself by gathering these items. I need 1 iron ore, 1 leather strip, and 1 ant leg segment."
end
elseif (GetPlayerFlags(mySession, "90105") == "0") then
if (level >=5) then
if (choice:find("ready")) then
multiDialogue = { "Sister Falhelm: I truly hope you are my dear playerName, for the task I have for you is necessarily fraught with peril.",
    "Sister Falhelm: The Shining Shield Mercenaries depends on caravan shipments from the west for a great deal of supplies to operate.",
    "Sister Falhelm: Just recently, a band of highwaymen have been hijacking caravans into Freeport in both the deserts and grasslands.",
    "Sister Falhelm: Those were our supplies. They have stolen from us."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Will you use the knowledge we have taught you thus far to recover the stolen goods and bring these criminals to justice?"
    diagOptions = { "I will, Sister Falhelm.", "Sorry, I can't help you with this." }
elseif (choice:find("can\'t")) then
multiDialogue = { "Sister Falhelm: Perhaps you are not appreciating what I am trying to do for you here. If you expect to succeed as a cleric, you must heed my command. Take a moment to think about that, and try again."
 } 
elseif (choice:find("Falhelm")) then
multiDialogue = { "Sister Falhelm: Great wisdom will be the reward for your bravery. However this is no challenge to take lightly or alone.",
    "Sister Falhelm: For this you should seek the company of other adventurers in Freeport. The Smiling Serpent tavern is a good place to start.",
    "Sister Falhelm: I am sure that we are not the only guild being made to suffer by these criminals, and that there are others who will help.",
    "Sister Falhelm: Go now my apprentice, and return when you've recovered the stolen goods.",
    "You have received a quest!"
}
StartQuest(mySession, 90105, quests[90105][0].log)
else
    npcDialogue = "Stay present, and speak clearly."
    diagOptions = { "I am ready for my next task." }
end
else
npcDialogue ="Sister Falhelm: I have something for you, but you aren't yet ready. Go slay a few critters and check back with me."
end
elseif (GetPlayerFlags(mySession, "90105") == "1") then
if (CheckQuestItem(mySession, items.STOLEN_GOODS, 1))
 then
if (choice:find("quite")) then
npcDialogue = "Sister Falhelm: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
elseif (choice:find("have")) then
multiDialogue = { "Sister Falhelm: This is most commendable! To be honest, I wasn't sure if I would ever see you again, but here you are proving yourself more useful...",
    "Sister Falhelm: We owe you a debt. I'm not sure what we would have done without these supplies.",
    "Sister Falhelm: For this, I am proud to have you as a true Cleric of The Shining Shield Mercenaries. I award you with this Holy Shock Scroll.",
    "Sister Falhelm: I will certainly have more tasks for you, but I need some time to look into a few things. Come see me again later, playerName.",
"You have given away the stolen goods.",
"You have received a Holy Shock Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 90105, quests[90105][1].xp, 90107 )
TurnInItem(mySession, items.STOLEN_GOODS, 1)
GrantItem(mySession, items.HOLY_SHOCK, 1)
else
    npcDialogue = "Have you yet retrieved the stolen goods?"
    diagOptions = { "I have. Here they are...", "Not quite yet." }
end
else
npcDialogue = "Sister Falhelm: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
end
elseif (GetPlayerFlags(mySession, "90107") == "0") then
if (level >=7) then
if (choice:find("should")) then
multiDialogue = { "Sister Falhelm: ...And just what do you think you mean by that? You had best attend this post fully prepared and ready to serve, or I will show you the painful end of a holy blast!"
 } 
elseif (choice:find("assignment")) then
multiDialogue = { "Sister Falhelm: Well then, you've returned not a moment too soon. I have a task that needs attention but I am preoccupied at the moment.",
    "Sister Falhelm: I've just received word of a dangerous creature spotted near freeport. The Chichan is a poisonous eel that preys upon unwitting travelers.",
    "Sister Falhelm: The thing is, the eels are not native of this region. Someone must have brought it to freeport intentionally.",
    "Sister Falhelm: I'll need a piece of the eel to investigate further. Bring to me it's venom sac. You will find it north of Freeport, along the river. Hurry along now, playerName.",
    "You have received a quest!"
}
StartQuest(mySession, 90107, quests[90107][0].log)
else
    npcDialogue = "Stay present, and speak clearly."
    diagOptions = { "I am ready for my next assignment.", "I guess I should come back later." }
end
else
npcDialogue ="Sister Falhelm: I need a little more time to finish a spell I am working on. Come back a little later."
end
elseif (GetPlayerFlags(mySession, "90107") == "1") then
if (CheckQuestItem(mySession, items.EEL_VENOM_SAC, 1))
 then
if (choice:find("sorry")) then
npcDialogue = "Sister Falhelm: Stay focused. Bring me a Chichan Eel venom sac. You will find it north of Freeport, along the river."
elseif (choice:find("right")) then
multiDialogue = { "Sister Falhelm: Well done. You might be a bright spot in this otherwise dreary place.",
    "Sister Falhelm: I must investigate this venom sac immediately.",
    "Sister Falhelm: As for your reward, take this Endure Ailment Scroll and these Trubar's Leggings.",
    "Sister Falhelm: Thank you for your services, playerName. Come see me later, I will have more for you.",
"You have given away an eel venom sac.",
"You have received an Endure Ailment Scroll.",
"You have received Trubar's Leggings.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 90107, quests[90107][1].xp, 90110 )
TurnInItem(mySession, items.EEL_VENOM_SAC, 1)
GrantItem(mySession, items.ENDURE_AILMENT, 1)
GrantItem(mySession, items.TRUBARS_LEGGINGS, 1)
else
    npcDialogue = "Do you have the venom sac?"
    diagOptions = { "It is right here.", "Oh sorry, not yet." }
end
else
npcDialogue = "Sister Falhelm: Stay focused. Bring me a Chichan Eel venom sac. You will find it north of Freeport, along the river."
end
elseif (GetPlayerFlags(mySession, "90110") == "0") then
if (level >=10) then
if (choice:find("isn\'t")) then
multiDialogue = { "Sister Falhelm: Just what is it you do in your free time that causes you to be so empty minded when attending your master? Take a hard look at yourself or this is going to sour between us quite hastily."
 } 
elseif (choice:find("Falhelm")) then
multiDialogue = { "Sister Falhelm: I have so many requests for assistance now I can barely keep up. I must now have you help me with this next task.",
    "Sister Falhelm: Guard Sareken is in need of assistance. Please find him at the north entrance of Freeport.",
    "Sister Falhelm: I may call upon you later, so please check back when you can.",
    "You have received a quest!"
}
StartQuest(mySession, 90110, quests[90110][0].log)
else
    npcDialogue = "playerName, please tell me you are here to help..."
    diagOptions = { "I am, Sister Falhelm.", "I was, but my mind still isn't clear..." }
end
else
npcDialogue ="Sister Falhelm: You'll need to develop your skills a little further before you can handle what I have in store for you."
end
elseif (GetPlayerFlags(mySession, "90113") == "0") then
if (level >=13) then
if (choice:find("perhaps")) then
multiDialogue = { "Sister Falhelm: Just what is it you do in your free time that causes you to be so empty minded when attending your master? Take a hard look at yourself or this is going to sour between us quite hastily."
 } 
elseif (choice:find("Falhelm")) then
multiDialogue = { "Sister Falhelm: The wizard Ilenar Crelwin has come from a long distance on business. He is a colleague of mine, but to be honest, I cant stand him. He's had a bit of a mishap, and needs our...your help.",
    "Sister Falhelm: Go speak with him at once. I must warn you, he can be a bit...demanding. See to it that his wishes are fulfilled.",
    "Sister Falhelm: You can find Ilenar in the other corner of this room.",
    "You have received a quest!"
}
StartQuest(mySession, 90113, quests[90113][0].log)
else
    npcDialogue = "You have arrived at the perfect moment, playerName."
    diagOptions = { "How may I serve you, Sister Falhelm?", "Oh, but perhaps I'm busy right now..." }
end
else
npcDialogue ="Sister Falhelm: Things aren't going so well. I'll have a report for you a little later..."
end
elseif (GetPlayerFlags(mySession, "90115") == "5") then
multiDialogue = { "Sister Falhelm: I've received word that the venerable Ilenar Crelwin is pleased with your work as of late. You have proved yourself a worthy agent, playerName.",
    "Sister Falhelm: For all of your accomplishments, it is time you wore something befitting of your rank. I therefore award you with these Reverent Bracers.",
    "Sister Falhelm: I also award you with this Endure Affliction Scroll. May these things protect you, in dangerous places.",
    "Sister Falhelm: I believe it is time for you to explore the world a bit more. But do check back later, I may yet have another quest to fit your skills.",
"You have received an Endure Affliction Scroll.",
"You have received Reverent Bracers.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 90115, quests[90115][5].xp, 90120 )
GrantItem(mySession, items.ENDURE_AFFLICTION, 1)
GrantItem(mySession, items.REVERENT_BRACERS, 1)
elseif (GetPlayerFlags(mySession, "90120") == "0") then
if (level >=20) then
if (choice:find("whatever")) then
multiDialogue = { "Sister Falhelm: I know that things have been rough for you lately playerName, but you seem to be handling it well.",
    "Sister Falhelm: It is time for you to make a choice that may shape your destiny. To prepare for this choice, Ilenar Crelwin has a final test for you. You should see to him at once.",
    "You have received a quest!"
}
StartQuest(mySession, 90120, quests[90120][0].log)
else
    npcDialogue = "You have returned...relatively unscathed I see. I am glad you are here, now that I have something you might enjoy..."
    diagOptions = { "I can handle whatever you can throw at me." }
end
else
npcDialogue ="Sister Falhelm: I have something very important coming up for you. Please check back with me later."
end
elseif (GetPlayerFlags(mySession, "90120") == "9") then
multiDialogue = { "Sister Falhelm: I have received word from Ilenar. He is very pleased.",
    "Sister Falhelm: As payment for your services, you've earned a set of rewards. The first is a Disease Ward Scroll, a potent protection spell that boosts your resistance to disease. It comes with a Magical Staff.",
    "Sister Falhelm: I also reward you with this Field Dress Scroll, an instant healing spell most useful in combat. It comes with an Enchanted Mace.",
"You have received a Disease Ward Scroll.",
"You have received a Magical Staff.",
"You have received a Field Dress Scroll.",
"You have received an Enchanted Mace.",
    "You have finished a quest!",
"Sister Falhelm: Incredibly, you have learned all that I can teach you. You must go out into the world and seek out new magic.",
"Sister Falhelm: Don't forget what we have accomplished here, and remember to let the darkness guide your way. Farewell playerName."
}
CompleteQuest(mySession, 90120, quests[90120][9].xp, 90121 )
GrantItem(mySession, items.DISEASE_WARD, 1)
GrantItem(mySession, items.MAGICAL_STAFF, 1)
GrantItem(mySession, items.FIELD_DRESS, 1)
GrantItem(mySession, items.ENCHANTED_MACE, 1)
  else
        npcDialogue =
"Sister Falhelm: To survive as a cleric here you must endure many painful trials. Don't think that just because you have a healing spell means that getting wounded in battle isn't still agonizingly painful."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

