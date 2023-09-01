local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Shadowknight(3) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "30103") == "0") then
if (choice:find("Crimsonhand")) then
multiDialogue = { "Stolfson Krieger: Very well, we shall start killing things at once. If you survive, you'll have a heart of darkness and despair when I am done with you.",
    "Stolfson Krieger: Equip whatever pathetic weapon you may have and slay orc pawns. Bring me 2 slashed pawn belts as proof of your deed.",
    "Stolfson Krieger: After I receive the belts, I will reward you with the Malice Scroll, a shadowknight's specialty.",
    "Stolfson Krieger: Get to it. I don't like to be kept waiting.",
    "You have received a quest!"
}
StartQuest(mySession, 30103, quests[30103][0].log)
else
    npcDialogue = "You must be here for your battle training."
    diagOptions = { "Malethai Crimsonhand sent me to you." }
end
elseif (GetPlayerFlags(mySession, "30103") == "1") then
if (CheckQuestItem(mySession, items.SLASHED_PAWN_BELT, 2))
 then
if (choice:find("perhaps")) then
npcDialogue = "Stolfson Krieger: How long must I wait for you? I need two slashed pawn belts."
elseif (choice:find("slashed")) then
multiDialogue = { "Stolfson Krieger: I see that you can follow through with my instructions. Perhaps I will spare your life a little longer.",
    "Stolfson Krieger: As I said, you will be rewarded. Take this scroll and study it well. The skill is trivial compared to my abilities, but it's a good start for you, playerName.",
    "You have finished a quest!",
"You have given away a slashed pawn belt.",
"You have given away a slashed pawn belt.",
"You have received a Malice Scroll.",
"Stolfson Krieger: I have some discipline to attend to at this moment. One of the other apprentices have failed me for the last time. Check back with me shortly."
}
CompleteQuest(mySession, 30103, quests[30103][1].xp, 30104 )
TurnInItem(mySession, items.SLASHED_PAWN_BELT, 2)
GrantItem(mySession, items.MALICE, 1)
else
    npcDialogue = "Took you long enough."
    diagOptions = { "I have the slashed pawn belts.", "And perhaps I need a little more time..." }
end
else
npcDialogue = "Stolfson Krieger: How long must I wait for you? I need two slashed pawn belts."
end
elseif (GetPlayerFlags(mySession, "30104") == "0") then
if (level >=4) then
if (choice:find("course")) then
multiDialogue = { "Stolfson Krieger: You wont be killing much with that archaic weapon. You must at least equip an Iron Hatchet if you wish to call yourself a shadowknight.",
    "Stolfson Krieger: Fortunately, I have one ready for you, but you must first aid in gathering the supplies to craft the next one.",
"Stolfson Krieger: What I require from you is an iron ore, a wooden staff, and a sliver of snake meat.",
"Stolfson Krieger: You will need to purchase an iron ore from Merchant Shohan.",
"Stolfson Krieger: Merchant Shohan also has wooden staffs for sale. I will need one. He is near the blacksmith at the north side of Freeport.",
"Stolfson Krieger: I'll also need a sliver of snake meat. Perhaps the guards wouldn't mind if you collected the meat from the snakes outside.",
"Stolfson Krieger: Now then, don't come back until you have everything in proper order. I'll just be here contemplating your punishment if you do.",
    "You have received a quest!"
}
StartQuest(mySession, 30104, quests[30104][0].log)
else
    npcDialogue = "Ah, the killer of young orcs has returned. Excellent."
    diagOptions = { "Of course, and now I seek my next task." }
end
else
npcDialogue ="Stolfson Krieger: Why are you bothering me with this? You haven't the strength for this task."
end
elseif (GetPlayerFlags(mySession, "30104") == "1") then
if (CheckQuestItem(mySession, items.IRON_ORE, 1)
and CheckQuestItem(mySession, items.WOODEN_STAFF, 1)
and CheckQuestItem(mySession, items.SLIVER_OF_SNAKE_MEAT, 1))
 then
multiDialogue = { "Stolfson Krieger: You\'re not as thick skulled as most of the \"talent\" that comes through here. Maybe you can be of actual use, I suspect.",
    "Stolfson Krieger: And here is that fancy Iron Hatchet you have most certainly earned. I'm sure it will suit you well.",
    "Stolfson Krieger: What was your name again? Oh yes... playerName. I'll keep you in mind for my future needs.",
"You have given away an iron ore.",
"You have given away a wooden staff.",
"You have given away a sliver of snake meat.",
"You have received an Iron Hatchet.",
    "You have finished a quest!",
"Stolfson Krieger: You will find as you use your weapons and armor in battle, they will begin to wear down and lose their effectiveness.",
"Stolfson Krieger: If you visit a blacksmith they will repair your weapons and armor for a price.",
"Stolfson Krieger: You can find Blacksmith Coalbrick at the Bazaar on the north side of the city.",
"Stolfson Krieger: I'll have a new task for you shortly. Check back when you are properly equipped."
}
CompleteQuest(mySession, 30104, quests[30104][1].xp, 30105 )
TurnInItem(mySession, items.IRON_ORE, 1)
TurnInItem(mySession, items.WOODEN_STAFF, 1)
TurnInItem(mySession, items.SLIVER_OF_SNAKE_MEAT, 1)
GrantItem(mySession, items.IRON_HATCHET, 1)
else
npcDialogue = "Stolfson Krieger: How long must I wait for you? I need 1 iron ore, 1 wooden staff, and 1 sliver of snake meat."
end
elseif (GetPlayerFlags(mySession, "30105") == "0") then
if (level >=5) then
if (choice:find("darkness")) then
multiDialogue = { "Stolfson Krieger: This next task will be quite difficult. It will be a good test of your ability to hunt a deadly target.",
    "Stolfson Krieger: The Shining Shield Mercenaries depends on caravan shipments from the west for a great deal of supplies to operate.",
    "Stolfson Krieger: Just recently, a band of highwaymen have been hijacking caravans into Freeport in both the deserts and grasslands.",
    "Stolfson Krieger: The roads are plagued with highwaymen. This cannot be allowed to happen."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Will you use the skills we have taught you thus far to recover the stolen goods and bring these criminals to justice?"
    diagOptions = { "I will, master.", "Sorry, I can't help you with this." }
elseif (choice:find("can\'t")) then
multiDialogue = { "Stolfson Krieger: Do you feel this? That feeling of life draining away from your very blood? This is but a taste of my vengeance if you do not obey my every command."
 } 
elseif (choice:find("master")) then
multiDialogue = { "Stolfson Krieger: One day, I may order you to sacrifice for our order, but not without purpose. That day for such a sacrifice has not come yet.",
    "Stolfson Krieger: For this task you should seek the company of other adventurers in Freeport. The Smiling Serpent tavern is a good place to start.",
    "Stolfson Krieger: I am sure that we are not the only guild being made to suffer by these criminals, and that there are others who will help.",
    "Stolfson Krieger: Go now playerName, and return when you've recovered the stolen goods.",
    "You have received a quest!"
}
StartQuest(mySession, 30105, quests[30105][0].log)
else
    npcDialogue = "The young dark knight returns..."
    diagOptions = { "Please show me the darkness." }
end
else
npcDialogue ="Stolfson Krieger: Why are you bothering me with this? You haven't the strength for this task."
end
elseif (GetPlayerFlags(mySession, "30105") == "1") then
if (CheckQuestItem(mySession, items.STOLEN_GOODS, 1))
 then
if (choice:find("quite")) then
npcDialogue = "Stolfson Krieger: How long must I wait for you? Find the highwaymen and retrieve from them the stolen goods."
elseif (choice:find("course")) then
multiDialogue = { "Stolfson Krieger: Ah, escaped death again? To be honest, I wasn't sure if I would ever see you again but here you are proving yourself ever more valuable...",
    "Stolfson Krieger: It is quite thrilling watching you grow in power. Perhaps someday, you will be the master.",
    "Stolfson Krieger: For this, I will now call you a true Shadowknight of The Shining Shield Mercenaries. I award you with this Harm Touch Scroll.",
    "Stolfson Krieger: I'll be watching you from a distance for now, but I believe Malethai may need your assistance soon.",
"You have given away the stolen goods.",
"You have received a Harm Touch Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 30105, quests[30105][1].xp, 30107 )
TurnInItem(mySession, items.STOLEN_GOODS, 1)
GrantItem(mySession, items.HARM_TOUCH, 1)
else
    npcDialogue = "Have you yet retrieved the stolen goods?"
    diagOptions = { "Of course I have. Here they are...", "Not quite yet." }
end
else
npcDialogue = "Stolfson Krieger: How long must I wait for you? Find the highwaymen and retrieve from them the stolen goods."
end
  else
        npcDialogue =
"Stolfson Krieger: Malice must be channeled to order to shape the mind of a young shadowknight. Without it, they just become weak, frustrated young persons running around in dark armor."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end

