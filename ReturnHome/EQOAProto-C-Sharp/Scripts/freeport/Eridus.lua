function  event_say(choice)
diagOptions = {}
    npcDialogue = "Did you see that lady over there?  She is a beautiful creature, no question about it."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end