function  event_say(choice)
diagOptions = {}
    npcDialogue = "You tread on grounds you don't belong. I advise you to leave, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end