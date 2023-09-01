function  event_say(choice)
diagOptions = {}
    npcDialogue = "I hope you find a moment of respite here in Bobble, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end