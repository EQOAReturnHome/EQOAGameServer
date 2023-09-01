function  event_say(choice)
diagOptions = {}
    npcDialogue = "Perhaps I could teach you a thing or two about weaponcrafting?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end