function event_say()
    local ch = tostring(choice)
    if(ch:find("Kerplunk"))
    then
    npcDialogue = "Going to Kerplunk Outpost it is"
    TeleportPlayer(mySession, 0, 14799.97266, 54.12599564, 29241.68945, 1.305586219)
    elseif(ch:find("Hazinak"))
    then
    npcDialogue = "Going to the Hazinak Docks then"
    TeleportPlayer(mySession, 0, 25136.71875, 54.12599564, 27155.86719, -1.345191002)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Kerplunk Outpost%Get me a horse to the Hazinak Docks:::"
    end
end
