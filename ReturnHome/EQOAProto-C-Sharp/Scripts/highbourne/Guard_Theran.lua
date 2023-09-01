function event_say()
diagOptions = {}
    npcDialogue = "South of us is the Bank of Highbourne."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end