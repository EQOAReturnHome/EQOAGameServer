function event_say()
diagOptions = {}
    npcDialogue = "Gotta be quick when you go for the eggs. Usually takes a distraction of sorts, ferrets work well."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end