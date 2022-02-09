function event_say()
    local ch = tostring(choice)
    if(ch:find("Kerplunk"))
    then
    npcDialogue = "Going to Kerplunk Outpost it is"
    TeleportPlayer(mySession, 0, 14799.97266, 54.12599564, 29241.68945, 1.305586219)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Kerplunk Outpost:::"
    end
end
