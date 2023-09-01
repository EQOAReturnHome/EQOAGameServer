function event_say()
diagOptions = {}
    npcDialogue = "Chiasta is surrounded on all sides. It is safe to let ones guard down and focus on the peacefulness of world we are a part of."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end