function  event_say(choice)
diagOptions = {}
    npcDialogue = "This looks to be a very interesting book. Perhaps someday you'll have the time to read it."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end