function  event_say(choice)
diagOptions = {}
    npcDialogue = "Grandma came down with an unusual illness, and couldn't be around the other villagers in Hagley. We couldn't let her live alone so we all moved here together."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end