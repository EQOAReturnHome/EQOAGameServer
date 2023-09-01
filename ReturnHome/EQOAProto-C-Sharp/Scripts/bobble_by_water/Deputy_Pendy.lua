function  event_say(choice)
diagOptions = {}
    npcDialogue = "We lost some supplies in a battle with the dark elves on the river! We needed those supplies to help Rivervale. Perhaps it isn't too late to catch them..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end