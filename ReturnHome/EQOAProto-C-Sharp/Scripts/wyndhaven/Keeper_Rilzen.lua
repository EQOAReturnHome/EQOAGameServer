function  event_say(choice)
diagOptions = {}
    npcDialogue = "The war between good and evil continues. Which side are you on?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end