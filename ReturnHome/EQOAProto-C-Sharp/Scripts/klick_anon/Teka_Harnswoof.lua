function  event_say(choice)
diagOptions = {}
    npcDialogue = "Brell's blessings upon you, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end