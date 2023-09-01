function  event_say(choice)
diagOptions = {}
    npcDialogue = "playerName, you probably want a souldbind. I do it, ok?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end