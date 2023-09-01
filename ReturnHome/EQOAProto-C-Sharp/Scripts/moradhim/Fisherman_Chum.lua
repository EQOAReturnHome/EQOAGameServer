function  event_say(choice)
diagOptions = {}
    npcDialogue = "Lookin' ta learn the ways of fishing, now? Let me show ye a thing er two."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end