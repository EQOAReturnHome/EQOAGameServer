function event_say()
diagOptions = {}
    npcDialogue = "I wonder how our fellow elves are doing over at Tethelin across the lake. Perhaps I should go and visit them."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end