local ch = tostring(choice)
function event_say()
    local diagOptions = {}
    local questText = ""
    local npcDialogue = ""
    SetPlayerFlags(mySession, "EasternMagician0", true)
    if (mySession.MyCharacter.Class == 10 and mySession.MyCharacter.Race == 0 and mySession.MyCharacter.HumTypeNum == 0 and
    GetPlayerFlags(mySession, "EasternMagician0") and GetPlayerFlags(mySession, "EasternMagician1") == false and mySession.MyCharacter.Level >= 2)
    npcDialogue = "Would you like me to bind your spirit to this location, child?"
    then
        npcDialogue = "todo"
        diagOptions = { "todo" }
        if() then
        end
    else
        npcDialogue = "Would you like me to bind your spirit to this location, child?"
    end
    SendDialogue(mySession, npcDialogue, diagOptions)
end