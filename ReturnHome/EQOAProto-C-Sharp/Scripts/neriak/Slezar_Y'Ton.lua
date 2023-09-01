function  event_say(choice)
diagOptions = {}
    npcDialogue = "You would be wise to leave this place, cretin."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end