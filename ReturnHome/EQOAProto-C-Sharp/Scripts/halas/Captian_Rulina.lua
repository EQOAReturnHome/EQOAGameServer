function  event_say(choice)
diagOptions = {}
    npcDialogue = "Out of my sight, citizen. I have nothing for you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end