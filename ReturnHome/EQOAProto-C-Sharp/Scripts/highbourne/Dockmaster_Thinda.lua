function event_say()
diagOptions = {}
    npcDialogue = "I can send you to Arcadin with a snap of my fingers thanks to my valiant colleagues who braved the voyage to Odus."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end