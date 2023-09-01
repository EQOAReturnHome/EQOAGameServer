function event_say()
diagOptions = {}
    npcDialogue = "We must keep the forest clear of pests, but try not to clear too many of them at once. Each critter brings its own balance to the wildlife."
SendDialogue(mySession, npcDialogue, diagOptions)
end