function  event_say(choice)
diagOptions = {}
    npcDialogue = "The King is having an important meeting with the ambassador from Highbourne at the moment. Perhaps you should visit another day..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end