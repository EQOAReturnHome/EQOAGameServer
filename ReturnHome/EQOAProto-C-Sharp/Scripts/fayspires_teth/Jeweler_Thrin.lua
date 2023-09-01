function event_say()
diagOptions = {}
    npcDialogue = "I can teach you the ways of jewelrycrafting, if you have the gift for it. Care to learn?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end