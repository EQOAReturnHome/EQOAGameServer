function event_say()
diagOptions = {}
    npcDialogue = "My accuracy is such that, I could find a fish in Winter's Deep with an arrow from here if I so wished it."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end