function event_say()
diagOptions = {}
    npcDialogue = "Can you believe it!? I've been given the honor of translating runes made by Skahyir herself. She mentions gold a lot but I think that is a trait that all dragons share."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end