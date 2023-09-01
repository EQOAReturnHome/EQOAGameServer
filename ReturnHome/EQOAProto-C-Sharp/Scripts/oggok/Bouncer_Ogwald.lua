function  event_say(choice)
diagOptions = {}
    npcDialogue = "In the northern mountains is the Cawtou Aviak village. These aviaks are martial artists and very dangerous. Their speed and flight will keep any ogre on his toes."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end