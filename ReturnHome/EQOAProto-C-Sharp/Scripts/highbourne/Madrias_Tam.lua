function event_say()
diagOptions = {}
    npcDialogue = "The treacherous dark master Kaah wields powers you cannot possibly comprehend."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end