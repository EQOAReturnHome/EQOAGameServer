local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Cleric(9) Human(0) Eastern(1)
if
            (class == "Cleric" and race == "Human" and humanType == "Eastern" and
            GetPlayerFlags(mySession, "90101") == "noFlags")
then
        SetPlayerFlags(mySession, "90101", "0")
end
if (GetPlayerFlags(mySession, "90101") == "0") then
if (choice:find("Shining")) then
multiDialogue = { "Denouncer Alshea: I see. Of course you must complete a number of tasks before you can be welcomed as an acolyte.",
    "Denouncer Alshea: Your first task is to acquire a petitioner's cap from Merchant Olkan. Your fee for this will be waived.",
    "Denouncer Alshea: When you have the petitioner's cap, return to me and I'll send you on your second task...If you can manage.",
    "You have received a quest!"
}
StartQuest(mySession, 90101, quests[90101][0].log)
else
    npcDialogue = "Is there something you want from this hall of darkness?"
    diagOptions = { "I wish to be a cleric of The Shining Shield." }
end
elseif (GetPlayerFlags(mySession, "90101") == "1") then
if (CheckQuestItem(mySession, items.PETITIONERS_CAP, 1))
 then
if (choice:find("actually")) then
npcDialogue = "Denouncer Alshea: I'll need you to purchase the petitioner's cap from Merchant Olkan, then return to me."
elseif (choice:find("Yes")) then
multiDialogue = { "Denouncer Alshea: So...you aren't a dimwit after all. That is no ordinary cap. A small amount of magic has been infused into the material.",
    "Denouncer Alshea: If you still wish to become an acolyte, return to me shortly. I have other things to attend to.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 90101, quests[90101][1].xp, 90102 )
else
    npcDialogue = "I take it you have the cap I sent you for?"
    diagOptions = { "Yes I do", "Well, actually, no." }
end
else
npcDialogue = "Denouncer Alshea: I'll need you to purchase the petitioner's cap from Merchant Olkan, then return to me."
end
elseif (GetPlayerFlags(mySession, "90102") == "0") then
if (choice:find("returned")) then
multiDialogue = { "Denouncer Alshea: Many from the Shining Shield head out into the wild, and most eventually become wounded. Some will even perish in battle.",
    "Denouncer Alshea: Someone needs to cure their ailments, and heal their wounds before it is too late. This will fall to you, playerName.",
    "Denouncer Alshea: This is our pact. From this darkness, we will survive. Once you have been anointed with this magic, there is no return.",
"Denouncer Alshea: Now listen carefully. I need you to speak to Spiritmaster Keika.",
"Denouncer Alshea: You can find her near the south east corner of the city. Go out the doorway of The Shining Shield, through the east city exit, head south along the wall, and take a right around the corner.",
"Denouncer Alshea: Return only when you complete any tasks she gives you.",
    "You have received a quest!"
}
StartQuest(mySession, 90102, quests[90102][0].log)
else
    npcDialogue = "You must have a reason for disturbing my meditation."
    diagOptions = { "I've returned for more wisdom." }
end
elseif (GetPlayerFlags(mySession, "90102") == "3") then
if (choice:find("apologies")) then
multiDialogue = { "Denouncer Alshea: You'll find that we guildmasters don't like to be kept waiting. I suggest you tend to the task at hand. Consult your quest log if you have lost track of your tasks."
 } 
elseif (choice:find("done")) then
multiDialogue = { "Denouncer Alshea: That is good. Be sure to have yourself bound often. It is quite inconvenient to be defeated far from your last binding.",
    "Denouncer Alshea: Now that you've completed that, I have another task for you. Go see Sister Falhelm, she will assist you.",
    "Denouncer Alshea: You can find Sister Falhelm up the ramp behind you, left through the door way, and again left of the entrance.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 90102, quests[90102][3].xp, 90103 )
else
    npcDialogue = "Have you fulfilled my request?"
    diagOptions = { "Yes, it is done.", "No, my apologies Denouncer." }
end
  else
        npcDialogue =
"Denouncer Alshea: The darkness will eventually overtake us, playerName. However, in the absence of light, the truth may be revealed, and our true power will then emerge."
    end
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

