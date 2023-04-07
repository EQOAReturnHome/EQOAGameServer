local diagOptions = {}
local npcDialogue = ""
function  event_say(choice)
    npcDialogue = "Come here to pick up a sword, 'ave ya playerName?  Or are ya just passin' through?"
SendDialogue(mySession, npcDialogue, diagOptions)
end
