function event_say()
diagOptions = {}
    npcDialogue = "The Tranquil Gardens are to the south and the Grand Amphitheater is a stones throw northwest of us."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end