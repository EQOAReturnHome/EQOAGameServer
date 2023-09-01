function event_say()
diagOptions = {}
    npcDialogue = "If you ever need any of my cleansing solution, remember it is 600 tunar, all up front."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end