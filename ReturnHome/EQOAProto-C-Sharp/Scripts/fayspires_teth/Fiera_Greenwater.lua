function event_say()
diagOptions = {}
    npcDialogue = "Winter's Deep Lake is a perfect source for our tonics and healing waters. In fact, something mysterious about the lake has actually improved the results of our recipes, but I haven't yet figured out why."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end