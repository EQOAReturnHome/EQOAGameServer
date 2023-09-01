function  event_say(choice)
diagOptions = {}
    npcDialogue = "I can bind you here if you wish, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end