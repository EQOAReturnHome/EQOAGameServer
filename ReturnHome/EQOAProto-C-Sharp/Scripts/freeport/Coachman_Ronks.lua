function event_say()
    local ch = tostring(choice)
    SetPlayerFlags(mySession, "bobble_coach", true)
    if (GetPlayerFlags(mySession, "freeport_coach")) then
        if (ch:find("Highpass")) then
            npcDialogue = "Going to Highpass it is"
            TeleportPlayer(mySession, 0, 16776.640625, 187.8125, 15351.3916015625, 0.8122978806495667)
        elseif (ch:find("Tea")) then
            npcDialogue =
                "Coachman Ronks: I'm gonna give you this here horse, but it has no name. Now you be careful traveling through that desert."
            TeleportPlayer(mySession, 0, 23359.685546875, 54.12599563598633, 19635.919921875, 1.325449824333191)
        elseif (ch:find("Bobble")) then
            npcDialogue = "Off you go to Bobble By Water"
            TeleportPlayer(mySession, 0, 24654.671875, 54.12599563598633, 11853.1748046875, -0.8971897959709167)
        else
            npcDialogue = "Where would you like to go?:::"
            if (GetPlayerFlags(mySession, "highpass_coach")) then
                npcDialogue = npcDialogue .. "Get me a horse to Highpass%"
                if (GetPlayerFlags(mySession, "bobble_coach")) then
                    npcDialogue = npcDialogue .. "Get me a horse to Bobble By Water%"
                    if (GetPlayerFlags(mySession, "tea_garden_coach")) then
                        npcDialogue = npcDialogue .. "Get me a horse to Muniel's Tea Garden"
                    end
                end
            end
        end
    elseif (ch:find("Yes")) then
        SetPlayerFlags(mySession, "freeport_coach", true)
    elseif (ch:find("No")) then
        npcDialogue = "If you aren't interested then why are you wasting my time."
    else
        npcDialogue = "Would you like to sign the coachman's ledger?:::Yes%No:::"
    end
end
