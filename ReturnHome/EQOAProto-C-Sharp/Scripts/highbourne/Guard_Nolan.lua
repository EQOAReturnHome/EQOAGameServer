function event_say()
diagOptions = {}
    npcDialogue = "To the southwest you'll find the Tranquil Gardens, cleric, and paladin guilds. The Coach of Highbourne is just north of us."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end