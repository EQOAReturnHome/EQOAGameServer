local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Enchanter(12) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "120103") == "0") then
if (choice:find("Azlynn")) then
multiDialogue = { "Opanheim: A yes, fresh talent. It will be my pleasure to see just what you are made of. But I suppose you'll need some training first.",
    "Opanheim: I'll ask you then to equip whatever mighty weapon you may have and slay beetles. Bring me 2 beetle carapace fragments as proof of your accomplishments.",
    "Opanheim: After I receive the fragments, I will reward you with an Crawling Skin Scroll, a enchanter's specialty.",
    "Opanheim: Run along now. I'll be watching you with great interest.",
    "You have received a quest!"
}
StartQuest(mySession, 120103, quests[120103][0].log)
else
    npcDialogue = "Hello there young one."
    diagOptions = { "Azlynn sent me to you." }
end
elseif (GetPlayerFlags(mySession, "120103") == "1") then
if (CheckQuestItem(mySession, items.BEETLE_CARAPACE_FRAGMENT, 2))
 then
if (choice:find("excuse")) then
npcDialogue = "Opanheim: It's important that you bring me what I have asked from you. I need two beetle carapace fragments."
elseif (choice:find("pieces")) then
multiDialogue = { "Opanheim: I find your work to be quite satisfactory. Maybe I will play with you a bit longer.",
    "Opanheim: As I said, you will be rewarded. Take this scroll and study it well. The skill is trivial compared to my abilities, but it's a good start for you, playerName.",
    "You have finished a quest!",
"You have given away a beetle carapace fragment.",
"You have given away a beetle carapace fragment.",
"You have received a Crawling Skin Scroll.",
"Opanheim: I'm just putting the finishing touches on an invisibility spell. Check back with me in few moments little one."
}
CompleteQuest(mySession, 120103, quests[120103][1].xp, 120104 )
TurnInItem(mySession, items.BEETLE_CARAPACE_FRAGMENT, 2)
GrantItem(mySession, items.CRAWLING_SKIN, 1)
else
    npcDialogue = "Ah, my little friend has returned."
    diagOptions = { "I have pieces of beetle.", "Oh, excuse me?" }
end
else
npcDialogue = "Opanheim: It's important that you bring me what I have asked from you. I need two beetle carapace fragments."
end
elseif (GetPlayerFlags(mySession, "120104") == "0") then
if (level >=4) then
if (choice:find("enchantments")) then
multiDialogue = { "Opanheim: In order to continue your studies, you must at least wear a fancy Yellow Enchanter's Robe if you wish to call yourself an enchanter.",
    "Opanheim: Fortunately, I have one ready for you, but you must first aid in gathering the supplies to craft the next one.",
    "Opanheim: This small test of your wit, making your way around the city, will prepare you for the tasks to come.",
"Opanheim: What I require is a plain robe, a silk cord, and a ruined spider fur.",
"Opanheim: Purchase a plain robe from Merchant  Yulia.",
"Opanheim: Merchant  Shohan also has silk cord's for sale. I will need one. He is near the blacksmith at the north side of Freeport.",
"Opanheim: I'll also need a ruined spider fur. I'm sure the guards wouldn't mind if you collected the fur from the spiders outside.",
"Opanheim: Now then, don't come back until you have everything in proper order.",
    "You have received a quest!"
}
StartQuest(mySession, 120104, quests[120104][0].log)
else
    npcDialogue = "My little magic user has returned."
    diagOptions = { "Yes, I seek more enchantments." }
end
else
npcDialogue ="Opanheim: I have to take care of something, however you should check back with me soon."
end
elseif (GetPlayerFlags(mySession, "120104") == "1") then
if (CheckQuestItem(mySession, items.PLAIN_ROBE, 1)
and CheckQuestItem(mySession, items.SILK_CORD, 1)
and CheckQuestItem(mySession, items.RUINED_SPIDER_FUR, 1))
 then
multiDialogue = { "Opanheim: I am quite pleased with your work. Such diligence, and enthusiasm. This is going to be great for my...well, nevermind that.",
    "Opanheim: And here is that fancy Yellow Enchanter's Robe you have most certainly earned. I'm sure it will look great on you.",
    "Opanheim: What was your name again? Oh... playerName. I won't be forgetting you any time soon.",
"You have given away a plain robe.",
"You have given away a silk cord.",
"You have given away a ruined spider fur.",
"You have given away a ruined spider fur.",
"You have received a Yellow Enchanter's Robe.",
    "You have finished a quest!",
"Opanheim: You will find as you use your weapons and armor in battle, they will begin to wear down and lose their effectiveness.",
"Opanheim: If you visit a blacksmith they will repair your weapons and armor for a price.",
"Opanheim: You can find Blacksmith Coalbrick at the Bazaar on the north side of the city.",
"Opanheim: I'll have a new task for you in a moment. Check back when you are properly equipped."
}
CompleteQuest(mySession, 120104, quests[120104][1].xp, 120105 )
TurnInItem(mySession, items.PLAIN_ROBE, 1)
TurnInItem(mySession, items.SILK_CORD, 1)
TurnInItem(mySession, items.RUINED_SPIDER_FUR, 2)
GrantItem(mySession, items.YELLOW_ENCHANTERS_ROBE, 1)
else
npcDialogue = "Opanheim: I want to see you prove yourself by gathering these items. I need 1 plain robe, 1 silk cord, and 2 ruined spider furs."
end
elseif (GetPlayerFlags(mySession, "120105") == "0") then
if (level >=5) then
if (choice:find("please")) then
multiDialogue = { "Opanheim: This next task is so deadly, I shutter to send a poor flower such as yourself to your doom. But a task is a task...",
    "Opanheim: The Academy of Arcane Science depends on caravan shipments from the west for a great deal of supplies to operate.",
    "Opanheim: Just recently, a band of highwaymen have been hijacking caravans into Freeport in both the deserts and grasslands.",
    "Opanheim: The roads are plagued with highwaymen. This cannot be allowed to happen."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "Will you use the knowledge we have taught you thus far to recover the stolen goods and bring these criminals to justice?"
    diagOptions = { "I will, Opanheim.", "Sorry, I can't help you with this." }
elseif (choice:find("can\'t")) then
multiDialogue = { "Opanheim: I understand how you feel. Some days, I can barely get out of bed. Please, take some time for yourself and when you feel better, come and see me. I don't want my little flower to wilt."
 } 
elseif (choice:find("Opanheim")) then
multiDialogue = { "Opanheim: I would be in your debt. However this is no challenge to take lightly or alone.",
    "Opanheim: For this you should seek the company of other adventurers in Freeport. The Smiling Serpent tavern is a good place to start.",
    "Opanheim: I am sure that we are not the only guild being made to suffer by these criminals, and that there are others who will help.",
    "Opanheim: Go now playerName, and return when you've recovered the stolen goods.",
    "You have received a quest!"
}
StartQuest(mySession, 120105, quests[120105][0].log)
else
    npcDialogue = "Shall we continue with your studies?"
    diagOptions = { "Yes please." }
end
else
npcDialogue ="Opanheim: I have something for you, but you aren't yet ready. Go slay a few critters and check back with me."
end
elseif (GetPlayerFlags(mySession, "120105") == "1") then
if (CheckQuestItem(mySession, items.STOLEN_GOODS, 1))
 then
if (choice:find("quite")) then
npcDialogue = "Opanheim: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
elseif (choice:find("have")) then
multiDialogue = { "Opanheim: I am...quite surprised that you made it. To be honest, I wasn't sure if I would ever see you again but here you are proving yourself ever more valuable...",
    "Opanheim: It is quite thrilling watching you grow in power.",
    "Opanheim: For this, I am proud to have you as a true Enchanter of The Academy of Arcane Science. I award you with this Heavy Arms Scroll.",
    "Opanheim: I'll be watching you from a distance for now, but I believe Azlynn may need your assistance soon.",
"You have given away the stolen goods.",
"You have received a Heavy Arms Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 120105, quests[120105][1].xp, 120107 )
TurnInItem(mySession, items.STOLEN_GOODS, 1)
GrantItem(mySession, items.HEAVY_ARMS, 1)
else
    npcDialogue = "Be a dear and tell me you have retrieved the stolen goods?"
    diagOptions = { "I have. Here they are...", "Not quite yet." }
end
else
npcDialogue = "Opanheim: Get your head together, and focus on the task at hand. Find the highwaymen and retrieve from them the stolen goods."
end
  else
        npcDialogue =
"Opanheim: Hmm, you don't look like one of my playthings. I am not interested in you at all. Begone with you."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end

