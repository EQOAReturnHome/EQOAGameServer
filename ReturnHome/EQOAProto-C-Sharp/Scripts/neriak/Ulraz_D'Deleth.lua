function  event_say(choice)
diagOptions = {}
    npcDialogue = "You should leave. We don't like outsiders in our home."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end