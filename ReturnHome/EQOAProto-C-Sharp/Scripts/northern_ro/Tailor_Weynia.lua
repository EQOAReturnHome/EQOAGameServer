local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Enchanter(12) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "120115") == "1") then
if (choice:find("thank")) then
multiDialogue = { "Tailor Weynia: Perhaps taking in the fresh ocean air will clear your mind and put you back on task."
 } 
elseif (choice:find("Poacher\'s")) then
multiDialogue = { "Tailor Weynia: Oh, yes. Azlynn often sends me her assistants for upgrades to their equipment.",
    "Tailor Weynia: I will need you to gather several items.",
    "Tailor Weynia: First, make your way into the hills to the west. Search for and slay sidewinder snakes. Collect a skin and return it to me.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120115, quests[120115][1].log)
else
    npcDialogue = "Have you something I can mend?"
    diagOptions = { "Azlynn sent me to acquire Poacher's Leggings.", "No, thank you." }
end
elseif (GetPlayerFlags(mySession, "120115") == "2") then
if (CheckQuestItem(mySession, items.SIDEWINDER_SKIN, 1))
 then
multiDialogue = { "Tailor Weynia: I see you have found the snakes. This is a little mangled, but it will do.",
    "Tailor Weynia: For the next item, follow the beach to the south and search for sand skippers.",
    "Tailor Weynia: Slay the sand skippers and retrieve a carapace. Then return to me.",
"You have given away a sidewinder skin.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120115, quests[120115][2].log)
TurnInItem(mySession, items.SIDEWINDER_SKIN, 1)
else
npcDialogue = "Tailor Weynia: You must stay focused on this task now. I need a sidewinder skin."
end
elseif (GetPlayerFlags(mySession, "120115") == "3") then
if (CheckQuestItem(mySession, items.SAND_SKIPPER_CARAPACE, 1))
 then
multiDialogue = { "Tailor Weynia: Looks like you are still in one piece. Those skippers have a deadly grip if you aren't fast on your feet...",
    "Tailor Weynia: For the next component, you must travel west. Keep heading west until you come to some pillars in the sand.",
    "Tailor Weynia: Hunt in this area for a larger-than-normal tarantula called Gargantula.",
    "Tailor Weynia: Kill Gargantula and retrieve a bundle of pristine silk. Bring that silk to me.",
"You have given away a sand skipper carapace.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120115, quests[120115][3].log)
TurnInItem(mySession, items.SAND_SKIPPER_CARAPACE, 1)
else
npcDialogue = "Tailor Weynia: You must stay focused on this task now. I need a sand skipper carapace."
end
elseif (GetPlayerFlags(mySession, "120115") == "4") then
if (CheckQuestItem(mySession, items.BUNDLE_OF_PRISTINE_SILK, 1))
 then
multiDialogue = { "Tailor Weynia: I am... a bit surprised that a wafer like you defeated Gargantula. All the other assistants never come back...",
    "Tailor Weynia: Anyway, for the final item, I need you to purchase vulture feathers from Dteven Savis. He can be found near the west gate of Freeport.",
"You have given away a bundle of pristine silk.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120115, quests[120115][4].log)
TurnInItem(mySession, items.BUNDLE_OF_PRISTINE_SILK, 1)
else
npcDialogue = "Tailor Weynia: You must stay focused on this task now. I need a bundle of pristine silk."
end
elseif (GetPlayerFlags(mySession, "120115") == "5") then
if (CheckQuestItem(mySession, items.VULTURE_FEATHERS, 1))
 then
multiDialogue = { "Tailor Weynia: Again, few assistants actually get through this list, but it seems you are made of something different.",
    "Tailor Weynia: Here, I have crafted some Poacher's Leggings. Be sure to tell Azlynn that I appreciate the business.",
"You have given away a vulture feathers.",
"You have received a Poacher's Leggings.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120115, quests[120115][5].log)
TurnInItem(mySession, items.VULTURE_FEATHERS, 1)
GrantItem(mySession, items.POACHERS_LEGGINGS, 1)
else
npcDialogue = "Tailor Weynia: You must stay focused on this task now. I need vulture feathers from Dteven Savis."
end
  else
        npcDialogue =
"Tailor Weynia: Sorry you swam all the way out here but my services are only available to my distinguished clients. Hope you enjoy the view."
    end
------
--Necromancer(11) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "110115") == "1") then
if (choice:find("thank")) then
multiDialogue = { "Tailor Weynia: Perhaps taking in the fresh ocean air will clear your mind and put you back on task."
 } 
elseif (choice:find("Poacher\'s")) then
multiDialogue = { "Tailor Weynia: Oh, yes. Corious Slaerin often sends me his assistants for upgrades to their equipment.",
    "Tailor Weynia: I will need you to gather several items.",
    "Tailor Weynia: First, make your way into the hills to the west. Search for and slay sidewinder snakes. Collect a skin and return it to me.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110115, quests[110115][1].log)
else
    npcDialogue = "Have you something I can mend?"
    diagOptions = { "Corious Slaerin sent me to acquire Poacher's Leggings.", "No, thank you." }
end
elseif (GetPlayerFlags(mySession, "110115") == "2") then
if (CheckQuestItem(mySession, items.SIDEWINDER_SKIN, 1))
 then
multiDialogue = { "Tailor Weynia: I see you have found the snakes. This is a little mangled, but it will do.",
    "Tailor Weynia: For the next item, follow the beach to the south and search for sand skippers.",
    "Tailor Weynia: Slay the sand skippers and retrieve a carapace. Then return to me.",
"You have given away a sidewinder skin.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110115, quests[110115][2].log)
TurnInItem(mySession, items.SIDEWINDER_SKIN, 1)
else
npcDialogue = "Tailor Weynia: You must preside over the darkness, or it will preside over you! You must learn to become it's master! I need a sidewinder skin."
end
elseif (GetPlayerFlags(mySession, "110115") == "3") then
if (CheckQuestItem(mySession, items.SAND_SKIPPER_CARAPACE, 1))
 then
multiDialogue = { "Tailor Weynia: Looks like you are still in one piece. Those skippers have a deadly grip if you aren't fast on your feet...",
    "Tailor Weynia: For the next component, you must travel west. Keep heading west until you come to some pillars in the sand.",
    "Tailor Weynia: Hunt in this area for a larger-than-normal tarantula called Gargantula.",
    "Tailor Weynia: Kill Gargantula and retrieve a bundle of pristine silk. Bring that silk to me.",
"You have given away a sand skipper carapace.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110115, quests[110115][3].log)
TurnInItem(mySession, items.SAND_SKIPPER_CARAPACE, 1)
else
npcDialogue = "Tailor Weynia: You must preside over the darkness, or it will preside over you! You must learn to become it's master! I need a sand skipper carapace."
end
elseif (GetPlayerFlags(mySession, "110115") == "4") then
if (CheckQuestItem(mySession, items.BUNDLE_OF_PRISTINE_SILK, 1))
 then
multiDialogue = { "Tailor Weynia: I am... a bit surprised that a wafer like you defeated Gargantula. All the other assistants never come back...",
    "Tailor Weynia: Anyway, for the final item, I need you to purchase vulture feathers from Dteven Savis. He can be found near the west gate of Freeport.",
"You have given away a bundle of pristine silk.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110115, quests[110115][4].log)
TurnInItem(mySession, items.BUNDLE_OF_PRISTINE_SILK, 1)
else
npcDialogue = "Tailor Weynia: You must preside over the darkness, or it will preside over you! You must learn to become it's master! I need a bundle of pristine silk."
end
elseif (GetPlayerFlags(mySession, "110115") == "5") then
if (CheckQuestItem(mySession, items.VULTURE_FEATHERS, 1))
 then
multiDialogue = { "Tailor Weynia: Again, few assistants actually get through this list, but it seems you are made of something different.",
    "Tailor Weynia: Here, I have crafted some Poacher's Leggings. Be sure to tell Corious that I appreciate the business.",
"You have given away a vulture feathers.",
"You have received a Poacher's Leggings.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110115, quests[110115][5].log)
TurnInItem(mySession, items.VULTURE_FEATHERS, 1)
GrantItem(mySession, items.POACHERS_LEGGINGS, 1)
else
npcDialogue = "Tailor Weynia: You must preside over the darkness, or it will preside over you! You must learn to become it's master! I need vulture feathers from Dteven Savis."
end
  else
        npcDialogue =
"Tailor Weynia: Sorry you swam all the way out here but my services are only available to my distinguished clients. Hope you enjoy the view."
    end
------
--Shadowknight(3) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "30115") == "1") then
if (choice:find("thank")) then
multiDialogue = { "Tailor Weynia: Perhaps taking in the fresh ocean air will clear your mind and put you back on task."
 } 
elseif (choice:find("Crimsonhand")) then
multiDialogue = { "Tailor Weynia: Oh, yes. Malethai often sends me his apprentices for upgrades to their equipment.",
    "Tailor Weynia: I will need you to gather several items. These will be dangerous tasks, but I'm sure a brave shadowknight such as yourself can handle it.",
    "Tailor Weynia: First, make your way into the hills to the west. Search for and slay sidewinder snakes. Collect a skin and return it to me.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 30115, quests[30115][1].log)
else
    npcDialogue = "Have you something I can mend?"
    diagOptions = { "Malethai Crimsonhand sent me to acquire Poacher's Leggings.", "No, thank you." }
end
elseif (GetPlayerFlags(mySession, "30115") == "2") then
if (CheckQuestItem(mySession, items.SIDEWINDER_SKIN, 1))
 then
multiDialogue = { "Tailor Weynia: I see you have found the snakes. This is a little mangled, but it will do.",
    "Tailor Weynia: For the next item, follow the beach to the south and search for sand skippers.",
    "Tailor Weynia: Slay the sand skippers and retrieve a carapace. Then return to me.",
"You have given away a sidewinder skin.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 30115, quests[30115][2].log)
TurnInItem(mySession, items.SIDEWINDER_SKIN, 1)
else
npcDialogue = "Tailor Weynia: You will never accomplish this without staying on task. You must become the master of the darkness. I need a sidewinder skin."
end
elseif (GetPlayerFlags(mySession, "30115") == "3") then
if (CheckQuestItem(mySession, items.SAND_SKIPPER_CARAPACE, 1))
 then
multiDialogue = { "Tailor Weynia: Looks like you are still in one piece. Those skippers have a deadly grip if you aren't fast on your feet...",
    "Tailor Weynia: For the next component, you must travel west. Keep heading west until you come to some pillars in the sand.",
    "Tailor Weynia: Hunt in this area for a larger-than-normal tarantula called Gargantula.",
    "Tailor Weynia: Kill Gargantula and retrieve a bundle of pristine silk. Bring that silk to me.",
"You have given away a sand skipper carapace.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 30115, quests[30115][3].log)
TurnInItem(mySession, items.SAND_SKIPPER_CARAPACE, 1)
else
npcDialogue = "Tailor Weynia: You will never accomplish this without staying on task. You must become the master of the darkness. I need a sand skipper carapace."
end
elseif (GetPlayerFlags(mySession, "30115") == "4") then
if (CheckQuestItem(mySession, items.BUNDLE_OF_PRISTINE_SILK, 1))
 then
multiDialogue = { "Tailor Weynia: At last, someone has defeated Gargantula! I've had trouble sleeping knowing that such a creature exists. Why do you think I'm on this island?",
    "Tailor Weynia: Anyway, for the final item, I need you to purchase vulture feathers from Dteven Savis. He can be found near the west gate of Freeport.",
"You have given away a bundle of pristine silk.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 30115, quests[30115][4].log)
TurnInItem(mySession, items.BUNDLE_OF_PRISTINE_SILK, 1)
else
npcDialogue = "Tailor Weynia: You will never accomplish this without staying on task. You must become the master of the darkness. I need a bundle of pristine silk."
end
elseif (GetPlayerFlags(mySession, "30115") == "5") then
if (CheckQuestItem(mySession, items.VULTURE_FEATHERS, 1))
 then
multiDialogue = { "Tailor Weynia: I must say, few apprentices actually get through this list, but it seems you are made of something different.",
    "Tailor Weynia: Here, I have crafted some Poacher's Leggings for you. Be sure to tell Malethai that I appreciate the business.",
"You have given away a vulture feathers.",
"You have received a Poacher's Leggings.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 30115, quests[30115][5].log)
TurnInItem(mySession, items.VULTURE_FEATHERS, 1)
GrantItem(mySession, items.POACHERS_LEGGINGS, 1)
else
npcDialogue = "Tailor Weynia: You will never accomplish this without staying on task. You must become the master of the darkness. I need vulture feathers from Dteven Savis."
end
  else
        npcDialogue =
"Tailor Weynia: Sorry you swam all the way out here but my services are only available to my distinguished clients. Hope you enjoy the view."
    end
------
--Wizard(13) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "130115") == "1") then
if (choice:find("thank")) then
multiDialogue = { "Tailor Weynia: Perhaps taking in the fresh ocean air will clear your mind and put you back on task."
 } 
elseif (choice:find("Sivrendesh")) then
multiDialogue = { "Tailor Weynia: Oh, yes. Sivrendesh often sends me his pupils for upgrades to their equipment.",
    "Tailor Weynia: I will need you to gather several items. These will be dangerous tasks, but I'm sure a brave wizard such as yourself can handle it.",
    "Tailor Weynia: First, make your way into the hills to the west. Search for and slay sidewinder snakes. Collect a skin and return it to me.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 130115, quests[130115][1].log)
else
    npcDialogue = "Have you something I can mend?"
    diagOptions = { "Sivrendesh sent me to acquire Poacher's Leggings.", "No, thank you." }
end
elseif (GetPlayerFlags(mySession, "130115") == "2") then
if (CheckQuestItem(mySession, items.SIDEWINDER_SKIN, 1))
 then
multiDialogue = { "Tailor Weynia: I see you have found the snakes. This is a little mangled, but it will do.",
    "Tailor Weynia: For the next item, follow the beach to the south and search for sand skippers.",
    "Tailor Weynia: Slay the sand skippers and retrieve a carapace. Then return to me.",
"You have given away a sidewinder skin.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 130115, quests[130115][2].log)
TurnInItem(mySession, items.SIDEWINDER_SKIN, 1)
else
npcDialogue = "Tailor Weynia: You must stay on task if you wish to become a master of the arcane. I need a sidewinder skin."
end
elseif (GetPlayerFlags(mySession, "130115") == "3") then
if (CheckQuestItem(mySession, items.SAND_SKIPPER_CARAPACE, 1))
 then
multiDialogue = { "Tailor Weynia: Looks like you are still in one piece. Those skippers have a deadly grip if you aren't fast on your feet...",
    "Tailor Weynia: For the next component, you must travel west. Keep heading west until you come to some pillars in the sand.",
    "Tailor Weynia: Hunt in this area for a larger-than-normal tarantula called Gargantula.",
    "Tailor Weynia: Kill Gargantula and retrieve a bundle of pristine silk. Bring that silk to me.",
"You have given away a sand skipper carapace.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 130115, quests[130115][3].log)
TurnInItem(mySession, items.SAND_SKIPPER_CARAPACE, 1)
else
npcDialogue = "Tailor Weynia: You must stay on task if you wish to become a master of the arcane. I need a sand skipper carapace."
end
elseif (GetPlayerFlags(mySession, "130115") == "4") then
if (CheckQuestItem(mySession, items.BUNDLE_OF_PRISTINE_SILK, 1))
 then
multiDialogue = { "Tailor Weynia: At last, someone has defeated Gargantula! I've had trouble sleeping knowing that such a creature exists. Why do you think I'm on this island?",
    "Tailor Weynia: Anyway, for the final item, I need you to purchase vulture feathers from Dteven Savis. He can be found near the west gate of Freeport.",
"You have given away a bundle of pristine silk.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 130115, quests[130115][4].log)
TurnInItem(mySession, items.BUNDLE_OF_PRISTINE_SILK, 1)
else
npcDialogue = "Tailor Weynia: You must stay on task if you wish to become a master of the arcane. I need a bundle of pristine silk."
end
elseif (GetPlayerFlags(mySession, "130115") == "5") then
if (CheckQuestItem(mySession, items.VULTURE_FEATHERS, 1))
 then
multiDialogue = { "Tailor Weynia: I must say, few pupils actually get through this list, but it seems you are made of something different.",
    "Tailor Weynia: Here, I have crafted some Poacher's Leggings for you. Be sure to tell Sivrendesh that I appreciate the business.",
"You have given away vulture feathers.",
"You have received a Poacher's Leggings.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 130115, quests[130115][5].log)
TurnInItem(mySession, items.VULTURE_FEATHERS, 1)
GrantItem(mySession, items.POACHERS_LEGGINGS, 1)
else
npcDialogue = "Tailor Weynia: You must stay on task if you wish to become a master of the arcane. I need vulture feathers from Dteven Savis."
end
  else
        npcDialogue =
"Tailor Weynia: Sorry you swam all the way out here but my services are only available to my distinguished clients. Hope you enjoy the view."
    end
------






SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end

