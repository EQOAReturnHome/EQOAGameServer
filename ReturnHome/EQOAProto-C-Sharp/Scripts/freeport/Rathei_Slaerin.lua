local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Necromancer(11) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "110103") == "0") then
if (choice:find("Corious")) then
multiDialogue = { "Rathei Slaerin: I see. We shall see if you can stomach what is yet to come. But I suppose you'll need some training first.",
    "Rathei Slaerin: I'll ask you then to equip whatever mighty weapon you may have and slay some dragonfly pests. Bring me 2 damaged dragonfly wings as proof of your accomplishments.",
    "Rathei Slaerin: After I receive the wings, I will reward you with an Life Tap Scroll, a necromancer's specialty.",
    "Rathei Slaerin: Run along now. Not that I expect a slithery creature such as yourself to return...",
    "You have received a quest!"
}
StartQuest(mySession, 110103, quests[110103][0].log)
else
    npcDialogue = "Everything dies. Are you next?"
    diagOptions = { "Corious Slaerin sent me to you." }
end
elseif (GetPlayerFlags(mySession, "110103") == "1") then
if (CheckQuestItem(mySession, items.DAMAGED_DRAGONFLY_WING, 2))
 then
if (choice:find("excuse")) then
npcDialogue = "Rathei Slaerin: If you can't follow my instructions, why shouldn't I just turn you into one of my pets right now? I need two damaged dragonfly wings."
elseif (choice:find("dragonfly")) then
multiDialogue = { "Rathei Slaerin: I see that you can follow through with my instructions. Perhaps I won't make you my next undead pet.",
    "Rathei Slaerin: As I said, you will be rewarded. Take this scroll and study it well. The skill is trivial compared to my abilities, but it's a good start for you, playerName.",
    "You have finished a quest!",
"You have given away a damaged dragonfly wing.",
"You have given away a damaged dragonfly wing.",
"You have received a Life Tap Scroll.",
"Rathei Slaerin: I'm just putting the finishing touches on a reinforcement of undead spell. Check back with me in few moments."
}
CompleteQuest(mySession, 110103, quests[110103][1].xp, 110104 )
TurnInItem(mySession, items.DAMAGED_DRAGONFLY_WING, 2)
GrantItem(mySession, items.LIFE_TAP, 1)
else
    npcDialogue = "Ah, you somehow managed."
    diagOptions = { "I have the damaged dragonfly wing's.", "Oh, excuse me?" }
end
else
npcDialogue = "Rathei Slaerin: If you can't follow my instructions, why shouldn't I just turn you into one of my pets right now? I need two damaged dragonfly wings."
end
elseif (GetPlayerFlags(mySession, "110104") == "0") then
if (level >=4) then
if (choice:find("Show")) then
multiDialogue = { "Rathei Slaerin: I can't really take you seriously in those rags. You'll need to wear a black robe if you wish to call yourself a necromancer.",
    "Rathei Slaerin: Fortunately, I have one ready for you, but you must first aid in gathering the supplies to craft the next one.",
"Rathei Slaerin: What I require from you is a plain robe, a silk cord, and a sliver of snake meat.",
"Rathei Slaerin: Purchase a plain robe from Merchant Yulia. She is near the Academy of Arcane Science in the west part of the city.",
"Rathei Slaerin: Merchant Yesam also has silk cord's for sale. I will need one. He is near the blacksmith at the north side of Freeport.",
"Rathei Slaerin: I'll also need a sliver of snake meat. Perhaps the guards wouldn't mind if you collected the meat from the snakes outside.",
"Rathei Slaerin: Now then, don't come back until you have everything in proper order. I'll just be here contemplating your death if you do.",
    "You have received a quest!"
}
StartQuest(mySession, 110104, quests[110104][0].log)
else
    npcDialogue = "The darkness calls for you."
    diagOptions = { "Show me." }
end
else
npcDialogue ="Rathei Slaerin: You are too weak and feeble to be useful. Be gone with you, and don't come back until you are stronger."
end
elseif (GetPlayerFlags(mySession, "110104") == "1") then
if (CheckQuestItem(mySession, items.PLAIN_ROBE, 1)
and CheckQuestItem(mySession, items.SILK_CORD, 1)
and CheckQuestItem(mySession, items.SLIVER_OF_SNAKE_MEAT, 1))
 then
multiDialogue = { "Rathei Slaerin: This is good. Such diligence, and enthusiasm. Perhaps you will be useful after all.",
    "Rathei Slaerin: And here is that fancy black robe you have most certainly earned. I'm sure it will look great on you.",
    "Rathei Slaerin: What was your name again? Oh... playerName. I'll keep you in mind for my future needs.",
"You have given away a plain robe.",
"You have given away a silk cord.",
"You have given away a sliver of snake meat.",
"You have received a black robe.",
    "You have finished a quest!",
"Rathei Slaerin: You will find as you use your weapons and armor in battle, they will begin to wear down and lose their effectiveness.",
"Rathei Slaerin: If you visit a blacksmith they will repair your weapons and armor for a price.",
"Rathei Slaerin: You can find Blacksmith Coalbrick at the Bazaar on the north side of the city.",
"Rathei Slaerin: I'll have a new task for you in a moment. Check back when you are properly equipped."
}
CompleteQuest(mySession, 110104, quests[110104][1].xp, 110105 )
TurnInItem(mySession, items.PLAIN_ROBE, 1)
TurnInItem(mySession, items.SILK_CORD, 1)
TurnInItem(mySession, items.SLIVER_OF_SNAKE_MEAT, 1)
GrantItem(mySession, items.BLACK_ROBE, 1)
else
npcDialogue = "Rathei Slaerin: If you can't follow my instructions, why shouldn't I just turn you into one of my pets right now? I need 1 plain robe, 1 silk cord, and 1 sliver of snake meat."
end
elseif (GetPlayerFlags(mySession, "110105") == "0") then
if (level >=5) then
if (choice:find("darkness")) then
multiDialogue = { "Rathei Slaerin: This next task is quite deadly, but perhaps a taste of death is exactly what you need...",
    "Rathei Slaerin: The House Slaerin depends on caravan shipments from the west for a great deal of supplies to operate.",
    "Rathei Slaerin: Just recently, a band of highwaymen have been hijacking caravans into Freeport in both the deserts and grasslands.",
    "Rathei Slaerin: The roads are plagued with highwaymen. This cannot be allowed to happen."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Will you use the knowledge we have taught you thus far to recover the stolen goods and bring these criminals to justice?"
    diagOptions = { "I will, master.", "Sorry, I can't help you with this." }
elseif (choice:find("can\'t")) then
multiDialogue = { "Rathei Slaerin: Do you feel this? That feeling of life draining away from your very blood? This is but a taste of my vengeance if you do not obey my every command."
 } 
elseif (choice:find("master")) then
multiDialogue = { "Rathei Slaerin: We would be in your debt. However this is no challenge to take lightly or alone.",
    "Rathei Slaerin: For this you should seek the company of other adventurers in Freeport. The Smiling Serpent tavern is a good place to start.",
    "Rathei Slaerin: I am sure that we are not the only guild being made to suffer by these criminals, and that there are others who will help.",
    "Rathei Slaerin: Go now playerName, and return when you've recovered the stolen goods.",
    "You have received a quest!"
}
StartQuest(mySession, 110105, quests[110105][0].log)
else
    npcDialogue = "Back for more punishment?"
    diagOptions = { "Please show me the darkness." }
end
else
npcDialogue ="Rathei Slaerin: You are too weak and feeble to be useful. Be gone with you, and don't come back until you are stronger."
end
elseif (GetPlayerFlags(mySession, "110105") == "1") then
if (CheckQuestItem(mySession, items.STOLEN_GOODS, 1))
 then
if (choice:find("quite")) then
npcDialogue = "Rathei Slaerin: If you can't follow my instructions, why shouldn't I just turn you into one of my pets right now? Find the highwaymen and retrieve from them the stolen goods."
elseif (choice:find("have")) then
multiDialogue = { "Rathei Slaerin: Ah, escaped death again? To be honest, I wasn't sure if I would ever see you again but here you are proving yourself ever more valuable...",
    "Rathei Slaerin: It is quite thrilling watching you grow in power. Perhaps someday, you will be the master.",
    "Rathei Slaerin: For this, I am proud to have you as a true Necromancer of The House Slaerin. I award you with this Rabid Infection Scroll.",
    "Rathei Slaerin: I'll be watching you from a distance for now, but I believe Corious may need your assistance soon.",
"You have given away the stolen goods.",
"You have received a Rabid Infection Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 110105, quests[110105][1].xp, 110107 )
TurnInItem(mySession, items.STOLEN_GOODS, 1)
GrantItem(mySession, items.RABID_INFECTION, 1)
else
    npcDialogue = "Have you yet retrieved the stolen goods?"
    diagOptions = { "I have. Here they are...", "Not quite yet." }
end
else
npcDialogue = "Rathei Slaerin: If you can't follow my instructions, why shouldn't I just turn you into one of my pets right now? Find the highwaymen and retrieve from them the stolen goods."
end
  else
        npcDialogue =
"Rathei Slaerin: You are too weak and feeble to be useful for my purposes. Be gone with you, and don't come back unless you wish to become one of my pets."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

