local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Bard(5) Human(0) Eastern(1)
if
            (class == "Bard" and race == "Human" and humanType == "Eastern" and
            GetPlayerFlags(mySession, "50101") == "noFlags")
then
        SetPlayerFlags(mySession, "50101", "0")
end
if (GetPlayerFlags(mySession, "50101") == "0") then
if (choice:find("Corufost")) then
multiDialogue = { "William Corufost: From me? Oh well, I don't know. I'm just an old man these days, only a few chords left in my song. I'm not sure I can help.",
    "William Corufost: But I guess if you're willing I might be able to teach you some things, my associates and I that is.",
    "William Corufost: Well, first of all we need you to get you outfitted. Why don't you go see Merchant Dolson, downstairs.",
    "William Corufost: Buy some raw silk boots from him, it wont cost you anything. Once you have your boots, come back and see me.",
    "You have received a quest!"
}
StartQuest(mySession, 50101, quests[50101][0].log)
else
    npcDialogue = "Something I can do for you traveler?"
    diagOptions = { "I've come to learn from you, Corufost." }
end
elseif (GetPlayerFlags(mySession, "50101") == "1") then
if (CheckQuestItem(mySession, items.RAW_SILK_BOOTS, 1))
 then
if (choice:find("actually")) then
npcDialogue = "William Corufost: You will need the raw silk boots from Merchant Dolson, then return to me."
elseif (choice:find("Yes")) then
multiDialogue = { "William Corufost: Wonderful. That is no ordinary ring. A small amount of power has been infused into the metal. We'll discuss more of that later.",
    "William Corufost: Take some rest now. Return when you are ready and you shall have your next task.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 50101, quests[50101][1].xp, 50102 )
else
    npcDialogue = "I take it you have the boots I sent you for?"
    diagOptions = { "Yes I do", "Well, actually, no." }
end
else
npcDialogue = "William Corufost: You will need the raw silk boots from Merchant Dolson, then return to me."
end
elseif (GetPlayerFlags(mySession, "50102") == "0") then
if (choice:find("another")) then
multiDialogue = { "William Corufost: Of course, but first stay and listen for a moment as I pass on to you our ways. As a bard you are trained as a entertainer, though bards tend to be more powerful than they appear.",
    "William Corufost: You will be one of the most versatile of adventurers, as you will take on many different roles.",
    "William Corufost: When necessary, you can join in hand-to-hand combat, buff party members to strengthen them, or assist with healing and power recovery.",
    "William Corufost: As a bard, you must have faith in those with you, and likewise they must trust you.",
"William Corufost: Now I'll need you to speak with Spiritmaster Imaryn. You can find her just outside of the West gate.",
"William Corufost: Go downstairs and exit the building to the West. The archway to the North will lead you to the Midroad.",
    "You have received a quest!"
}
StartQuest(mySession, 50102, quests[50102][0].log)
else
    npcDialogue = "Something I can do for you traveler?"
    diagOptions = { "Have you another task for me?" }
end
elseif (GetPlayerFlags(mySession, "50102") == "3") then
if (choice:find("haven\'t")) then
multiDialogue = { "William Corufost: You'll find that we guildmasters don't like to be kept waiting. I suggest you tend to the task at hand. Consult your quest log if you have lost track of your tasks."
 } 
elseif (choice:find("response")) then
multiDialogue = { "William Corufost: Thank you. Be sure to have yourself bound whenever you get the chance. It would be very inconvenient not to.",
    "William Corufost: That is all I have for you now. Go down stairs and see Solenia. She will have another task for you.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 50102, quests[50102][3].xp, 50103 )
else
    npcDialogue = "What did Imaryn have to say?"
    diagOptions = { "Many things. Here is her response.", "I haven't spoken to her yet." }
end
  else
        npcDialogue =
"William Corufost: Though trained as entertainers, bards are more powerful than they appear. They're one of the most versatile classes, they can take on many different roles."
    end
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
SendMultiDialogue(mySession, multiDialogue)
end

