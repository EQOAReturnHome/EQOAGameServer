function  event_say(choice)
diagOptions = {}
    npcDialogue = "My blade hopes you have something worthwhile to tell me."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end