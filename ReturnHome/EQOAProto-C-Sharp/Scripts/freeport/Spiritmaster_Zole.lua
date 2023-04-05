local ch = tostring(choice)

local function event_say()
local npcDialogue = ""
local diagOptions = {}
    if (ch:find("bind")) then
        npcDialogue = "Your soul will now return here, playerName."
        print("binding")
        --BindPlayer()
    elseif (ch:find("Not")) then
        print("Not binding")
        npcDialogue = "Please come back if you change your mind."
    else
        npcDialogue = "Would you like me to bind your soul to this location?"
        diagOptions = {"Yes, please bind my soul.", "Not at this time."}
    end
    SendDialogue(mySession, npcDialogue, diagOptions)
end
