function  event_say(choice)
diagOptions = {}
    npcDialogue = "Eyes on your surroundings, traveler.  You could be ambushed at any moment."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end