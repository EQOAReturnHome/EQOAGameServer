function event_say()
diagOptions = {}
    npcDialogue = "No time to spare. Snafitzer's requests require me to work all sorts of crazy hours just to keep up."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end