local ch = tostring(choice)
function event_say()
    local diagOptions = {}
    local questText = ""
    local npcDialogue = ""
    --Human
    if (race == "Human") then
        --Freeport/Eastern
        if (humanType == "Eastern") then
            npcDialogue =
                "Welcome to Freeport, playerName. This is the city of trade and the eastern home of the humans."
            if (class == "Magician") then
                multiDialogue = {
                    "As a new magician you been invited to join the Academy of Arcane Science. They are the Magician's guild here in Freeport.",
                    "Before we get too far, do you need instructions on how to use basic controls?",
                    "You are now ready to move on to your first quest. Speak to Malsis, he is the guildmaster standing before you."
                }
            elseif (class == "Necromancer") then
                multiDialogue = {
                    "As a new necromancer, you have been invited to join the House Slaerin. They are the necromancer's guild here in Freeport.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Corious Slaerin, he is the guildmaster standing before you."
                }
            elseif (class == "Alchemist") then
                multiDialogue = {
                    "As a new alchemist, you have been invited to join the Academy of Arcane Science. They are the alchemist's guild here in Freeport.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Dilina McNerian, she is the guildmaster standing before you."
                }
            elseif (class == "Bard") then
                multiDialogue = {
                    "As a new bard, you have been invited to join The Silken Gauntlet. They are the bard's guild here in Freeport.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to William Corufost, he is the guildmaster standing before you."
                }
            elseif (class == "Cleric") then
                multiDialogue = {
                    "As a new cleric, you have been invited to join The Spiteful Shield. They are the cleric's guild here in Freeport.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Denouncer Alshea, she is the guildmaster standing before you."
                }
            elseif (class == "Enchanter") then
                multiDialogue = {
                    "As a new enchanter, you have been invited to join the Academy of Arcane Science. They are the enchanter's guild here in Freeport.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Azlynn, she is the guildmaster standing before you."
                }
            elseif (class == "Rogue") then
                multiDialogue = {
                    "As a new rogue, you have been invited to join The Spiteful Shield. They are the rogue's guild here in Freeport.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Necorik the Ghost, he is the guildmaster standing before you."
                }
            elseif (class == "ShadowKnight") then
                multiDialogue = {
                    "As a new shadowknight, you have been invited to join The Spiteful Shield. They are the shadowknight's guild here in Freeport.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Malethai Crimsonhand, he is the guildmaster standing before you."
                }
            elseif (class == "Warrior") then
                multiDialogue = {
                    "As a new warrior, you have been invited to join The Freeport Militia. They are the warrior's guild here in Freeport.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Commander Nothard, he is the guildmaster standing before you."
                }
            elseif (class == "Wizard") then
                multiDialogue = {
                    "As a new wizard, you have been invited to join the Academy of Arcane Science. They are the wizard's guild here in Freeport.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Sivrendesh, he is the guildmaster standing before you."
                }
            end
        elseif (humanType == "Western") then
                    --Qeynos/Western
            npcDialogue =
                "Welcome to Qeynos, playerName. This is the western home of the humans and the newly crowned king, Antonius Bayle II."
            if (class == "Alchemist") then
                multiDialogue = {
                    "As a new alchemist, you have been invited to join the Anagogical Order. They are the alchemist's guild here in Qeynos.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Jergish Anaebarum, he is the guildmaster standing before you."
                }
            elseif (class == "Bard") then
                multiDialogue = {
                    "As a new bard, you have been invited to join the Qeynos Troupe. They are the bard's guild here in Qeynos.",
                    "Before we get too far, do you need instructions on how to use the basic controls",
                    "Then you are ready to move on to your first quest. Speak to Thrush Baird, he is the guildmaster standing before you."
                }
            elseif (class == "Cleric") then
                multiDialogue = {
                    "As a new cleric, you have been invited to join the Defenders of Erollisi Marr. They are the cleric's guild here in Qeynos.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Vedilion Brithstar, he is the guildmaster standing before you."
                }
            elseif (class == "Druid") then
                multiDialogue = {
                    "Welcome to Surefall Glade playerName, sacred home to the human rangers and druids of Tunaria.",
                    "As a new druid, you have been invited to join the Jaggedpine Treefolk. They are the druid's guild here in Surefall Glade.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Shiol'Anara, she is the guildmaster standing before you."
                }
            elseif (class == "Enchanter") then
                multiDialogue = {
                    "As a new enchanter, you have been invited to join the Anagogical Order. They are the enchanter's guild here in Qeynos.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Shiassa Radian, she is the guildmaster standing before you."
                }
            elseif (class == "Magician") then
                multiDialogue = {
                    "As a new magician, you have been invited to join the Anagogical Order. They are the magician's guild here in Qeynos.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Ammathor Lithkin, he is the guildmaster standing before you."
                }
            elseif (class == "Monk") then
                multiDialogue = {
                    "As a new monk, you have been invited to join the Order of the Silent Fist. They are the monk's guild here in Qeynos.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Master T'an Chen, he is the guildmaster standing before you."
                }
            elseif (class == "Paladin") then
                multiDialogue = {
                    "As a new paladin, you have been invited to join the Crusaders of Mithaniel Marr. They are the paladin's guild here in Qeynos.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Sir Hanst Breach, he is the guildmaster standing before you."
                }
            elseif (class == "Ranger") then
                multiDialogue = {
                    "Welcome to Surefall Glade playerName, sacred home to the human rangers and druids of Tunaria.",
                    "As a new ranger, you have been invited to join the Protectors of the Pine. They are the ranger's guild here in Surefall Glade.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Sigmor Fallbourne, he is the guildmaster standing before you."
                }
            elseif (class == "Rogue") then
                multiDialogue = {
                    "As a new rogue, you have been invited to join the The Heavy Purse. They are the rogue's guild here in Qeynos.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Snyde Cragsmear, he is the guildmaster standing before you."
                }
            elseif (class == "Warrior") then
                multiDialogue = {
                    "As a new warrior, you have been invited to join the The Qeynos Guard. They are the warrior's guild here in Qeynos.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Roger Stoutheart, he is the guildmaster standing before you."
                }
            elseif (class == "Wizard") then
                multiDialogue = {
                    "As a new wizard, you have been invited to join the Anagogical Order. They are the wizard's guild here in Qeynos.",
                    "Before we get too far, do you need instructions on how to use the basic controls?",
                    "Then you are ready to move on to your first quest. Speak to Gadenon Flamefist, he is the guildmaster standing before you."
                }
            end
        end
    elseif (race == "Barbarian") then
        --Barbarian
        npcDialogue = "Welcome to Halas, playerName. This is the city of Barbarians, here on Tunaria."
        if (class == "Rogue") then
            multiDialogue = {
                "As a new rogue, you have been invited to join the Eyes of the Tribunal. They are the rogue's guild here in Halas.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Juno Felligan, she is the guildmaster standing before you."
            }
        elseif (class == "Shaman") then
            multiDialogue = {
                "As a new shaman, you have been invited to join the Seers of the Tribunal. They are the shaman's guild here in Halas.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Beril O'Leary, he is the guildmaster standing before you."
            }
        elseif (class == "Warrior") then
            multiDialogue = {
                "As a new warrior, you have been invited to join The Wolves of the North. They are the warrior's guild here in Halas.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Marik McPherson, he is the guildmaster standing before you."
            }
        end
    elseif (race == "Dark_Elf") then
        --Dark Elf
        npcDialogue = "Welcome to Neriak, playerName. This is the home of the Teir'Dal and it is wrought with hate."
        if (class == "Alchemist") then
            multiDialogue = {
                "As a new alchemist, you have been invited to join The Spurned. They are the alchemist's guild here in Neriak.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Raemiss D`Mariji, he is the guildmaster standing before you."
            }
        elseif (class == "Cleric") then
            multiDialogue = {
                "As a new cleric, you have been invited to join the Church of Innoruuk. They are the cleric's guild here in Neriak.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Theologist S`Tai, he is the guildmaster standing before you."
            }
        elseif (class == "Enchanter") then
            multiDialogue = {
                "As a new enchanter, you have been invited to join The Spurned. They are the enchanter's guild here in Neriak.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Dominary K`Jartan, he is the guildmaster standing before you."
            }
        elseif (class == "Magician") then
            multiDialogue = {
                "As a new magician, you have been invited to join The Spurned. They are the magician's guild here in Neriak.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Elementalist R`Virr, he is the guildmaster standing before you."
            }
        elseif (class == "Necromancer") then
            multiDialogue = {
                "As a new necromancer, you have been invited to join the Lodge of the Dead. They are the necromancer's guild here in Neriak.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "When you are ready to move on to your first quest. Speak to Grand Defiler J`Narus, he is the guildmaster standing before you."
            }
        elseif (class == "Rogue") then
            multiDialogue = {
                "As a new rogue, you have been invited to join The Ebon Mask. They are the rogue's guild here in Neriak.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Kriyn, she is the guildmaster standing before you."
            }
        elseif (class == "ShadowKnight") then
            multiDialogue = {
                "As a new shadowknight, you have been invited to join the Lodge of the Dead. They are the shadowknight's guild here in Neriak.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Zethril Do`Vexis, he is the guildmaster standing before you."
            }
        elseif (class == "Warrior") then
            multiDialogue = {
                "As a new warrior, you have been invited to join the The Indigo Brotherhood. They are the warrior's guild here in Neriak.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Scornmaster U`Dedne, he is the guildmaster standing before you."
            }
        elseif (class == "Wizard") then
            multiDialogue = {
                "As a new wizard, you have been invited to join The Spurned. They are the wizard's guild here in Neriak.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Sorceress X`Lottl, she is the guildmaster standing before you."
            }
        end
    elseif (race == "Dwarf") then
    --Dwarf
        npcDialogue = "Welcome to Moradhim, playerName, home of the stout dwarves and the fiercest brew of these lands."
        if (class == "Cleric") then
            multiDialogue = {
                "As a new cleric, you have been invited to join The Church of Below. They are the cleric's guild here in Moradhim",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Bishop Gundar, he is the guildmaster standing before you."
            }
        elseif (class == "Paladin") then
            multiDialogue = {
                "As a new paladin, you have been invited to join The Doomseekers. They are the paladin's guild here in Moradhim.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Ungrist the Prophet, he is the guildmaster standing before you."
            }
        elseif (class == "Rogue") then
            multiDialogue = {
                "As a new rogue, you have been invited to join the Miners Guild 231. They are the rogue's guild here in Moradhim.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Foreman Druza, she is the guildmaster standing before you."
            }
        elseif (class == "Warrior") then
            multiDialogue = {
                "As a new warrior, you have been invited to join The Stone Guard. They are the warrior's guild here in Moradhim.",
                "Before we get too far, do you need instructions on how to use the basic controls?",
                "Then you are ready to move on to your first quest. Speak to Field General Oxfist, he is the guildmaster standing before you."
            }
        end
    end
    SetPlayerFlags(mySession, "NewPlayerIntro", "1")
    SendDialogue(mySession, npcDialogue, diagOptions)
    SendMultiDialogue(mySession, multiDialogue)
end
