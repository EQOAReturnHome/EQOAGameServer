function  event_say(choice)
diagOptions = {}
    npcDialogue = "That Mazanda has got herself wrapped around someone new again. Poor ol' Zenilor will never stop chasing her."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end