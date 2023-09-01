function  event_say(choice)
diagOptions = {}
    npcDialogue = "playerName, please do not disturb their meeting unless it is urgent."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end