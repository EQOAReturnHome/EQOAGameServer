function  event_say(choice)
diagOptions = {}
    npcDialogue = "Sorry playerName, you'll have to come back some other time. I am swarmed with duties I must attend to."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end