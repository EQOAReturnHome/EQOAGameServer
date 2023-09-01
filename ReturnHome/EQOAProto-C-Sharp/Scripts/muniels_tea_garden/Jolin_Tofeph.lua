function event_say()
diagOptions = {}
    npcDialogue = "Belran Nightrift is a slimy two timing sneak thief. Glad I made off with that tome when I did."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end