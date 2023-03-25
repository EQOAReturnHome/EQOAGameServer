local ch = tostring(choice)

function event_say()
    if (ch:find("bind")) then
        print("In the bind")
        npcDialogue = "Your soul will now return here, playerName."
        BindPlayer()
    elseif (ch:find("Not")) then
        npcDialogue = "Please come back if you change your mind."
    else
        npcDialogue = "Would you like me to bind your soul to this location?"
        diagOptions = {"Yes, please bind my soul.", "Not at this time."}
    end
    SendDialogue(mySession, npcDialogue, diagOptions)
end
