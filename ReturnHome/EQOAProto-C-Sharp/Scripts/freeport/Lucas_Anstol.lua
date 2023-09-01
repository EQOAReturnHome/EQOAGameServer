function  event_say(choice)
diagOptions = {}
    npcDialogue = "What can I do you for??"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end