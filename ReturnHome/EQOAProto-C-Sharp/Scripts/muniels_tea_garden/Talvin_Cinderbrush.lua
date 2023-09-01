function event_say()
diagOptions = {}
    npcDialogue = "I have been searching for answers for longer than I care to remember and yet I'm no closer to my goal."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end