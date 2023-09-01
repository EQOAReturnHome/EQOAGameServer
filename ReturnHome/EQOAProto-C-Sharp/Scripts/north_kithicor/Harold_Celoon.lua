function event_say()
diagOptions = {}
    npcDialogue = "Our discussion is none of your concern, adventurer. Be on your way."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end