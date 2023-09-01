function  event_say(choice)
diagOptions = {}
    npcDialogue = "Where will you be heading off to?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end