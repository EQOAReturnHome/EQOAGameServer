local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Magician(10) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "100120") == "2") then
if (choice:find("Ilnenar")) then
multiDialogue = { "Crim Arikson: Geldwins Grimoire! Only madmen crave such power. Still, I suppose Ilenar wouldn't have sent just anyone. I'll do the forgery of course, but I'll need an incentive.",
    "Crim Arikson: I'll require a healthy sum of 525 tunar. Additionally, I'll need the seal and signature of Sir Hanst.",
    "Crim Arikson: He often sends missives to the Temple through Bastable. The pages who deliver such missives usually travel through Highpass and come along the road through Bastable.",
    "Crim Arikson: The road outside of town would make an excellent place for an ambush. Return to me when you have a page of Sir Hanst, as well as my fee.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100120, quests[100120][2].log)
else
    npcDialogue = "Well now, it has been a spell since my last visitor. What in the world might you want?"
    diagOptions = { "I've been sent by Ilnenar to arrange a forgery..." }
end
elseif (GetPlayerFlags(mySession, "100120") == "4") then
if (mySession.MyCharacter.Inventory.Tunar >= 525
and CheckQuestItem(mySession, items.MISSIVES_FROM_QEYNOS, 79))
 then
if (choice:find("this")) then
multiDialogue = { "Crim Arikson: Yes...Yes this looks right. It will do nicely. Give me just a moment here and I will have the forged letter finished.",
    "Crim Arikson: You are quite efficient, you know. Ok, here we are. Those paladins wont know the difference. For all they will know Sir Hanst will have sent you himself.",
    "Crim Arikson: Now then, another matter to attend to. Ilenar will also be needing you to fetch another relic, known as the Amulet of Deception.",
    "Crim Arikson: The Amulet once belonged to a fellow named Swiftwind Galeehart. He suffered a curse long ago that took his very life. Unfortunately, the curse has made him one of the living dead.",
    "Crim Arikson: It has been rumored that he roams the countryside near Hangman's Hill. I don't envy you, having to go anywhere near that wretched place.",
"You have given away a missives from Qeynos.",
"You have given away 525 Tunar.",
"You have received a Forged Letter.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100120, quests[100120][4].log)
TurnInItem(mySession, items.MISSIVES_FROM_QEYNOS, 1)
RemoveTunar(mySession, 525 )
GrantItem(mySession, items.FORGED_LETTER, 1)
else
    npcDialogue = "My my, have you the signature and seal already?"
    diagOptions = { "Is this what you need?" }
end
else
npcDialogue = "Crim Arikson: Time is running out. You must complete this mission. I need a Page of Sir Hanst. Look for Joseph Robert on the road through Bastable."
end
  else
        npcDialogue =
"Crim Arikson: I'm just a writer of old novels. Nothing of interest to most folk. Bastable has been a nice quiet place to retire."
    end
------
--Bard(5) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "50120") == "2") then
if (choice:find("Ilnenar")) then
multiDialogue = { "Crim Arikson: Geldwins Grimoire! Only madmen crave such power. Still, I suppose Ilenar wouldn't have sent just anyone. I'll do the forgery of course, but I'll need an incentive.",
    "Crim Arikson: I'll require a healthy sum of 525 tunar. Additionally, I'll need the seal and signature of Sir Hanst.",
    "Crim Arikson: He often sends missives to the Temple through Bastable. The pages who deliver such missives usually travel through Highpass and come along the road through Bastable.",
    "Crim Arikson: The road outside of town would make an excellent place for an ambush. You'll need to \"convince\" the page to give you the missive. Return to me when you have a missive from Qeynos, as well as my fee.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50120, quests[50120][2].log)
else
    npcDialogue = "Well now, it has been a spell since my last visitor. What in the world might you want?"
    diagOptions = { "I've been sent by Ilnenar to arrange a forgery..." }
end
elseif (GetPlayerFlags(mySession, "50120") == "4") then
if (mySession.MyCharacter.Inventory.Tunar >= 525
and CheckQuestItem(mySession, items.MISSIVES_FROM_QEYNOS, 79))
 then
if (choice:find("this")) then
multiDialogue = { "Crim Arikson: Yes...Yes this looks right. It will do nicely. Give me just a moment here and I will have the forged letter finished.",
    "Crim Arikson: You are quite efficient, you know. Ok, here we are. Those paladins won't know the difference. For all they will know Sir Hanst will have sent you himself.",
    "Crim Arikson: Now then, another matter to attend to. Ilenar will also be needing you to fetch another relic, known as the Amulet of Deception. He's been trying to track it down for some time. I've uncovered a lead.",
    "Crim Arikson: The Amulet once belonged to a fellow named Swiftwind Galeehart. He suffered a curse long ago that took his very life. Unfortunately, the curse has made him one of the living dead.",
    "Crim Arikson: It has been rumored that he roams the countryside near Hangman's Hill. I don't envy you, having to go anywhere near that wretched place, but I suppose, this part is up to you now.",
    "Crim Arikson: I will let Ilenar know that I have sent you to for the amulet. He'll want it right away.",
"You have given away the missives from Qeynos.",
"You have given away 525 Tunar.",
"You have received a Forged Letter.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50120, quests[50120][4].log)
TurnInItem(mySession, items.MISSIVES_FROM_QEYNOS, 1)
RemoveTunar(mySession, 525 )
GrantItem(mySession, items.FORGED_LETTER, 1)
else
    npcDialogue = "My my, have you the missive already?"
    diagOptions = { "Is this what you need?" }
end
else
npcDialogue = "Crim Arikson: Time is running out. You must complete this mission. I need a missive from Qeynos. Look for Joseph Robert on the road through Bastable."
end
  else
        npcDialogue =
"Crim Arikson: I'm just a writer of old novels. Nothing of interest to most folk. Bastable has been a nice quiet place to retire."
    end
------
--Cleric(9) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "90120") == "2") then
if (choice:find("Ilnenar")) then
multiDialogue = { "Crim Arikson: Geldwins Grimoire! Only madmen crave such power. Still, I suppose Ilenar wouldn't have sent just anyone. I'll do the forgery of course, but I'll need an incentive.",
    "Crim Arikson: I'll require a healthy sum of 525 tunar. Additionally, I'll need the seal and signature of Sir Hanst.",
    "Crim Arikson: He often sends missives to the Temple through Bastable. The pages who deliver such missives usually travel through Highpass and come along the road through Bastable.",
    "Crim Arikson: The road outside of town would make an excellent place for an ambush. Return to me when you have a page of Sir Hanst, likely in the form of a missive from Qeynos, as well as my fee.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90120, quests[90120][2].log)
else
    npcDialogue = "Well now, it has been a spell since my last visitor. What in the world might you want?"
    diagOptions = { "I've been sent by Ilnenar to arrange a forgery..." }
end
elseif (GetPlayerFlags(mySession, "90120") == "4") then
if (mySession.MyCharacter.Inventory.Tunar >= 525
and CheckQuestItem(mySession, items.MISSIVES_FROM_QEYNOS, 79))
 then
if (choice:find("this")) then
multiDialogue = { "Crim Arikson: Yes...Yes this looks right. It will do nicely. Give me just a moment here and I will have the forged letter finished.",
    "Crim Arikson: You are quite efficient, you know. Ok, here we are. Those paladins wont know the difference. For all they will know Sir Hanst will have sent you himself.",
    "Crim Arikson: Now then, another matter to attend to. Ilenar will also be needing you to fetch another relic, known as the Amulet of Deception. I have just received a lead on its whereabouts.",
    "Crim Arikson: The Amulet once belonged to a fellow named Swiftwind Galeehart. He suffered a curse long ago that took his very life. Unfortunately, the curse has made him one of the living dead.",
    "Crim Arikson: It has been rumored that he roams the countryside near Hangman's Hill. I don't envy you, having to go anywhere near that wretched place.",
    "Crim Arikson: Hangman's Hill is just east of Bobble-by-Water near the coast. Ilenar will want both the letter and the amulet.",
"You have given away missives from Qeynos.",
"You have given away 525 Tunar.",
"You have received a Forged Letter.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90120, quests[90120][4].log)
TurnInItem(mySession, items.MISSIVES_FROM_QEYNOS, 1)
RemoveTunar(mySession, 525 )
GrantItem(mySession, items.FORGED_LETTER, 1)
else
    npcDialogue = "My my, have you the signature and seal already?"
    diagOptions = { "Is this what you need?" }
end
else
npcDialogue = "Crim Arikson: Time is running out. You must complete this mission. I need a Page of Sir Hanst. Look for Joseph Robert on the road through Bastable."
end
  else
        npcDialogue =
"Crim Arikson: I'm just a writer of old novels. Nothing of interest to most folk. Bastable has been a nice quiet place to retire."
    end
------
--Rogue(6) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "60120") == "2") then
if (choice:find("Ilnenar")) then
multiDialogue = { "Crim Arikson: Geldwins Grimoire! Only madmen crave such power. Still, I suppose Ilenar wouldn't have sent just anyone. I'll do the forgery of course, but I'll need an incentive.",
    "Crim Arikson: I'll require a healthy sum of 525 tunar. Additionally, I'll need the seal and signature of Sir Hanst.",
    "Crim Arikson: He often sends missives to the Temple through Bastable. The pages who deliver such missives usually travel through Highpass and come along the road through Bastable.",
    "Crim Arikson: The road outside of town would make an excellent place for an ambush. Return to me when you have a page of Sir Hanst, likely in the form of a missive from Qeynos, as well as my fee.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60120, quests[60120][2].log)
else
    npcDialogue = "Well now, it has been a spell since my last visitor. What in the world might you want?"
    diagOptions = { "I've been sent by Ilnenar to arrange a forgery..." }
end
elseif (GetPlayerFlags(mySession, "60120") == "4") then
if (mySession.MyCharacter.Inventory.Tunar >= 525
and CheckQuestItem(mySession, items.MISSIVES_FROM_QEYNOS, 79))
 then
if (choice:find("this")) then
multiDialogue = { "Crim Arikson: Yes...Yes this looks right. It will do nicely. Give me just a moment here and I will have the forged letter finished.",
    "Crim Arikson: You are quite efficient, you know. Ok, here we are. Those paladins wont know the difference. For all they will know Sir Hanst will have sent you himself.",
    "Crim Arikson: Now then, another matter to attend to. Ilenar will also be needing you to fetch another relic, known as the Amulet of Deception. I have just received a lead on its whereabouts.",
    "Crim Arikson: The Amulet once belonged to a fellow named Swiftwind Galeehart. He suffered a curse long ago that took his very life. Unfortunately, the curse has made him one of the living dead.",
    "Crim Arikson: It has been rumored that he roams the countryside near Hangman's Hill. I don't envy you, having to go anywhere near that wretched place.",
    "Crim Arikson: Hangman's Hill is just east of Bobble-by-Water near the coast. Ilenar will want both the letter and the amulet.",
"You have given away missives from Qeynos.",
"You have given away 525 Tunar.",
"You have received a Forged Letter.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60120, quests[60120][4].log)
TurnInItem(mySession, items.MISSIVES_FROM_QEYNOS, 1)
RemoveTunar(mySession, 525 )
GrantItem(mySession, items.FORGED_LETTER, 1)
else
    npcDialogue = "My my, have you the signature and seal already?"
    diagOptions = { "Is this what you need?" }
end
else
npcDialogue = "Crim Arikson: Time is running out. You must complete this mission. I need a Page of Sir Hanst. Look for Joseph Robert on the road through Bastable."
end
  else
        npcDialogue =
"Crim Arikson: I'm just a writer of old novels. Nothing of interest to most folk. Bastable has been a nice quiet place to retire."
    end
------
--Warrior(0) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "120") == "2") then
if (choice:find("Ilnenar")) then
multiDialogue = { "Crim Arikson: Geldwins Grimoire! Only madmen crave such power. Still, I suppose Ilenar wouldn't have sent just anyone. I'll do the forgery of course, but I'll need an incentive.",
    "Crim Arikson: I'll require a healthy sum of 525 tunar. Additionally, I'll need the seal and signature of Sir Hanst.",
    "Crim Arikson: He often sends missives to the Temple through Bastable. The pages who deliver such missives usually travel through Highpass and come along the road through Bastable.",
    "Crim Arikson: The road outside of town would make an excellent place for an ambush. Return to me when you have a page of Sir Hanst, likely in the form of a missive from Qeynos, as well as my fee.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120, quests[120][2].log)
else
    npcDialogue = "Well now, it has been a spell since my last visitor. What in the world might you want?"
    diagOptions = { "I've been sent by Ilnenar to arrange a forgery..." }
end
elseif (GetPlayerFlags(mySession, "120") == "4") then
if (mySession.MyCharacter.Inventory.Tunar >= 525
and CheckQuestItem(mySession, items.MISSIVES_FROM_QEYNOS, 79))
 then
if (choice:find("this")) then
multiDialogue = { "Crim Arikson: Yes...Yes this looks right. It will do nicely. Give me just a moment here and I will have the forged letter finished.",
    "Crim Arikson: You are quite efficient, you know. Ok, here we are. Those paladins wont know the difference. For all they will know Sir Hanst will have sent you himself.",
    "Crim Arikson: Now then, another matter to attend to. Ilenar will also be needing you to fetch another relic, known as the Amulet of Deception. I have just received a lead on its whereabouts.",
    "Crim Arikson: The Amulet once belonged to a fellow named Swiftwind Galeehart. He suffered a curse long ago that took his very life. Unfortunately, the curse has made him one of the living dead.",
    "Crim Arikson: It has been rumored that he roams the countryside near Hangman's Hill. I don't envy you, having to go anywhere near that wretched place.",
    "Crim Arikson: Hangman's Hill is just east of Bobble-by-Water near the coast. Ilenar will want both the letter and the amulet.",
"You have given away missives from Qeynos.",
"You have given away 525 Tunar.",
"You have received a Forged Letter.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120, quests[120][4].log)
TurnInItem(mySession, items.MISSIVES_FROM_QEYNOS, 1)
RemoveTunar(mySession, 525 )
GrantItem(mySession, items.FORGED_LETTER, 1)
else
    npcDialogue = "My my, have you the signature and seal already?"
    diagOptions = { "Is this what you need?" }
end
else
npcDialogue = "Crim Arikson: Time is running out. You must complete this mission. I need a Page of Sir Hanst. Look for Joseph Robert on the road through Bastable."
end
  else
        npcDialogue =
"Crim Arikson: I'm just a writer of old novels. Nothing of interest to most folk. Bastable has been a nice quiet place to retire."
    end
------



SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end


