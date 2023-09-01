function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm developing a way to teleport to Freeport. Shall I try to send you there playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end