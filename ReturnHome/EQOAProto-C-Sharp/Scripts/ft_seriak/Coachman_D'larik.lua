function event_say()
    local ch = tostring(choice)
    if(ch:find("Highpass"))
    then
    npcDialogue = "Going to Highpass it is"
    TeleportPlayer(mySession, 0, 16776.640625, 187.8125, 15351.3916015625, 0.8122978806495667)
    elseif(ch:find("Last Inn"))
    then
    npcDialogue = "Going to the Last Inn then"
    TeleportPlayer(mySession, 0, 13703.66113, 54.12599564, 11352.54492, 0.373888314)
    elseif(ch:find("Neriak"))
    then
    npcDialogue = "Going to Neriak then"
    TeleportPlayer(mySession, 0, 24924.66797, 29.43331718, 9420.388672, 0.08655221)
    else
    npcDialogue = "Where would you like to go?:::Get me a horse to Highpass%Get me a horse to the Last Inn%Get me a horse to Neriak:::"
    end
end
