function  event_say(choice)
diagOptions = {}
    npcDialogue = "How are you feeling, playerName? Well, you look fine to me."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end