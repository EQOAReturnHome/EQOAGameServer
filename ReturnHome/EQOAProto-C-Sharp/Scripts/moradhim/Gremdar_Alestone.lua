function  event_say(choice)
diagOptions = {}
    npcDialogue = "Come and have a seat playerName, enjoy some dwarven hospitality and I'll regale you with a few tales of my younger years..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end