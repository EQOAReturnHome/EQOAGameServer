function event_say()
diagOptions = {}
    npcDialogue = "Have a seat. Open your mind to the world around you. Only then may Prexus deem you worthy of an audience."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end