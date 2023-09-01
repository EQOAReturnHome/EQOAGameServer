function event_say()
diagOptions = {}
    npcDialogue = "I encourage you to bind your spirit here. The rest of the city doesn't take to kindly to those that practice the darker arts."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end