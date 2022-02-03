function event_say()
    if(choice == 0)
    then
    npcDialogue = "Going to Highpass it is"
    elseif(choice == 1)
    then
    npcDialogue = "Going to Muniel's Tea Garden then"
    elseif(choice == 2)
    then
    npcDialogue = "Off you go to Bobble By Water"
    else
        npcDialogue = "Where would you like to go?:::Highpass%Muniel's Tea Garden%Bobble By Water:::"
    end
end
