function event_say()
diagOptions = {}
    npcDialogue = "I am a fully trained magician, so naturally the Senate decided to station me here. Honestly, I think the other senators hoped I could quell some of the rogue summoning acts throughout the city. Did you hear about the Djinn Vizier damaging the shipyards last week?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end