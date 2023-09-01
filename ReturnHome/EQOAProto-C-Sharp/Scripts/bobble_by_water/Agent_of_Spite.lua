function  event_say(choice)
diagOptions = {}
    npcDialogue = "My advice to you is examine the facts, figure out where your passion lies...or what it lies against, and then act."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end