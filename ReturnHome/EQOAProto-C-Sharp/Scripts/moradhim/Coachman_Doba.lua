function event_say()
    local ch = tostring(choice)
    if(ch:find("Darvar"))
    then
    npcDialogue = "Going to Darvar Manor it is"
    TeleportPlayer(mySession, 0, 13587.5, 54.12599564, 14761.5127, 1.487539649)
    elseif(ch:find("Fayspires"))
    then
    npcDialogue = "Going to Fayspires then"
    TeleportPlayer(mySession, 0, 19467.31641, 54.12599564, 7050.958008, 2.262443781)
    elseif(ch:find("Halas"))
    then
    npcDialogue = "Going to Halas then"
    TeleportPlayer(mySession, 0, 12978.21094, 53.22509384, 4678.135254, -0.903002858)
    elseif(ch:find("Rivervale"))
    then
    npcDialogue = "Going to Rivervale then"
    TeleportPlayer(mySession, 0, 18340.88477, 54.12599564, 11336.58008, -0.136409953)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Darvar Manor%Get me a horse to Fayspires%Get me a horse to Halas%Get me a horse to Rivervale:::"
    end
end

