function event_say()
    npcDialogue = "Out of my way, fool.  I have things to do, and I can't have a simple minded fool like you distracting me."
    diagOptions = "Devin loves men?%Devin loves men!"
end

function event_say_continue()
    if(choice and choice == 1)then
    dialogue = "Devin loves men?."
    elseif(choice == 2 and counter ==1)then
    dialogue = "Devin loves men!"
    end
end
