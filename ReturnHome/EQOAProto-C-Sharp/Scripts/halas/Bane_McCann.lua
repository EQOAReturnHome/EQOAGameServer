function  event_say(choice)
diagOptions = {}
    npcDialogue = "Not lost are you? To the northwest, you'll find Coachman Dudley. Head south, through the tunnel, and you'll find your way out of the city."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end