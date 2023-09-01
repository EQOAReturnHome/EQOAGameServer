function  event_say(choice)
diagOptions = {}
    npcDialogue = "Have you something to report to me, citizen?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end