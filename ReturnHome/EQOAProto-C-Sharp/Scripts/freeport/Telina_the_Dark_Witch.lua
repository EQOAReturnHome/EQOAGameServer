local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Enchanter(12) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "120120") == "2") then
if (choice:find("future")) then
multiDialogue = { "Telina the Dark Witch: Yes my dear. I can see deeply into your soul... Your future will be lost in adventure, with dear friends and real danger... But not until you have first served me and my great eye."
 } 
elseif (choice:find("Wilkenson")) then
multiDialogue = { "Telina the Dark Witch: Lose an important mark, did we? No worries deary. Madame Telina will help you. I can see things from a distance, and things that have yet to occur...",
    "Telina the Dark Witch: I believe the mark you are looking for is in the hands of a Troll. He travels with nasehir cutthroats.",
    "Telina the Dark Witch: From the docks, travel southwest to search for the nasehir camps. They are not far from the docks. There is only one troll with them.",
    "Telina the Dark Witch: His name is Roj Eir Sew' Eil. Take the mark from him and return to me.",
    "Telina the Dark Witch: I'll be watching you now, wherever you go.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120120, quests[120120][2].log)
else
    npcDialogue = "What a delightful sight the spirits have brought me."
    diagOptions = { "Agent Wilkenson said you have a lead on the real mark.", "Oh, can you tell me my future?" }
end
elseif (GetPlayerFlags(mySession, "120120") == "3") then
if (CheckQuestItem(mySession, items.MARK_OF_LOUOTH, 1))
 then
multiDialogue = { "Telina the Dark Witch: I saw what you did. There is no need to tell me. I watched you every step of the way.",
    "Telina the Dark Witch: This is the true mark you were looking for. I am going to study it for awhile though. Don't worry, I will make sure it is returned to William Nothard.",
    "Telina the Dark Witch: It is now your turn to serve me. You didn't think you would get my help for free, did you?",
    "Telina the Dark Witch: You will now travel south along the coast for a long distance. Far to the south is Muniel's Tea Garden. Be alert young one, this will be very dangerous.",
    "Telina the Dark Witch: Swim to the island off the coast of Muniel's Tea Garden. There are several skeleton pirates on this small expanse of land.",
    "Telina the Dark Witch: Search the island for sand covered chests. Slay the skeletons and search the chests for the Chiseled Great Axe of Doom. Return to me with this axe.",
    "Telina the Dark Witch: Remember, I can see everything you do. From this condition I will not release you, until my wishes are fulfilled. Now go.",
"You have given away the Mark of Louoth.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120120, quests[120120][3].log)
TurnInItem(mySession, items.MARK_OF_LOUOTH, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the Mark of Louoth."
end
elseif (GetPlayerFlags(mySession, "120120") == "4") then
if (CheckQuestItem(mySession, items.CHISELED_GREAT_AXE_OF_DOOM, 1))
 then
multiDialogue = { "Telina the Dark Witch: Again, I saw what you did. There is no need to tell me.",
    "Telina the Dark Witch: Give me the axe. I must peer into its metals and see into its past...",
    "Telina the Dark Witch: ...",
    "Telina the Dark Witch: This axe brings me a vision. I see a deep valley. I see tall, pointed mountains. Next to the mountain, is an obelisk in a small pool of water. In the water is a treasure...",
    "Telina the Dark Witch: I now know where this is... You will go and fetch for me this next treasure.",
    "Telina the Dark Witch: From the west gate of Freeport, follow the road south through the desert until you come to a gypsy village.",
    "Telina the Dark Witch: From the center of this camp, look back towards the northwest and you will see Razor Back Fang.",
    "Telina the Dark Witch: Razor Back Fang is a large mountain in the shape of two fangs with a valley in the middle.",
    "Telina the Dark Witch: Climb to the top of fang mountains to avoid all the undead in the valley. You will see a small obelisk in a pool of water.",
    "Telina the Dark Witch: Explore the water around the obelisk and locate the waterlogged chest.",
    "Telina the Dark Witch: Bring me the treasure you find inside. Go now.",
"You have given away the Chiseled Great Axe of Doom.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120120, quests[120120][4].log)
TurnInItem(mySession, items.CHISELED_GREAT_AXE_OF_DOOM, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the Chiseled Great Axe of Doom."
end
elseif (GetPlayerFlags(mySession, "120120") == "5") then
if (CheckQuestItem(mySession, items.ETCHED_HELMET_OF_GREATNESS, 1))
 then
multiDialogue = { "Telina the Dark Witch: This is it. This is the treasure that my heart has long desired. I will now have the means to defeat my true enemy. His name is...Oh, nevermind that.",
    "Telina the Dark Witch: You have served me well. I hereby release you from my watchful eye. For now, anyway. I wont forget that charming face though.",
    "Telina the Dark Witch: Take this note to Azlynn. Your service to me is now fulfilled.",
"You have given away the Etched Helmet of Greatness.",
"You have received a note from Telina.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120120, quests[120120][5].log)
TurnInItem(mySession, items.ETCHED_HELMET_OF_GREATNESS, 1)
GrantItem(mySession, items.NOTE_FROM_TELINA, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the treasure."
end
  else
        npcDialogue =
"Telina the Dark Witch: Now that you have seen me, my watchful eye will be able to see you, wherever you go, whatever you do, for as long as I wish it. You will have to fulfill my task to be freed of my watchful eye."
    end
------
--Necromancer(11) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "110120") == "2") then
if (choice:find("future")) then
multiDialogue = { "Telina the Dark Witch: Yes my dear. I can see deeply into your soul... Your future will be lost in darkness, with great power and real danger... But not until you have first served me and my great eye."
 } 
elseif (choice:find("Wilkenson")) then
multiDialogue = { "Telina the Dark Witch: Lose an important mark, did we? No worries deary. Madame Telina will help you. I can see things from a distance, and things that have yet to occur...",
    "Telina the Dark Witch: I believe the mark you are looking for is in the hands of a Troll. He travels with nasehir cutthroats.",
    "Telina the Dark Witch: From the docks, travel southwest to search for the nasehir camps. They are not far from the docks. There is only one troll with them.",
    "Telina the Dark Witch: His name is Roj Eir Sew' Eil. Take the mark from him and return to me.",
    "Telina the Dark Witch: I'll be watching you now, wherever you go.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110120, quests[110120][2].log)
else
    npcDialogue = "What a delightful sight the spirits have brought me."
    diagOptions = { "Agent Wilkenson said you have a lead on the real mark.", "Oh, can you tell me my future?" }
end
elseif (GetPlayerFlags(mySession, "110120") == "3") then
if (CheckQuestItem(mySession, items.MARK_OF_LOUOTH, 1))
 then
multiDialogue = { "Telina the Dark Witch: I saw what you did. There is no need to tell me. I watched you every step of the way.",
    "Telina the Dark Witch: This is the true mark you were looking for. I am going to study it for awhile though. Don't worry, I will make sure it is returned to William Nothard.",
    "Telina the Dark Witch: It is now your turn to serve me. You didn't think you would get my help for free, did you?",
    "Telina the Dark Witch: You will now travel south along the coast for a long distance. Far to the south is Muniel's Tea Garden. Be alert young one, this will be very dangerous.",
    "Telina the Dark Witch: Swim to the island off the coast of Muniel's Tea Garden. There are several skeleton pirates on this small expanse of land.",
    "Telina the Dark Witch: Search the island for sand covered chests. Slay the skeletons and search the chests for the Chiseled Great Axe of Doom. Return to me with this axe.",
    "Telina the Dark Witch: Remember, I can see everything you do. From this condition I will not release you, until my wishes are fulfilled. Now go.",
"You have given away the Mark of Louoth.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110120, quests[110120][3].log)
TurnInItem(mySession, items.MARK_OF_LOUOTH, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the Mark of Louoth."
end
elseif (GetPlayerFlags(mySession, "110120") == "4") then
if (CheckQuestItem(mySession, items.CHISELED_GREAT_AXE_OF_DOOM, 1))
 then
multiDialogue = { "Telina the Dark Witch: Again, I saw what you did. There is no need to tell me.",
    "Telina the Dark Witch: Give me the axe. I must peer into its metals and see into its past...",
    "Telina the Dark Witch: ...",
    "Telina the Dark Witch: This axe brings me a vision. I see a deep valley. I see tall, pointed mountains. Next to the mountain, is an obelisk in a small pool of water. In the water is a treasure...",
    "Telina the Dark Witch: I now know where this is... You will go and fetch for me this next treasure.",
    "Telina the Dark Witch: From the west gate of Freeport, follow the road south through the desert until you come to a gypsy village.",
    "Telina the Dark Witch: From the center of this camp, look back towards the northwest and you will see Razor Back Fang.",
    "Telina the Dark Witch: Razor Back Fang is a large mountain in the shape of two fangs with a valley in the middle.",
    "Telina the Dark Witch: Climb to the top of fang mountains to avoid all the undead in the valley. You will see a small obelisk in a pool of water.",
    "Telina the Dark Witch: Explore the water around the obelisk and locate the waterlogged chest.",
    "Telina the Dark Witch: Bring me the treasure you find inside. Go now.",
"You have given away the Chiseled Great Axe of Doom.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110120, quests[110120][4].log)
TurnInItem(mySession, items.CHISELED_GREAT_AXE_OF_DOOM, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the Chiseled Great Axe of Doom."
end
elseif (GetPlayerFlags(mySession, "110120") == "5") then
if (CheckQuestItem(mySession, items.ETCHED_HELMET_OF_GREATNESS, 1))
 then
multiDialogue = { "Telina the Dark Witch: This is it. This is the treasure that my heart has long desired. I will now have the means to defeat my true enemy. His name is...Oh, nevermind that.",
    "Telina the Dark Witch: You have served me well. I hereby release you from my watchful eye. For now, anyway. I wont forget that charming face though.",
    "Telina the Dark Witch: Take this note to Corious Slaerin. Your service to me is now fulfilled.",
"You have given away the Etched Helmet of Greatness.",
"You have received a note from Telina.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110120, quests[110120][5].log)
TurnInItem(mySession, items.ETCHED_HELMET_OF_GREATNESS, 1)
GrantItem(mySession, items.NOTE_FROM_TELINA, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the treasure."
end
  else
        npcDialogue =
"Telina the Dark Witch: Now that you have seen me, my watchful eye will be able to see you, wherever you go, whatever you do, for as long as I wish it. You will have to fulfill my task to be freed of my watchful eye."
    end
------
--Shadowknight(3) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "30120") == "2") then
if (choice:find("future")) then
multiDialogue = { "Telina the Dark Witch: Yes my dear. I can see deeply into your soul... Your future will be lost in darkness, with great power and real danger... But not until you have first served me and my great eye."
 } 
elseif (choice:find("Wilkenson")) then
multiDialogue = { "Telina the Dark Witch: Lose an important mark, did we? No worries deary. Madame Telina will help you. I can see things from a distance, and things that have yet to occur...",
    "Telina the Dark Witch: I believe the mark you are looking for is in the hands of a Troll. He travels with nasehir cutthroats.",
    "Telina the Dark Witch: From the docks, travel southwest to search for the nasehir camps. They are not far from the docks. There is only one troll with them.",
    "Telina the Dark Witch: His name is Roj Eir Sew' Eil. Take the mark from him and return to me.",
    "Telina the Dark Witch: I'll be watching you now, wherever you go.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 30120, quests[30120][2].log)
else
    npcDialogue = "What a delightful sight the spirits have brought me."
    diagOptions = { "Agent Wilkenson said you have a lead on the real mark.", "Oh, can you tell me my future?" }
end
elseif (GetPlayerFlags(mySession, "30120") == "3") then
if (CheckQuestItem(mySession, items.MARK_OF_LOUOTH, 1))
 then
multiDialogue = { "Telina the Dark Witch: I saw what you did. There is no need to tell me. I watched you every step of the way.",
    "Telina the Dark Witch: This is the true mark you were looking for. I am going to study it for awhile though. Don't worry, I will make sure it is returned to William Nothard.",
    "Telina the Dark Witch: It is now your turn to serve me. You didn't think you would get my help for free, did you?",
    "Telina the Dark Witch: You will now travel south along the coast for a long distance. Far to the south is Muniel's Tea Garden. Be alert young one, this will be very dangerous.",
    "Telina the Dark Witch: Swim to the island off the coast of Muniel's Tea Garden. There are several skeleton pirates on this small expanse of land.",
    "Telina the Dark Witch: Search the island for sand covered chests. Slay the skeletons and search the chests for the Chiseled Great Axe of Doom. Return to me with this axe.",
    "Telina the Dark Witch: Remember, I can see everything you do. From this condition I will not release you, until my wishes are fulfilled. Now go.",
"You have given away the Mark of Louoth.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 30120, quests[30120][3].log)
TurnInItem(mySession, items.MARK_OF_LOUOTH, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the Mark of Louoth."
end
elseif (GetPlayerFlags(mySession, "30120") == "4") then
if (CheckQuestItem(mySession, items.CHISELED_GREAT_AXE_OF_DOOM, 1))
 then
multiDialogue = { "Telina the Dark Witch: Again, I saw what you did. There is no need to tell me.",
    "Telina the Dark Witch: Give me the axe. I must peer into it's metals and see into it's past...",
    "Telina the Dark Witch: ...",
    "Telina the Dark Witch: This axe brings me a vision. I see a deep valley. I see tall, pointed mountains. Next to the mountain, is an obelisk in a small pool of water. In the water is a treasure...",
    "Telina the Dark Witch: I now know where this is... You will go and fetch for me this next treasure.",
    "Telina the Dark Witch: From the west gate of Freeport, follow the road south through the desert until you come to a gypsy village.",
    "Telina the Dark Witch: From the center of this camp, look back towards the northwest and you will see Razor Back Fang.",
    "Telina the Dark Witch: Razor Back Fang is a large mountain in the shape of two fangs with a valley in the middle.",
    "Telina the Dark Witch: Climb to the top of fang mountains to avoid all the undead in the valley. You will see a small obelisk in a pool of water.",
    "Telina the Dark Witch: Explore the water around the obelisk and locate the waterlogged chest.",
    "Telina the Dark Witch: Bring me the treasure you find inside. Go now.",
"You have given away the Chiseled Great Axe of Doom.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 30120, quests[30120][4].log)
TurnInItem(mySession, items.CHISELED_GREAT_AXE_OF_DOOM, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the Chiseled Great Axe of Doom."
end
elseif (GetPlayerFlags(mySession, "30120") == "5") then
if (CheckQuestItem(mySession, items.ETCHED_HELMET_OF_GREATNESS, 1))
 then
multiDialogue = { "Telina the Dark Witch: This is it. This is the treasure that my heart has long desired. I will now have the means to defeat my true enemy. His name is...Oh, nevermind that.",
    "Telina the Dark Witch: You have served me well. I hereby release you from my watchful eye. For now, anyway. I wont forget that charming face though.",
    "Telina the Dark Witch: Take this note to Malethai Crimsonhand. Your service to me is now fulfilled.",
"You have given away the Etched Helmet of Greatness.",
"You have received a note from Telina.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 30120, quests[30120][5].log)
TurnInItem(mySession, items.ETCHED_HELMET_OF_GREATNESS, 1)
GrantItem(mySession, items.NOTE_FROM_TELINA, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the treasure."
end
  else
        npcDialogue =
"Telina the Dark Witch: Now that you have seen me, my watchful eye will be able to see you, wherever you go, whatever you do, for as long as I wish it. You will have to fulfill my task to be freed of my watchful eye."
    end
------
--Wizard(13) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "130120") == "2") then
if (choice:find("future")) then
multiDialogue = { "Telina the Dark Witch: Yes my dear. I can see deeply into your soul... Your future will be lost in ominous magic, with great power and real danger... But not until you have first served me and my great eye."
 } 
elseif (choice:find("Wilkenson")) then
multiDialogue = { "Telina the Dark Witch: Lose an important mark, did we? No worries deary. Madame Telina will help you. I can see things from a distance, and things that have yet to occur...",
    "Telina the Dark Witch: I believe the mark you are looking for is in the hands of a Troll. He travels with nasehir cutthroats.",
    "Telina the Dark Witch: From the docks, travel southwest to search for the nasehir camps. They are not far from the docks. There is only one troll with them.",
    "Telina the Dark Witch: His name is Roj Eir Sew' Eil. Take the mark from him and return to me.",
    "Telina the Dark Witch: I'll be watching you now, wherever you go.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 130120, quests[130120][2].log)
else
    npcDialogue = "What a delightful sight the spirits have brought me."
    diagOptions = { "Agent Wilkenson said you have a lead on the real mark.", "Oh, can you tell me my future?" }
end
elseif (GetPlayerFlags(mySession, "130120") == "3") then
if (CheckQuestItem(mySession, items.MARK_OF_LOUOTH, 1))
 then
multiDialogue = { "Telina the Dark Witch: I saw what you did. There is no need to tell me. I watched you every step of the way.",
    "Telina the Dark Witch: This is the true mark you were looking for. I am going to study it for awhile though. Don't worry, I will make sure it is returned to William Nothard.",
    "Telina the Dark Witch: It is now your turn to serve me. You didn't think you would get my help for free, did you?",
    "Telina the Dark Witch: You will now travel south along the coast for a long distance. Far to the south is Muniel's Tea Garden. Be alert young one, this will be very dangerous.",
    "Telina the Dark Witch: Swim to the island off the coast of Muniel's Tea Garden. There are several skeleton pirates on this small expanse of land.",
    "Telina the Dark Witch: Search the island for sand covered chests. Slay the skeletons and search the chests for the Chiseled Great Axe of Doom. Return to me with this axe.",
    "Telina the Dark Witch: Remember, I can see everything you do. From this condition I will not release you, until my wishes are fulfilled. Now go.",
"You have given away the Mark of Louoth.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 130120, quests[130120][3].log)
TurnInItem(mySession, items.MARK_OF_LOUOTH, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the Mark of Louoth."
end
elseif (GetPlayerFlags(mySession, "130120") == "4") then
if (CheckQuestItem(mySession, items.CHISELED_GREAT_AXE_OF_DOOM, 1))
 then
multiDialogue = { "Telina the Dark Witch: Again, I saw what you did. There is no need to tell me.",
    "Telina the Dark Witch: Give me the axe. I must peer into it's metals and see into it's past...",
    "Telina the Dark Witch: ...",
    "Telina the Dark Witch: This axe brings me a vision. I see a deep valley. I see tall, pointed mountains. Next to the mountain, is an obelisk in a small pool of water. In the water is a treasure...",
    "Telina the Dark Witch: I now know where this is... You will go and fetch for me this next treasure.",
    "Telina the Dark Witch: From the west gate of Freeport, follow the road south through the desert until you come to a gypsy village.",
    "Telina the Dark Witch: From the center of this camp, look back towards the northwest and you will see Razor Back Fang.",
    "Telina the Dark Witch: Razor Back Fang is a large mountain in the shape of two fangs with a valley in the middle.",
    "Telina the Dark Witch: Climb to the top of fang mountains to avoid all the undead in the valley. You will see a small obelisk in a pool of water.",
    "Telina the Dark Witch: Explore the water around the obelisk and locate the waterlogged chest.",
    "Telina the Dark Witch: Bring me the treasure you find inside. Go now.",
"You have given away the Chiseled Great Axe of Doom.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 130120, quests[130120][4].log)
TurnInItem(mySession, items.CHISELED_GREAT_AXE_OF_DOOM, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the Chiseled Great Axe of Doom."
end
elseif (GetPlayerFlags(mySession, "130120") == "5") then
if (CheckQuestItem(mySession, items.ETCHED_HELMET_OF_GREATNESS, 1))
 then
multiDialogue = { "Telina the Dark Witch: This is it. This is the treasure that my heart has long desired. I will now have the means to defeat my true enemy. His name is...Oh, nevermind that.",
    "Telina the Dark Witch: You have served me well. I hereby release you from my watchful eye. For now, anyway. I wont forget that charming face though.",
    "Telina the Dark Witch: Take this note to Sivrendesh. Your service to me is now fulfilled.",
"You have given away the Etched Helmet of Greatness.",
"You have received a note from Telina.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 130120, quests[130120][5].log)
TurnInItem(mySession, items.ETCHED_HELMET_OF_GREATNESS, 1)
GrantItem(mySession, items.NOTE_FROM_TELINA, 1)
else
npcDialogue = "Telina the Dark Witch: Time is running out. You must complete this mission. Go find the treasure."
end
  else
        npcDialogue =
"Telina the Dark Witch: Now that you have seen me, my watchful eye will be able to see you, wherever you go, whatever you do, for as long as I wish it. You will have to fulfill my task to be freed of my watchful eye."
    end
------






SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end

