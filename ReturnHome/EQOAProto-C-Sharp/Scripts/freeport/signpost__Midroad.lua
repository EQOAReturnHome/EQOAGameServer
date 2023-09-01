function  event_say(choice)
diagOptions = {}
    npcDialogue = "Continue North to find the Bazaar. There you will find all of our merchants of trade, as well as our bankers!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end