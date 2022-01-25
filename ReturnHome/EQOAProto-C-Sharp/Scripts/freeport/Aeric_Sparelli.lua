function event_say()
    npcDialogue = "Out of my way, fool.  I have things to do, and I can't have a simple minded fool like you distracting me."
    diagOptions = "Test%Test2"
end

function event_say_continue()
    if(choice and choice == 1)then
    dialogue = "Test1 was selected."
    elseif(choice == 2 and counter ==1)then
    dialogue = "Test2 was Selected."
    end
end
