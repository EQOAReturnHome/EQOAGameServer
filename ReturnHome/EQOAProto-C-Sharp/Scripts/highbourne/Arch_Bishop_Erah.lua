function event_say()
diagOptions = {}
    npcDialogue = "Prayer and books are not always the answer. There are moments where we must resort to our hammers and fire to progress."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end