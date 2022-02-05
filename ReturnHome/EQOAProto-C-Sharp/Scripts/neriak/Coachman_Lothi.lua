function event_say()
    local ch = tostring(choice)
    if(ch:find("Klick"))
    then
    npcDialogue = "Going to Klick'Anon it is"
    TeleportPlayer(mySession, 0, 23450.37695, 54.12599564, 6476.621582, 0.746291041)
    elseif(ch:find("Seriak"))
    then
    npcDialogue = "Going to Ft. Seriak then"
    TeleportPlayer(mySession, 0, 21778.76563, 54.12599564, 8378.056641, 0.129010573)
    elseif(ch:find("Freeport"))
    then
    npcDialogue = "Going to Freeport then"
    TeleportPlayer(mySession, 0, 25273.03125, 54.125, 15723.29102, 3.138683081)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Klick'Anon%Get me a horse to Ft. Seriak%Get me a horse to Freeport:::"
    end
end
