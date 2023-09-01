function  event_say(choice)
diagOptions = {}
    npcDialogue = "May I bind your spirit here, playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end