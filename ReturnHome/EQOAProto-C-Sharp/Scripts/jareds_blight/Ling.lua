function  event_say(choice)
diagOptions = {}
    npcDialogue = "Apprentice Buema owes me quite the sum of tunar. I'm not letting him out of my sight until he pays up."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end