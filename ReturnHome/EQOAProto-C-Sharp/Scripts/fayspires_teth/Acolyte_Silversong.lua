function event_say()
diagOptions = {}
    npcDialogue = "One of a clerics special powers is to smite the undead. The light of the Goddess can be channeled to cleanse this land of the unholy, and thus we will make it pure again."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end