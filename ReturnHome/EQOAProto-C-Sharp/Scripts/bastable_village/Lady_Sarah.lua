function event_say()
diagOptions = {}
    npcDialogue = "Welcome to this holy place. While you are here, no harm will come to you. Please rest and be at ease."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end