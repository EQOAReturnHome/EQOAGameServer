function  event_say(choice)
diagOptions = {}
    npcDialogue = "I detest the freezeblood with every fiber of my being. Guntak will pay for crimes commited against the Northmen."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end