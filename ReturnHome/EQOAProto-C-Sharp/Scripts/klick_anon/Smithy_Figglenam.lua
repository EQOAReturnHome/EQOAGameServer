function  event_say(choice)
diagOptions = {}
    npcDialogue = "Care to learn the ways of armorcrafting?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end