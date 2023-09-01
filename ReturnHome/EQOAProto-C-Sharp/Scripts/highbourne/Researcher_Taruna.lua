function event_say()
diagOptions = {}
    npcDialogue = "What day is it? I don't think I've left the Library Archives in over a week. Perhaps I should go shower."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end