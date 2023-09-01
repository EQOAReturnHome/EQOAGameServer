function  event_say(choice)
diagOptions = {}
    npcDialogue = "Top officials of Moradhim meet here, and we're makin' sure of their safety. Mind yerself now, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end