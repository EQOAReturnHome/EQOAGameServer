function event_say()
    local ch = tostring(choice)
    if(ch:find("Freeport"))
    then
    npcDialogue = "Going to Freeport it is"
    TeleportPlayer(mySession, 0, 25273.03125, 54.125, 15723.29102, 3.138683081)
    elseif(ch:find("Klick"))
    then
    npcDialogue = "Going to Klick'Anon then"
    TeleportPlayer(mySession, 0, 23450.37695, 54.12599564, 6476.621582, 0.746291041)
    elseif(ch:find("Rivervale"))
    then
    npcDialogue = "Going to Rivervale then"
    TeleportPlayer(mySession, 0, 18340.88477, 54.12599564, 11336.58008, -0.136409953)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Freeport%Get me a horse to Klick'Anon%Get me a horse to Rivervale:::"
    end
end
