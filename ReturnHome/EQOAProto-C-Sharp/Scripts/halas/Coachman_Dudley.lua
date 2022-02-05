function event_say()
    local ch = tostring(choice)
    if(ch:find("Lightwolf"))
    then
    npcDialogue = "Going to Castle Lightwolf it is"
    TeleportPlayer(mySession, 0, 9277.765625, 54.12599564, 6508.779785, -3.099494934)
    elseif(ch:find("Moradhim"))
    then
    npcDialogue = "Going to Moradhim then"
    TeleportPlayer(mySession, 0, 15354.98145, 54.01245499, 9281.591797, 3.000839472)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Castle Lightwolf%Get me a horse to Moradhim:::"
    end
end
