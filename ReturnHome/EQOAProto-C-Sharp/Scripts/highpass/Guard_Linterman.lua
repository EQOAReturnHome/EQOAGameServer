function  event_say(choice)
diagOptions = {}
    npcDialogue = "Mind yourself, traveler."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end