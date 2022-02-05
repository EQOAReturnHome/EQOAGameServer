function event_say()
    local ch = tostring(choice)
    if(ch:find("Forkwatch"))
    then
    npcDialogue = "Going to Forkwatch it is"
    TeleportPlayer(mySession, 0, 12533.68457, 120.0009995, 18758.88672, -0.017951392)
    elseif(ch:find("Highbourne"))
    then
    npcDialogue = "Going to Highbourne then"
    TeleportPlayer(mySession, 0, 4850.741699, 54.12599564, 21450.79102, -0.021275084)
    elseif(ch:find("Wyndhaven"))
    then
    npcDialogue = "Going to Wyndhaven then"
    TeleportPlayer(mySession, 0, 4711.34668, 59.40724564, 13216.51758, 2.945064545)
    elseif(ch:find("Surefall"))
    then
    npcDialogue = "Going to Surefall Glade then"
    TeleportPlayer(mySession, 0, 9139.639648, 57.65724564, 12750.20313, -2.825037956)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Forkwatch%Get me a horse to Highbourne%Get me a horse to Wyndhaven%Get me a horse to Surefall Glade:::"
    end
end
