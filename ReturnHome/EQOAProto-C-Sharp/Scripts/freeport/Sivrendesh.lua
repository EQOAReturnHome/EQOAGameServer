local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Wizard(13) Human(0) Eastern(1)
if
            (class == "Wizard" and race == "Human" and humanType == "Eastern" and
            GetPlayerFlags(mySession, "130101") == "noFlags")
then
        SetPlayerFlags(mySession, "130101", "0")
end
if (GetPlayerFlags(mySession, "130101") == "0") then
if (choice:find("Academy")) then
multiDialogue = { "Sivrendesh: Most who seek this magic do not realize how much that they will need a will greater than any foe, any force, or any spirit of the world.",
    "Sivrendesh: But if you insist, I will expect you to complete a number of tasks before you will earn the title of wizard.",
    "Sivrendesh: Your first task is to acquire a brass ring from Merchant Yulia. He is just outside the building. Your fee for this will be waived.",
    "Sivrendesh: When you have the brass ring, return to me and I'll send you on your second task...If that is your true will.",
    "You have received a quest!"
}
StartQuest(mySession, 130101, quests[130101][0].log)
else
    npcDialogue = "What brings you to this house of magic?"
    diagOptions = { "I wish to be an wizard of The Academy of Arcane Science." }
end
elseif (GetPlayerFlags(mySession, "130101") == "1") then
if (CheckQuestItem(mySession, items.BRASS_RING, 1))
 then
if (choice:find("Please")) then
npcDialogue = "Sivrendesh: I'll need you to purchase the brass ring from Merchant Yulia, then return to me."
elseif (choice:find("have")) then
multiDialogue = { "Sivrendesh: It's going to take more than running errands to learn this power. We are going to have to test your wit and your will...",
    "Sivrendesh: I will have your next task ready in a few moments. Don't wander off now...",
    "You have finished a quest!"
}
CompleteQuest(mySession, 130101, quests[130101][1].xp, 130102 )
else
    npcDialogue = "I have much more important things to attend to than the likes of you."
    diagOptions = { "I have the ring.", "Please, tell me of my task again." }
end
else
npcDialogue = "Sivrendesh: I'll need you to purchase the brass ring from Merchant Yulia, then return to me."
end
elseif (GetPlayerFlags(mySession, "130102") == "0") then
if (choice:find("ready")) then
multiDialogue = { "Sivrendesh: Wizards are the most powerful spell casters in Tunaria. They inflict massive damage with a single cast, but their strength is also their bane. Wizards cannot go toe-to-toe with high-level creatures.",
    "Sivrendesh: They can only wear light armor and have little health, and if not used with control and skill, their powerful blasts draw the attention of monsters they attack.",
    "Sivrendesh: A skilled wizard learns when to attack at full force or to hold back. If they fail to learn this they will die often and have a difficult time finding groups, as they consider poor wizards to be burdens.",
    "Sivrendesh: Wizards also can teleport themselves and party members to various locations around the world, making getting from place to place a breeze.",
"Sivrendesh: Now listen carefully. I need you to speak to Spiritmaster Alshan.",
"Sivrendesh: You can find him just outside the Academy, near the bottom of the stairs. Return only when you complete any tasks he gives you.",
"Sivrendesh:",
    "You have received a quest!"
}
StartQuest(mySession, 130102, quests[130102][0].log)
else
    npcDialogue = "I hope you have a good reason to be here."
    diagOptions = { "I am ready for my next task." }
end
elseif (GetPlayerFlags(mySession, "130102") == "3") then
if (choice:find("perhaps")) then
multiDialogue = { "Sivrendesh: You'll find that we guildmasters don't like to be kept waiting. I suggest you tend to the task at hand. Consult your quest log if you have lost track of your tasks."
 } 
elseif (choice:find("done")) then
multiDialogue = { "Sivrendesh: Excellent. Be sure to have yourself bound often. It is quite inconvenient to be defeated far from your last binding.",
    "Sivrendesh: Now that you've completed that, I have another task for you. Go see Nefar, he will assist you.",
    "Sivrendesh: You can find Nefar outside, directly behind this building.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 130102, quests[130102][3].xp, 130103 )
else
    npcDialogue = "Have you completed the simple tasks I gave you?"
    diagOptions = { "Yes, it is done.", "I don't know, perhaps it's too much for me." }
end
elseif (GetPlayerFlags(mySession, "130107") == "0") then
if (level >=7) then
if (choice:find("Perhaps")) then
multiDialogue = { "Sivrendesh: Here in this academy you are duty bound to me. Nothing short of true surrender to my guidance will allow you to ascend to become a master of the arcane!"
 } 
elseif (choice:find("assignment")) then
multiDialogue = { "Sivrendesh: You've returned not a moment too soon. I have a task that needs attention but I am preoccupied at the moment.",
    "Sivrendesh: Thieves broke into the Library of Magic and stole an ancient rune stone. We cannot allow such a precious item to be forever lost.",
    "Sivrendesh: From Freeport's north gate, travel north along the coastline to reach the Town of Temby.",
    "Sivrendesh: Find the smuggler named Bandelan. Wait for him to wander away from the other villagers. Secure the rune stone of Ghizsa and bring it to me.",
    "You have received a quest!"
}
StartQuest(mySession, 130107, quests[130107][0].log)
else
    npcDialogue = "I have important work for you."
    diagOptions = { "I am ready for my next assignment.", "Perhaps later, I've other things to attend to." }
end
else
npcDialogue ="Sivrendesh: I need a little more time to finish something I am working on. Come back a little later."
end
elseif (GetPlayerFlags(mySession, "130107") == "1") then
if (CheckQuestItem(mySession, items.RUNE_STONE_OF_GHIZSA, 1))
 then
if (choice:find("forgiveness")) then
npcDialogue = "Sivrendesh: You must stay focused on the task at hand. Bring me the rune stone of Ghizsa. You will find it on Bandelan in Temby."
elseif (choice:find("right")) then
multiDialogue = { "Sivrendesh: Impressive! You are back with the stone already? On behalf of the Academy, we owe you a debt, small as that might be.",
    "Sivrendesh: It is a relief to have that stone here where it belongs. In the wrong hands it could wreck some havoc.",
    "Sivrendesh: As for your reward, take this Burning Flare Scroll and these Fancy Slippers.",
    "Sivrendesh: I am satisfied with your work for today. Now, I'll be busy for a while pouring through some ancient texts. I'll be busy for awhile, but check back with me after a while, playerName.",
"You have given away the rune stone of Ghizsa.",
"You have received a Burning Flare Scroll.",
"You have received Fancy Slippers.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 130107, quests[130107][1].xp, 130110 )
TurnInItem(mySession, items.RUNE_STONE_OF_GHIZSA, 1)
GrantItem(mySession, items.BURNING_FLARE, 1)
GrantItem(mySession, items.FANCY_SLIPPERS, 1)
else
    npcDialogue = "Have you acquired the rune stone of Ghizsa?"
    diagOptions = { "It is right here.", "I ask forgiveness master, not yet." }
end
else
npcDialogue = "Sivrendesh: You must stay focused on the task at hand. Bring me the rune stone of Ghizsa. You will find it on Bandelan in Temby."
end
elseif (GetPlayerFlags(mySession, "130110") == "0") then
if (level >=10) then
if (choice:find("Perhaps")) then
multiDialogue = { "Sivrendesh: There will be plenty of time to rest when the gods forsake you, and choose not to bring you back to where your spirit is bound. There, in the void you will obtain all the rest you crave."
 } 
elseif (choice:find("ready")) then
multiDialogue = { "Sivrendesh: My library vault plans were stolen from my courier and I want you to retrieve them. They were stolen by bandits south of Hodstock.",
    "Sivrendesh: Follow the riverbank north of Freeport until you reach a broken bridge. Wait until nightfall for Yilinyaka Novandear to appear.",
    "Sivrendesh: You may have to kill weaker bandits to get him to make an appearance.",
    "Sivrendesh: Bring me the library vault plans . Do not disappoint me.",
    "You have received a quest!"
}
StartQuest(mySession, 130110, quests[130110][0].log)
else
    npcDialogue = "I have important work for you."
    diagOptions = { "I am ready for it.", "Perhaps I need a moment to rest first." }
end
else
npcDialogue ="Sivrendesh: You are almost strong enough for this next task, but not quite. Please return to me after you have practiced your skills a bit further."
end
elseif (GetPlayerFlags(mySession, "130110") == "1") then
if (CheckQuestItem(mySession, items.LIBRARY_VAULT_PLANS, 1))
 then
if (choice:find("myself")) then
npcDialogue = "Sivrendesh: This really isn't asking for much. Return to me with the library vault plans and you will be rewarded."
elseif (choice:find("course")) then
multiDialogue = { "Sivrendesh: Ahh, yes, there it is. The Academy would be in serious trouble if this fell into the wrong hands.",
    "Sivrendesh: You have done well, but now I have another task for you.",
    "Sivrendesh: I need lantern oil to restock my supplies. Merchant Landi has some for sale. Bring this to me and you will be rewarded.",
"You have given away the library vault plans.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 130110, quests[130110][1].log)
TurnInItem(mySession, items.LIBRARY_VAULT_PLANS, 1)
else
    npcDialogue = "Have you retrieved the library vault plans?"
    diagOptions = { "Of course, I have it right here.", "I wish to keep it for myself." }
end
else
npcDialogue = "Sivrendesh: This really isn't asking for much. Return to me with the library vault plans and you will be rewarded."
end
elseif (GetPlayerFlags(mySession, "130110") == "2") then
if (CheckQuestItem(mySession, items.LANTERN_OIL, 1))
 then
if (choice:find("Unfortunately")) then
npcDialogue = "Sivrendesh: This really isn't asking for much. I need lantern oil to restock my supplies. Merchant Landi has some for sale. Bring some to me and you will be rewarded."
elseif (choice:find("lantern")) then
multiDialogue = { "Sivrendesh: Of course not, but we can't go parading illegal substances down the streets, now can we?",
    "Sivrendesh: This will no doubt bolster my supplies.",
    "Sivrendesh: Take this Staff of Malconius as your reward. Now go practice your skills for awhile. I have other matters of the arcane to attend to. I will have more lessons for you when you are stronger.",
"You have given away the lantern oil.",
"You have received the Staff of Malconius.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 130110, quests[130110][2].xp, 130113 )
TurnInItem(mySession, items.LANTERN_OIL, 1)
GrantItem(mySession, items.STAFF_OF_MALCONIUS, 1)
else
    npcDialogue = "Have you returned with the lantern oil yet?"
    diagOptions = { "Yes. I have it right here. Is it really lantern oil?", "Unfortunately, not yet." }
end
else
npcDialogue = "Sivrendesh: This really isn't asking for much. I need lantern oil to restock my supplies. Merchant Landi has some for sale. Bring some to me and you will be rewarded."
end
elseif (GetPlayerFlags(mySession, "130113") == "0") then
if (level >=13) then
if (choice:find("something")) then
multiDialogue = { "Sivrendesh: It might! You are in the wrong line of business if you think that you can somehow learn this craft without facing danger."
 } 
elseif (choice:find("quite")) then
multiDialogue = { "Sivrendesh: Take this bag of coins to Agent Wilkenson. He is on the docks. Complete any tasks that he may have for you and then return to me.",
    "Sivrendesh: Remember, you represent me on this mission. Don't let me down.",
"You have received a bag of coins.",
    "You have received a quest!"
}
StartQuest(mySession, 130113, quests[130113][0].log)
GrantItem(mySession, items.BAG_OF_COINS, 1)
else
    npcDialogue = "Are you ready for your next mission, playerName?"
    diagOptions = { "I am quite ready.", "Is it something that will try to kill me again?" }
end
else
npcDialogue ="Sivrendesh: Soon I will have a task that requires your services."
end
elseif (GetPlayerFlags(mySession, "130113") == "4") then
if (choice:find("Practically")) then
multiDialogue = { "Sivrendesh: While you may be a talented wizard, I can quite easily tell when you are just stuffing my ears with nonsense."
 } 
elseif (choice:find("Louhmanta")) then
multiDialogue = { "Sivrendesh: The Mark of Louhmanta! This has been missing since my colleague went missing recently. It seems you have thwarted a plot against the Academy of Arcane Science.",
    "Sivrendesh: I am quite pleased with your performance. And I sense a great power within you. Perhaps it will serve you well if you continue on this path.",
    "Sivrendesh: I will have more work for you after I've sorted a few things out, so check back with me later. For your reward, take this powerful scroll.",
"You have given away the Mark of Louhmanta.",
"You have received an Element Guard Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 130113, quests[130113][4].xp, 130115 )
TurnInItem(mySession, items.MARK_OF_LOUHMANTA, 1)
GrantItem(mySession, items.ELEMENT_GUARD, 1)
else
    npcDialogue = "Have you completed the mission?"
    diagOptions = { "Yes. Here is the Mark of Louhmanta.", "Almost done... Practically finished, master." }
end
elseif (GetPlayerFlags(mySession, "130115") == "0") then
if (level >=15) then
if (choice:find("feeling")) then
multiDialogue = { "Sivrendesh: I'll need you to dig deeper within your being and find the will. Behind your mind is the true \"you\". Clear out all the noise and you will find what you are looking for."
 } 
elseif (choice:find("ready")) then
multiDialogue = { "Sivrendesh: I am looking at your armor and I see that you need an upgrade. You need something that could amplify your abilities while offering protection.",
    "Sivrendesh: I am sending you to Taylor Weynia, she can be found near the lighthouse, south of Freeport. Follow the coastline south. Before long you'll see the lighthouse on a small island just east of the coastline.",
    "Sivrendesh: Tell her I would like some Poacher's Leggings. Follow all of her instructions and then return to me.",
    "You have received a quest!"
}
StartQuest(mySession, 130115, quests[130115][0].log)
else
    npcDialogue = "Are you ready for another task?"
    diagOptions = { "I am ready.", "I'm not feeling it today, master." }
end
else
npcDialogue ="Sivrendesh: I have nothing to share with you at the moment. But don't forget to check back with me soon!"
end
elseif (GetPlayerFlags(mySession, "130115") == "6") then
if (CheckQuestItem(mySession, items.POACHERS_LEGGINGS, 1))
 then
if (choice:find("forgotten")) then
npcDialogue = "Sivrendesh: You must stay on task if you wish to become a master of the arcane. I'll need Poacher's Leggings from Tailor Weynia to upgrade your armor."
elseif (choice:find("Poacher\'s")) then
multiDialogue = { "Sivrendesh: It seems like so long ago that I sent you away that I'd lost track of you, playerName.",
    "Sivrendesh: Anyway, I'll take those Poacher's Leggings. Give me a few moments to perform the enchantments...",
    "Sivrendesh: ...",
    "Sivrendesh: Here we are, these should suit you well. Take these Leggings of Wrath. They're quite befitting a wizard such as yourself.",
    "Sivrendesh: I do believe you have also earned this Shocking Gaze Scroll. Now then, that is all for now. I'll be looking forward to our next meeting.",
"You have given away the Poacher's Leggings.",
"You have received the Leggings of Wrath.",
"You have received a Shocking Gaze Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 130115, quests[130115][6].xp, 130120 )
TurnInItem(mySession, items.POACHERS_LEGGINGS, 1)
GrantItem(mySession, items.LEGGINGS_OF_WRATH, 1)
GrantItem(mySession, items.SHOCKING_GAZE, 1)
else
    npcDialogue = "Did I send you off for something?"
    diagOptions = { "Well, I have these Poacher's Leggings.", "I've quite forgotten what it was myself." }
end
else
npcDialogue = "Sivrendesh: You must stay on task if you wish to become a master of the arcane. I'll need Poacher's Leggings from Tailor Weynia to upgrade your armor."
end
elseif (GetPlayerFlags(mySession, "130120") == "0") then
if (level >=20) then
if (choice:find("never")) then
multiDialogue = { "Sivrendesh: One thing that will never end are the corrupted hearts of the world, playerName. Find peace in knowing that eventually, you will have the power and wisdom to change the course of events for all beings."
 } 
elseif (choice:find("am")) then
multiDialogue = { "Sivrendesh: You have done well for yourself. I expected nothing less as my pupil. Soon, you will grow too strong for me to control, but I have one final mission for you to complete.",
    "Sivrendesh: It took some time to discover, but the mark you retrieved before is a fake.",
    "Sivrendesh: Unfortunately it has likely passed hands a few times by now. It's imperative that the proper mark be returned to me. I have promised it to William Nothard for safe keeping.",
    "Sivrendesh: Agent Wilkenson may have a lead. Go find him on the docks right away.",
    "Sivrendesh: You are the hand of The Academy of Arcane Science in this matter. For the reputation of our house, you must succeed.",
    "You have received a quest!"
}
StartQuest(mySession, 130120, quests[130120][0].log)
else
    npcDialogue = "Are you ready for my final mission?"
    diagOptions = { "I am.", "I never want it to end." }
end
else
npcDialogue ="Sivrendesh: I have something very important coming up for you. Please check back with me later."
end
elseif (GetPlayerFlags(mySession, "130120") == "6") then
if (CheckQuestItem(mySession, items.NOTE_FROM_TELINA, 1))
 then
if (choice:find("details")) then
npcDialogue = "Sivrendesh: Time is running out. You must complete this mission. Go speak to Telina the Dark Witch."
elseif (choice:find("Telina")) then
multiDialogue = { "Sivrendesh: Oh my...That is very interesting. She...is going to... Nevermind that.",
    "Sivrendesh: Well it appears that all of our affairs are in order here.",
    "Sivrendesh: Once again, you have proven yourself worthy as an wizard of The Academy of Arcane Science.",
    "Sivrendesh: As payment for your services, you've earned a set of rewards. The first is a Blazing Clash Scroll, which is a concussive blast that injures and bewilders the target.",
    "Sivrendesh: I also reward you with this Mental Focus Scroll, which returns power to you.",
"You have given away a note from Telina.",
"You have received a Blazing Clash Scroll.",
"You have received a Mental Focus Scroll.",
    "You have finished a quest!",
"Sivrendesh: Incredibly, you have learned all that I can teach you, playerName. I know that you feel this power within.",
"Sivrendesh: This is rare, but I can no longer call you my apprentice. You have grown too strong. One day you may even surpass me in power. Tunaria needs powerful and wise people like you. Go on now."
}
CompleteQuest(mySession, 130120, quests[130120][6].xp, 130121 )
TurnInItem(mySession, items.NOTE_FROM_TELINA, 1)
GrantItem(mySession, items.BLAZING_CLASH, 1)
GrantItem(mySession, items.MENTAL_FOCUS, 1)
else
    npcDialogue = "What update do you have for me?"
    diagOptions = { "I have a note from Telina.", "Just a few details to wrap up..." }
end
else
npcDialogue = "Sivrendesh: Time is running out. You must complete this mission. Go speak to Telina the Dark Witch."
end
  else
        npcDialogue =
"Sivrendesh: We wizards must know when to hold back our dangerous spells, especially when we become aggravated at the constant rabble that traipse through here."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

