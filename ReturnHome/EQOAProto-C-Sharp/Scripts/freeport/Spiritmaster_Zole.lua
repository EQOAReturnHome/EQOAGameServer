-- 
local npcDialogue = ""
local diagOptions = {}

function event_say(choice)
    if (choice:find("bind")) then
        npcDialogue = "Your soul will now return here, playerName."
        BindPlayer(thisEntity.ObjectID)
    elseif (choice:find("Not")) then
        npcDialogue = "Please come back if you change your mind."
    else
        npcDialogue = "Would you like me to bind your soul to this location?"
        diagOptions = {"Yes, please bind my soul.", "Not at this time."}
    end
    SendDialogue(mySession, npcDialogue, diagOptions)
end
