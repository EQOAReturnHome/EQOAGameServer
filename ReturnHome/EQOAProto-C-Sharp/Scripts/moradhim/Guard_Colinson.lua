function  event_say(choice)
diagOptions = {}
    npcDialogue = "Outta ma way, playerName!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end