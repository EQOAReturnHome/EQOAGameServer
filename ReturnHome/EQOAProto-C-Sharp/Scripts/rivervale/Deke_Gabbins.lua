function  event_say(choice)
diagOptions = {}
    npcDialogue = "Karana's blessings t'ye, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end