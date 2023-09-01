function  event_say(choice)
diagOptions = {}
    npcDialogue = "You should spend your time reading, and gaining knowledge, not bothering me."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end