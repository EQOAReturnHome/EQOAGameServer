function  event_say(choice)
diagOptions = {}
    npcDialogue = "Freeport is an interesting city, an interesting city indeed.  I'm sorry, I was lost in thought, was there something you needed of me?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end