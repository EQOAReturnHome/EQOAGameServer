function  event_say(choice)
diagOptions = {}
    npcDialogue = "There's plenty of vegetables to eat around here but not nearly enough meat."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end