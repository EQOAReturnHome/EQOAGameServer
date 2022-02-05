function event_say()
    local ch = tostring(choice)
    if(ch:find("Forkwatch"))
    then
    npcDialogue = "Going to Forkwatch it is"
    TeleportPlayer(mySession, 0, 12533.68457, 120.0009995, 18758.88672, -0.017951392)
    elseif(ch:find("Highpass"))
    then
    npcDialogue = "Going to Highpass then"
    TeleportPlayer(mySession, 0, 16776.640625, 187.8125, 15351.3916015625, 0.8122978806495667)
    elseif(ch:find("Moradhim"))
    then
    npcDialogue = "Going to Moradhim then"
    TeleportPlayer(mySession, 0, 15354.98145, 54.01245499, 9281.591797, 3.000839472)
    elseif(ch:find("Surefall"))
    then
    npcDialogue = "Going to Surefall Glade then"
    TeleportPlayer(mySession, 0, 9139.639648, 57.65724564, 12750.20313, -2.825037956)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Forkwatch%Get me a horse to Highpass%Get me a horse to Moradhim%Get me a horse to Surefall Glade:::"
    end
end
