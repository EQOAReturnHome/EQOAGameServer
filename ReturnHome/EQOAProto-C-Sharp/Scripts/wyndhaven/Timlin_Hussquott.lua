function  event_say(choice)
diagOptions = {}
    npcDialogue = "Stay clear of those Bogman barbarians on the coast to the north. They are not the friendly type you might be used to in Halas."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end