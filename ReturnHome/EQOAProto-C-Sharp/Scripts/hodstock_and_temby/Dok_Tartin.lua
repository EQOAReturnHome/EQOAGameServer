function  event_say(choice)
diagOptions = {}
    npcDialogue = "How about a mug of ale?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end