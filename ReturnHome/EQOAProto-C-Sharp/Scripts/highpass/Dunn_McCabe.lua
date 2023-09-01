function  event_say(choice)
diagOptions = {}
    npcDialogue = "Welcome to Highpass, Traveler!!  I hope you enjoy your stay while in this keep!!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end