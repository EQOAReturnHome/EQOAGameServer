local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Shadowknight(3) Human(0) Eastern(1)
if
            (class == "Shadowknight" and race == "Human" and humanType == "Eastern" and
            GetPlayerFlags(mySession, "30101") == "noFlags")
then
        SetPlayerFlags(mySession, "30101", "0")
end
if (GetPlayerFlags(mySession, "30101") == "0") then
if (choice:find("shadowknight")) then
multiDialogue = { "Malethai Crimsonhand: Most who seek this path do not realize how much that they will be trading in their old life for the new one. You must give yourself freely to the dark powers.",
    "Malethai Crimsonhand: But if you insist, I will expect you to complete a number of tasks before you will earn the title of shadowknight.",
    "Malethai Crimsonhand: Your first task is to acquire a recruit's shield from Merchant Olkan. He is just outside the building. Your fee for this will be waived.",
    "Malethai Crimsonhand: When you have the recruit's shield, return to me and I'll send you on your second task...If that is your true will.",
    "You have received a quest!"
}
StartQuest(mySession, 30101, quests[30101][0].log)
else
    npcDialogue = "I hope you have a good reason to be here."
    diagOptions = { "I wish to be an shadowknight of The Shining Shield Mercenaries." }
end
elseif (GetPlayerFlags(mySession, "30101") == "1") then
if (CheckQuestItem(mySession, items.RECRUITS_SHIELD, 1))
 then
if (choice:find("Please")) then
npcDialogue = "Malethai Crimsonhand: I'll need you to purchase the recruit's shield from Merchant Olkan, then return to me."
elseif (choice:find("shield")) then
multiDialogue = { "Malethai Crimsonhand: Death could call for you at any moment. We are going to have to test your wit and your will, if you wish to extend your life...",
    "Malethai Crimsonhand: I will have your next task ready in a few moments. Don't wander off now...",
    "You have finished a quest!"
}
CompleteQuest(mySession, 30101, quests[30101][1].xp, 30102 )
else
    npcDialogue = "Have you finished with this small task?"
    diagOptions = { "I have the shield.", "Please, tell me again." }
end
else
npcDialogue = "Malethai Crimsonhand: I'll need you to purchase the recruit's shield from Merchant Olkan, then return to me."
end
elseif (GetPlayerFlags(mySession, "30102") == "0") then
if (choice:find("ready")) then
multiDialogue = { "Malethai Crimsonhand: Shadowknights are a force to be reckoned with, as they often journey through the world alone.",
    "Malethai Crimsonhand: As all tanks are, they are skilled at hand-to-hand combat, and have a mix of spells to aid them in battle.",
    "Malethai Crimsonhand: Use of their life tap spells can sustain a shadowknight through torturous battles.",
"Malethai Crimsonhand: Now listen carefully. I need you to speak to Spiritmaster Keika.",
"Malethai Crimsonhand: You can find her near the south east corner of the city. Go out the doorway of The Shining Shield, through the east city exit, head south along the wall, and take a right around the corner.",
"Malethai Crimsonhand: Return only when you complete any tasks she gives you.",
    "You have received a quest!"
}
StartQuest(mySession, 30102, quests[30102][0].log)
else
    npcDialogue = "I hope you have a good reason to be here."
    diagOptions = { "I am ready for my next task." }
end
elseif (GetPlayerFlags(mySession, "30102") == "3") then
if (choice:find("perhaps")) then
multiDialogue = { "Malethai Crimsonhand: You'll find that we guildmasters don't like to be kept waiting. I suggest you tend to the task at hand. Consult your quest log if you have lost track of your tasks."
 } 
elseif (choice:find("finished")) then
multiDialogue = { "Malethai Crimsonhand: Even so, be sure to have yourself bound often. It is quite inconvenient to be defeated far from your last binding.",
    "Malethai Crimsonhand: Now that you've completed that, I have another task for you. Go see Stolfson Krieger, he will assist you.",
    "Malethai Crimsonhand: You can find Stolfson Krieger up the ramp, and in the room to the left.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 30102, quests[30102][3].xp, 30103 )
else
    npcDialogue = "This had better be good."
    diagOptions = { "I have finished my tasks.", "I don't know, perhaps it's too much for me." }
end
elseif (GetPlayerFlags(mySession, "30107") == "0") then
if (level >=7) then
if (choice:find("elsewhere")) then
multiDialogue = { "Malethai Crimsonhand: Do you think you can trick me with such a lie? I am leagues beyond you, playerName. I can see the tendrils of darkness swirl around you. I know where it leads, and I can change it at will."
 } 
elseif (choice:find("master")) then
multiDialogue = { "Malethai Crimsonhand: I have a task that needs attention but I am preoccupied at the moment.",
    "Malethai Crimsonhand: I need you to travel to Temby. The villagers there are complaining of an Elf that has wandered into their town. I've been hired to take care of the Elf.",
    "Malethai Crimsonhand: From Freeport's north gate, travel north along the coastline to reach the Town of Temby.",
    "Malethai Crimsonhand: Slay the squire named Janxt. Wait for him to wander away from the other villagers. Secure the cracked symbol of Tunare and bring it to me.",
    "You have received a quest!"
}
StartQuest(mySession, 30107, quests[30107][0].log)
else
    npcDialogue = "I have important work for you."
    diagOptions = { "I am ready, master.", "The darkness calls me elsewhere." }
end
else
npcDialogue ="Malethai Crimsonhand: Your power is too weak for my uses. Do not return to until you have grown stronger."
end
elseif (GetPlayerFlags(mySession, "30107") == "1") then
if (CheckQuestItem(mySession, items.CRACKED_SYMBOL_OF_TUNARE, 1))
 then
if (choice:find("forgiveness")) then
npcDialogue = "Malethai Crimsonhand: The last shadowknight to disappoint me was shown the true meaning of pain. I would not hesitate to show you. Bring me the cracked symbol of Tunare. You will find it on Janxt in Temby."
elseif (choice:find("right")) then
multiDialogue = { "Malethai Crimsonhand: You have taken out this Elf already? I must say, I am quite impressed.",
    "Malethai Crimsonhand: To think that your abilities have come this far. Perhaps you are more deeply connected to our dark arts than I realized.",
    "Malethai Crimsonhand: As for your reward, take this Shadow Tunic Scroll and these Damaged Shield of Hate.",
    "Malethai Crimsonhand: I am satisfied with your work for today. Now, I'll be busy for a while pouring through some ancient texts. Leave me. Though you will check back with me after a time, playerName.",
"You have given away the cracked symbol of Tunare.",
"You have received a Shadow Tunic Scroll.",
"You have received Damaged Shield of Hate.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 30107, quests[30107][1].xp, 30110 )
TurnInItem(mySession, items.CRACKED_SYMBOL_OF_TUNARE, 1)
GrantItem(mySession, items.SHADOW_TUNIC, 1)
GrantItem(mySession, items.DAMAGED_SHIELD_OF_HATE, 1)
else
    npcDialogue = "Have you acquired the cracked symbol of Tunare?"
    diagOptions = { "It is right here.", "I ask forgiveness master, not yet." }
end
else
npcDialogue = "Malethai Crimsonhand: The last shadowknight to disappoint me was shown the true meaning of pain. I would not hesitate to show you. Bring me the cracked symbol of Tunare. You will find it on Janxt in Temby."
end
elseif (GetPlayerFlags(mySession, "30110") == "0") then
if (level >=10) then
if (choice:find("Perhaps")) then
multiDialogue = { "Malethai Crimsonhand: There will be plenty of time to rest when the gods forsake you, and choose not to bring you back to where your spirit is bound. There, in the void you will obtain all the rest you crave."
 } 
elseif (choice:find("command")) then
multiDialogue = { "Malethai Crimsonhand: A precious item of mine was stolen from my courier. I have already recovered it, but I want the thief to pay for his insolence.",
    "Malethai Crimsonhand: Follow the riverbank north of Freeport until you reach a broken bridge. Wait until nightfall for Nanik Novandear to appear.",
    "Malethai Crimsonhand: You may have to kill weaker bandits to get him to make an appearance.",
    "Malethai Crimsonhand: Bring me the bandit's eye as proof that it is done.",
    "You have received a quest!"
}
StartQuest(mySession, 30110, quests[30110][0].log)
else
    npcDialogue = "I have important work for you."
    diagOptions = { "I am ready for your command.", "Perhaps I need a moment to rest first." }
end
else
npcDialogue ="Malethai Crimsonhand: You are almost strong enough for this next task, but not quite. Please return to me after you have practiced your skills a bit further."
end
elseif (GetPlayerFlags(mySession, "30110") == "1") then
if (CheckQuestItem(mySession, items.BANDITS_EYE, 1))
 then
if (choice:find("myself")) then
npcDialogue = "Malethai Crimsonhand: If your will is too weak I shall just  make you my next ritual sacrifice! Return to me with the bandit's eye and you will be rewarded."
elseif (choice:find("course")) then
multiDialogue = { "Malethai Crimsonhand: Ahh, yes, there it is. Revenge, my apprentice. Nothing is as sweet as revenge...",
    "Malethai Crimsonhand: You have done well, but now I have another task for you.",
    "Malethai Crimsonhand: I need lantern oil to restock my supplies. Merchant Landi has some for sale. Bring this to me and you will be rewarded.",
"You have given away the bandit's eye.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 30110, quests[30110][1].log)
TurnInItem(mySession, items.BANDITS_EYE, 1)
else
    npcDialogue = "Have you retrieved the bandit's eye?"
    diagOptions = { "Of course, I have it right here.", "I wish to keep it for myself." }
end
else
npcDialogue = "Malethai Crimsonhand: If your will is too weak I shall just  make you my next ritual sacrifice! Return to me with the bandit's eye and you will be rewarded."
end
elseif (GetPlayerFlags(mySession, "30110") == "2") then
if (CheckQuestItem(mySession, items.LANTERN_OIL, 1))
 then
if (choice:find("Unfortunately")) then
npcDialogue = "Malethai Crimsonhand: If your will is too weak I shall just  make you my next ritual sacrifice! I need lantern oil to restock my supplies. Merchant Landi has some  for sale. Bring some to me and you will be rewarded."
elseif (choice:find("lantern")) then
multiDialogue = { "Malethai Crimsonhand: Of course not, but we can't go parading illegal substances down the streets, now can we?",
    "Malethai Crimsonhand: This will no doubt bolster my supplies.",
    "Malethai Crimsonhand: Take this Blade of the Nanchael as your reward. Now go practice your skills for awhile. I have other matters in the darkness to attend to. I will have more lessons for you when you are stronger.",
"You have given away the lantern oil.",
"You have received the Blade of the Nanchael.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 30110, quests[30110][2].xp, 30113 )
TurnInItem(mySession, items.LANTERN_OIL, 1)
GrantItem(mySession, items.BLADE_OF_THE_NANCHAEL, 1)
else
    npcDialogue = "Have you returned with the lantern oil yet?"
    diagOptions = { "Yes. I have it right here. Is it really lantern oil?", "Unfortunately, not yet." }
end
else
npcDialogue = "Malethai Crimsonhand: If your will is too weak I shall just  make you my next ritual sacrifice! I need lantern oil to restock my supplies. Merchant Landi has some  for sale. Bring some to me and you will be rewarded."
end
elseif (GetPlayerFlags(mySession, "30113") == "0") then
if (level >=13) then
if (choice:find("distances")) then
multiDialogue = { "Malethai Crimsonhand: You will be much more tired after I have buried you alive in the grave that I have already prepared for you behind this building. Perhaps you would like to reconsider?"
 } 
elseif (choice:find("darkness")) then
multiDialogue = { "Malethai Crimsonhand: Take this bag of coins to Agent Wilkenson. He is on the docks. Complete any tasks that he may have for you and then return to me.",
    "Malethai Crimsonhand: Remember, you represent me on this mission. Don't let me down.",
"You have received a bag of coins.",
    "You have received a quest!"
}
StartQuest(mySession, 30113, quests[30113][0].log)
GrantItem(mySession, items.BAG_OF_COINS, 1)
else
    npcDialogue = "I have important work for you, playerName."
    diagOptions = { "I am ready to serve in the darkness.", "I'd rather not travel long distances today. I am tired." }
end
else
npcDialogue ="Malethai Crimsonhand: I have seen things in the darkness. I will require your services soon."
end
elseif (GetPlayerFlags(mySession, "30113") == "4") then
if (choice:find("Practically")) then
multiDialogue = { "Malethai Crimsonhand: While you may be a talented shadowknight, I can quite easily tell when you are just stuffing my ears with nonsense. For each lie, I shall cause one of your toes to turn undead."
 } 
elseif (choice:find("Louhmanta")) then
multiDialogue = { "Malethai Crimsonhand: The Mark of Louhmanta! This has been missing since my colleague went missing recently. It seems you have thwarted a plot against the Shining Shield Mercenaries.",
    "Malethai Crimsonhand: I am quite pleased with your performance. And I sense a great power within you. Perhaps it will serve you well if you continue on this path.",
    "Malethai Crimsonhand: I will have more work for you after I've sorted a few things out, so check back with me later. For your reward, take this powerful scroll.",
"You have given away the Mark of Louhmanta.",
"You have received a Scream of Pain Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 30113, quests[30113][4].xp, 30115 )
TurnInItem(mySession, items.MARK_OF_LOUHMANTA, 1)
GrantItem(mySession, items.SCREAM_OF_PAIN, 1)
else
    npcDialogue = "Have you completed the mission?"
    diagOptions = { "Yes. Here is the Mark of Louhmanta.", "Almost done... Practically finished, master." }
end
elseif (GetPlayerFlags(mySession, "30115") == "0") then
if (level >=15) then
if (choice:find("feeling")) then
multiDialogue = { "Malethai Crimsonhand: Dust, playerName. That is all you will be if you do not heed my commands. Dust."
 } 
elseif (choice:find("ready")) then
multiDialogue = { "Malethai Crimsonhand: I am looking at your armor and I see that you need an upgrade. You need something that could amplify your abilities while offering protection.",
    "Malethai Crimsonhand: I am sending you to Tailor Weynia, she can be found near the lighthouse, south of Freeport. Follow the coastline south.",
    "Malethai Crimsonhand: Before long you'll see the lighthouse on a small island just east of the coastline. Tell her I would like some Poacher's Leggings. Follow all of her instructions and then return to me.",
    "You have received a quest!"
}
StartQuest(mySession, 30115, quests[30115][0].log)
else
    npcDialogue = "Are you ready to walk in darkness?"
    diagOptions = { "I was born ready.", "I'm not feeling it today, master." }
end
else
npcDialogue ="Malethai Crimsonhand: Why are you bothering me with this? You haven't the strength for this task."
end
elseif (GetPlayerFlags(mySession, "30115") == "6") then
if (CheckQuestItem(mySession, items.POACHERS_LEGGINGS, 1))
 then
if (choice:find("darkness")) then
npcDialogue = "Malethai Crimsonhand: You will never accomplish this without staying on task. You must become the master of the darkness. I'll need Poacher's Leggings from Tailor Weynia to upgrade your armor."
elseif (choice:find("Poacher\'s")) then
multiDialogue = { "Malethai Crimsonhand: It seems like so long ago that I sent you away that I'd lost track of you, playerName.",
    "Malethai Crimsonhand: Anyway, I'll take those Poacher's Leggings. Give me a few moments to perform the rituals...",
    "Malethai Crimsonhand: ...",
    "Malethai Crimsonhand: Here we are, these should suit you well. Take these Leggings of Darkness. They're quite befitting a shadowknight such as yourself.",
    "Malethai Crimsonhand: I do believe you have also earned this Punish Death Scroll. Now then, that is all for now. I'll be looking forward to our next meeting.",
"You have given away a Poacher's Leggings.",
"You have received the Leggings of Darkness.",
"You have received a Punish Death Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 30115, quests[30115][6].xp, 30120 )
TurnInItem(mySession, items.POACHERS_LEGGINGS, 1)
GrantItem(mySession, items.LEGGINGS_OF_DARKNESS, 1)
GrantItem(mySession, items.PUNISH_DEATH, 1)
else
    npcDialogue = "What dark tidings have you accomplished?"
    diagOptions = { "Well, I have these Poacher's Leggings.", "I am lost in the darkness, master." }
end
else
npcDialogue = "Malethai Crimsonhand: You will never accomplish this without staying on task. You must become the master of the darkness. I'll need Poacher's Leggings from Tailor Weynia to upgrade your armor."
end
elseif (GetPlayerFlags(mySession, "30120") == "0") then
if (level >=20) then
if (choice:find("never")) then
multiDialogue = { "Malethai Crimsonhand: One thing that will never end are the corrupted hearts of the world, playerName. Find peace in knowing that eventually, you will return to the darkness that will cover the world for eternity."
 } 
elseif (choice:find("am")) then
multiDialogue = { "Malethai Crimsonhand: You have done well for yourself. I expected nothing less as my apprentice. Soon, you will grow too strong for me to control, but I have one final mission for you to complete.",
    "Malethai Crimsonhand: It took some time to discover, but the mark you retrieved before is a fake.",
    "Malethai Crimsonhand: Unfortunately it has likely passed hands a few times by now. It's imperative that the proper mark be returned to me. I have promised it to William Nothard for safe keeping.",
    "Malethai Crimsonhand: Agent Wilkenson may have a lead. Go find him on the docks right away.",
    "Malethai Crimsonhand: You are the hand of The Shining Shield Mercenaries in this matter. For the reputation of our house, you must succeed.",
    "You have received a quest!"
}
StartQuest(mySession, 30120, quests[30120][0].log)
else
    npcDialogue = "Are you ready for my final mission?"
    diagOptions = { "I am.", "I never want it to end." }
end
else
npcDialogue ="Malethai Crimsonhand: I have something very important coming up for you. Please check back with me later."
end
elseif (GetPlayerFlags(mySession, "30120") == "6") then
if (CheckQuestItem(mySession, items.NOTE_FROM_TELINA, 1))
 then
if (choice:find("details")) then
npcDialogue = "Malethai Crimsonhand: Time is running out. You must complete this mission. Go speak to Telina the Dark Witch."
elseif (choice:find("Telina")) then
multiDialogue = { "Malethai Crimsonhand: Oh my...That is very interesting. She...is going to... Nevermind that.",
    "Malethai Crimsonhand: Well it appears that all of our affairs are in order here.",
    "Malethai Crimsonhand: Once again, you have proven yourself worthy as an shadowknight of The Shining Shield Mercenaries.",
    "Malethai Crimsonhand: As payment for your services, you've earned a set of rewards. The first is a Shadow Tower Scroll, which increases your armor and taunts your enemy. It comes with a Magic Axe.",
    "Malethai Crimsonhand: I also reward you with this Bloodwasp Scroll, which drains your enemy's health and gives it to you. It comes with a Magic Partisan.",
"You have given away a note from Telina.",
"You have received a Shadow Tower Scroll.",
"You have received a Magic Axe.",
"You have received a Bloodwasp Scroll.",
"You have received a Magic Partisan.",
    "You have finished a quest!",
"Malethai Crimsonhand: Incredibly, you have learned all that I can teach you, playerName. But now, you are too strong. So...",
"Malethai Crimsonhand: I can't have you here. Eventually, you would try to take my place, and I won't have that. You belong out there in the world. There are many dark places for you to uncover. Go now, playerName."
}
CompleteQuest(mySession, 30120, quests[30120][6].xp, 30121 )
TurnInItem(mySession, items.NOTE_FROM_TELINA, 1)
GrantItem(mySession, items.SHADOW_TOWER, 1)
GrantItem(mySession, items.MAGIC_AXE, 1)
GrantItem(mySession, items.BLOODWASP, 1)
GrantItem(mySession, items.MAGIC_PARTISAN, 1)
else
    npcDialogue = "I see that you have returned from your dark tidings."
    diagOptions = { "I have a note from Telina.", "Just a few details to wrap up..." }
end
else
npcDialogue = "Malethai Crimsonhand: Time is running out. You must complete this mission. Go speak to Telina the Dark Witch."
end
  else
        npcDialogue =
"Malethai Crimsonhand: Most who seek the path of the shadowknight do not realize how much that they will be trading in their old life for a new one. They must give themselves freely to the dark powers."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end

