function  event_say(choice)
diagOptions = {}
    npcDialogue = "Have anything interesting to sell?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end