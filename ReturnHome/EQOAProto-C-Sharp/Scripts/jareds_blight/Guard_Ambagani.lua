function  event_say(choice)
diagOptions = {}
    npcDialogue = "Watch out for bandits. The land just on the other side of this river is crawling with them."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end