function  event_say(choice)
diagOptions = {}
    npcDialogue = "I am here to guide the young clerics as they struggle to find their paths in this deadly world."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end