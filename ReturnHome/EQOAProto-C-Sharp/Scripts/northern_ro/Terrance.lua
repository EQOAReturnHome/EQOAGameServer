local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
    npcDialogue = "One day I may have something you need to hear. Today is not that day..."
        SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
        SendMultiDialogue(mySession, multiDialogue)
end