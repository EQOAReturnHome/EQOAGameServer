function  event_say(choice)
diagOptions = {}
    npcDialogue = "Good tidings, citizen."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end