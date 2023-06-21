local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Enchanter(12) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "120113") == "2") then
if (CheckQuestItem(mySession, items.NOTE_FROM_WILKINSON, 1))
 then
if (choice:find("just")) then
npcDialogue = "Duminven: Take a look around... I don't think there is anything for you here...is there?"
elseif (choice:find("have")) then
multiDialogue = { "Duminven: Agent Wilkenson doesn't mince words. I like that. Straight to the point. Right then.",
    "Duminven: Your mission is to head southwest into Bastable village and locate the thief Eliene and follow her. She will be meeting a contact.",
    "Duminven: They often meet under a bridge. Once she is done talking to her contact, kill them both. Return to me with any items they were carrying.",
"You have given away a note from Wilkinson.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120113, quests[120113][2].log)
TurnInItem(mySession, items.NOTE_FROM_WILKINSON, 1)
else
    npcDialogue = "What business would the likes of you possibly have here?"
    diagOptions = { "I have this note...", "I am just here for the view." }
end
else
npcDialogue = "Duminven: Take a look around... I don't think there is anything for you here...is there?"
end
elseif (GetPlayerFlags(mySession, "120113") == "3") then
if (CheckQuestItem(mySession, items.MARK_OF_LOUHMANTA, 1)
and CheckQuestItem(mySession, items.CRACKED_RED_CRYSTAL, 1))
 then
if (choice:find("I\'ll")) then
npcDialogue = "Duminven: Take a look around... I'll need Eliene's belongings before I send you back to Azlynn."
elseif (choice:find("these")) then
multiDialogue = { "Duminven: I see that Azlynn is selecting better enchanter's these days. You have done well here. The last enchanter didn't fare so well after I threw him off the cliff.",
    "Duminven: It seems as though you've prevented a timely misfortune for the Academy of Arcane Science. Those thieves were after something much bigger from the sounds of it.",
    "Duminven: I will be retaining the cracked red crystal for my payment. Azlynn will want the Mark of Louhmanta. We are finished here.",
"You have given away a cracked red crystal.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 120113, quests[120113][3].log)
TurnInItem(mySession, items.CRACKED_RED_CRYSTAL, 1)
else
    npcDialogue = "Failing to retrieve Eliene's belongings would mean severe punishment. What do you report?"
    diagOptions = { "The task is done. I have these...", "I'll be right back." }
end
else
npcDialogue = "Duminven: Take a look around... I'll need Eliene's belongings before I send you back to Azlynn."
end
  else
        npcDialogue =
"Duminven: I can appreciate the spectacle of these towers, but unfortunately we do not give tours as we have official business to attend to. Now if you don't mind, please move along."
    end
------
--Necromancer(11) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "110113") == "2") then
if (CheckQuestItem(mySession, items.NOTE_FROM_WILKINSON, 1))
 then
if (choice:find("just")) then
npcDialogue = "Duminven: Take a look around... I don't think there is anything for you here...is there?"
elseif (choice:find("have")) then
multiDialogue = { "Duminven: Agent Wilkenson doesn't mince words. I like that. Straight to the point. Right then.",
    "Duminven: Your mission is to head southwest into Bastable village and locate the thief Eliene and follow her. She will be meeting a contact.",
    "Duminven: They often meet under a bridge. Once she is done talking to her contact, kill them both. Return to me with any items they were carrying.",
"You have given away a note from Wilkinson.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110113, quests[110113][2].log)
TurnInItem(mySession, items.NOTE_FROM_WILKINSON, 1)
else
    npcDialogue = "What business would the likes of you possibly have here?"
    diagOptions = { "I have this note...", "I am just here for the view." }
end
else
npcDialogue = "Duminven: Take a look around... I don't think there is anything for you here...is there?"
end
elseif (GetPlayerFlags(mySession, "110113") == "3") then
if (CheckQuestItem(mySession, items.MARK_OF_LOUHMANTA, 1)
and CheckQuestItem(mySession, items.CRACKED_RED_CRYSTAL, 1))
 then
if (choice:find("I\'ll")) then
npcDialogue = "Duminven: Take a look around... I'll need Eliene's belongings before I send you back to Corious Slaerin."
elseif (choice:find("these")) then
multiDialogue = { "Duminven: I see that Corious is selecting better necromancer's these days. You have done well here. The last necromancer didn't fare so well after I threw him off the cliff.",
    "Duminven: It seems as though you've prevented a timely misfortune for the House Slaerin. Those thieves were after something much bigger from the sounds of it.",
    "Duminven: I will be retaining the cracked red crystal for my payment. Corious will want the Mark of Louhmanta. We are finished here.",
"You have given away a cracked red crystal.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110113, quests[110113][3].log)
TurnInItem(mySession, items.CRACKED_RED_CRYSTAL, 1)
else
    npcDialogue = "Failing to retrieve Eliene's belongings would mean severe punishment. What do you report?"
    diagOptions = { "The task is done. I have these...", "I'll be right back." }
end
else
npcDialogue = "Duminven: Take a look around... I'll need Eliene's belongings before I send you back to Corious Slaerin."
end
  else
        npcDialogue =
"Duminven: I can appreciate the spectacle of these towers, but unfortunately we do not give tours as we have official business to attend to. Now if you don't mind, please move along."
    end
------


SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

