function  event_say(choice)
diagOptions = {}
    npcDialogue = "Freeport was festering with corruption, so I fled the city and came here. I don't know if it's any better, but at least it's pretty quiet."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end