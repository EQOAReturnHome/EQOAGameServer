function  event_say(choice)
diagOptions = {}
    npcDialogue = "Hey, you lookin to buy a...err...nevermind."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end