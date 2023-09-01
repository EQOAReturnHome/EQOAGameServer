function event_say()
diagOptions = {}
    npcDialogue = "If you aren't a carpenter or in some way helping to build ships, you don't belong here. Move along."
SendDialogue(mySession, npcDialogue, diagOptions)
end