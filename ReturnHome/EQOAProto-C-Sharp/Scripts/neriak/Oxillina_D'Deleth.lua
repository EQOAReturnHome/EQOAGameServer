function  event_say(choice)
diagOptions = {}
    npcDialogue = "You have no business here. Get out!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end