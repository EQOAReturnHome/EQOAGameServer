function  event_say(choice)
diagOptions = {}
    npcDialogue = "Hail playerName!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end