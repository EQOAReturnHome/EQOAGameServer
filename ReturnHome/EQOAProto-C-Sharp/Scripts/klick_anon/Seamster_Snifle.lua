function  event_say(choice)
diagOptions = {}
    npcDialogue = "Would you like to learn the ways of tailoring?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end