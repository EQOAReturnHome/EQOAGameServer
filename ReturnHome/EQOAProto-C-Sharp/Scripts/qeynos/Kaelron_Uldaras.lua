function  event_say(choice)
diagOptions = {}
    npcDialogue = "The Shadowhand Bandits in the Qeynos farmlands have been pillaging the poor farms, and if we don't do anything about it our citizens will starve!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end