function  event_say(choice)
diagOptions = {}
    npcDialogue = "I know the ways to forge metal into armor. Wouldya care learn this feat?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end