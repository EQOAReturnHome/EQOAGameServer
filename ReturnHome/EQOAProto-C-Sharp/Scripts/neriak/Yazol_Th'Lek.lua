function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm sure you have a lot to say but I don't have the time to hear it. Go bother someone else."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end