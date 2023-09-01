function  event_say(choice)
diagOptions = {}
    npcDialogue = "Greetings, playerName. Shall I bind your spirit here?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end