function event_say()
diagOptions = {}
    npcDialogue = "I have a few suspects in mind for the murder in the Southwest Tower that I'm investigating. Keep your stay short or I might add a name to my list."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end