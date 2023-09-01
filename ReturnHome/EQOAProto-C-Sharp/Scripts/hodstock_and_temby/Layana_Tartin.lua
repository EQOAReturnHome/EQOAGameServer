function  event_say(choice)
diagOptions = {}
    npcDialogue = "Doesn't some fresh cooked fish sound delicious?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end