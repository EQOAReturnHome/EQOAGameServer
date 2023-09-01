function  event_say(choice)
diagOptions = {}
    npcDialogue = "I could use some help gathering ant chitins, would you care to assist playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end