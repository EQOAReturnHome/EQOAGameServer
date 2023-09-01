function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm a little busy at the moment.  Could you bother someone else?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end