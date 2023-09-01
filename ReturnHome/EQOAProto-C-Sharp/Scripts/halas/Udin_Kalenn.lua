function  event_say(choice)
diagOptions = {}
    npcDialogue = "I've traveled this world from top to bottom, east to west, even above and below. Nothing will get me to leave my home again. Not after what I saw...an enormous spider in the Eternal Desert, Terrorantula."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end