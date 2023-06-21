local quests = require("Scripts/FreeportQuests")
local items = require("Scripts/items")
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
merchantDialogue = "Tailor Weynia: I have a some exotic items here, anything catch your eye?"
function event_say(choice)
    --Enchanter(12) Human(0) Eastern(1)
    if (GetPlayerFlags(mySession, "120115") == "1") then
        if (choice:find("thank")) then
            multiDialogue = {
                "Tailor Weynia: Perhaps taking in the fresh ocean air will clear your mind and put you back on task."
            }
        elseif (choice:find("Poacher's")) then
            multiDialogue = {
                "Tailor Weynia: Oh, yes. Azlynn often sends me her assistants for upgrades to their equipment.",
                "Tailor Weynia: I will need you to gather several items.",
                "Tailor Weynia: First, make your way into the hills to the west. Search for and slay sidewinder snakes. Collect a skin and return it to me.",
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 120115, quests[120115][1].log)
        else
            npcDialogue = "Have you something I can mend?"
            diagOptions = {"Azlynn sent me to acquire Poacher's Leggings.", "No, thank you."}
        end
    elseif (GetPlayerFlags(mySession, "120115") == "2") then
        if (CheckQuestItem(mySession, items.SIDEWINDER_SKIN, 1)) then
            multiDialogue = {
                "Tailor Weynia: I see you have found the snakes. This is a little mangled, but it will do.",
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
        if (CheckQuestItem(mySession, items.SAND_SKIPPER_CARAPACE, 1)) then
            multiDialogue = {
                "Tailor Weynia: Looks like you are still in one piece. Those skippers have a deadly grip if you aren't fast on your feet...",
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
        if (CheckQuestItem(mySession, items.BUNDLE_OF_PRISTINE_SILK, 1)) then
            multiDialogue = {
                "Tailor Weynia: I am... a bit surprised that a wafer like you defeated Gargantula. All the other assistants never come back...",
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
        if (CheckQuestItem(mySession, items.VULTURE_FEATHERS, 1)) then
            multiDialogue = {
                "Tailor Weynia: Again, few assistants actually get through this list, but it seems you are made of something different.",
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
            npcDialogue =
                "Tailor Weynia: You must stay focused on this task now. I need vulture feathers from Dteven Savis."
        end
    else
        TriggerMerchantMenu(mySession, thisEntity)
    end
    ------
    --Necromancer(11) Human(0) Eastern(1)
    if (GetPlayerFlags(mySession, "110115") == "1") then
        if (choice:find("thank")) then
            multiDialogue = {
                "Tailor Weynia: Perhaps taking in the fresh ocean air will clear your mind and put you back on task."
            }
        elseif (choice:find("Poacher's")) then
            multiDialogue = {
                "Tailor Weynia: Oh, yes. Corious Slaerin often sends me his assistants for upgrades to their equipment.",
                "Tailor Weynia: I will need you to gather several items.",
                "Tailor Weynia: First, make your way into the hills to the west. Search for and slay sidewinder snakes. Collect a skin and return it to me.",
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 110115, quests[110115][1].log)
        else
            npcDialogue = "Have you something I can mend?"
            diagOptions = {"Corious Slaerin sent me to acquire Poacher's Leggings.", "No, thank you."}
        end
    elseif (GetPlayerFlags(mySession, "110115") == "2") then
        if (CheckQuestItem(mySession, items.SIDEWINDER_SKIN, 1)) then
            multiDialogue = {
                "Tailor Weynia: I see you have found the snakes. This is a little mangled, but it will do.",
                "Tailor Weynia: For the next item, follow the beach to the south and search for sand skippers.",
                "Tailor Weynia: Slay the sand skippers and retrieve a carapace. Then return to me.",
                "You have given away a sidewinder skin.",
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 110115, quests[110115][2].log)
            TurnInItem(mySession, items.SIDEWINDER_SKIN, 1)
        else
            npcDialogue =
                "Tailor Weynia: You must preside over the darkness, or it will preside over you! You must learn to become it's master! I need a sidewinder skin."
        end
    elseif (GetPlayerFlags(mySession, "110115") == "3") then
        if (CheckQuestItem(mySession, items.SAND_SKIPPER_CARAPACE, 1)) then
            multiDialogue = {
                "Tailor Weynia: Looks like you are still in one piece. Those skippers have a deadly grip if you aren't fast on your feet...",
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
            npcDialogue =
                "Tailor Weynia: You must preside over the darkness, or it will preside over you! You must learn to become it's master! I need a sand skipper carapace."
        end
    elseif (GetPlayerFlags(mySession, "110115") == "4") then
        if (CheckQuestItem(mySession, items.BUNDLE_OF_PRISTINE_SILK, 1)) then
            multiDialogue = {
                "Tailor Weynia: I am... a bit surprised that a wafer like you defeated Gargantula. All the other assistants never come back...",
                "Tailor Weynia: Anyway, for the final item, I need you to purchase vulture feathers from Dteven Savis. He can be found near the west gate of Freeport.",
                "You have given away a bundle of pristine silk.",
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 110115, quests[110115][4].log)
            TurnInItem(mySession, items.BUNDLE_OF_PRISTINE_SILK, 1)
        else
            npcDialogue =
                "Tailor Weynia: You must preside over the darkness, or it will preside over you! You must learn to become it's master! I need a bundle of pristine silk."
        end
    elseif (GetPlayerFlags(mySession, "110115") == "5") then
        if (CheckQuestItem(mySession, items.VULTURE_FEATHERS, 1)) then
            multiDialogue = {
                "Tailor Weynia: Again, few assistants actually get through this list, but it seems you are made of something different.",
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
            npcDialogue =
                "Tailor Weynia: You must preside over the darkness, or it will preside over you! You must learn to become it's master! I need vulture feathers from Dteven Savis."
        end
    else
        TriggerMerchantMenu(mySession, thisEntity)
    end
    ------

    SendDialogue(mySession, npcDialogue, diagOptions)
    SendMultiDialogue(mySession, multiDialogue)
end
