function event_say()
    local ch = tostring(choice)
    if(ch:find("Bobble"))
    then
    npcDialogue = "Going to Bobble By Water it is"
    TeleportPlayer(mySession, 0, 24654.671875, 54.12599563598633, 11853.1748046875, -0.8971897959709167)
    elseif(ch:find("Fayspires"))
    then
    npcDialogue = "Going to Fayspires then"
    TeleportPlayer(mySession, 0, 19467.31641, 54.12599564, 7050.958008,2.262443781)
    elseif(ch:find("Moradhim"))
    then
    npcDialogue = "Going to Moradhim then"
    TeleportPlayer(mySession, 0, 15354.98145, 54.01245499, 9281.591797, 3.000839472)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Bobble By Water%Get me a horse to Fayspires%Get me a horse to Moradhim:::"
    end
end
