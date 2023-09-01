function event_say()
diagOptions = {}
    npcDialogue = "Wherever you go playerName, Tunare's light will follow."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end