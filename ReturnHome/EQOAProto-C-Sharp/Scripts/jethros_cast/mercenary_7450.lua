function  event_say(choice)
diagOptions = {}
    npcDialogue = "I need to find a new employer, Jethro's Cast is not exactly where I wanted to end up as a mercenary."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end