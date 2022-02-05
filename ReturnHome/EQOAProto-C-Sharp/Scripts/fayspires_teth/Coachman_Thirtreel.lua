function event_say()
    local ch = tostring(choice)
    if(ch:find("Klick"))
    then
    npcDialogue = "Going to Klick'Anon it is"
    TeleportPlayer(mySession, 0, 23450.37695, 54.12599564, 6476.621582, 0.746291041)
    elseif(ch:find("Moradhim"))
    then
    npcDialogue = "Going to Moradhim then"
    TeleportPlayer(mySession, 0, 15354.98145, 54.01245499, 9281.591797, 3.000839472)
    elseif(ch:find("Tethelin"))
    then
    npcDialogue = "Going to Tethelin then"
    TeleportPlayer(mySession, 0, 18414.07422, 54.12599564, 7501.665039, 2.374024868)
    elseif(ch:find("Rivervale"))
    then
    npcDialogue = "Going to Rivervale then"
    TeleportPlayer(mySession, 0, 18340.88477, 54.12599564, 11336.58008, -0.136409953)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Klick'Anon%Get me a horse to Moradhim%Get me a horse to Tethelin%Get me a horse to Rivervale:::"
    end
end
