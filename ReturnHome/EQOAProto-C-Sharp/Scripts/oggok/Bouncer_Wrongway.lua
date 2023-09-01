function  event_say(choice)
diagOptions = {}
    npcDialogue = "I can't read what it says. I never learned to read. Will they still let me fight?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end