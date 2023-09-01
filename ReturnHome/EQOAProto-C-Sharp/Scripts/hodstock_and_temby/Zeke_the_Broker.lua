function  event_say(choice)
diagOptions = {}
    npcDialogue = "If you can keep it quiet from the deputy, I might be able to loan you a few tunar."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end