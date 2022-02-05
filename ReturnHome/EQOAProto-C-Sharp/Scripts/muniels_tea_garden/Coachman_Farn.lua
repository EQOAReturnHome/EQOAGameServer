function event_say()
    local ch = tostring(choice)
    if(ch:find("Freeport"))
    then
    npcDialogue = "Going to Freeport it is"
    TeleportPlayer(mySession, 0, 25273.03125, 54.125, 15723.29102, 3.138683081)
    elseif(ch:find("Oasis"))
    then
    npcDialogue = "On to the Oasis of Marr"
    TeleportPlayer(mySession, 0, 21109.69531, 54.12599564, 25173.51172, 1.570796371)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Freeport%Get me a horse to the Oasis of Marr:::"
    end
end
