local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Magician(10) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "100113") == "1") then
if (choice:find("building")) then
multiDialogue = { "Ilenar Crelwin: If you weren't sent to me, then you will leave at once or I will call the guards to have you removed."
 } 
elseif (choice:find("your")) then
multiDialogue = { "Ilenar Crelwin: Oh I am not having a pleasant time at all. Not one bit I tell you. First my robe was damaged on the way here, and now I find sand everywhere in my quarters....",
    "Ilenar Crelwin: No matter, you will serve my needs just fine. Do you see this awful tear in my robe? It must be stitched at once. I can't be seen around Freeport without it like some vagabond from Qeynos.",
    "Ilenar Crelwin: The only one with the skill to fix this is all the way in Bobble-by-Water. You must take my robe to him.",
    "Ilenar Crelwin: Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe. Make haste and return to me.",
"You have received a damaged robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100113, quests[100113][1].log)
GrantItem(mySession, items.DAMAGED_ROBE, 1)
else
    npcDialogue = "You had better be the one sent from The Academy of Arcane Science. I must me tended to at once!"
    diagOptions = { "...And how is your day?", "Oh sorry, I must be in the wrong building." }
end
elseif (GetPlayerFlags(mySession, "100113") == "5") then
multiDialogue = { "Ilenar Crelwin: Where have you been? While you have been taking your sweet time, I've had to deal with the indecency of being seen without my official robe!",
    "Ilenar Crelwin: ...I'll take my robe now....What is this? It is covered in chocolate! This is preposterous! What idiot ruined my precious robe?",
    "Ilenar Crelwin: This is the sloppy work of Delwin Stitchfinger! I am furious!...He will soon know what it means to cross me.",
    "Ilenar Crelwin: I have another mission for you. One even more serious. We will send him a special \"thank you\" along with something he holds so dear.",
    "Ilenar Crelwin: Please return to Delwin with this box of special chocolates. Be sure to let him know Ilenar \"appreciates\" his service. Return to me when this is done.",
    "You have finished a quest!",
    "You have received a quest!",
"You have given away a chocolate stained robe.",
"You have received a special box of chocolates."
}
ContinueQuest(mySession, 100113, quests[100113][5].log)
TurnInItem(mySession, items.CHOCOLATE_STAINED_ROBE, 1)
GrantItem(mySession, items.SPECIAL_BOX_OF_CHOCOLATES, 1)
elseif (GetPlayerFlags(mySession, "100113") == "7") then
if (choice:find("leaving")) then
multiDialogue = { "Ilenar Crelwin: I am not accustomed to being denied my will. Few people live to tell about it. I will give you one last chance to complete your task, or if I must, I can make you disappear!",
"Ilenar Crelwin: Now, go, and do not return to me until this task is done!"
 } 
elseif (choice:find("chocolate")) then
multiDialogue = { "Ilenar Crelwin: Ah yes. The sweet taste of revenge. These moments must be savored, you know. They wont come often enough.",
    "Ilenar Crelwin: You have pleased me for now. I will let Kellina know of my satisfaction.",
    "Ilenar Crelwin: I will have more work for you after I've sorted a few things out, so check back with me later. For your reward, take this powerful scroll.",
"You have received a Lava Wind Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 100113, quests[100113][7].xp, 100115 )
GrantItem(mySession, items.LAVA_WIND, 1)
else
    npcDialogue = "Tell me at once, playerName. What you have done?"
    diagOptions = { "I gave Delwin the chocolate. He's dead.", "You are mad, I am leaving." }
end
elseif (GetPlayerFlags(mySession, "100115") == "0") then
if (level >=15) then
if (choice:find("Actually")) then
multiDialogue = { "Ilenar Crelwin: If you think you can escape my command, I assure you playerName, I will have you hunted, and brought to justice. My justice."
 } 
elseif (choice:find("another")) then
multiDialogue = { "Ilenar Crelwin: You served me well before playerName, so I will forgive your tardiness, but don't let it happen again.",
    "Ilenar Crelwin: I am working on a new spell, something never before attempted. But I need some rare ingredients.",
    "Ilenar Crelwin: Nightworm roots are banned in most cities. It is a rare plant that grows only in the fetid marshes of the south, and their poisonous properties make them illegal.",
    "Ilenar Crelwin: Fortunately I have a contact, Dagget Klem, who can get some. Klem runs a smuggling ring in a small fishing village called Temby, along the coast not far north of Freeport.",
    "Ilenar Crelwin: Journey to Temby and arrange for the roots through Dagget Klem. Return to me when you have them.",
    "You have received a quest!"
}
StartQuest(mySession, 100115, quests[100115][0].log)
else
    npcDialogue = "So you finally decided to grace me with your presence?"
    diagOptions = { "I am back for another mission.", "Actually, I'd rather not talk to you right now..." }
end
else
npcDialogue ="Ilenar Crelwin: I have nothing to say to you at the moment. But don't forget to check back with me soon!"
end
elseif (GetPlayerFlags(mySession, "100115") == "3") then
if (CheckQuestItem(mySession, items.NIGHTWORM_ROOTS, 1))
 then
multiDialogue = { "Ilenar Crelwin: The roots! You have them. I would recognize that smell anywhere. No one saw you with them right?",
    "Ilenar Crelwin: Now then, on to the next task. I'll need something quite dangerous to for you to fetch. I need the blood of madmen.",
    "Ilenar Crelwin: Along the coast, not too far south of Freeport, you will find the ruins of a great stone monolith. Search there for madmen.",
    "Ilenar Crelwin: You might need help with this. Those madmen are deadly. Don't return to me till you have some of their blood.",
"You have given away the nightworm roots.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100115, quests[100115][3].log)
TurnInItem(mySession, items.NIGHTWORM_ROOTS, 1)
else
npcDialogue = "Ilenar Crelwin: Bring these items to me as soon as you possibly can. I need nightworm root from Dagget Klem in Temby. What are you doing wasting time here?"
end
elseif (GetPlayerFlags(mySession, "100115") == "4") then
if (CheckQuestItem(mySession, items.BLOOD_OF_THE_DESERT_MADMAN, 1))
 then
multiDialogue = { "Ilenar Crelwin: I can see by the sand on your boots that you've been to the desert. And there it is...Blood of madmen.",
    "Ilenar Crelwin: Once again, you returned to me alive, when almost any other young assistant would have perished.",
    "Ilenar Crelwin: These items you have brought me will do wonders for my research. My magic will one day be the talk of Tunaria. You may take pride in knowing that you have played a small part in that story.",
    "Ilenar Crelwin: I am done with you. I believe Kellina has something for you at this moment. Be gone with you now!",
"You have given away the blood of the desert madman.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100115, quests[100115][4].log)
TurnInItem(mySession, items.BLOOD_OF_THE_DESERT_MADMAN, 1)
else
npcDialogue = "Ilenar Crelwin: Bring these items to me as soon as you possibly can. Find madmen at ruins south of Freeport. Return to me with some of their blood. Stop dallying!"
end
elseif (GetPlayerFlags(mySession, "100120") == "1") then
if (choice:find("ready")) then
multiDialogue = { "Ilenar Crelwin: Excellent. We may make a powerful magician out of you yet, playerName. What I am about to tell you is a secret that you must take to your grave There is a book known as Geldwins Grimoire.",
    "Ilenar Crelwin: It contains powerful and dangerous knowledge. Many have tried to destroy the book and failed. The Grimoire is held in the Temple of Light just west of here, along with various other forbidden texts.",
    "Ilenar Crelwin: The Temple is guarded by The Paladins of Light. They wouldn't just hand over such a dangerous relic, so we will have to be creative.",
    "Ilenar Crelwin: I know a forger named Crim Arikson. He could forge a letter for you to present to the paladins, if he was properly motivated.",
    "Ilenar Crelwin: Crim is at the inn in the village of Bastable, which lies along the west road to Highpass Hold. Beware...these are dangerous roads. Now, go...find Crim.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100120, quests[100120][1].log)
else
    npcDialogue = "Not tired of me yet? I thought for a moment you would have grown rather weary of my tasks."
    diagOptions = { "I am ready for what is next." }
end
elseif (GetPlayerFlags(mySession, "100120") == "5") then
if (CheckQuestItem(mySession, items.AMULET_OF_DECEPTION, 1)
and CheckQuestItem(mySession, items.FORGED_LETTER, 1))
 then
if (choice:find("requested")) then
multiDialogue = { "Ilenar Crelwin: Well done. You are quite resourceful! This Amulet will do wonders for my research. Now then, it is time for you to travel to the Temple of Light, west of Freeport.",
    "Ilenar Crelwin: When you are ready, exit Freeport from the north, then travel west along the mountainside to reach the Temple of Light.",
    "Ilenar Crelwin: Within the temple walls, you will find a library where the forbidden texts are kept. The chief librarian there is named Leandro Novan. Present him with the forged letter.",
    "Ilenar Crelwin: You may be questioned as to where you are from. Remember that Sir Hanst Breach resides in Qeynos, and serves at the pleasure of Antonius Bayle II.",
    "Ilenar Crelwin: Go now. I need the Grimoire for my next stage of research. We are so close, I can taste it.",
"You have given away an Amulet of Deception.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100120, quests[100120][5].log)
TurnInItem(mySession, items.AMULET_OF_DECEPTION, 1)
else
    npcDialogue = "I had no doubt that you could pull this off playerName."
    diagOptions = { "Here is the amulet and letter you requested..." }
end
else
npcDialogue = "Ilenar Crelwin: Time is running out. You must complete this mission. I need a forged Letter from Crim, and the Amulet of deception from Swiftwind Galeehart near Hangman's Hill."
end
elseif (GetPlayerFlags(mySession, "100120") == "8") then
if (CheckQuestItem(mySession, items.GELDWINS_GRIMOIRE, 1))
 then
if (choice:find("right")) then
multiDialogue = { "Ilenar Crelwin: This is it! I cannot believe my eyes, that this great and terrible thing has finally come to me. I owe you so much. You have truly proven yourself to me. Soon the rest of the world will know, I'm sure.",
    "Ilenar Crelwin: Go see Kellina at once playerName. I have already made arrangements for your reward. Now leave me.",
"You have given away a Geldwins Grimoire.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100120, quests[100120][8].log)
TurnInItem(mySession, items.GELDWINS_GRIMOIRE, 1)
else
    npcDialogue = "Please tell me that you have the Grimoire. I've put my life's work into this magic. I must have it."
    diagOptions = { "I have it right here." }
end
else
npcDialogue = "Ilenar Crelwin: Time is running out. You must complete this mission. I need Geldwins Grimoire to complete my life's work. You must retrieve it from The Temple of Light."
end
  else
        npcDialogue =
"Ilenar Crelwin: If you weren't sent to me, then you will leave at once or I will call the guards to have you removed."
    end
------
--Bard(5) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "50113") == "1") then
if (choice:find("building")) then
multiDialogue = { "Ilenar Crelwin: If you weren't sent to me, then you will leave at once or I will call the guards to have you removed."
 } 
elseif (choice:find("your")) then
multiDialogue = { "Ilenar Crelwin: Oh I am not having a pleasant time at all. Not one bit I tell you. First my robe was damaged on the way here, and now I find sand everywhere in my quarters....",
    "Ilenar Crelwin: No matter, you will serve my needs just fine. Do you see this awful tear in my robe? It must be stitched at once. I can't be seen around Freeport without it like some vagabond from Qeynos.",
    "Ilenar Crelwin: The only one with the skill to fix this is all the way in Bobble-by-Water. You must take my robe to him.",
    "Ilenar Crelwin: Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe. Make haste and return to me.",
"You have received a damaged robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50113, quests[50113][1].log)
GrantItem(mySession, items.DAMAGED_ROBE, 1)
else
    npcDialogue = "You had better be the one sent from The Silken Gauntlet. I must be tended to at once!"
    diagOptions = { "...And how is your day?", "Oh sorry, I must be in the wrong building." }
end
elseif (GetPlayerFlags(mySession, "50113") == "5") then
multiDialogue = { "Ilenar Crelwin: Where have you been? While you have been taking your sweet time, I've had to deal with the indecency of being seen without my official robe!",
    "Ilenar Crelwin: ...I'll take my robe now....What is this? It is covered in chocolate! This is preposterous! What idiot ruined my precious robe?",
    "Ilenar Crelwin: This is the sloppy work of Delwin Stitchfinger! I am furious!...He will soon know what it means to cross me.",
    "Ilenar Crelwin: I have another mission for you. One even more serious. We will send him a special \"thank you\" along with something he holds so dear.",
    "Ilenar Crelwin: Please return to Delwin with this box of special chocolates. Be sure to let him know Ilenar \"appreciates\" his service. Return to me when this is done.",
    "Ilenar Crelwin: Oh, I almost forgot. Solenia has requested your audience, playerName. Do so quickly, and return to Delwin right away.",
    "You have finished a quest!",
    "You have received a quest!",
"You have given away a chocolate stained robe.",
"You have received a special box of chocolates."
}
ContinueQuest(mySession, 50113, quests[50113][5].log)
TurnInItem(mySession, items.CHOCOLATE_STAINED_ROBE, 1)
GrantItem(mySession, items.SPECIAL_BOX_OF_CHOCOLATES, 1)
elseif (GetPlayerFlags(mySession, "50113") == "8") then
if (choice:find("leaving")) then
multiDialogue = { "Ilenar Crelwin: I am not accustomed to being denied my will. Few people live to tell about it. I will give you one last chance to complete your task, or if I must, I can make you disappear!",
"Ilenar Crelwin: Now, go, and do not return to me until this task is done!"
 } 
elseif (choice:find("chocolate")) then
multiDialogue = { "Ilenar Crelwin: Ah yes. The sweet taste of revenge. These moments must be savored, you know. They wont come often enough.",
    "Ilenar Crelwin: You have pleased me for now. I will let Solenia know of my satisfaction.",
    "Ilenar Crelwin: I will have more work for you after I've sorted a few things out, so check back with me later. For your reward, take this powerful scroll.",
"You have received an Anthem of Light Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 50113, quests[50113][8].xp, 50115 )
GrantItem(mySession, items.ANTHEM_OF_LIGHT, 1)
else
    npcDialogue = "Tell me at once what you have done, playerName."
    diagOptions = { "I gave Delwin the chocolate. He's dead.", "You are mad, I am leaving." }
end
elseif (GetPlayerFlags(mySession, "50115") == "0") then
if (level >=15) then
if (choice:find("Actually")) then
multiDialogue = { "Ilenar Crelwin: If you think you can escape my command, I assure you playerName, I will have you hunted, and brought to justice. My justice."
 } 
elseif (choice:find("returned")) then
multiDialogue = { "Ilenar Crelwin: You served me well before playerName, so I will forgive your tardiness, but don't let it happen again.",
    "Ilenar Crelwin: I am working on a new spell, something never before attempted. But I need some rare ingredients.",
    "Ilenar Crelwin: Nightworm roots are banned in most cities. It is a rare plant that grows only in the fetid marshes of the south, and their poisonous properties make them illegal.",
    "Ilenar Crelwin: Fortunately I have a contact, Dagget Klem, who can get some. Klem runs a smuggling ring in a small fishing village called Temby, along the coast not far north of Freeport.",
    "Ilenar Crelwin: Journey to Temby and arrange for the roots through Dagget Klem. Return to me when you have them.",
    "You have received a quest!"
}
StartQuest(mySession, 50115, quests[50115][0].log)
else
    npcDialogue = "So you have finally decided to grace me with your presence?"
    diagOptions = { "I have returned to assist you further.", "Actually, I'd rather not talk to you right now..." }
end
else
npcDialogue ="Ilenar Crelwin: I have nothing to say to you at the moment. But don't forget to check back with me soon!"
end
elseif (GetPlayerFlags(mySession, "50115") == "3") then
if (CheckQuestItem(mySession, items.NIGHTWORM_ROOTS, 1))
 then
multiDialogue = { "Ilenar Crelwin: The roots! You have them. I would recognize that smell anywhere. No one saw you with them right?",
    "Ilenar Crelwin: Now then, on to the next task. I'll need something quite dangerous to for you to fetch. I need the blood of madmen.",
    "Ilenar Crelwin: Along the coast, not too far south of Freeport, you will find the ruins of a great stone monolith. Search there for madmen.",
    "Ilenar Crelwin: You might need help with this. Those madmen are deadly. Don't return to me till you have some of their blood.",
"You have given away the nightworm roots.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50115, quests[50115][3].log)
TurnInItem(mySession, items.NIGHTWORM_ROOTS, 1)
else
npcDialogue = "Ilenar Crelwin: Bring this to me as soon as you possibly can. I need nightworm root from Dagget Klem in Temby. What are you doing wasting time here?"
end
elseif (GetPlayerFlags(mySession, "50115") == "4") then
if (CheckQuestItem(mySession, items.BLOOD_OF_THE_DESERT_MADMAN, 1))
 then
multiDialogue = { "Ilenar Crelwin: I can see by the sand on your boots that you've been to the desert. And there it is...Blood of madmen.",
    "Ilenar Crelwin: Once again playerName, you've returned to me alive, when almost any other young assistant would have perished.",
    "Ilenar Crelwin: These items you have brought me will do wonders for my research. My magic will one day be the talk of Tunaria. You may take pride in knowing that you have played a small part in that story.",
    "Ilenar Crelwin: I am done with you. I believe Solenia has something for you at this moment. Be gone with you now!",
"You have given away the blood of the desert madman.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50115, quests[50115][4].log)
TurnInItem(mySession, items.BLOOD_OF_THE_DESERT_MADMAN, 1)
else
npcDialogue = "Ilenar Crelwin: Bring this to me as soon as you possibly can. Find madmen at ruins south of Freeport. Return to me with some of their blood. Stop dallying!"
end
elseif (GetPlayerFlags(mySession, "50120") == "1") then
if (choice:find("ready")) then
multiDialogue = { "Ilenar Crelwin: Excellent, playerName. We may make a powerful bard out of you yet. What I am about to tell you is a secret that you must take to your grave.",
    "Ilenar Crelwin: There is a book known as Geldwins Grimoire, which contains powerful and dangerous knowledge. Many have tried to destroy the book and failed.",
    "Ilenar Crelwin: The Grimoire is being held in the Temple of Light just west of here, along with various other forbidden texts. The Temple is guarded by The Paladins of Light.",
    "Ilenar Crelwin: They wouldn't just hand over such a dangerous relic, so we will have to be creative. I know a forger named Crim Arikson. He could forge a letter for you to present to the paladins...",
    "Ilenar Crelwin: ...If he was properly motivated, perhaps. Crim is at the inn in the village of Bastable, which lies along the west road to Highpass Hold. Beware...these are dangerous roads. Now, go...find Crim.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50120, quests[50120][1].log)
else
    npcDialogue = "Not tired of me yet? I thought for a moment you would have grown rather weary of my tasks."
    diagOptions = { "I am ready for what is next." }
end
elseif (GetPlayerFlags(mySession, "50120") == "5") then
if (CheckQuestItem(mySession, items.AMULET_OF_DECEPTION, 1)
and CheckQuestItem(mySession, items.FORGED_LETTER, 1))
 then
if (choice:find("requested")) then
multiDialogue = { "Ilenar Crelwin: Well done. You are quite resourceful! This Amulet will do wonders for my research. Now then, it is time for you to travel to the Temple of Light, west of Freeport.",
    "Ilenar Crelwin: When you are ready, exit Freeport from the north, then travel west along the mountainside to reach the Temple of Light.",
    "Ilenar Crelwin: Within the temple walls, you will find a library where the forbidden texts are kept. The chief librarian there is named Leandro Novan. Present him with the forged letter.",
    "Ilenar Crelwin: You may be questioned as to where you are from. Remember that Sir Hanst Breach resides in Qeynos, and serves at the pleasure of Antonius Bayle II.",
    "Ilenar Crelwin: You should check with Solenia first to make sure you are fully prepared for this journey. Go now, playerName. I need the Grimoire for my next stage of research. We are so close, I can taste it.",
"You have given away the Amulet of Deception.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50120, quests[50120][5].log)
TurnInItem(mySession, items.AMULET_OF_DECEPTION, 1)
else
    npcDialogue = "I had no doubt that you could pull this off playerName."
    diagOptions = { "Here is the amulet and letter you requested..." }
end
else
npcDialogue = "Ilenar Crelwin: Time is running out. You must complete this mission. I need a forged Letter from Crim, and the Amulet of deception from Swiftwind Galeehart near Hangman's Hill."
end
elseif (GetPlayerFlags(mySession, "50120") == "10") then
if (CheckQuestItem(mySession, items.FAKE_GELDWINS_GRIMOIRE, 1))
 then
if (choice:find("right")) then
multiDialogue = { "Ilenar Crelwin: This is it! I cannot believe my eyes, that this great and terrible thing has finally come to me. I owe you so much. You have truly proven yourself to me. Soon the rest of the world will know, I'm sure.",
    "Ilenar Crelwin: Go see Solenia at once playerName. I have already made arrangements for your reward. Now leave me.",
"You have given away a Fake Geldwins Grimoire.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50120, quests[50120][10].log)
TurnInItem(mySession, items.FAKE_GELDWINS_GRIMOIRE, 1)
else
    npcDialogue = "Please tell me that you have the Grimoire. I've put my life's work into this magic. I must have it."
    diagOptions = { "I have it right here." }
end
else
npcDialogue = "Ilenar Crelwin: Time is running out. You must complete this mission. I need Geldwins Grimoire to complete my life's work. You must retrieve it from The Temple of Light."
end
  else
        npcDialogue =
"Ilenar Crelwin: If you weren't sent to me, then you will leave at once or I will call the guards to have you removed."
    end
------
--Cleric(9) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "90113") == "1") then
if (choice:find("building")) then
multiDialogue = { "Ilenar Crelwin: If you weren't sent to me, then you will leave at once or I will call the guards to have you removed."
 } 
elseif (choice:find("your")) then
multiDialogue = { "Ilenar Crelwin: Oh I am not having a pleasant time at all. Not one bit I tell you. First my robe was damaged on the way here, and now I find sand everywhere in my quarters....",
    "Ilenar Crelwin: No matter, you will serve my needs just fine. Do you see this awful tear in my robe? It must be stitched at once. I can't be seen around Freeport without it like some vagabond from Qeynos.",
    "Ilenar Crelwin: The only one with the skill to fix this is all the way in Bobble-by-Water. You must take my robe to him.",
    "Ilenar Crelwin: Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe. Make haste and return to me.",
"You have received a damaged robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90113, quests[90113][1].log)
GrantItem(mySession, items.DAMAGED_ROBE, 1)
else
    npcDialogue = "You had better be the one sent to me from The Shining Shield Mercenaries. I must me tended to at once!"
    diagOptions = { "...And how is your day?", "Oh sorry, I must be in the wrong building." }
end
elseif (GetPlayerFlags(mySession, "90113") == "5") then
multiDialogue = { "Ilenar Crelwin: Where have you been? While you have been taking your sweet time, I've had to deal with the indecency of being seen without my official robe!",
    "Ilenar Crelwin: ...I'll take my robe now....What is this? It is covered in chocolate! This is preposterous! What idiot ruined my precious robe?",
    "Ilenar Crelwin: This is the sloppy work of Delwin Stitchfinger! I am furious!...He will soon know what it means to cross me.",
    "Ilenar Crelwin: I have another mission for you. One even more serious. We will send him a special \"thank you\" along with something he holds so dear.",
    "Ilenar Crelwin: Please return to Delwin with this box of special chocolates. Be sure to let him know Ilenar \"appreciates\" his service. Return to me when this is done.",
    "You have finished a quest!",
    "You have received a quest!",
"You have given away a chocolate stained robe.",
"You have received a special box of chocolates."
}
ContinueQuest(mySession, 90113, quests[90113][5].log)
TurnInItem(mySession, items.CHOCOLATE_STAINED_ROBE, 1)
GrantItem(mySession, items.SPECIAL_BOX_OF_CHOCOLATES, 1)
elseif (GetPlayerFlags(mySession, "90113") == "7") then
if (choice:find("leaving")) then
multiDialogue = { "Ilenar Crelwin: I am not accustomed to being denied my will. Few people live to tell about it. I will give you one last chance to complete your task, or if I must, I can make you disappear!",
"Ilenar Crelwin: Now, go, and do not return to me until this task is done!"
 } 
elseif (choice:find("chocolate")) then
multiDialogue = { "Ilenar Crelwin: Ah yes. The sweet taste of revenge. These moments must be savored, you know. They wont come often enough.",
    "Ilenar Crelwin: You have pleased me for now. I will let Sister know of my satisfaction.",
    "Ilenar Crelwin: I will have more work for you after I've sorted a few things out, so check back with me later. For your reward, take this powerful scroll.",
"You have received a Ward Death Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 90113, quests[90113][7].xp, 90115 )
GrantItem(mySession, items.WARD_DEATH, 1)
else
    npcDialogue = "Tell me at once, playerName. What you have done?"
    diagOptions = { "I gave Delwin the chocolate. He's dead.", "You are mad, I am leaving." }
end
elseif (GetPlayerFlags(mySession, "90115") == "0") then
if (level >=15) then
if (choice:find("Actually")) then
multiDialogue = { "Ilenar Crelwin: If you think you can escape my command, I assure you playerName, I will have you hunted, and brought to justice. My justice."
 } 
elseif (choice:find("another")) then
multiDialogue = { "Ilenar Crelwin: You served me well before playerName, so I will forgive your tardiness, but don't let it happen again.",
    "Ilenar Crelwin: I am working on a new spell, something never before attempted. But I need some rare ingredients.",
    "Ilenar Crelwin: Nightworm roots are banned in most cities. It is a rare plant that grows only in the fetid marshes of the south, and their poisonous properties make them illegal.",
    "Ilenar Crelwin: Fortunately I have a contact, Dagget Klem, who can get some. Klem runs a smuggling ring in a small fishing village called Temby, along the coast not far north of Freeport.",
    "Ilenar Crelwin: Journey to Temby and arrange for the roots through Dagget Klem. Return to me when you have them.",
    "You have received a quest!"
}
StartQuest(mySession, 90115, quests[90115][0].log)
else
    npcDialogue = "So you finally decided to grace me with your presence?"
    diagOptions = { "I am back for another mission.", "Actually, I'd rather not talk to you right now..." }
end
else
npcDialogue ="Ilenar Crelwin: I have nothing to say to you at the moment. But don't forget to check back with me soon!"
end
elseif (GetPlayerFlags(mySession, "90115") == "3") then
if (CheckQuestItem(mySession, items.NIGHTWORM_ROOTS, 1))
 then
multiDialogue = { "Ilenar Crelwin: The roots! You have them. I would recognize that smell anywhere. No one saw you with them right?",
    "Ilenar Crelwin: Now then, on to the next task. I'll need something quite dangerous to for you to fetch. I need the blood of madmen.",
    "Ilenar Crelwin: Along the coast, not too far south of Freeport, you will find the ruins of a great stone monolith. Search there for madmen.",
    "Ilenar Crelwin: You might need help with this. Those madmen are deadly. Don't return to me till you have some of their blood.",
"You have given away the nightworm roots.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90115, quests[90115][3].log)
TurnInItem(mySession, items.NIGHTWORM_ROOTS, 1)
else
npcDialogue = "Ilenar Crelwin: Bring these items to me as soon as you possibly can. I need nightworm root from Dagget Klem in Temby. What are you doing wasting time here?"
end
elseif (GetPlayerFlags(mySession, "90115") == "4") then
if (CheckQuestItem(mySession, items.BLOOD_OF_THE_DESERT_MADMAN, 1))
 then
multiDialogue = { "Ilenar Crelwin: I can see by the sand on your boots that you've been to the desert. And there it is...Blood of madmen.",
    "Ilenar Crelwin: Once again, you returned to me alive, when almost any other young assistant would have perished.",
    "Ilenar Crelwin: These items you have brought me will do wonders for my research. My magic will one day be the talk of Tunaria. You may take pride in knowing that you have played a small part in that story.",
    "Ilenar Crelwin: I am done with you. I believe Sister has something for you at this moment. Be gone with you now!",
"You have given away the blood of the desert madman.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90115, quests[90115][4].log)
TurnInItem(mySession, items.BLOOD_OF_THE_DESERT_MADMAN, 1)
else
npcDialogue = "Ilenar Crelwin: Bring these items to me as soon as you possibly can. Find madmen at ruins south of Freeport. Return to me with some of their blood. Stop dallying!"
end
elseif (GetPlayerFlags(mySession, "90120") == "1") then
if (choice:find("ready")) then
multiDialogue = { "Ilenar Crelwin: Excellent. We may make a powerful cleric out of you yet, playerName. What I am about to tell you is a secret that you must take to your grave. There is a book known as Geldwins Grimoire.",
    "Ilenar Crelwin: It contains powerful and dangerous knowledge. Many have tried to destroy the book and failed. The Grimoire is held in the Temple of Light just west of here, along with various other forbidden texts.",
    "Ilenar Crelwin: The Temple is guarded by The Paladins of Light. They wouldn't just hand over such a dangerous relic, so we will have to be creative.",
    "Ilenar Crelwin: I know a forger named Crim Arikson. He could forge a letter for you to present to the paladins, if he was properly motivated.",
    "Ilenar Crelwin: Crim is at the inn in the village of Bastable, which lies along the west road to Highpass Hold. Beware...these are dangerous roads. Now, go...speak with Crim.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90120, quests[90120][1].log)
else
    npcDialogue = "Not tired of me yet? I thought for a moment you would have grown rather weary of my tasks."
    diagOptions = { "I am ready for what is next." }
end
elseif (GetPlayerFlags(mySession, "90120") == "5") then
if (CheckQuestItem(mySession, items.AMULET_OF_DECEPTION, 1)
and CheckQuestItem(mySession, items.FORGED_LETTER, 1))
 then
if (choice:find("requested")) then
multiDialogue = { "Ilenar Crelwin: Well done. You are quite resourceful! This amulet will do wonders for my research. Now then, it is time for you to travel to the Temple of Light, west of Freeport.",
    "Ilenar Crelwin: When you are ready, exit Freeport from the north, then travel west along the mountainside to reach the Temple of Light.",
    "Ilenar Crelwin: Within the temple walls, you will find a library where the forbidden texts are kept. The chief librarian there is named Leandro Novan. Present him with the forged letter.",
    "Ilenar Crelwin: You may be questioned as to where you are from. Remember that Sir Hanst Breach resides in Qeynos, and serves at the pleasure of Antonius Bayle II.",
    "Ilenar Crelwin: Go now. I need the Grimoire for my next stage of research. We are so close, I can taste it.",
"You have given away an Amulet of Deception.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90120, quests[90120][5].log)
TurnInItem(mySession, items.AMULET_OF_DECEPTION, 1)
else
    npcDialogue = "I had no doubt that you could pull this off playerName."
    diagOptions = { "Here is the amulet and letter you requested..." }
end
else
npcDialogue = "Ilenar Crelwin: Time is running out. You must complete this mission. I need a forged Letter from Crim, and the Amulet of deception from Swiftwind Galeehart near Hangman's Hill."
end
elseif (GetPlayerFlags(mySession, "90120") == "8") then
if (CheckQuestItem(mySession, items.GELDWINS_GRIMOIRE, 1))
 then
if (choice:find("right")) then
multiDialogue = { "Ilenar Crelwin: This is it! I cannot believe my eyes, that this great and terrible thing has finally come to me. I owe you so much. You have truly proven yourself to me. Soon the rest of the world will know, I'm sure.",
    "Ilenar Crelwin: Go see Sister Falhelm at once playerName. I have already made arrangements for your reward. Now leave me.",
"You have given away a Geldwins Grimoire.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90120, quests[90120][8].log)
TurnInItem(mySession, items.GELDWINS_GRIMOIRE, 1)
else
    npcDialogue = "Please tell me that you have the Grimoire. I've put my life's work into this magic. I must have it."
    diagOptions = { "I have it right here." }
end
else
npcDialogue = "Ilenar Crelwin: Time is running out. You must complete this mission. I need Geldwins Grimoire to complete my life's work. You must retrieve it from The Temple of Light."
end
  else
        npcDialogue =
"Ilenar Crelwin: If you weren't sent to me, then you will leave at once or I will call the guards to have you removed."
    end
------
--Rogue(6) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "60113") == "1") then
if (choice:find("building")) then
multiDialogue = { "Ilenar Crelwin: If you weren't sent to me, then you will leave at once or I will call the guards to have you removed."
 } 
elseif (choice:find("your")) then
multiDialogue = { "Ilenar Crelwin: Oh I am not having a pleasant time at all. Not one bit I tell you. First my robe was damaged on the way here, and now I find sand everywhere in my quarters....",
    "Ilenar Crelwin: No matter, you will serve my needs just fine. Do you see this awful tear in my robe? It must be stitched at once. I can't be seen around Freeport without it like some vagabond from Qeynos.",
    "Ilenar Crelwin: The only one with the skill to fix this is all the way in Bobble-by-Water. You must take my robe to him.",
    "Ilenar Crelwin: Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe. Make haste and return to me.",
"You have received a damaged robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60113, quests[60113][1].log)
GrantItem(mySession, items.DAMAGED_ROBE, 1)
else
    npcDialogue = "You had better be the one sent to me from The Shining Shield Mercenaries. I must be tended to at once!"
    diagOptions = { "...And how is your day?", "Oh sorry, I must be in the wrong building." }
end
elseif (GetPlayerFlags(mySession, "60113") == "5") then
multiDialogue = { "Ilenar Crelwin: Where have you been? While you have been taking your sweet time, I've had to deal with the indecency of being seen without my official robe!",
    "Ilenar Crelwin: ...I'll take my robe now....What is this? It is covered in chocolate! This is preposterous! What idiot ruined my precious robe?",
    "Ilenar Crelwin: This is the sloppy work of Delwin Stitchfinger! I am furious!...He will soon know what it means to cross me.",
    "Ilenar Crelwin: I have another mission for you. One even more serious. We will send him a special \"thank you\" along with something he holds so dear.",
    "Ilenar Crelwin: Please return to Delwin with this box of special chocolates. Be sure to let him know Ilenar \"appreciates\" his service. Return to me when this is done.",
    "You have finished a quest!",
    "You have received a quest!",
"You have given away a chocolate stained robe.",
"You have received a special box of chocolates."
}
ContinueQuest(mySession, 60113, quests[60113][5].log)
TurnInItem(mySession, items.CHOCOLATE_STAINED_ROBE, 1)
GrantItem(mySession, items.SPECIAL_BOX_OF_CHOCOLATES, 1)
elseif (GetPlayerFlags(mySession, "60113") == "7") then
if (choice:find("leaving")) then
multiDialogue = { "Ilenar Crelwin: I am not accustomed to being denied my will. Few people live to tell about it. I will give you one last chance to complete your task, or if I must, I can make you disappear!",
"Ilenar Crelwin: Now, go, and do not return to me until this task is done!"
 } 
elseif (choice:find("chocolate")) then
multiDialogue = { "Ilenar Crelwin: Ah yes. The sweet taste of revenge. These moments must be savored, you know. They wont come often enough.",
    "Ilenar Crelwin: You have pleased me for now. I will let Athera know of my satisfaction.",
    "Ilenar Crelwin: I will have more work for you after I've sorted a few things out, so check back with me later. For your reward, take this powerful scroll. It should suit someone of your skill.",
"You have received a Night Breath Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 60113, quests[60113][7].xp, 60115 )
GrantItem(mySession, items.NIGHT_BREATH, 1)
else
    npcDialogue = "Tell me at once, playerName. What you have done?"
    diagOptions = { "I gave Delwin the chocolate. He's dead.", "You are mad, I am leaving." }
end
elseif (GetPlayerFlags(mySession, "60115") == "0") then
if (level >=15) then
if (choice:find("Actually")) then
multiDialogue = { "Ilenar Crelwin: If you think you can escape my command, I assure you playerName, I will have you hunted, and brought to justice. My justice."
 } 
elseif (choice:find("another")) then
multiDialogue = { "Ilenar Crelwin: You served me well before playerName, so I will forgive your tardiness, but don't let it happen again.",
    "Ilenar Crelwin: I am working on a new spell, something never before attempted. But I need some rare ingredients.",
    "Ilenar Crelwin: Nightworm roots are banned in most cities. It is a rare plant that grows only in the fetid marshes of the south, and their poisonous properties make them illegal.",
    "Ilenar Crelwin: Fortunately I have a contact, Dagget Klem, who can get some. Klem runs a smuggling ring in a small fishing village called Temby, along the coast not far north of Freeport.",
    "Ilenar Crelwin: Journey to Temby and arrange for the roots through Dagget Klem. Return to me when you have them.",
    "You have received a quest!"
}
StartQuest(mySession, 60115, quests[60115][0].log)
else
    npcDialogue = "So you finally decided to grace me with your presence?"
    diagOptions = { "I am back for another mission.", "Actually, I'd rather not talk to you right now..." }
end
else
npcDialogue ="Ilenar Crelwin: I have nothing to say to you at the moment. But don't forget to check back with me soon!"
end
elseif (GetPlayerFlags(mySession, "60115") == "3") then
if (CheckQuestItem(mySession, items.NIGHTWORM_ROOTS, 1))
 then
multiDialogue = { "Ilenar Crelwin: The roots! You have them. I would recognize that smell anywhere. No one saw you with them right?",
    "Ilenar Crelwin: Now then, on to the next task. I'll need something quite dangerous to for you to fetch. I need the blood of madmen.",
    "Ilenar Crelwin: Along the coast, not too far south of Freeport, you will find the ruins of a great stone monolith. Search there for madmen.",
    "Ilenar Crelwin: You might need help with this. Those madmen are deadly. Don't return to me till you have some of their blood.",
"You have given away the nightworm roots.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60115, quests[60115][3].log)
TurnInItem(mySession, items.NIGHTWORM_ROOTS, 1)
else
npcDialogue = "Ilenar Crelwin: Bring these items to me as soon as you possibly can. I need nightworm root from Dagget Klem in Temby. What are you doing wasting time here?"
end
elseif (GetPlayerFlags(mySession, "60115") == "4") then
if (CheckQuestItem(mySession, items.BLOOD_OF_THE_DESERT_MADMAN, 1))
 then
multiDialogue = { "Ilenar Crelwin: I can see by the sand on your boots that you've been to the desert. And there it is...Blood of madmen.",
    "Ilenar Crelwin: Once again, you returned to me alive, when almost any other young assistant would have perished.",
    "Ilenar Crelwin: These items you have brought me will do wonders for my research. My magic will one day be the talk of Tunaria. You may take pride in knowing that you have played a small part in that story.",
    "Ilenar Crelwin: I am done with you. I believe Athera has something for you at this moment. Be gone with you now!",
"You have given away the blood of the desert madman.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60115, quests[60115][4].log)
TurnInItem(mySession, items.BLOOD_OF_THE_DESERT_MADMAN, 1)
else
npcDialogue = "Ilenar Crelwin: Bring these items to me as soon as you possibly can. Find madmen at ruins south of Freeport. Return to me with some of their blood. Stop dallying!"
end
elseif (GetPlayerFlags(mySession, "60120") == "1") then
if (choice:find("ready")) then
multiDialogue = { "Ilenar Crelwin: Excellent. We may make a powerful rogue out of you yet, playerName. What I am about to tell you is a secret that you must take to your grave. There is a book known as Geldwins Grimoire.",
    "Ilenar Crelwin: It contains powerful and dangerous knowledge. Many have tried to destroy the book and failed. The Grimoire is held in the Temple of Light just west of here, along with various other forbidden texts.",
    "Ilenar Crelwin: The Temple is guarded by The Paladins of Light. They wouldn't just hand over such a dangerous relic, so we will have to be creative.",
    "Ilenar Crelwin: I know a forger named Crim Arikson. He could forge a letter for you to present to the paladins, if he was properly motivated.",
    "Ilenar Crelwin: Crim is at the inn in the village of Bastable, which lies along the west road to Highpass Hold. Beware...these are dangerous roads. Now, go...speak with Crim.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60120, quests[60120][1].log)
else
    npcDialogue = "Not tired of me yet? I thought for a moment you would have grown rather weary of my tasks."
    diagOptions = { "I am ready for what is next." }
end
elseif (GetPlayerFlags(mySession, "60120") == "5") then
if (CheckQuestItem(mySession, items.AMULET_OF_DECEPTION, 1)
and CheckQuestItem(mySession, items.FORGED_LETTER, 1))
 then
if (choice:find("requested")) then
multiDialogue = { "Ilenar Crelwin: Well done. You are quite resourceful! This amulet will do wonders for my research. Now then, it is time for you to travel to the Temple of Light, west of Freeport.",
    "Ilenar Crelwin: When you are ready, exit Freeport from the north, then travel west along the mountainside to reach the Temple of Light.",
    "Ilenar Crelwin: Within the temple walls, you will find a library where the forbidden texts are kept. The chief librarian there is named Leandro Novan. Present him with the forged letter.",
    "Ilenar Crelwin: You may be questioned as to where you are from. Remember that Sir Hanst Breach resides in Qeynos, and serves at the pleasure of Antonius Bayle II.",
    "Ilenar Crelwin: Go now. I need the Grimoire for my next stage of research. We are so close, I can taste it.",
"You have given away an Amulet of Deception.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60120, quests[60120][5].log)
TurnInItem(mySession, items.AMULET_OF_DECEPTION, 1)
else
    npcDialogue = "I had no doubt that you could pull this off playerName."
    diagOptions = { "Here is the amulet and letter you requested..." }
end
else
npcDialogue = "Ilenar Crelwin: Time is running out. You must complete this mission. I need a forged Letter from Crim, and the Amulet of deception from Swiftwind Galeehart near Hangman's Hill."
end
elseif (GetPlayerFlags(mySession, "60120") == "8") then
if (CheckQuestItem(mySession, items.GELDWINS_GRIMOIRE, 1))
 then
if (choice:find("right")) then
multiDialogue = { "Ilenar Crelwin: This is it! I cannot believe my eyes, that this great and terrible thing has finally come to me. I owe you so much. You have truly proven yourself to me. Soon the rest of the world will know, I'm sure.",
    "Ilenar Crelwin: Go see Athera at once playerName. I have already made arrangements for your reward. Now leave me.",
"You have given away a Geldwins Grimoire.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60120, quests[60120][8].log)
TurnInItem(mySession, items.GELDWINS_GRIMOIRE, 1)
else
    npcDialogue = "Please tell me that you have the Grimoire. I've put my life's work into this magic. I must have it."
    diagOptions = { "I have it right here." }
end
else
npcDialogue = "Ilenar Crelwin: Time is running out. You must complete this mission. I need Geldwins Grimoire to complete my life's work. You must retrieve it from The Temple of Light."
end
  else
        npcDialogue =
"Ilenar Crelwin: If you weren't sent to me, then you will leave at once or I will call the guards to have you removed."
    end
------
--Warrior(0) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "113") == "1") then
if (choice:find("building")) then
multiDialogue = { "Ilenar Crelwin: If you weren't sent to me, then you will leave at once or I will call the guards to have you removed."
 } 
elseif (choice:find("your")) then
multiDialogue = { "Ilenar Crelwin: Oh I am not having a pleasant time at all. Not one bit I tell you. First my robe was damaged on the way here, and now I find sand everywhere in my quarters....",
    "Ilenar Crelwin: No matter, you will serve my needs just fine. Do you see this awful tear in my robe? It must be stitched at once. I can't be seen around Freeport without it like some vagabond from Qeynos.",
    "Ilenar Crelwin: The only one with the skill to fix this is all the way in Bobble-by-Water. You must take my robe to him.",
    "Ilenar Crelwin: Travel north along the river until you reach Bobble-by-Water. Find Delwin Stitchfinger, and have him repair the robe. Make haste and return to me.",
"You have received a damaged robe.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 113, quests[113][1].log)
GrantItem(mySession, items.DAMAGED_ROBE, 1)
else
    npcDialogue = "You had better be the one sent to me from The Freeport Militia. I must be tended to at once!"
    diagOptions = { "...And how is your day?", "Oh sorry, I must be in the wrong building." }
end
elseif (GetPlayerFlags(mySession, "113") == "5") then
multiDialogue = { "Ilenar Crelwin: Where have you been? While you have been taking your sweet time, I've had to deal with the indecency of being seen without my official robe!",
    "Ilenar Crelwin: ...I'll take my robe now....What is this? It is covered in chocolate! This is preposterous! What idiot ruined my precious robe?",
    "Ilenar Crelwin: This is the sloppy work of Delwin Stitchfinger! I am furious!...He will soon know what it means to cross me.",
    "Ilenar Crelwin: I have another mission for you. One even more serious. We will send him a special \"thank you\" along with something he holds so dear.",
    "Ilenar Crelwin: Please return to Delwin with this box of special chocolates. Be sure to let him know Ilenar \"appreciates\" his service. Return to me when this is done.",
    "You have finished a quest!",
    "You have received a quest!",
"You have given away a chocolate stained robe.",
"You have received a special box of chocolates."
}
ContinueQuest(mySession, 113, quests[113][5].log)
TurnInItem(mySession, items.CHOCOLATE_STAINED_ROBE, 1)
GrantItem(mySession, items.SPECIAL_BOX_OF_CHOCOLATES, 1)
elseif (GetPlayerFlags(mySession, "113") == "7") then
if (choice:find("leaving")) then
multiDialogue = { "Ilenar Crelwin: I am not accustomed to being denied my will. Few people live to tell about it. I will give you one last chance to complete your task, or if I must, I can make you disappear!",
"Ilenar Crelwin: Now, go, and do not return to me until this task is done!"
 } 
elseif (choice:find("chocolate")) then
multiDialogue = { "Ilenar Crelwin: Ah yes. The sweet taste of revenge. These moments must be savored, you know. They wont come often enough.",
    "Ilenar Crelwin: You have pleased me for now. I will let Captain Norgam know of my satisfaction.",
    "Ilenar Crelwin: I will have more work for you after I've sorted a few things out, so check back with me later. For your reward, take this powerful scroll. It should suit someone of your skill.",
"You have received a Rapid Strike Scroll.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 113, quests[113][7].xp, 115 )
GrantItem(mySession, items.RAPID_STRIKE, 1)
else
    npcDialogue = "Tell me at once, playerName. What you have done?"
    diagOptions = { "I gave Delwin the chocolate. He's dead.", "You are mad, I am leaving." }
end
elseif (GetPlayerFlags(mySession, "115") == "0") then
if (level >=15) then
if (choice:find("Actually")) then
multiDialogue = { "Ilenar Crelwin: If you think you can escape my command, I assure you playerName, I will have you hunted, and brought to justice. My justice."
 } 
elseif (choice:find("another")) then
multiDialogue = { "Ilenar Crelwin: You served me well before playerName, so I will forgive your tardiness, but don't let it happen again.",
    "Ilenar Crelwin: I am working on a new spell, something never before attempted. But I need some rare ingredients.",
    "Ilenar Crelwin: Nightworm roots are banned in most cities. It is a rare plant that grows only in the fetid marshes of the south, and their poisonous properties make them illegal.",
    "Ilenar Crelwin: Fortunately I have a contact, Dagget Klem, who can get some. Klem runs a smuggling ring in a small fishing village called Temby, along the coast not far north of Freeport.",
    "Ilenar Crelwin: Journey to Temby and arrange for the roots through Dagget Klem. Return to me when you have them.",
    "You have received a quest!"
}
StartQuest(mySession, 115, quests[115][0].log)
else
    npcDialogue = "So you finally decided to grace me with your presence?"
    diagOptions = { "I am back for another mission.", "Actually, I'd rather not talk to you right now..." }
end
else
npcDialogue ="Ilenar Crelwin: I have nothing to say to you at the moment. But don't forget to check back with me soon!"
end
elseif (GetPlayerFlags(mySession, "115") == "3") then
if (CheckQuestItem(mySession, items.NIGHTWORM_ROOTS, 1))
 then
multiDialogue = { "Ilenar Crelwin: The roots! You have them. I would recognize that smell anywhere. No one saw you with them right?",
    "Ilenar Crelwin: Now then, on to the next task. I'll need something quite dangerous to for you to fetch. I need the blood of madmen.",
    "Ilenar Crelwin: Along the coast, not too far south of Freeport, you will find the ruins of a great stone monolith. Search there for madmen.",
    "Ilenar Crelwin: You might need help with this. Those madmen are deadly. Don't return to me till you have some of their blood.",
"You have given away the nightworm roots.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 115, quests[115][3].log)
TurnInItem(mySession, items.NIGHTWORM_ROOTS, 1)
else
npcDialogue = "Ilenar Crelwin: Bring these items to me as soon as you possibly can. I need nightworm root from Dagget Klem in Temby. What are you doing wasting time here?"
end
elseif (GetPlayerFlags(mySession, "115") == "4") then
if (CheckQuestItem(mySession, items.BLOOD_OF_THE_DESERT_MADMAN, 1))
 then
multiDialogue = { "Ilenar Crelwin: I can see by the sand on your boots that you've been to the desert. And there it is...Blood of madmen.",
    "Ilenar Crelwin: Once again, you returned to me alive, when almost any other young assistant would have perished.",
    "Ilenar Crelwin: These items you have brought me will do wonders for my research. My magic will one day be the talk of Tunaria. You may take pride in knowing that you have played a small part in that story.",
    "Ilenar Crelwin: I am done with you. I believe Captain has something for you at this moment. Be gone with you now!",
"You have given away the blood of the desert madman.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 115, quests[115][4].log)
TurnInItem(mySession, items.BLOOD_OF_THE_DESERT_MADMAN, 1)
else
npcDialogue = "Ilenar Crelwin: Bring these items to me as soon as you possibly can. Find madmen at ruins south of Freeport. Return to me with some of their blood. Stop dallying!"
end
elseif (GetPlayerFlags(mySession, "120") == "1") then
if (choice:find("ready")) then
multiDialogue = { "Ilenar Crelwin: Excellent. We may make a powerful warrior out of you yet, playerName. What I am about to tell you is a secret that you must take to your grave. There is a book known as Geldwins Grimoire.",
    "Ilenar Crelwin: It contains powerful and dangerous knowledge. Many have tried to destroy the book and failed. The Grimoire is held in the Temple of Light just west of here, along with various other forbidden texts.",
    "Ilenar Crelwin: The Temple is guarded by The Paladins of Light. They wouldn't just hand over such a dangerous relic, so we will have to be creative.",
    "Ilenar Crelwin: I know a forger named Crim Arikson. He could forge a letter for you to present to the paladins, if he was properly motivated.",
    "Ilenar Crelwin: Crim is at the inn in the village of Bastable, which lies along the west road to Highpass Hold. Beware...these are dangerous roads. Now, go...speak with Crim.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120, quests[120][1].log)
else
    npcDialogue = "Not tired of me yet? I thought for a moment you would have grown rather weary of my tasks."
    diagOptions = { "I am ready for what is next." }
end
elseif (GetPlayerFlags(mySession, "120") == "5") then
if (CheckQuestItem(mySession, items.AMULET_OF_DECEPTION, 1)
and CheckQuestItem(mySession, items.FORGED_LETTER, 1))
 then
if (choice:find("requested")) then
multiDialogue = { "Ilenar Crelwin: Well done. You are quite resourceful! This amulet will do wonders for my research. Now then, it is time for you to travel to the Temple of Light, west of Freeport.",
    "Ilenar Crelwin: When you are ready, exit Freeport from the north, then travel west along the mountainside to reach the Temple of Light.",
    "Ilenar Crelwin: Within the temple walls, you will find a library where the forbidden texts are kept. The chief librarian there is named Leandro Novan. Present him with the forged letter.",
    "Ilenar Crelwin: You may be questioned as to where you are from. Remember that Sir Hanst Breach resides in Qeynos, and serves at the pleasure of Antonius Bayle II.",
    "Ilenar Crelwin: Go now. I need the Grimoire for my next stage of research. We are so close, I can taste it.",
"You have given away an Amulet of Deception.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120, quests[120][5].log)
TurnInItem(mySession, items.AMULET_OF_DECEPTION, 1)
else
    npcDialogue = "I had no doubt that you could pull this off playerName."
    diagOptions = { "Here is the amulet and letter you requested..." }
end
else
npcDialogue = "Ilenar Crelwin: Time is running out. You must complete this mission. I need a forged Letter from Crim, and the Amulet of deception from Swiftwind Galeehart near Hangman's Hill."
end
elseif (GetPlayerFlags(mySession, "120") == "8") then
if (CheckQuestItem(mySession, items.GELDWINS_GRIMOIRE, 1))
 then
if (choice:find("right")) then
multiDialogue = { "Ilenar Crelwin: This is it! I cannot believe my eyes, that this great and terrible thing has finally come to me. I owe you so much. You have truly proven yourself to me. Soon the rest of the world will know, I'm sure.",
    "Ilenar Crelwin: Go see Captain Norgam at once playerName. I have already made arrangements for your reward. Now leave me.",
"You have given away a Geldwins Grimoire.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120, quests[120][8].log)
TurnInItem(mySession, items.GELDWINS_GRIMOIRE, 1)
else
    npcDialogue = "Please tell me that you have the Grimoire. I've put my life's work into this magic. I must have it."
    diagOptions = { "I have it right here." }
end
else
npcDialogue = "Ilenar Crelwin: Time is running out. You must complete this mission. I need Geldwins Grimoire to complete my life's work. You must retrieve it from The Temple of Light."
end
  else
        npcDialogue =
"Ilenar Crelwin: If you weren't sent to me, then you will leave at once or I will call the guards to have you removed."
    end
------




SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end



