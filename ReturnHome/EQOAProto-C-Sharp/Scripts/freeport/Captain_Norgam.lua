local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Warrior(0) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "103") == "0") then
if (choice:find("Commander")) then
multiDialogue = { "Captain Norgam: Another hopeless cadet... The last one cried rather loudly when he failed me. I suppose you'll need some training first.",
    "Captain Norgam: Equip whatever mighty weapon you may have and slay orc pawns. Bring me 2 slashed pawn belts as proof of your accomplishments.",
    "Captain Norgam: After I receive the belts, I will reward you with a Kick Scroll, a warrior's specialty.",
    "Captain Norgam: Don't disappoint me. I have an Iron Maiden ready for fresh blood.",
    "You have received a quest!"
}
StartQuest(mySession, 103, quests[103][0].log)
else
    npcDialogue = "State your business."
    diagOptions = { "Commander Nothard sent me to you." }
end
elseif (GetPlayerFlags(mySession, "103") == "1") then
if (CheckQuestItem(mySession, items.SLASHED_PAWN_BELT, 2))
 then
if (choice:find("Nothing")) then
npcDialogue = "Captain Norgam: It's important that you bring me what I have asked from you. I need two slashed pawn belts."
elseif (choice:find("belts")) then
multiDialogue = { "Captain Norgam: I guess you do well in following instruction. Maybe you will last a little longer than the others.",
    "Captain Norgam: As I said, you will be rewarded. Take this scroll and study it well. The skill is trivial compared to my abilities, but it's a good start for you, playerName.",
    "You have finished a quest!",
"You have given away a slashed pawn belt.",
"You have given away a slashed pawn belt.",
"You have received a Kick Scroll.",
"Captain Norgam: I need a minute to clean up a bloody mess that my last cadet left behind so come back in a moment. He didn't follow orders as well as you."
}
CompleteQuest(mySession, 103, quests[103][1].xp, 104 )
TurnInItem(mySession, items.SLASHED_PAWN_BELT, 2)
GrantItem(mySession, items.KICK, 1)
else
    npcDialogue = "What do you want now?"
    diagOptions = { "I have the belts.", "Nothing sir, thank you." }
end
else
npcDialogue = "Captain Norgam: It's important that you bring me what I have asked from you. I need two slashed pawn belts."
end
elseif (GetPlayerFlags(mySession, "104") == "0") then
if (level >=4) then
if (choice:find("warrior")) then
multiDialogue = { "Captain Norgam: You'll have to start looking the part, so you must at least wield a Militia Short Sword first if you wish to call yourself a warrior.",
    "Captain Norgam: Fortunately, I have one ready for you, but you must first aid in gathering the supplies to build the next one.",
    "Captain Norgam: These things must be acquired with your own blood, sweat and tears, I'm certainly not giving it to you for free.",
"Captain Norgam: The supplies I require are an iron ore, a leather strip, and two broken vulture feathers.",
"Captain Norgam: Purchase iron ore from Merchant Shohan near the blacksmith at the north side of Freeport.",
"Captain Norgam: Merchant Shohan also has leather strip's for sale. I will need one.",
"Captain Norgam: I'll also need two broken vulture feathers. I'm sure the guards wouldn't mind if you collected the feathers from the pests outside.",
"Captain Norgam: Don't come back until you have everything in proper order.",
    "You have received a quest!"
}
StartQuest(mySession, 104, quests[104][0].log)
else
    npcDialogue = "Ready for your next torture err...task?"
    diagOptions = { "Yes. I still seek the ways of the warrior." }
end
else
npcDialogue ="Captain Norgam: I have to take care of something, however you should check back with me soon."
end
elseif (GetPlayerFlags(mySession, "104") == "1") then
if (CheckQuestItem(mySession, items.IRON_ORE, 1)
and CheckQuestItem(mySession, items.LEATHER_STRIP, 1)
and CheckQuestItem(mySession, items.BROKEN_VULTURE_FEATHER, 1))
 then
multiDialogue = { "Captain Norgam: It appears as though this sword is yours. Don't hurt yourself with it.",
"You have given away an iron ore.",
"You have given away a leather strip.",
"You have given away a broken vulture feather.",
"You have given away a broken vulture feather.",
"You have received a Militia Short Sword.",
    "You have finished a quest!",
"Captain Norgam: You will find as you use your weapons and armor in battle, they will begin to wear down and lose their effectiveness.",
"Captain Norgam: If you visit a blacksmith they will repair your weapons and armor for a price.",
"Captain Norgam: You can find Blacksmith Coalbrick at the Bazaar on the north side of the city.",
"Captain Norgam: I have new orders for you. Check back when you are properly equipped."
}
CompleteQuest(mySession, 104, quests[104][1].xp, 105 )
TurnInItem(mySession, items.IRON_ORE, 1)
TurnInItem(mySession, items.LEATHER_STRIP, 1)
TurnInItem(mySession, items.BROKEN_VULTURE_FEATHER, 2)
GrantItem(mySession, items.MILITIA_SHORT_SWORD, 1)
else
npcDialogue = "Captain Norgam: You'll need to prove yourself by gathering these items. I need 1 iron ore, 1 leather strip, and 2 broken vulture feathers."
end
elseif (GetPlayerFlags(mySession, "105") == "0") then
if (level >=5) then
if (choice:find("ready")) then
multiDialogue = { "Captain Norgam: We will see about that. This next task is a bit more deadly.",
    "Captain Norgam: The Freeport Militia depends on caravan shipments from the west for a great deal of supplies to operate.",
    "Captain Norgam: Just recently, a band of highwaymen have been hijacking caravans into Freeport in both the deserts and grasslands.",
    "Captain Norgam: The roads are plagued with highwaymen and they must be made to pay."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Will you use the knowledge we have taught you thus far to recover the stolen goods and bring these criminals to justice?"
    diagOptions = { "I will, Captain Norgam.", "Sorry, I can't help you with this." }
elseif (choice:find("can\'t")) then
multiDialogue = { "Captain Norgam: Oh, you think you have a choice? Respond like that again and you will be polishing my boots for a week!"
 } 
elseif (choice:find("Captain")) then
multiDialogue = { "Captain Norgam: If you succeed, it may buy you another day. However this is no challenge to take lightly or alone.",
    "Captain Norgam: For this you should seek the company of other adventurers in Freeport. The Smiling Serpent tavern is a good place to start.",
    "Captain Norgam: I am sure that we are not the only guild being made to suffer by these criminals, and that there are others who will help.",
    "Captain Norgam: Go now playerName, and return when you've recovered the stolen goods.",
    "You have received a quest!"
}
StartQuest(mySession, 105, quests[105][0].log)
else
    npcDialogue = "New orders have just come in."
    diagOptions = { "I am ready to serve." }
end
else
npcDialogue ="Captain Norgam: I have something for you, but you aren't yet ready. Go slay a few critters and check back with me."
end
elseif (GetPlayerFlags(mySession, "105") == "1") then
if (CheckQuestItem(mySession, items.STOLEN_GOODS, 1))
 then
if (choice:find("quite")) then
npcDialogue = "Captain Norgam: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
elseif (choice:find("have")) then
multiDialogue = { "Captain Norgam: I am...quite surprised that you made it. To be honest, I wasn't sure if I would ever see you again, and here you are proving yourself ever persistent...",
    "Captain Norgam: Maybe you will be of use to us after all...",
    "Captain Norgam: For this, I am proud to have you as a true Warrior of The Freeport Militia. I award you with this Taunt Scroll.",
    "Captain Norgam: I will certainly have more work for you, but I need some time to look into a few things. Come see me again later, playerName.",
"You have given away the stolen goods.",
"You have received a Taunt Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 105, quests[105][1].xp, 107 )
TurnInItem(mySession, items.STOLEN_GOODS, 1)
GrantItem(mySession, items.TAUNT, 1)
else
    npcDialogue = "Have you yet retrieved the stolen goods?"
    diagOptions = { "I have. Here they are...", "Not quite yet." }
end
else
npcDialogue = "Captain Norgam: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
end
elseif (GetPlayerFlags(mySession, "107") == "0") then
if (level >=7) then
if (choice:find("I\'ll")) then
multiDialogue = { "Captain Norgam: That's fine, I wouldn't want you to be too sober while attending to these tasks. Maybe you'll fall off a tall barstool while you are at it and rid me of your idiocy."
 } 
elseif (choice:find("assignment")) then
multiDialogue = { "Captain Norgam: Well then, you've returned not a moment too soon. I have a task that needs attention but I am preoccupied at the moment.",
    "Captain Norgam: I've just received word of a dangerous creature spotted near freeport. The Chichan is a poisonous eel that preys upon unwitting travelers.",
    "Captain Norgam: The thing is, the eels are not native of this region. Someone must have brought it to freeport intentionally.",
    "Captain Norgam: I'll need a piece of the eel to investigate further. Bring to me it's venom sac. You will find it north of Freeport, along the river. Hurry along now, playerName.",
    "You have received a quest!"
}
StartQuest(mySession, 107, quests[107][0].log)
else
    npcDialogue = "New orders have just come in."
    diagOptions = { "I am ready for my next assignment.", "I'll need a pint first." }
end
else
npcDialogue ="Captain Norgam: I need a little more time to finish a spell I am working on. Come back a little later."
end
elseif (GetPlayerFlags(mySession, "107") == "1") then
if (CheckQuestItem(mySession, items.EEL_VENOM_SAC, 1))
 then
if (choice:find("Perhaps")) then
npcDialogue = "Captain Norgam: Get it together! Bring me a Chichan Eel venom sac. You will find it north of Freeport, along the river."
elseif (choice:find("right")) then
multiDialogue = { "Captain Norgam: Once again, you survive another day.",
    "Captain Norgam: I must have this venom sac investigated immediately.",
    "Captain Norgam: As for your reward, take this Furious Defense Scroll and these Druben's Leggings.",
    "Captain Norgam: I've got my eye on you. You had better not be after my job. I'll have more work for you later.",
"You have given away an eel venom sac.",
"You have received a Furious Defense Scroll.",
"You have received Druben's Leggings.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 107, quests[107][1].xp, 110 )
TurnInItem(mySession, items.EEL_VENOM_SAC, 1)
GrantItem(mySession, items.FURIOUS_DEFENSE, 1)
GrantItem(mySession, items.DRUBENS_LEGGINGS, 1)
else
    npcDialogue = "Do you have the venom sac?"
    diagOptions = { "It is right here.", "Perhaps not. I'll get to it later." }
end
else
npcDialogue = "Captain Norgam: Get it together! Bring me a Chichan Eel venom sac. You will find it north of Freeport, along the river."
end
elseif (GetPlayerFlags(mySession, "110") == "0") then
if (level >=10) then
if (choice:find("Maybe")) then
multiDialogue = { "Captain Norgam: There is no later. There is only right now. I will happily dispatch some severe discipline if that will help you figure out what time it is."
 } 
elseif (choice:find("Captain")) then
multiDialogue = { "Captain Norgam: I must order you to take care of this next task.",
    "Captain Norgam: Guard Sareken is in need of assistance. Go find him at the north entrance of Freeport.",
    "Captain Norgam: I wouldn't normally bother with this, but I happen to own him a favor. He looked the other way once when I was conducting some...lets say, \"unsavory\" business with a former cadet.",
    "You have received a quest!"
}
StartQuest(mySession, 110, quests[110][0].log)
else
    npcDialogue = "Are you ready to serve again, playerName?"
    diagOptions = { "I am, Captain Norgam.", "Maybe later..." }
end
else
npcDialogue ="Captain Norgam: You'll need to develop your skills a little further before you can handle what I have in store for you."
end
elseif (GetPlayerFlags(mySession, "113") == "0") then
if (level >=13) then
if (choice:find("think")) then
multiDialogue = { "Captain Norgam: I can help you permanently relax if that's what you really need. I will happily show you the underside of my boot."
 } 
elseif (choice:find("Captain")) then
multiDialogue = { "Captain Norgam: The wizard Ilenar Crelwin has come from a long distance on business. He's had a bit of a mishap, and needs an assistant.",
    "Captain Norgam: Go speak with him at once. I must warn you, he can be a bit...demanding. See to it that his wishes are fulfilled.",
    "Captain Norgam: Do what he tells you, no questions asked.",
    "Captain Norgam: From here head west into the midroad, then south a little ways but take the very next eastern doorway.",
    "Captain Norgam: Pass through the Smiling Serpent, and find Ilenar in the Shining Shield Mercenaries Guild Hall.",
    "You have received a quest!"
}
StartQuest(mySession, 113, quests[113][0].log)
else
    npcDialogue = "I have new orders for you, playerName."
    diagOptions = { "How may I serve you, Captain Norgam?", "Oh? Well I think I need to relax first." }
end
else
npcDialogue ="Captain Norgam: Things aren't going so well. I'll have a report for you a little later..."
end
elseif (GetPlayerFlags(mySession, "115") == "5") then
multiDialogue = { "Captain Norgam: I've received word that the venerable Ilenar Crelwin is pleased with your work as of late. You have proved yourself a worthy agent, playerName.",
    "Captain Norgam: For all of your accomplishments, it is time you wore something befitting of your rank. I therefore award you with these Firesplint Leggings.",
    "Captain Norgam: I also award you with this Stomp Scroll. It's one of my favorites.",
    "Captain Norgam: Now look, I can't have you showing me up all the time like this. So why don't you go find your own work for awhile. Check in later though alright?",
"You have received a Stomp Scroll.",
"You have received Firesplint Leggings.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 115, quests[115][5].xp, 120 )
GrantItem(mySession, items.STOMP, 1)
GrantItem(mySession, items.FIRESPLINT_LEGGINGS, 1)
elseif (GetPlayerFlags(mySession, "120") == "0") then
if (level >=20) then
if (choice:find("command")) then
multiDialogue = { "Captain Norgam: You seem well built these days. But muscle wont always save you, sometimes it is your wit that will make the difference between life or death.",
    "Captain Norgam: I am curious to see how you handle this. Ilenar Crelwin has a final test for you. You should see to him at once.",
    "You have received a quest!"
}
StartQuest(mySession, 120, quests[120][0].log)
else
    npcDialogue = "I have an important mission for you."
    diagOptions = { "I am at your command." }
end
else
npcDialogue ="Captain Norgam: I have something very important coming up for you. Please check back with me later."
end
elseif (GetPlayerFlags(mySession, "120") == "9") then
multiDialogue = { "Captain Norgam: I have received word from Ilenar. He is very pleased.",
    "Captain Norgam: As payment for your services, you've earned a set of rewards. The first is a Bellow Scroll, a powerful roar ability that greatly increases your hit points. It comes with an Ensorcelled Greataxe.",
    "Captain Norgam: I also reward you with this Pillar of Might Scroll, a powerful taunt that also increases your defense. It comes with an Ensorcelled Longsword.",
"You have received a Bellow Scroll.",
"You have received an Ensorcelled Greataxe.",
"You have received a Pillar of Might Scroll.",
"You have received an Ensorcelled Longsword.",
    "You have finished a quest!",
"Captain Norgam: I guess you have become a decent warrior. Incredibly, you have learned all that I can teach you. It's time for you to go out and find what you truly desire.",
"Captain Norgam: Besides, I can't have you getting in my way around here. You're still rough around the edges, but don't ever let them see it. Farewell, playerName. I guess you weren't after my job after all."
}
CompleteQuest(mySession, 120, quests[120][9].xp, 121 )
GrantItem(mySession, items.BELLOW, 1)
GrantItem(mySession, items.ENSORCELLED_GREATAXE, 1)
GrantItem(mySession, items.PILLAR_OF_MIGHT, 1)
GrantItem(mySession, items.ENSORCELLED_LONGSWORD, 1)
  else
        npcDialogue =
"Captain Norgam: The last cadet that failed me cried so loudly due to his punishment that he could be heard all throughout the city. I most certainly do not tolerate deviation from my code."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end

