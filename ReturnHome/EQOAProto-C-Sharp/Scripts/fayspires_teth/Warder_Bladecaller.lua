function event_say()
diagOptions = {}
    npcDialogue = "Caution, in your travels to the southeast. Cemeteries, undead, and if you travel for enough, dark elves. These things are quite unnatural, I promise you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end