function  event_say(choice)
diagOptions = {}
    npcDialogue = "Pardon me, but I am extremely busy and have no use for you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end