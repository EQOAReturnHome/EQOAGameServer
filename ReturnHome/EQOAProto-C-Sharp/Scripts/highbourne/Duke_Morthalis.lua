function event_say()
diagOptions = {}
    npcDialogue = "You are wasting my time, whelp. Return to me when you can be of service to the dark arts."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end