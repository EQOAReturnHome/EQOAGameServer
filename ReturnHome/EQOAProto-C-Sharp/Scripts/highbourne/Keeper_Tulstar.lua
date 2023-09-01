function event_say()
diagOptions = {}
    npcDialogue = "We all reach plateaus at various points on our journey to enlightenment. I've been stuck on the Super Nova spell for weeks. I always end up burning my hands."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end