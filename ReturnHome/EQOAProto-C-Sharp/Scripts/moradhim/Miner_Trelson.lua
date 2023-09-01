function  event_say(choice)
diagOptions = {}
    npcDialogue = "Surely, ye've someone else to bother."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end