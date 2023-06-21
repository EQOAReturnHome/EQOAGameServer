local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Bard(5) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "50103") == "0") then
if (choice:find("Corufost")) then
multiDialogue = { "Solenia Freyar: You've come to learn from us then I take it. Wonderful. However, as happy as we are to have you, we must test you.",
    "Solenia Freyar: To prove your worth, you must destroy the bothersome dragonflies found outside the city. Bring me 2 damaged dragonfly wings.",
    "Solenia Freyar: After I receive the damaged dragonfly wings I will reward you with a Chant of Battle Scroll, a bard's specialty.",
    "Solenia Freyar: Once I have those wings as proof of your combat ability, I will teach you a song that will tear through your enemies. Good luck.",
    "You have received a quest!"
}
StartQuest(mySession, 50103, quests[50103][0].log)
else
    npcDialogue = "Good day, good citizen."
    diagOptions = { "I was sent by Corufost." }
end
elseif (GetPlayerFlags(mySession, "50103") == "1") then
if (CheckQuestItem(mySession, items.DAMAGED_DRAGONFLY_WING, 2))
 then
if (choice:find("not")) then
npcDialogue = "Solenia Freyar: It's important that you bring me what I have asked from you. I need two damaged dragonfly wings ."
elseif (choice:find("here")) then
multiDialogue = { "Solenia Freyar: Good work. We'll get you something better to fight with soon. For now, take this scroll as a reward. Study it well.",
    "You have finished a quest!",
"You have given away a damaged dragonfly wing.",
"You have given away a damaged dragonfly wing.",
"You have received a Chant of Battle Scroll.",
"Solenia Freyar: Now, before we talk about getting you a proper weapon, get some rest. Come back when you are prepared."
}
CompleteQuest(mySession, 50103, quests[50103][1].xp, 50104 )
TurnInItem(mySession, items.DAMAGED_DRAGONFLY_WING, 2)
GrantItem(mySession, items.CHANT_OF_BATTLE, 1)
else
    npcDialogue = "Have the wings yet?"
    diagOptions = { "Yes, here they are.", "No, not yet." }
end
else
npcDialogue = "Solenia Freyar: It's important that you bring me what I have asked from you. I need two damaged dragonfly wings ."
end
elseif (GetPlayerFlags(mySession, "50104") == "0") then
if (level >=4) then
if (choice:find("prepared")) then
multiDialogue = { "Solenia Freyar: Very well. We'll get you that new weapon then. Well....You'll get that new weapon for yourself, rather.",
    "Solenia Freyar: I have a rapier in for you but we'll need the materials to replace it before I can issue it to you.",
    "Solenia Freyar: I'll need 3 items. The first is iron ore from merchant Shohan. You can find him near the north gate, in the bazaar.",
    "Solenia Freyar: The second is a leather strip, also from Merchant Shohan.",
    "Solenia Freyar: I also need 1 beetle leg segment. You'll need to find beetles out in the field and slay them.",
    "Solenia Freyar: Return with those 3 items and you shall have your new rapier. Good luck.",
    "You have received a quest!"
}
StartQuest(mySession, 50104, quests[50104][0].log)
else
    npcDialogue = "Good day, good citizen."
    diagOptions = { "I am prepared for another task." }
end
else
npcDialogue ="Solenia Freyar: I have to take care of something, however you should check back with me soon."
end
elseif (GetPlayerFlags(mySession, "50104") == "1") then
if (CheckQuestItem(mySession, items.IRON_ORE, 1)
and CheckQuestItem(mySession, items.LEATHER_STRIP, 1)
and CheckQuestItem(mySession, items.BEETLE_LEG_SEGMENT, 1))
 then
if (choice:find("sorry")) then
npcDialogue = "Solenia Freyar: You'll need to prove yourself by gathering these items. I need 1 iron ore, 1 leather strip, and 1 beetle leg segment."
elseif (choice:find("have")) then
multiDialogue = { "Solenia Freyar: Excellent. Well done. Here is your rapier. It hasn't seen much use in a very long time, but it's still functional.",
    "Solenia Freyar: Now get some rest. When you're ready, we'll send you on your last mission as a would-be bard.",
"You have given away an iron ore.",
"You have given away a leather strip.",
"You have given away a beetle leg segment.",
"You have received a traveler's rapier.",
    "You have finished a quest!",
"Solenia Freyar: You will find as you use your weapons and armor in battle, they will begin to wear down and lose their effectiveness.",
"Solenia Freyar: If you visit a blacksmith they will repair your weapons and armor for a price.",
"Solenia Freyar: You can find Blacksmith Coalbrick at the Bazaar on the north side of the city."
}
CompleteQuest(mySession, 50104, quests[50104][1].xp, 50105 )
TurnInItem(mySession, items.IRON_ORE, 1)
TurnInItem(mySession, items.LEATHER_STRIP, 1)
TurnInItem(mySession, items.BEETLE_LEG_SEGMENT, 1)
GrantItem(mySession, items.TRAVELERS_RAPIER, 1)
else
    npcDialogue = "Have you retrieved those items I asked for?"
    diagOptions = { "Yes I have. Here you go.", "I'm sorry, not yet." }
end
else
npcDialogue = "Solenia Freyar: You'll need to prove yourself by gathering these items. I need 1 iron ore, 1 leather strip, and 1 beetle leg segment."
end
elseif (GetPlayerFlags(mySession, "50105") == "0") then
if (level >=5) then
if (choice:find("ready")) then
multiDialogue = { "Solenia Freyar: In addition to recording and retelling epic tales of adventure you will often find yourself to be the hero of such tales.",
    "Solenia Freyar: Such will be the case if you can accomplish this next task. Bandits have been raiding caravans into the city as of late.",
    "Solenia Freyar: Their theft of imported goods include food and supplies for the poor, a lot which cannot withstand any more bad breaks.",
    "Solenia Freyar: If you truly wish to earn a place in our halls, you must put a stop to these highwaymen and recover the stolen goods."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Will you aid our city's most disheartened citizens by embarking on this task?"
    diagOptions = { "I will gladly help my fellow man.", "Sounds a little too dangerous..." }
elseif (choice:find("dangerous")) then
multiDialogue = { "Solenia Freyar: Let me tell you a story about Thomas the bard. Thomas was lazy, rarely helped his friends when they were in need. He was a loner. He never made sure his group was safe or had enough to eat.",
"Solenia Freyar: After awhile, all of Thomas's friends left him. Then one day, he came across a gang of nasty bandits who took everything he had.",
"Solenia Freyar: He had called out for his friends but none of them were there. The bandits left him barely alive.",
"Solenia Freyar: Don't end up like Thomas. Bards need others to survive, and they need you. You must look for every opportunity to help others, and in turn, they will be there for you.",
"Solenia Freyar: Now, you think on that for awhile, playerName. You know where I'll be if you change your mind."
 } 
elseif (choice:find("gladly")) then
multiDialogue = { "Solenia Freyar: Spoken like a true adventurer, Freeport will be in your debt.",
    "Solenia Freyar: Our scouts tell me that the highwaymen responsible can be found in the desserts and grasslands just beyond the cities borders.",
    "Solenia Freyar: Unfortunately I've also heard that they are a particularly tough lot, and that going alone against such foes would be folly.",
    "Solenia Freyar: Fortunately the Smiling Serpent is the best place you can be to find help on this mission.",
    "Solenia Freyar: Gather a party of adventurers to bring these bandits to justice and return the stolen goods to me.",
    "Solenia Freyar: Good luck playerName, may your story be a long one.",
    "You have received a quest!"
}
StartQuest(mySession, 50105, quests[50105][0].log)
else
    npcDialogue = "Good day, playerName. A pleasure to see you again."
    diagOptions = { "I am ready for my final test." }
end
else
npcDialogue ="Solenia Freyar: I have something for you, but you aren't yet ready. Go slay a few critters and check back with me."
end
elseif (GetPlayerFlags(mySession, "50105") == "1") then
if (CheckQuestItem(mySession, items.STOLEN_GOODS, 1))
 then
if (choice:find("quite")) then
npcDialogue = "Solenia Freyar: Take a deep breath, and consider where you need to go next. Find the highwaymen and retrieve from them the stolen goods."
elseif (choice:find("have")) then
multiDialogue = { "Solenia Freyar: This is most impressive. I had a feeling about you...",
    "Solenia Freyar: You can rest assured that these supplies will be given to those most in need. I'm not sure what we would have done without them.",
    "Solenia Freyar: For this, I am proud to have you as a true bard of The Silken Gauntlet. I award you with this Funeral March Scroll.",
    "Solenia Freyar: playerName I will certainly have more tasks for you, but I need some time to look into a few things. Come see me again later.",
"You have given away the stolen goods.",
"You have received a Funeral March Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 50105, quests[50105][1].xp, 50107 )
TurnInItem(mySession, items.STOLEN_GOODS, 1)
GrantItem(mySession, items.FUNERAL_MARCH, 1)
else
    npcDialogue = "Have you yet retrieved the stolen goods?"
    diagOptions = { "I have. Here they are...", "Not quite yet." }
end
else
npcDialogue = "Solenia Freyar: Take a deep breath, and consider where you need to go next. Find the highwaymen and retrieve from them the stolen goods."
end
elseif (GetPlayerFlags(mySession, "50107") == "0") then
if (level >=7) then
if (choice:find("quite")) then
multiDialogue = { "Solenia Freyar: I understand. Some days it is hard to wake up and just be an adventurer. Sometimes you have to just live your life and get back to that adventure later. It's ok. I'll be here when you are ready."
 } 
elseif (choice:find("ready")) then
multiDialogue = { "Solenia Freyar: I am so glad you are back. You've returned not a moment too soon. I have a task that needs attention but I am preoccupied at the moment.",
    "Solenia Freyar: I've just received word of a dangerous creature spotted near freeport. The Chichan is a poisonous eel that preys upon unwitting travelers.",
    "Solenia Freyar: The thing is, the eels are not native of this region. Someone must have brought them to Freeport intentionally.",
    "Solenia Freyar: To help us restore the safety of the travelers, you must defeat an eel. Bring me it's venom sac. You will find it north of Freeport, along the river.",
    "You have received a quest!"
}
StartQuest(mySession, 50107, quests[50107][0].log)
else
    npcDialogue = "Welcome back playerName. Ready for another good deed?"
    diagOptions = { "I am ready.", "Not quite." }
end
else
npcDialogue ="Solenia Freyar: I need a little more time to finish a spell I am working on. Come back a little later."
end
elseif (GetPlayerFlags(mySession, "50107") == "1") then
if (CheckQuestItem(mySession, items.EEL_VENOM_SAC, 1))
 then
if (choice:find("sorry")) then
npcDialogue = "Solenia Freyar: Stay focused. Bring me a Chichan Eel venom sac. You will find it north of Freeport, along the river."
elseif (choice:find("right")) then
multiDialogue = { "Solenia Freyar: This is very good. You haven't let me down yet. I believe the world is a little safer with you in it, playerName.",
    "Solenia Freyar: I will be passing this sac on to the Academy of Arcane Science. Perhaps they can shed some light on its origin.",
    "Solenia Freyar: As for your reward, take this Artful Strike Scroll and these Silken Leggings.",
    "Solenia Freyar: Thank you for your services. Come see me later, I will surely have more tasks for you.",
"You have given away an eel venom sac.",
"You have received an Artful Strike Scroll.",
"You have received the Silken Leggings.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 50107, quests[50107][1].xp, 50110 )
TurnInItem(mySession, items.EEL_VENOM_SAC, 1)
GrantItem(mySession, items.ARTFUL_STRIKE, 1)
GrantItem(mySession, items.SILKEN_LEGGINGS, 1)
else
    npcDialogue = "Do you have the venom sac?"
    diagOptions = { "It is right here.", "Oh sorry, not yet." }
end
else
npcDialogue = "Solenia Freyar: Stay focused. Bring me a Chichan Eel venom sac. You will find it north of Freeport, along the river."
end
elseif (GetPlayerFlags(mySession, "50110") == "0") then
if (level >=10) then
if (choice:find("things")) then
multiDialogue = { "Solenia Freyar: That is okay. Maybe you just need a break from all of this right now, playerName. Let me know when you are ready to help again."
 } 
elseif (choice:find("service")) then
multiDialogue = { "Solenia Freyar: There is a lot going on these days in Freeport. I must now turn to my trusted bards such as yourself for support.",
    "Solenia Freyar: Guard Sareken is in need of assistance, please go and find him at the north entrance of Freeport.",
    "Solenia Freyar: I may call upon you later playerName, so please check back when you can.",
    "You have received a quest!"
}
StartQuest(mySession, 50110, quests[50110][0].log)
else
    npcDialogue = "I was just thinking of you. I need help with something..."
    diagOptions = { "How may I be of service?", "I want to help but I need to do a few things first.." }
end
else
npcDialogue ="Solenia Freyar: You are doing great, but I need you to be a little stronger for this next task."
end
elseif (GetPlayerFlags(mySession, "50113") == "0") then
if (level >=13) then
if (choice:find("prepare")) then
multiDialogue = { "Solenia Freyar: Okay, but please do hurry. I am a bit nervous about this next task."
 } 
elseif (choice:find("here")) then
multiDialogue = { "Solenia Freyar: This next one will be a bit dangerous. We will need you to play a part, playerName.",
    "Solenia Freyar: The Shining Shield has just brought in an outsider by the name of Ilenar Crelwin. We've arranged to hire out an assistant to Ilenar while he's in town, someone who can get close to him.",
    "Solenia Freyar: The job is yours. Do whatever Ilenar asks of you and learn what you can about him. After a while, I will send for you, and then you will report your findings to me.",
    "Solenia Freyar: You can find Ilenar in the Shining Shield Guild Hall.",
    "Solenia Freyar: To the east of the here is the Shining Shield Mercenaries. Watch your step there...",
    "You have received a quest!"
}
StartQuest(mySession, 50113, quests[50113][0].log)
else
    npcDialogue = "Perfect timing. I will need my best bard on this next mission."
    diagOptions = { "I am here for you.", "I am here for you, but I need a moment to prepare." }
end
else
npcDialogue ="Solenia Freyar: Things aren't going so well. I'll have a report for you a little later..."
end
elseif (GetPlayerFlags(mySession, "50113") == "6") then
if (choice:find("gathering")) then
multiDialogue = { "Solenia Freyar: I am glad you are being thorough, but perhaps it is best if you clue me in to what is going on playerName. You are new at this, remember?"
 } 
elseif (choice:find("chocolates")) then
multiDialogue = { "Solenia Freyar: Chocolates? Why would he be sending those? Let me take a closer look...",
    "Solenia Freyar: ...Yes these do appear to be poisoned. So this is what he resorts to? Murder? This is quite unsettling. We have to learn more about him. This isn't time to blow your cover.",
    "Solenia Freyar: I am adding a curative to the chocolates. When Delwin eats them, he will faint and appear dead, though after awhile he will be fine. At least, I think he will...",
    "Solenia Freyar: Carry out your mission, and report back to Ilenar with what he wants to hear.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50113, quests[50113][6].log)
else
    npcDialogue = "Do you have anything to report?"
    diagOptions = { "Take a look at these chocolates...", "I am still gathering evidence." }
end
elseif (GetPlayerFlags(mySession, "50115") == "5") then
multiDialogue = { "Solenia Freyar: I've received word that the venerable Ilenar Crelwin is pleased with your work as of late. You have proved yourself a worthy agent, playerName.",
    "Solenia Freyar: For all of your accomplishments, it is time you wore something befitting of your rank. I therefore award you with a Thespian Leggings.",
    "Solenia Freyar: I also award you with this Crashing Verses Scroll. May these things protect you, in dangerous places.",
    "Solenia Freyar: I believe it is time for you to explore the world a bit more. But do check back later, I may yet have another quest to fit your skills.",
"You have received a Crashing Verses Scroll.",
"You have received a Thespian Leggings.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 50115, quests[50115][5].xp, 50120 )
GrantItem(mySession, items.CRASHING_VERSES, 1)
GrantItem(mySession, items.THESPIAN_LEGGINGS, 1)
elseif (GetPlayerFlags(mySession, "50120") == "0") then
if (level >=20) then
if (choice:find("ready")) then
multiDialogue = { "Solenia Freyar: Songs of your adventures may very well be at hand. It is time for you to make a choice that may shape your destiny. But now, Ilenar Crelwin has another mission for you. You should see to him at once.",
    "You have received a quest!"
}
StartQuest(mySession, 50120, quests[50120][0].log)
else
    npcDialogue = "You have returned...And you are much stronger than when we first met."
    diagOptions = { "I am ready for more." }
end
else
npcDialogue ="Solenia Freyar: I have something very important coming up for you. Please check back with me later."
end
elseif (GetPlayerFlags(mySession, "50120") == "6") then
if (choice:find("Ilenar")) then
multiDialogue = { "Solenia Freyar: So, he wants the Grimoire. What could he be up to? We can't allow him to acquire such a dangerous relic.",
    "Solenia Freyar: You must follow through with his mission, however if you are able to acquire the Grimoire, bring it immediately to me, not Ilenar. I believe in you playerName. You can do this.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50120, quests[50120][6].log)
else
    npcDialogue = "I take it you have something to report?"
    diagOptions = { "Ilenar has made his next move..." }
end
elseif (GetPlayerFlags(mySession, "50120") == "9") then
if (choice:find("acquired")) then
multiDialogue = { "Solenia Freyar: This is both amazing and a bit terrifying. Looking at you, I can already hear the songs and tales they will tell of you. You truly are impressive.",
    "Solenia Freyar: I will trade you this Fake Grimoire for the real one. He will never know the difference. He will eventually become frustrated by its broken magic, but at least he can't harm anyone with it.",
    "Solenia Freyar: Give Ilenar the Fake Grimoire and then come see me again.",
"You have given away the Geldwins Grimoire.",
"You have received a Fake Geldwins Grimoire.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50120, quests[50120][9].log)
TurnInItem(mySession, items.GELDWINS_GRIMOIRE, 1)
GrantItem(mySession, items.FAKE_GELDWINS_GRIMOIRE, 1)
else
    npcDialogue = "I am glad you have returned unharmed."
    diagOptions = { "I have acquired the Grimoire." }
end
elseif (GetPlayerFlags(mySession, "50120") == "11") then
multiDialogue = { "Solenia Freyar: I have received word from Ilenar. He is very pleased. Do not worry, I will turn over the Grimoire to the proper authorities.",
    "Solenia Freyar: As payment for your services, you've earned new abilities and weapons. The first is a Power Dance Scroll, a group spell that transfers some of your power to your party. It comes with a Magic Foil.",
    "Solenia Freyar: I also reward you with a Sweeping Flurry Scroll, a group spell that increases everyone's prowess in combat. It comes with a Magic Sabre.",
"You have given away the Geldwins Grimoire.",
"You have received a Power Dance Scroll.",
"You have received a Magic Foil.",
"You have received a Sweeping Flurry Scroll.",
"You have received a Magic Sabre.",
    "You have finished a quest!",
"Solenia Freyar: Incredibly, you have learned all that I can teach you. You must go out into the world and help others.",
"Solenia Freyar: If you ever miss the time we had, you could write song and play it for me someday. Farewell playerName. I wish you a safe journey. And know that I believe in you."
}
CompleteQuest(mySession, 50120, quests[50120][11].xp, 50121 )
TurnInItem(mySession, items.GELDWINS_GRIMOIRE, 1)
GrantItem(mySession, items.POWER_DANCE, 1)
GrantItem(mySession, items.MAGIC_FOIL, 1)
GrantItem(mySession, items.SWEEPING_FLURRY, 1)
GrantItem(mySession, items.MAGIC_SABRE, 1)
  else
        npcDialogue =
"Solenia Freyar: Music isn't just a profession, playerName. It's a way of life. It is it's own art, magic and enchantment that we all can understand. Think about it. It is always there surrounding you, wherever you go."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

