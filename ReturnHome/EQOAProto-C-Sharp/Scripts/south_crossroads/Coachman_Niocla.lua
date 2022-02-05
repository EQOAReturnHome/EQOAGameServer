function event_say()
    local ch = tostring(choice)
    if(ch:find("Highpass"))
    then
    npcDialogue = "Going to Highpass it is"
    TeleportPlayer(mySession, 0, 16776.640625, 187.8125, 15351.3916015625, 0.8122978806495667)
    elseif(ch:find("Honjur"))
    then
    npcDialogue = "Going to Honjur then"
    TeleportPlayer(mySession, 0, 9806.492188, 57.65724564, 14993.47949, -2.801538467)
    elseif(ch:find("Kerplunk"))
    then
    npcDialogue = "Going to Kerplunk Outpost then"
    TeleportPlayer(mySession, 0, 14799.97266, 54.12599564, 29241.68945, 1.305586219)
    elseif(ch:find("Oasis"))
    then
    npcDialogue = "Going to Oasis then"
    TeleportPlayer(mySession, 0, 21109.69531, 54.12599564, 25173.51172, 1.570796371)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Darvar Manor%Get me a horse to Ft. Seriak%Get me a horse to Freeport%Get me a horse to Dark Solace%Get me a horse to Rivervale:::"
    end
end
