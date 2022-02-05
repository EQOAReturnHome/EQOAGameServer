function event_say()
    local ch = tostring(choice)
    if(ch:find("Darvar"))
    then
    npcDialogue = "Going to Darvar Manor it is"
    TeleportPlayer(mySession, 0, 13587.5, 54.12599564, 14761.5127, 1.487539649)
    elseif(ch:find("Seriak"))
    then
    npcDialogue = "Going to Ft. Seriak then"
    TeleportPlayer(mySession, 0, 21778.76563, 54.12599564, 8378.056641, 0.129010573)
    elseif(ch:find("Freeport"))
    then
    npcDialogue = "Going to Freeport then"
    TeleportPlayer(mySession, 0, 25273.03125, 54.125, 15723.29102, 3.138683081)
    elseif(ch:find("Solace"))
    then
    npcDialogue = "Going to Dark Solace then"
    TeleportPlayer(mySession, 0, 12535.52637, 83.88361359, 21185.85547, 3.127047777)
    elseif(ch:find("Rivervale"))
    then
    npcDialogue = "Going to Rivervale then"
    TeleportPlayer(mySession, 0, 18340.88477, 54.12599564, 11336.58008, -0.136409953)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Darvar Manor%Get me a horse to Ft. Seriak%Get me a horse to Freeport%Get me a horse to Dark Solace%Get me a horse to Rivervale:::"
    end
end
