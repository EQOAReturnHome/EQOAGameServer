function event_say()
diagOptions = {}
    npcDialogue = "Let's say you take a rest and learn the ways of fishing with me. Will you do this?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end