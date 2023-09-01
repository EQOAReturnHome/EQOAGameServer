function event_say()
diagOptions = {}
    npcDialogue = "I am Ree's personal bodyguard. Every breath you take, move you make, I'll be watching you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end