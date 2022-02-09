function event_say()
    local ch = tostring(choice)
    if(ch:find("Darvar"))
    then
    npcDialogue = "Going to Darvar Manor it is"
    TeleportPlayer(mySession, 0, 13587.5, 54.12599564, 14761.5127, 1.487539649)
    elseif(ch:find("Qeynos"))
    then
    npcDialogue = "Going to Qeynos then"
    TeleportPlayer(mySession, 0, 4810.861328, 57.65724564, 17130.74023, 0.561412513)
    elseif(ch:find("Wyndhaven"))
    then
    npcDialogue = "Going to Wyndhaven then"
    TeleportPlayer(mySession, 0, 4711.34668, 59.40724564, 13216.51758, 2.945064545)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Darvar Manor%Get me a horse to Qeynos%Get me a horse to Wyndhaven:::"
    end
end
