function  event_say(choice)
diagOptions = {}
    npcDialogue = "These mushroom pickers are acting up again. I don't know what I am gonna do if they break down. The city needs this food."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end