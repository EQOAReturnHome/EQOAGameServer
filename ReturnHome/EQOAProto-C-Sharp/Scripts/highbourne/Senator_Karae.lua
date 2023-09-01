function event_say()
diagOptions = {}
    npcDialogue = "I grow ever more weary of this political theater. All I think about, and look forward to, is the voyage to Arcadin where I can retire and let some other youthful fool try their hand at this bureaucratic non-sense."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end