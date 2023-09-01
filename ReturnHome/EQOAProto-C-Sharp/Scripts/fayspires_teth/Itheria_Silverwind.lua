function event_say()
diagOptions = {}
    npcDialogue = "We paladins are sworn to protect all of Tunare's children. By this we let no cruelty or malice go unchecked, nor let any lair of darkness fester. May the goddess light your path, playerName."
SendDialogue(mySession, npcDialogue, diagOptions)
end