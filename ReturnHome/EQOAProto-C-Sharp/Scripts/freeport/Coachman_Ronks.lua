local coaches = require("Scripts/ports")
local quests = require("Scripts/FreeportQuests")
local diagOptions = {}
local multiDialogue = {}
local npcDialogue = ""
local playerCoaches = {
    highpass_coach = "Get me a horse to Highpass.",
    bobble_coach = "Get me a horse to Bobble By Water.",
    tea_garden_coach = "Get me a horse to Muniel's Tea Garden.",
    neriak_coach = "Get me a horse to the dark city of Neriak."
}
SetPlayerFlags(mySession, "admin", "true")
local dialogueOptions = {}
function event_say(choice)
    --Warrior(0) Human(0) Eastern(1)
    if (GetPlayerFlags(mySession, "102") == "2") then
        --ShadowKnight(3) Human(0) Eastern(1)
        if (choice:find("Nothing")) then
            multiDialogue = {
                "Coachman Ronks: Well don't just stand around here, I have work to do!"
            }
        elseif (choice:find("Spiritmaster")) then
            multiDialogue = {
                "Coachman Ronks: Oh....from Spiritmaster Zole. You know, I'm getting sick and tired of th....well, nevermind.",
                'Coachman Ronks: Apparently, it is my duty as a citizen of this "fine" city to add you to the stable ledger.',
                "Coachman Ronks: This means that if you find yourself in a nearby city, you can visit the stable there and pay for a ride to Freeport.",
                "Coachman Ronks: Without that record, they cannot offer you passage. If you do, then one of their horses will take you to your destination.",
                "Coachman Ronks: All you need to do is talk to the coachman of the stable and he will add you to his ledger. I'll add you to mine now.",
                'Coachman Ronks: Ok, I\'ve done my "duty". You may return to your masters now. Be sure to tell them how much I "appreciate" the business.',
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 102, quests[102][2].log)
            SetPlayerFlags(mySession, "freeport_coach", "true")
        else
            npcDialogue = "What do you want?"
            diagOptions = {"Spiritmaster Zole sent me.", "Nothing."}
        end
    elseif (GetPlayerFlags(mySession, "30102") == "2") then
        --Bard(5) Human(0) Eastern(1)
        if (choice:find("Nothing")) then
            multiDialogue = {
                "Coachman Ronks: Well don't just stand around here, I have work to do!"
            }
        elseif (choice:find("Spiritmaster")) then
            multiDialogue = {
                "Coachman Ronks: Oh....from Spiritmaster Keika. You know, I'm getting sick and tired of th....well, nevermind.",
                'Coachman Ronks: Apparently, it is my duty as a citizen of this "fine" city to add you to the stable ledger.',
                "Coachman Ronks: This means that if you find yourself in a nearby city, you can visit the stable there and pay for a ride to Freeport.",
                "Coachman Ronks: Without that record, they cannot offer you passage. If you do, then one of their horses will take you to your destination.",
                "Coachman Ronks: All you need to do is talk to the coachman of the stable and he will add you to his ledger. I'll add you to mine now.",
                'Coachman Ronks: Ok, I\'ve done my "duty". You may return to your masters now. Be sure to tell them how much I "appreciate" the business.',
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 30102, quests[30102][2].log)
            SetPlayerFlags(mySession, "freeport_coach", "true")
        else
            npcDialogue = "What do you want?"
            diagOptions = {"Spiritmaster Keika sent me.", "Nothing."}
        end
    elseif (GetPlayerFlags(mySession, "50102") == "2") then
        --Rogue(6) Human(0) Eastern(1)
        if (choice:find("Nothing")) then
            multiDialogue = {
                "Coachman Ronks: Well don't just stand around here, I have work to do!"
            }
        elseif (choice:find("Spiritmaster")) then
            multiDialogue = {
                "Coachman Ronks: Oh....from Spiritmaster Imaryn. You know, I'm getting sick and tired of th....well, nevermind.",
                'Coachman Ronks: Apparently, it is my duty as a citizen of this "fine" city to add you to the stable ledger.',
                "Coachman Ronks: This means that if you find yourself in a nearby city, you can visit the stable there and pay for a ride to Freeport.",
                "Coachman Ronks: Without that record, they cannot offer you passage. If you do, then one of their horses will take you to your destination.",
                "Coachman Ronks: All you need to do is talk to the coachman of the stable and he will add you to his ledger. I'll add you to mine now.",
                'Coachman Ronks: Ok, I\'ve done my "duty". You may return to your masters now. Be sure to tell them how much I "appreciate" the business.',
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 50102, quests[50102][2].log)
            SetPlayerFlags(mySession, "freeport_coach", "true")
        else
            npcDialogue = "What do you want?"
            diagOptions = {"Spiritmaster Imaryn sent me.", "Nothing."}
        end
    elseif (GetPlayerFlags(mySession, "60102") == "2") then
        --Cleric(9) Human(0) Eastern(1)
        if (choice:find("Nothing")) then
            multiDialogue = {
                "Coachman Ronks: Well don't just stand around here, I have work to do!"
            }
        elseif (choice:find("Spiritmaster")) then
            multiDialogue = {
                "Coachman Ronks: Oh....from Spiritmaster Keika. You know, I'm getting sick and tired of th....well, nevermind.",
                'Coachman Ronks: Apparently, it is my duty as a citizen of this "fine" city to add you to the stable ledger.',
                "Coachman Ronks: This means that if you find yourself in a nearby city, you can visit the stable there and pay for a ride to Freeport.",
                "Coachman Ronks: Without that record, they cannot offer you passage. If you do, then one of their horses will take you to your destination.",
                "Coachman Ronks: All you need to do is talk to the coachman of the stable and he will add you to his ledger. I'll add you to mine now.",
                'Coachman Ronks: Ok, I\'ve done my "duty". You may return to your masters now. Be sure to tell them how much I "appreciate" the business.',
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 60102, quests[60102][2].log)
            SetPlayerFlags(mySession, "freeport_coach", "true")
        else
            npcDialogue = "What do you want?"
            diagOptions = {"Spiritmaster Keika sent me.", "Nothing."}
        end
    elseif (GetPlayerFlags(mySession, "90102") == "2") then
        --Magician(10) Human(0) Eastern(1)
        if (choice:find("Nothing")) then
            multiDialogue = {
                "Coachman Ronks: Well don't just stand around here, I have work to do!"
            }
        elseif (choice:find("Spiritmaster")) then
            multiDialogue = {
                "Coachman Ronks: Oh....from Spiritmaster Keika. You know, I'm getting sick and tired of th....well, nevermind.",
                'Coachman Ronks: Apparently, it is my duty as a citizen of this "fine" city to add you to the stable ledger.',
                "Coachman Ronks: This means that if you find yourself in a nearby city, you can visit the stable there and pay for a ride to Freeport.",
                "Coachman Ronks: Without that record, they cannot offer you passage. If you do, then one of their horses will take you to your destination.",
                "Coachman Ronks: All you need to do is talk to the coachman of the stable and he will add you to his ledger. I'll add you to mine now.",
                'Coachman Ronks: Ok, I\'ve done my "duty". You may return to your masters now. Be sure to tell them how much I "appreciate" the business.',
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 90102, quests[90102][2].log)
            SetPlayerFlags(mySession, "freeport_coach", "true")
        else
            npcDialogue = "What do you want?"
            diagOptions = {"Spiritmaster Keika sent me.", "Nothing."}
        end
    elseif (GetPlayerFlags(mySession, "100102") == "2") then
        --Necromancer(11) Human(0) Eastern(1)
        if (choice:find("Nothing")) then
            multiDialogue = {
                "Coachman Ronks: Well don't just stand around here, I have work to do!"
            }
        elseif (choice:find("Spiritmaster")) then
            multiDialogue = {
                "Coachman Ronks: Oh....from Spiritmaster Alshan. You know, I'm getting sick and tired of th....well, nevermind.",
                'Coachman Ronks: Apparently, it is my duty as a citizen of this "fine" city to add you to the stable ledger.',
                "Coachman Ronks: This means that if you find yourself in a nearby city, you can visit the stable there and pay for a ride to Freeport.",
                "Coachman Ronks: Without that record, they cannot offer you passage. If you do, then one of their horses will take you to your destination.",
                "Coachman Ronks: All you need to do is talk to the coachman of the stable and he will add you to his ledger. I'll add you to mine now.",
                'Coachman Ronks: Ok, I\'ve done my "duty". You may return to your masters now. Be sure to tell them how much I "appreciate" the business.',
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 100102, quests[100102][2].log)
            SetPlayerFlags(mySession, "freeport_coach", "true")
        else
            npcDialogue = "What do you want?"
            diagOptions = {"Spiritmaster Alshan sent me.", "Nothing."}
        end
    elseif (GetPlayerFlags(mySession, "110102") == "2") then
        --Enchanter(12) Human(0) Eastern(1)
        if (choice:find("Nothing")) then
            multiDialogue = {
                "Coachman Ronks: Well don't just stand around here, I have work to do!"
            }
        elseif (choice:find("Spiritmaster")) then
            multiDialogue = {
                "Coachman Ronks: Oh....from Spiritmaster Keika. You know, I'm getting sick and tired of th....well, nevermind.",
                'Coachman Ronks: Apparently, it is my duty as a citizen of this "fine" city to add you to the stable ledger.',
                "Coachman Ronks: This means that if you find yourself in a nearby city, you can visit the stable there and pay for a ride to Freeport.",
                "Coachman Ronks: Without that record, they cannot offer you passage. If you do, then one of their horses will take you to your destination.",
                "Coachman Ronks: All you need to do is talk to the coachman of the stable and he will add you to his ledger. I'll add you to mine now.",
                'Coachman Ronks: Ok, I\'ve done my "duty". You may return to your masters now. Be sure to tell them how much I "appreciate" the business.',
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 110102, quests[110102][2].log)
            SetPlayerFlags(mySession, "freeport_coach", "true")
        else
            npcDialogue = "What do you want?"
            diagOptions = {"Spiritmaster Keika sent me.", "Nothing."}
        end
    elseif (GetPlayerFlags(mySession, "120102") == "2") then
        --Wizard(13) Human(0) Eastern(1)
        if (choice:find("Nothing")) then
            multiDialogue = {
                "Coachman Ronks: Well don't just stand around here, I have work to do!"
            }
        elseif (choice:find("Spiritmaster")) then
            multiDialogue = {
                "Coachman Ronks: Oh....from Spiritmaster Alshan. You know, I'm getting sick and tired of th....well, nevermind.",
                'Coachman Ronks: Apparently, it is my duty as a citizen of this "fine" city to add you to the stable ledger.',
                "Coachman Ronks: This means that if you find yourself in a nearby city, you can visit the stable there and pay for a ride to Freeport.",
                "Coachman Ronks: Without that record, they cannot offer you passage. If you do, then one of their horses will take you to your destination.",
                "Coachman Ronks: All you need to do is talk to the coachman of the stable and he will add you to his ledger. I'll add you to mine now.",
                'Coachman Ronks: Ok, I\'ve done my "duty". You may return to your masters now. Be sure to tell them how much I "appreciate" the business.',
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 120102, quests[120102][2].log)
            SetPlayerFlags(mySession, "freeport_coach", "true")
        else
            npcDialogue = "What do you want?"
            diagOptions = {"Spiritmaster Alshan sent me.", "Nothing."}
        end
    elseif (GetPlayerFlags(mySession, "130102") == "2") then
        --Alchemist(14) Human(0) Eastern(1)
        if (choice:find("Nothing")) then
            multiDialogue = {
                "Coachman Ronks: Well don't just stand around here, I have work to do!"
            }
        elseif (choice:find("Spiritmaster")) then
            multiDialogue = {
                "Coachman Ronks: Oh....from Spiritmaster Alshan. You know, I'm getting sick and tired of th....well, nevermind.",
                'Coachman Ronks: Apparently, it is my duty as a citizen of this "fine" city to add you to the stable ledger.',
                "Coachman Ronks: This means that if you find yourself in a nearby city, you can visit the stable there and pay for a ride to Freeport.",
                "Coachman Ronks: Without that record, they cannot offer you passage. If you do, then one of their horses will take you to your destination.",
                "Coachman Ronks: All you need to do is talk to the coachman of the stable and he will add you to his ledger. I'll add you to mine now.",
                'Coachman Ronks: Ok, I\'ve done my "duty". You may return to your masters now. Be sure to tell them how much I "appreciate" the business.',
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 130102, quests[130102][2].log)
            SetPlayerFlags(mySession, "freeport_coach", "true")
        else
            npcDialogue = "What do you want?"
            diagOptions = {"Spiritmaster Alshan sent me.", "Nothing."}
        end
    elseif (GetPlayerFlags(mySession, "140102") == "2") then
        if (choice:find("Nothing")) then
            multiDialogue = {
                "Coachman Ronks: Well don't just stand around here, I have work to do!"
            }
        elseif (choice:find("Spiritmaster")) then
            multiDialogue = {
                "Coachman Ronks: Oh....from Spiritmaster Alshan. You know, I'm getting sick and tired of th....well, nevermind.",
                'Coachman Ronks: Apparently, it is my duty as a citizen of this "fine" city to add you to the stable ledger.',
                "Coachman Ronks: This means that if you find yourself in a nearby city, you can visit the stable there and pay for a ride to Freeport.",
                "Coachman Ronks: Without that record, they cannot offer you passage. If you do, then one of their horses will take you to your destination.",
                "Coachman Ronks: All you need to do is talk to the coachman of the stable and he will add you to his ledger. I'll add you to mine now.",
                'Coachman Ronks: Ok, I\'ve done my "duty". You may return to your masters now. Be sure to tell them how much I "appreciate" the business.',
                "You have finished a quest!",
                "You have received a quest!"
            }
            ContinueQuest(mySession, 140102, quests[140102][2].log)
            SetPlayerFlags(mySession, "freeport_coach", "true")
        else
            npcDialogue = "What do you want?"
            diagOptions = {"Spiritmaster Alshan sent me.", "Nothing."}
            end
      elseif (GetPlayerFlags(mySession, "freeport_coach") == "true" or GetPlayerFlags(mySession, "admin") == "true") then
            if (choice:find("Highpass")) then
                TeleportPlayer(
                    mySession,
                    GetWorld(coaches.highpass.world),
                    coaches.highpass.x,
                    coaches.highpass.y,
                    coaches.highpass.z,
                    coaches.highpass.facing
                )
            elseif (choice:find("Tea")) then
                print(mySession)
                TeleportPlayer(
                    mySession,
                    GetWorld(coaches.muniels_tea_garden.world),
                    coaches.muniels_tea_garden.x,
                    coaches.muniels_tea_garden.y,
                    coaches.muniels_tea_garden.z,
                    coaches.muniels_tea_garden.facing
                )
            elseif (choice:find("Bobble")) then
                TeleportPlayer(
                    mySession,
                    GetWorld(coaches.bobble_by_water.world),
                    coaches.bobble_by_water.x,
                    coaches.bobble_by_water.y,
                    coaches.bobble_by_water.z,
                    coaches.bobble_by_water.facing
                )
            elseif (choice:find("Neriak")) then
                TeleportPlayer(
                    mySession,
                    GetWorld(coaches.neriak.world),
                    coaches.neriak.x,
                    coaches.neriak.y,
                    coaches.neriak.z,
                    coaches.neriak.facing
                )
            else
                npcDialogue = "Where would you like to go?"
                for coach, diag in pairs(playerCoaches) do
                    if ((GetPlayerFlags(mySession, "admin") or GetPlayerFlags(mySession, coach)) == "true") then
                        table.insert(diagOptions, diag)
                    end
                  end
               end
        else
            if (choice:find("Yes")) then
                npcDialogue = "Excellent, you can now use this coach any time."
                SetPlayerFlags(mySession, "freeport_coach", "true")
                SendDialogue(mySession, npcDialogue, dialogueOptions)
            elseif (choice:find("No")) then
                npcDialogue = "If you aren't interested then why are you wasting my time."
                SendDialogue(mySession, npcDialogue, dialogueOptions)
            else
                npcDialogue = "Would you like to sign the coachman's ledger?"
                dialogueOptions = {"Yes", "No"}
            end
      end
    SendMultiDialogue(mySession, multiDialogue)
    SendDialogue(mySession, npcDialogue, diagOptions)
end
