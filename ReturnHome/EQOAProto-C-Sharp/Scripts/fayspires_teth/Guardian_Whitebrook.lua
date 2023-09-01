function event_say()
diagOptions = {}
    npcDialogue = "I will not rest until every last elf has been delivered to our home across the sea."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end