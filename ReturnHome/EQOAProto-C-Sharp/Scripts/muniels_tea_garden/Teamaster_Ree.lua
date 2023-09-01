function event_say()
diagOptions = {}
    npcDialogue = "I feel as though my tea has been suffering as of late. The orcs are up to something and it has been a bit distracting."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end