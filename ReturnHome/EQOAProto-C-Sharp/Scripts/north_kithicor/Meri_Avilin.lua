function event_say()
diagOptions = {}
    npcDialogue = "It really is a peaceful place to live. Part of why I come out here to practice my druidic abilities. I don't want to disturb anyone."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end