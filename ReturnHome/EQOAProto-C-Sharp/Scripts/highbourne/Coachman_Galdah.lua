function event_say()
    local ch = tostring(choice)
    if(ch:find("Forkwatch"))
    then
    npcDialogue = "Going to Forkwatch it is"
    TeleportPlayer(mySession, 0, 12533.68457, 120.0009995, 18758.88672, -0.017951392)
    elseif(ch:find("Qeynos"))
    then
    npcDialogue = "Going to Qeynos"
    TeleportPlayer(mySession, 0, 4810.861328, 57.65724564, 17130.74023, 0.561412513)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Forkwatch%Get me a horse to Qeynos:::"
    end
end
