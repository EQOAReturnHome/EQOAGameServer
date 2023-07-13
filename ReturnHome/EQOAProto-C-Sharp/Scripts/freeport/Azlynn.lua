local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Enchanter(12) Human(0) Eastern(1)
if
            (class == "Enchanter" and race == "Human" and humanType == "Eastern" and
            GetPlayerFlags(mySession, "120101") == "noFlags")
then
        SetPlayerFlags(mySession, "120101", "0")
end
if (GetPlayerFlags(mySession, "120101") == "0") then
if (choice:find("enchanter")) then
multiDialogue = { "Azlynn: Most who seek enchantments do not realize how much that they will be trading in their old life for the new one. This is much more than mastering illusions.",
    "Azlynn: But if you insist, I will expect you to complete a number of tasks before you will earn the title of enchanter.",
    "Azlynn: Your first task is to acquire a bronze ring from Merchant Yulia. Your fee for this will be waived.",
    "Azlynn: When you have the bronze ring, return to me and I'll send you on your second task...If that is your true will.",
    "You have received a quest!"
}
StartQuest(mySession, 120101, quests[120101][0].log)
else
    npcDialogue = "What brings you to this house of magic?"
    diagOptions = { "I wish to be an enchanter of The Academy of Arcane Science." }
end
elseif (GetPlayerFlags(mySession, "120101") == "1") then
if (CheckQuestItem(mySession, items.BRONZE_RING, 1))
 then
if (choice:find("forgotten")) then
npcDialogue = "Azlynn: I'll need you to purchase the bronze ring from Merchant Yulia, then return to me."
elseif (choice:find("have")) then
multiDialogue = { "Azlynn: It's going to take more than running errands to learn this power. We are going to have to test your wit and your will...",
    "Azlynn: I will have your next task ready in a few moments. Don't wander off now...",
    "You have finished a quest!"
}
CompleteQuest(mySession, 120101, quests[120101][1].xp, 120102 )
else
    npcDialogue = "You have much to learn about making an entrance."
    diagOptions = { "I have the ring.", "I have forgotten my task..." }
end
else
npcDialogue = "Azlynn: I'll need you to purchase the bronze ring from Merchant Yulia, then return to me."
end
elseif (GetPlayerFlags(mySession, "120102") == "0") then
if (choice:find("am")) then
multiDialogue = { "Azlynn: No class is as tricky and deceptive as the Enchanter. Able to disguise themselves with magic spells, they can slip into areas where one would normally be killed on sight and interact freely with their enemies.",
    "Azlynn: They can also toy with the mind, such as strengthening the resolve of those around them or forcing their enemies to switch sides and fight for the enchanter.",
    "Azlynn: Enchanters are poor fighters and die easily in combat, but they are a valuable support. The enchanter's spells can help keep the party's power flowing and strong.",
    "Azlynn: As an Enchanter, you will be able to diffuse serious situations by controlling your enemies.",
"Azlynn: Now listen carefully, if you still wish to learn. I need you to speak to Spiritmaster Ashlan.",
"Azlynn: You can find him just outside this building. Return only when you complete any tasks he gives you.",
    "You have received a quest!"
}
StartQuest(mySession, 120102, quests[120102][0].log)
else
    npcDialogue = "Are you prepared for your next task?"
    diagOptions = { "I am." }
end
elseif (GetPlayerFlags(mySession, "120102") == "3") then
if (choice:find("don\'t")) then
multiDialogue = { "Azlynn: You'll find that we guildmasters don't like to be kept waiting. I suggest you tend to the task at hand. Consult your quest log if you have lost track of your tasks."
 } 
elseif (choice:find("done")) then
multiDialogue = { "Azlynn: Excellent. Be sure to have yourself bound often. It is quite inconvenient to be defeated far from your last binding.",
    "Azlynn: Now that you've completed that, I have another task for you. Go see Opanheim, he will assist you.",
    "Azlynn: You can find Opanheim on the other side if this ramp.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 120102, quests[120102][3].xp, 120103 )
else
    npcDialogue = "Were you able to complete all of tasks you were set upon?"
    diagOptions = { "Yes, it is done.", "I don't know, it's too much for me." }
end
elseif (GetPlayerFlags(mySession, "120107") == "0") then
if (level >=7) then
if (choice:find("I\'ll")) then
multiDialogue = { "Azlynn: Perhaps you could learn a thing or two about being prepared when you enter this academy."
 } 
elseif (choice:find("Azlynn")) then
multiDialogue = { "Azlynn: You've returned not a moment too soon. I have a task that needs attention but I am preoccupied at the moment.",
    "Azlynn: Thieves broke into the Library of Magic and stole an ancient rune stone. We cannot allow such a precious item to be forever lost.",
    "Azlynn: Fortunately, we have tracked them down. Go north from Freeport's north gate, and continue to travel north along the coastline to reach the Town of Temby.",
    "Azlynn: Find Smuggler Bandelan. Wait for him to wander away from the other villagers. Secure the rune stone of Ghizsa and bring it to me.",
    "You have received a quest!"
}
StartQuest(mySession, 120107, quests[120107][0].log)
else
    npcDialogue = "Welcome. Are you prepared for another task?"
    diagOptions = { "I am, Azlynn.", "I'll be back later." }
end
else
npcDialogue ="Azlynn: I need a little more time to finish a spell I am working on. Come back a little later."
end
elseif (GetPlayerFlags(mySession, "120107") == "1") then
if (CheckQuestItem(mySession, items.RUNE_STONE_OF_GHIZSA, 1))
 then
if (choice:find("sorry")) then
npcDialogue = "Azlynn: You must make haste, playerName! Bring me the rune stone of Ghizsa. You will find it on Smuggler Bandelan in Temby."
elseif (choice:find("delivered")) then
multiDialogue = { "Azlynn: Impressive! You are back with the stone already? On behalf of the Academy, we owe you a debt, small as that might be.",
    "Azlynn: It is a relief to have that stone here where it belongs. In the wrong hands it could wreck some havoc.",
    "Azlynn: As for your reward, take this Endure Arcane Scroll and these Papyrus Wrist Wraps.",
    "Azlynn: You have done well for today. I'll be busy for a while pouring through some ancient texts. Please check back with me later, playerName.",
"You have given away the rune stone of Ghizsa.",
"You have received an Endure Arcane Scroll.",
"You have received the Papyrus Wrist Wraps.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 120107, quests[120107][1].xp, 120110 )
TurnInItem(mySession, items.RUNE_STONE_OF_GHIZSA, 1)
GrantItem(mySession, items.ENDURE_ARCANE, 1)
GrantItem(mySession, items.PAPYRUS_WRIST_WRAPS, 1)
else
    npcDialogue = "Do you have the rune stone of Ghizsa?"
    diagOptions = { "Yes, I delivered it straight to you.", "Oh sorry, not yet." }
end
else
npcDialogue = "Azlynn: You must make haste, playerName! Bring me the rune stone of Ghizsa. You will find it on Smuggler Bandelan in Temby."
end
elseif (GetPlayerFlags(mySession, "120110") == "0") then
if (level >=10) then
if (choice:find("Perhaps")) then
multiDialogue = { "Azlynn: Well, don't take to long. Your abilities can wither away if you don't keep up on your studies."
 } 
elseif (choice:find("Azlynn")) then
multiDialogue = { "Azlynn: A sparkling green gem was stolen from my courier and I want you to retrieve it. The gem was stolen by bandits south of Hodstock.",
    "Azlynn: Follow the riverbank north of Freeport until you reach a broken bridge. Wait until nightfall for Glarik Novandear to appear.",
    "Azlynn: You may have to kill weaker bandits to get him to make an appearance.",
    "Azlynn: I want that gem. Do not disappoint me.",
    "You have received a quest!"
}
StartQuest(mySession, 120110, quests[120110][0].log)
else
    npcDialogue = "Do you wish to learn more of your craft, playerName?"
    diagOptions = { "I do, Azlynn.", "Perhaps after a rest." }
end
else
npcDialogue ="Azlynn: You are almost strong enough for this next task, but not quite. Please return to me after you have practiced your skills a bit further."
end
elseif (GetPlayerFlags(mySession, "120110") == "1") then
if (CheckQuestItem(mySession, items.SPARKLING_GREEN_GEM, 1))
 then
if (choice:find("don\'t")) then
npcDialogue = "Azlynn: This really isn't asking for much. Return to me with the sparkling green gem and you will be rewarded."
elseif (choice:find("right")) then
multiDialogue = { "Azlynn: Ahh, yes, there she is. One of my most precious stones. Within it is a great Genie of the Sea. Good thing the thief had no idea how to awaken her.",
    "Azlynn: You have done well. There is no time to waste however, as I have another task for you.",
    "Azlynn: I need lantern oil to restock my supplies. Merchant Landi has some for sale. Bring this to me and you will be rewarded.",
"You have given away a sparkling green gem.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120110, quests[120110][1].log)
TurnInItem(mySession, items.SPARKLING_GREEN_GEM, 1)
else
    npcDialogue = "Please tell me you have returned with my precious gem, playerName..."
    diagOptions = { "Yes, I have it right here.", "I don't know what you mean..." }
end
else
npcDialogue = "Azlynn: This really isn't asking for much. Return to me with the sparkling green gem and you will be rewarded."
end
elseif (GetPlayerFlags(mySession, "120110") == "2") then
if (CheckQuestItem(mySession, items.LANTERN_OIL, 1))
 then
if (choice:find("Whoops")) then
npcDialogue = "Azlynn: This really isn't asking for much. I need lantern oil to restock my supplies. Merchant Landi has some  for sale. Bring some to me and you will be rewarded."
elseif (choice:find("right")) then
multiDialogue = { "Azlynn: Impressive. It seems as though I've been doing a better job with selecting new enchanters as of late, if your work here is anything to show for it.",
    "Azlynn: This will no doubt bolster our supplies. Thank you.",
    "Azlynn: Please take this Rod of Eyes as your reward. Now go practice your skills for awhile. I'll have more lessons for you when you are stronger.",
"You have given away the lantern oil.",
"You have received the Rod of Eyes.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 120110, quests[120110][2].xp, 120113 )
TurnInItem(mySession, items.LANTERN_OIL, 1)
GrantItem(mySession, items.ROD_OF_EYES, 1)
else
    npcDialogue = "Have you returned with the lantern oil already?"
    diagOptions = { "Yes. I have it all right here.", "Whoops, not yet." }
end
else
npcDialogue = "Azlynn: This really isn't asking for much. I need lantern oil to restock my supplies. Merchant Landi has some  for sale. Bring some to me and you will be rewarded."
end
elseif (GetPlayerFlags(mySession, "120113") == "0") then
if (level >=13) then
if (choice:find("something")) then
multiDialogue = { "Azlynn: It might! You are in the wrong line of business if you think that you can somehow learn this craft without facing danger."
 } 
elseif (choice:find("returned")) then
multiDialogue = { "Azlynn: Take this bag of coins to Agent Wilkenson. He is on the docks. Complete any tasks that he may have for you and then return to me.",
    "Azlynn: Remember, you represent me on this mission. Don't let me down.",
"You have received a bag of coins.",
    "You have received a quest!"
}
StartQuest(mySession, 120113, quests[120113][0].log)
GrantItem(mySession, items.BAG_OF_COINS, 1)
else
    npcDialogue = "I have important work for you, playerName."
    diagOptions = { "I have returned to learn more.", "Is it something that will try to kill me again?" }
end
else
npcDialogue ="Azlynn: Things aren't going so well. I'll have a report for you a little later..."
end
elseif (GetPlayerFlags(mySession, "120113") == "4") then
if (choice:find("Practically")) then
multiDialogue = { "Azlynn: While you may be a talented enchanter, I can quite easily tell when you are just stuffing my ears with nonsense."
 } 
elseif (choice:find("Louhmanta")) then
multiDialogue = { "Azlynn: The Mark of Louhmanta! This has been missing since my colleague went missing recently. It seems you have thwarted a plot against the Academy of Arcane Science.",
    "Azlynn: I am quite pleased with your performance. And I sense a great power within you. Perhaps it will serve you well if you continue on this path.",
    "Azlynn: I will have more work for you after I've sorted a few things out, so check back with me later. For your reward, take this powerful scroll.",
"You have given away a Mark of Louhmanta.",
"You have received a Lumbering Arms Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 120113, quests[120113][4].xp, 120115 )
TurnInItem(mySession, items.MARK_OF_LOUHMANTA, 1)
GrantItem(mySession, items.LUMBERING_ARMS, 1)
else
    npcDialogue = "Have you completed the mission?"
    diagOptions = { "Yes. Here is the Mark of Louhmanta.", "You know, almost done... Practically finished." }
end
elseif (GetPlayerFlags(mySession, "120115") == "0") then
if (level >=15) then
if (choice:find("quite")) then
multiDialogue = { "Azlynn: There are critical matters to attend to in this world, playerName. You would do well to focus your mind on the task at hand, or be swept away in the currents of ambiguity."
 } 
elseif (choice:find("ready")) then
multiDialogue = { "Azlynn: I am looking at your armor and I believe you need an upgrade. You need something that could amplify your abilities while offering protection.",
    "Azlynn: I am sending you to Tailor Weynia, she can be found near the lighthouse, south of Freeport. Follow the coastline south. Before long you'll see the lighthouse on a small island just east of the coastline.",
    "Azlynn: Tell her I would like some Poacher's Leggings. Follow all of her instructions and then return to me.",
    "You have received a quest!"
}
StartQuest(mySession, 120115, quests[120115][0].log)
else
    npcDialogue = "Are you ready for another lesson?"
    diagOptions = { "I am ready.", "Not quite yet." }
end
else
npcDialogue ="Azlynn: You are somewhat under skilled for this next task. Please go practice your skills further and then return to me."
end
elseif (GetPlayerFlags(mySession, "120115") == "6") then
if (CheckQuestItem(mySession, items.POACHERS_LEGGINGS, 1))
 then
if (choice:find("forgotten")) then
npcDialogue = "Azlynn: You must stay focused on this task now. I'll need Poacher's Leggings from Tailor Weynia to upgrade your armor."
elseif (choice:find("Poacher\'s")) then
multiDialogue = { "Azlynn: It seems like so long ago that I sent you away that I'd lost track of you.",
    "Azlynn: Anyway, I'll take those Poacher's Leggings. Give me a few moments to perform the enchantments...",
    "Azlynn: ...",
    "Azlynn: Here we are, these should do nicely. Take these Leggings of Insight. They should suit you perfectly.",
    "Azlynn: I do believe you have also earned this Alarming Visage Scroll. Now then, that is all for now. I'll be looking forward to our next meeting.",
"You have given away a Poacher's Leggings.",
"You have received a Leggings of Insight.",
"You have received an Alarming Visage Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 120115, quests[120115][6].xp, 120120 )
TurnInItem(mySession, items.POACHERS_LEGGINGS, 1)
GrantItem(mySession, items.LEGGINGS_OF_INSIGHT, 1)
GrantItem(mySession, items.ALARMING_VISAGE, 1)
else
    npcDialogue = "I see that look of satisfaction in your eyes. Tell me of your adventures..."
    diagOptions = { "Well, I have these Poacher's Leggings.", "I've quite forgotten myself." }
end
else
npcDialogue = "Azlynn: You must stay focused on this task now. I'll need Poacher's Leggings from Tailor Weynia to upgrade your armor."
end
elseif (GetPlayerFlags(mySession, "120120") == "0") then
if (level >=20) then
if (choice:find("never")) then
elseif (choice:find("am")) then
multiDialogue = { "Azlynn: You have done well for yourself. I expected nothing less as my assistant. Soon, you will fly from me, but I have one final mission for you to complete.",
    "Azlynn: It took some time to discover, but the mark you retrieved before is a fake.",
    "Azlynn: Unfortunately it has likely passed hands a few times by now. It's imperative that the proper mark be returned to me. I have promised it to William Nothard for safe keeping.",
    "Azlynn: Agent Wilkenson may have a lead. Go find him on the docks right away.",
    "Azlynn: You are the hand of The Academy of Arcane Science in this matter. For all our sakes, you must succeed.",
    "You have received a quest!"
}
StartQuest(mySession, 120120, quests[120120][0].log)
else
    npcDialogue = "Are you ready for your final lesson?"
    diagOptions = { "I am.", "I never want it to end." }
end
else
npcDialogue ="Azlynn: I have something very important coming up for you. Please check back with me later."
end
elseif (GetPlayerFlags(mySession, "120120") == "6") then
if (CheckQuestItem(mySession, items.NOTE_FROM_TELINA, 1))
 then
if (choice:find("details")) then
npcDialogue = "Azlynn: Time is running out. You must complete this mission. Go speak to Telina the Dark Witch."
elseif (choice:find("Telina")) then
multiDialogue = { "Azlynn: Oh my...That is very interesting. She...is going to... Nevermind that.",
    "Azlynn: Well it appears that all of our affairs are in order here.",
    "Azlynn: Once again, you have proven yourself worthy as an enchanter of The Academy of Arcane Science.",
    "Azlynn: As payment for your services, you've earned a set of rewards. The first is a Spacious Mind Scroll, which increases how much power you have for a short time. It comes with a Magic Scepter.",
    "Azlynn: I also reward you with this Power Boon Scroll, which gives your target a boost in how much power they have for a short time. It comes with a Magic Book.",
"You have given away a note from Telina.",
"You have received a Spacious Mind Scroll.",
"You have received a Magic Scepter.",
"You have received a Power Boon Scroll.",
"You have received a Magic Book.",
    "You have finished a quest!",
"Azlynn: Incredibly, you have learned all that I can teach you.",
"Azlynn: I have no doubt you are going to go out into the world and seek out new skills. Beware your own illusions, and you will have the clarity to achieve your greatest desires. Farewell."
}
CompleteQuest(mySession, 120120, quests[120120][6].xp, 120121 )
TurnInItem(mySession, items.NOTE_FROM_TELINA, 1)
GrantItem(mySession, items.SPACIOUS_MIND, 1)
GrantItem(mySession, items.MAGIC_SCEPTER, 1)
GrantItem(mySession, items.POWER_BOON, 1)
GrantItem(mySession, items.MAGIC_BOOK, 1)
else
    npcDialogue = "Have you an update for me?"
    diagOptions = { "I have a note from Telina.", "Just a few details to wrap up..." }
end
else
npcDialogue = "Azlynn: Time is running out. You must complete this mission. Go speak to Telina the Dark Witch."
end
  else
        npcDialogue =
"Azlynn: Hmm, where is my sparkling green gem… I know I had it here somewhere."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

