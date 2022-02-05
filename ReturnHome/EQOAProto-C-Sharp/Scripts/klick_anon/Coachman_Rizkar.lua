function event_say()
    local ch = tostring(choice)
    if(ch:find("Neriak"))
    then
    npcDialogue = "Going to Neriak it is"
    TeleportPlayer(mySession, 0, 24924.66797, 29.43331718, 9420.388672, 0.08655221)
    elseif(ch:find("Fayspires"))
    then
    npcDialogue = "On the way to Fayspires."
    TeleportPlayer(mySession, 0, 19467.31641, 54.12599564, 7050.958008, 2.262443781)
    elseif(ch:find("Bobble"))
    then
    npcDialogue = "Off you go to Bobble By Water"
    TeleportPlayer(mySession, 0, 24654.671875, 54.12599563598633, 11853.1748046875, -0.8971897959709167)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Fayspires%Get me a horse to Neriak%Get me a horse to Bobble By Water:::"
    end
end
