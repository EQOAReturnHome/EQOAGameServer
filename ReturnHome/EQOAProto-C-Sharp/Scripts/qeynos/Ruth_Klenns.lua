function  event_say(choice)
diagOptions = {}
    npcDialogue = "Surely you haven't come here for my services playerName. This is the Beggars District. Perhaps you are looking for your way to the Marketplace?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end