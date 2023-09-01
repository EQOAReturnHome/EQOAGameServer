function  event_say(choice)
diagOptions = {}
    npcDialogue = "Where are you headin'?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end