function  event_say(choice)
diagOptions = {}
    npcDialogue = "Pardon me, playerName, I have a keep to guard."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end