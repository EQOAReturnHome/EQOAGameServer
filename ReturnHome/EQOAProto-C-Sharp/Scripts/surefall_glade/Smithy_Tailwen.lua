function  event_say(choice)
diagOptions = {}
    npcDialogue = "Perhaps I can interest you in the ways of armorcrafting?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end