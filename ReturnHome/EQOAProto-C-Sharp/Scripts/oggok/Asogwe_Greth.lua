function  event_say(choice)
diagOptions = {}
    npcDialogue = "You must do everything I say, playerName. Then the eye won't kill you. You do everything I say and serve the eye, than you live."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end