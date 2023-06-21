local quests = require("Scripts/FreeportQuests")
local items = require("Scripts/items")
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function event_say(choice)
    --Necromancer(11) Human(0) Eastern(1)
    if
        (class == "Necromancer" and race == "Human" and humanType == "Eastern" and
            GetPlayerFlags(mySession, "110101") == "noFlags")
     then
        SetPlayerFlags(mySession, "110101", "0")
    end
    if (GetPlayerFlags(mySession, "110101") == "0") then
        if (choice:find("necromancer")) then
            multiDialogue = {
                "Corious Slaerin: You couldn't possibly know that, until you've seen with your own eyes the power of raising the dead to life with your own will.",
                "Corious Slaerin: But if you insist, I will expect you to complete a number of tasks before you will earn the title of necromancer.",
                "Corious Slaerin: Your first task is to acquire a bone earring from Merchant Gilgash. Your fee for this will be waived.",
                "Corious Slaerin: When you have the bone earring, return to me and I'll send you on your second task...If that is your true will.",
                "You have received a quest!"
            }
            StartQuest(mySession, 110101, quests[110101][0].log)
        else
            npcDialogue = "If you have something to say, then say it."
            diagOptions = {"I wish to be an necromancer of The House Slaerin."}
        end
    elseif (GetPlayerFlags(mySession, "110101") == "1") then
        if (CheckQuestItem(mySession, items.BONE_EARRING, 1)) then
            if (choice:find("Please")) then
                npcDialogue =
                    "Corious Slaerin: I'll need you to purchase the bone earring from Merchant Gilgash, then return to me."
            elseif (choice:find("earring")) then
                multiDialogue = {
                    "Corious Slaerin: Death could call for you at any moment. We are going to have to test your wit and your will, if you wish to extend your life...",
                    "Corious Slaerin: I will have your next task ready in a few moments. Don't wander off now...",
                    "You have finished a quest!"
                }
                CompleteQuest(mySession, 110101, quests[110101][1].xp, 110102)
            else
                npcDialogue = "Have you finished with this small task?"
                diagOptions = {"I have the earring.", "Please, tell me again."}
            end
        else
            npcDialogue =
                "Corious Slaerin: I'll need you to purchase the bone earring from Merchant Gilgash, then return to me."
        end
    elseif (GetPlayerFlags(mySession, "110102") == "0") then
        if (choice:find("ready")) then
            multiDialogue = {
                "Corious Slaerin: A necromancer's unholy spells specialize in robbing enemies of their physical abilities and health in order to bolster themselves and their party.",
                "Corious Slaerin: They also can command undead pets equal in strength to those of a magician. A skilled necromancer can fight alone or in a group, though many necromancers either choose or are forced to walk alone.",
                "Corious Slaerin: In a group, they can aid the attack by sending their pets into battle and assist with health and power restoration by transferring their own health and power to those in need.",
                "Corious Slaerin: As expected, most races shun necromancers due to their practice of the dark arts, making this a difficult path to pursue.",
                "Corious Slaerin: Now listen carefully. I need you to speak to Spiritmaster Keika.",
                "Corious Slaerin: You can find her just outside, to the east. Return only when you complete any tasks she gives you.",
                "You have received a quest!"
            }
            StartQuest(mySession, 110102, quests[110102][0].log)
        else
            npcDialogue = "If you have something to say, say it."
            diagOptions = {"I am ready for my next task."}
        end
    elseif (GetPlayerFlags(mySession, "110102") == "3") then
        if (choice:find("perhaps")) then
            multiDialogue = {
                "Corious Slaerin: You'll find that we guildmasters don't like to be kept waiting. I suggest you tend to the task at hand. Consult your quest log if you have lost track of your tasks."
            }
        elseif (choice:find("done")) then
            multiDialogue = {
                "Corious Slaerin: Without fail, be sure to have yourself bound often. It is quite inconvenient to be defeated far from your last binding.",
                "Corious Slaerin: Now that you've completed that, I have another task for you. Go see Rathei Slaerin, she will assist you.",
                "Corious Slaerin: You can find Rathei Slaerin just behind me.",
                "You have finished a quest!"
            }
            CompleteQuest(mySession, 110102, quests[110102][3].xp, 110103)
        else
            npcDialogue = "Were you able to complete all of tasks you were set upon?"
            diagOptions = {"Yes, it is done.", "I don't know, perhaps it's too much for me."}
        end
    elseif (GetPlayerFlags(mySession, "110107") == "0") then
        if (level >= 7) then
            if (choice:find("elsewhere")) then
                multiDialogue = {
                    "Corious Slaerin: Do you think you can trick me with such a lie? I am leagues beyond you, playerName. I can see the tendrils of darkness swirl around you. I know where it leads, and I can change it at will."
                }
            elseif (choice:find("master")) then
                multiDialogue = {
                    "Corious Slaerin: I have a task that needs attention but I am preoccupied at the moment.",
                    "Corious Slaerin: I need you to travel to Temby. The villagers there are complaining of an Elf that has wandered into their town. I've been hired to take care of the Elf.",
                    "Corious Slaerin: From Freeport's north gate, travel north along the coastline to reach the Town of Temby.",
                    "Corious Slaerin: Slay the squire named Janxt. Wait for him to wander away from the other villagers. Secure the cracked symbol of Tunare and bring it to me.",
                    "You have received a quest!"
                }
                StartQuest(mySession, 110107, quests[110107][0].log)
            else
                npcDialogue = "I have important work for you."
                diagOptions = {"I am ready, master.", "The darkness calls me elsewhere."}
            end
        else
            npcDialogue =
                "Corious Slaerin: Your power is too weak for my uses. Do not return to until you have grown stronger."
        end
    elseif (GetPlayerFlags(mySession, "110107") == "1") then
        if (CheckQuestItem(mySession, items.CRACKED_SYMBOL_OF_TUNARE, 1)) then
            if (choice:find("forgiveness")) then
                npcDialogue =
                    "Corious Slaerin: The last necromancer to disappoint me was turned to one of my pets. Shall I demonstrate this on you? Bring me the cracked symbol of Tunare. You will find it on Janxt in Temby."
            elseif (choice:find("right")) then
                multiDialogue = {
                    "Corious Slaerin: You have taken out this Elf already? I must say, I am quite impressed.",
                    "Corious Slaerin: To think that your abilities have come this far. Perhaps you are more deeply connected to our dark arts than I realized.",
                    "Corious Slaerin: As for your reward, take this Strengthen Bone Scroll and these Old Padded Pants.",
                    "Corious Slaerin: I am satisfied with your work for today. Now, I'll be busy for a while pouring through some ancient texts. Leave me. Though you will check back with me after a time, playerName.",
                    "You have given away the cracked symbol of Tunare.",
                    "You have received a Strengthen Bone Scroll.",
                    "You have received Old Padded Pants.",
                    "You have finished a quest!"
                }
                CompleteQuest(mySession, 110107, quests[110107][1].xp, 110110)
                TurnInItem(mySession, items.CRACKED_SYMBOL_OF_TUNARE, 1)
                GrantItem(mySession, items.STRENGTHEN_BONE, 1)
                GrantItem(mySession, items.OLD_PADDED_PANTS, 1)
            else
                npcDialogue = "Have you acquired the cracked symbol of Tunare?"
                diagOptions = {"It is right here.", "I ask forgiveness master, not yet."}
            end
        else
            npcDialogue =
                "Corious Slaerin: The last necromancer to disappoint me was turned to one of my pets. Shall I demonstrate this on you? Bring me the cracked symbol of Tunare. You will find it on Janxt in Temby."
        end
    elseif (GetPlayerFlags(mySession, "110110") == "0") then
        if (level >= 10) then
            if (choice:find("Maybe")) then
                multiDialogue = {
                    "Corious Slaerin: There will be plenty of time to rest when the gods have forsaken you, and have chosen not to bring you back to where your spirit was bound. There, in the void you will obtain all the rest you crave."
                }
            elseif (choice:find("ready")) then
                multiDialogue = {
                    "Corious Slaerin: A precious item of mine was stolen from my courier. I have already recovered it, but I want the thief to pay for his insolence.",
                    "Corious Slaerin: Follow the riverbank north of Freeport until you reach a broken bridge. Wait until nightfall for Charlik Novandear to appear.",
                    "Corious Slaerin: You may have to kill weaker bandits to get him to make an appearance.",
                    "Corious Slaerin: Bring me his warm heart as proof that it is done.",
                    "You have received a quest!"
                }
                StartQuest(mySession, 110110, quests[110110][0].log)
            else
                npcDialogue = "I have important work for you."
                diagOptions = {"I am ready.", "Maybe after a rest."}
            end
        else
            npcDialogue =
                "Corious Slaerin: You are almost strong enough for this next task, but not quite. Please return to me after you have practiced your skills a bit further."
        end
    elseif (GetPlayerFlags(mySession, "110110") == "1") then
        if (CheckQuestItem(mySession, items.WARM_HEART, 1)) then
            if (choice:find("myself")) then
                npcDialogue =
                    "Corious Slaerin: If your will is too weak I shall just  make you my next ritual sacrifice! Return to me with Charlik Novandear's warm heart and I will consider forgiving you."
            elseif (choice:find("difficult")) then
                multiDialogue = {
                    "Corious Slaerin: My dear playerName, you must have some fantastic will power! Few things taste as good as the blood of one's enemy.",
                    "Corious Slaerin: You have done well, but now I have another task for you.",
                    "Corious Slaerin: I need lantern oil to restock my supplies. Merchant Landi has some for sale. Bring this to me and you will be rewarded.",
                    "You have given away a warm heart.",
                    "You have finished a quest!",
                    "You have received a quest!"
                }
                ContinueQuest(mySession, 110110, quests[110110][1].log)
                TurnInItem(mySession, items.WARM_HEART, 1)
            else
                npcDialogue =
                    "Charlik Novandear's warm heart... I know you desire it for yourself, but do you have the strength to give it over to me?"
                diagOptions = {
                    "Yes, it is difficult, but I think can give it to you.",
                    "I wish to keep it for myself. Yes, I claim it as mine!"
                }
            end
        else
            npcDialogue =
                "Corious Slaerin: If your will is too weak I shall just  make you my next ritual sacrifice! Return to me with Charlik Novandear's warm heart and I will consider forgiving you."
        end
    elseif (GetPlayerFlags(mySession, "110110") == "2") then
        if (CheckQuestItem(mySession, items.LANTERN_OIL, 1)) then
            if (choice:find("Whoops")) then
                npcDialogue =
                    "Corious Slaerin: If your will is too weak I shall just  make you my next ritual sacrifice! I need lantern oil to restock my supplies. Merchant Landi has some  for sale. Bring some to me and you will be rewarded."
            elseif (choice:find("lantern")) then
                multiDialogue = {
                    "Corious Slaerin: Of course not, but we can't go parading illegal substances down the streets, now can we?",
                    "Corious Slaerin: This will no doubt bolster my supplies. Thank you.",
                    "Corious Slaerin: Please take this Dagger of Essence as your reward. Now go practice your skills for awhile. I have other matters in the darkness to attend to. I may have more lessons for you when you are stronger.",
                    "You have given away the lantern oil.",
                    "You have received the Dagger of Essence.",
                    "You have finished a quest!"
                }
                CompleteQuest(mySession, 110110, quests[110110][2].xp, 110113)
                TurnInItem(mySession, items.LANTERN_OIL, 1)
                GrantItem(mySession, items.DAGGER_OF_ESSENCE, 1)
            else
                npcDialogue = "Have you returned with the lantern oil already?"
                diagOptions = {"Yes. I have it right here. Is it really lantern oil?", "Whoops, not yet."}
            end
        else
            npcDialogue =
                "Corious Slaerin: If your will is too weak I shall just  make you my next ritual sacrifice! I need lantern oil to restock my supplies. Merchant Landi has some  for sale. Bring some to me and you will be rewarded."
        end
    elseif (GetPlayerFlags(mySession, "110113") == "0") then
        if (level >= 13) then
            if (choice:find("distances")) then
                multiDialogue = {
                    "Corious Slaerin: You will be much more tired after I have buried you alive in the grave I have already had prepared for you behind this building. Perhaps you would like to reconsider?"
                }
            elseif (choice:find("darkness")) then
                multiDialogue = {
                    "Corious Slaerin: Take this bag of coins to Agent Wilkenson. He is on the docks. Complete any tasks that he may have for you and then return to me.",
                    "Corious Slaerin: Remember, you represent House Slaerin on this mission. Don't let me down.",
                    "You have received a bag of coins.",
                    "You have received a quest!"
                }
                StartQuest(mySession, 110113, quests[110113][0].log)
                GrantItem(mySession, items.BAG_OF_COINS, 1)
            else
                npcDialogue = "I have important work for you, playerName."
                diagOptions = {
                    "I am ready to serve in the darkness.",
                    "I'd rather not travel long distances today. I am tired."
                }
            end
        else
            npcDialogue = "Corious Slaerin: I have seen things in the darkness. I will require your services soon."
        end
    elseif (GetPlayerFlags(mySession, "110113") == "4") then
        if (choice:find("Practically")) then
            multiDialogue = {
                "Corious Slaerin: While you may be a talented necromancer, I can quite easily tell when you are just stuffing my ears with nonsense. For each lie, I shall cause one of your toes to turn undead."
            }
        elseif (choice:find("Louhmanta")) then
            multiDialogue = {
                "Corious Slaerin: The Mark of Louhmanta! This has been missing since my colleague went missing recently. It seems you have thwarted a plot against the House Slaerin.",
                "Corious Slaerin: I am quite pleased with your performance. And I sense a great power within you. Perhaps it will serve you well if you continue on this path.",
                "Corious Slaerin: I will have more work for you after I've sorted a few things out, so check back with me later. For your reward, take this powerful scroll.",
                "You have received a Ward Death Scroll.",
                "You have finished a quest!"
            }
            CompleteQuest(mySession, 110113, quests[110113][4].xp, 110115)
            GrantItem(mySession, items.WARD_DEATH, 1)
        else
            npcDialogue = "Have you completed the mission?"
            diagOptions = {"Yes. Here is the Mark of Louhmanta.", "You know, almost done... Practically finished."}
        end
    elseif (GetPlayerFlags(mySession, "110115") == "0") then
        if (level >= 15) then
            if (choice:find("quite")) then
                multiDialogue = {
                    "Corious Slaerin: Dust, playerName. That is all you will be if you do not heed my commands. Dust."
                }
            elseif (choice:find("ready")) then
                multiDialogue = {
                    "Corious Slaerin: I am looking at your armor and I believe you need an upgrade. You need something that could amplify your abilities while offering protection.",
                    "Corious Slaerin: I am sending you to Tailor Weynia, she can be found near the lighthouse, south of Freeport. Follow the coastline south.",
                    "Corious Slaerin: Before long you'll see the lighthouse on a small island just east of the coastline. Tell her I would like some Poacher's Leggings. Follow all of her instructions and then return to me.",
                    "You have received a quest!"
                }
                StartQuest(mySession, 110115, quests[110115][0].log)
            else
                npcDialogue = "Are you ready for another task?"
                diagOptions = {"I am ready.", "Not quite yet."}
            end
        else
            npcDialogue =
                "Corious Slaerin: I have nothing to say to you at the moment. Perhaps check back when you are stronger!"
        end
    elseif (GetPlayerFlags(mySession, "110115") == "6") then
        if (CheckQuestItem(mySession, items.POACHERS_LEGGINGS, 1)) then
            if (choice:find("darkness")) then
                npcDialogue =
                    "Corious Slaerin: You must preside over the darkness, or it will preside over you! You must learn to become it's master! I'll need Poacher's Leggings from Tailor Weynia to upgrade your armor."
            elseif (choice:find("Poacher's")) then
                multiDialogue = {
                    "Corious Slaerin: It seems like so long ago that I sent you away that I'd lost track of you, playerName.",
                    "Corious Slaerin: Anyway, I'll take those Poacher's Leggings. Give me a few moments to perform the rituals...",
                    "Corious Slaerin: ...",
                    "Corious Slaerin: Here we are, these should suit you well. Take these Deathwalker Leggings. They're quite befitting a necromancer such as yourself.",
                    "Corious Slaerin: I do believe you have also earned this Endure Disease Scroll. Now then, that is all for now. I'll be looking forward to our next meeting.",
                    "You have given away a Poacher's Leggings.",
                    "You have received a Deathwalker Leggings.",
                    "You have received an Endure Disease Scroll.",
                    "You have finished a quest!"
                }
                CompleteQuest(mySession, 110115, quests[110115][6].xp, 110120)
                TurnInItem(mySession, items.POACHERS_LEGGINGS, 1)
                GrantItem(mySession, items.DEATHWALKER_LEGGINGS, 1)
                GrantItem(mySession, items.ENDURE_DISEASE, 1)
            else
                npcDialogue = "What dark tidings have you accomplished?"
                diagOptions = {"Well, I have these Poacher's Leggings.", "I am lost in the darkness, master."}
            end
        else
            npcDialogue =
                "Corious Slaerin: You must preside over the darkness, or it will preside over you! You must learn to become it's master! I'll need Poacher's Leggings from Tailor Weynia to upgrade your armor."
        end
    elseif (GetPlayerFlags(mySession, "110120") == "0") then
        if (level >= 20) then
            if (choice:find("never")) then
                multiDialogue = {
                    "Corious Slaerin: One thing that will never end are the corrupted hearts of the world, playerName. Find peace in knowing that eventually, you will return to the darkness that will cover the world for eternity."
                }
            elseif (choice:find("am")) then
                multiDialogue = {
                    "Corious Slaerin: You have done well for yourself. I expected nothing less as my assistant. Soon, you will grow too strong for me to control, but I have one final mission for you to complete.",
                    "Corious Slaerin: It took some time to discover, but the mark you retrieved before is a fake.",
                    "Corious Slaerin: Unfortunately it has likely passed hands a few times by now. It's imperative that the proper mark be returned to me. I have promised it to William Nothard for safe keeping.",
                    "Corious Slaerin: Agent Wilkenson may have a lead. Go find him on the docks right away.",
                    "Corious Slaerin: You are the hand of The House Slaerin in this matter. For the survival of our house, you must succeed.",
                    "You have received a quest!"
                }
                StartQuest(mySession, 110120, quests[110120][0].log)
            else
                npcDialogue = "Are you ready for my final mission?"
                diagOptions = {"I am.", "I never want it to end."}
            end
        else
            npcDialogue =
                "Corious Slaerin: I have something very important coming up for you. Please check back with me later."
        end
    elseif (GetPlayerFlags(mySession, "110120") == "6") then
        if (CheckQuestItem(mySession, items.NOTE_FROM_TELINA, 1)) then
            if (choice:find("details")) then
                npcDialogue =
                    "Corious Slaerin: Time is running out. You must complete this mission. Go speak to Telina the Dark Witch."
            elseif (choice:find("Telina")) then
                multiDialogue = {
                    "Corious Slaerin: Oh my...That is very interesting. She...is going to... Nevermind that.",
                    "Corious Slaerin: Well it appears that all of our affairs are in order here.",
                    "Corious Slaerin: Once again, you have proven yourself worthy as an necromancer of The House Slaerin.",
                    "Corious Slaerin: As payment for your services, you've earned a set of rewards. The first is a Blood Gale Scroll, which transfers life essence from your target to your group. It comes with a Magic Scepter.",
                    "Corious Slaerin: The other reward option is the Power Gale Scroll, which draws life essence from your target and converts it into power for your group. It comes with a Magic Book."
                }
                SendMultiDialogue(mySession, multiDialogue)
                multiDialogue = {}
                npcDialogue = "Which dark path will you choose playerName? The Blood Gale or Power Gale?"
                diagOptions = {
                    "I'll wash the world in blood. I'd like Blood Gale.",
                    "Power above all. I'll take Power Gale."
                }
            elseif (choice:find("Blood")) then
                multiDialogue = {
                    "You have received a Blood Gale Scroll.",
                    "You have received a Magic Scepter.",
                    "You have finished a quest!",
                    "Corious Slaerin: Incredibly, you have learned all that I can teach you, playerName.",
                    "Corious Slaerin: This is rare, but I can no longer call you my apprentice. You have grown too strong. One day you may even surpass me in power. Go on. You must find your own way now."
                }
                GrantItem(mySession, items.BLOOD_GALE, 1)
                GrantItem(mySession, items.MAGIC_SCEPTER, 1)
                CompleteQuest(mySession, 110120, quests[110120][6].xp, 110121)

            elseif (choice:find("Power")) then
                multiDialogue = {
                    "You have received a Power Gale Scroll.",
                    "You have received a Magic Book.",
                    "You have finished a quest!",
                    "Corious Slaerin: Incredibly, you have learned all that I can teach you, playerName.",
                    "Corious Slaerin: This is rare, but I can no longer call you my apprentice. You have grown too strong. One day you may even surpass me in power. Go on. You must find your own way now."
                }

                CompleteQuest(mySession, 110120, quests[110120][6].xp, 110121)
                GrantItem(mySession, items.POWER_GALE, 1)
                GrantItem(mySession, items.MAGIC_BOOK, 1)
            else
                npcDialogue = "I see that you have returned from your dark tidings."
                diagOptions = {"I have a note from Telina.", "Just a few details to wrap up..."}
            end
        else
            npcDialogue =
                "Corious Slaerin: Time is running out. You must complete this mission. Go speak to Telina the Dark Witch."
        end
    else
        npcDialogue =
            "Corious Slaerin: Away with you! Go now, or I will turn your corpse into a mound of bone and flesh!"
    end
    ------
    SendDialogue(mySession, npcDialogue, diagOptions)
    SendMultiDialogue(mySession, multiDialogue)
end
