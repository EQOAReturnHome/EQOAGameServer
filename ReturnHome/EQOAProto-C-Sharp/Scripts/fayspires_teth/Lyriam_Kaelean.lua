function event_say()
diagOptions = {}
    npcDialogue = "Many orcs are mindless, vicious beasts. However some orcs have managed to conjure some rudimentary magics. I say rudimentary, but they can still be quite dangerous. playerName, if you see an orc wizard, perhaps a prompt escape is in order."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end