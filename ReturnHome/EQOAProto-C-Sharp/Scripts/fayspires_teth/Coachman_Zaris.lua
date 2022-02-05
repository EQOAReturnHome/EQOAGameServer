function event_say()
    local ch = tostring(choice)
    if(ch:find("Fayspires"))
    then
    npcDialogue = "Going to Fayspires it is"
    TeleportPlayer(mySession, 0, 19467.31641, 54.12599564, 7050.958008, 2.262443781)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Fayspires:::"
    end
end
