function  event_say(choice)
diagOptions = {}
    npcDialogue = "You are welcome to read one of the many books, most of which are about our history. Just be sure to put it back, ok playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end