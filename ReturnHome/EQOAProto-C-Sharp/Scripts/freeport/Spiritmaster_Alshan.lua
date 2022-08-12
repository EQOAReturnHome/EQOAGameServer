local ch = tostring(choice)
function event_say()
    local diagOptions = {}
    local questText = ""
    local npcDialogue = ""
    SetPlayerFlags(mySession, "EasternMagician0", true)
    if (mySession.MyCharacter.Class == 10 and mySession.MyCharacter.Race == 0 and mySession.MyCharacter.HumTypeNum == 0 and
    GetPlayerFlags(mySession, "IronRing1"))
    then
        npcDialogue = "Hello."
        diagOptions = { "Sorry to bother, but Malsis sent me.", "Good day!" }
        if(ch:find("sorry")) then
            diagOptions = {}
            npcDialogue = "You were sent to me for binding, or rather, the binding of your spirit."
            SendDialogue(mySession, npcDialogue, diagOptions)
        end
    else
        npcDialogue = "Would you like me to bind your spirit to this location, child?"
    end
    SendDialogue(mySession, npcDialogue, diagOptions)
end