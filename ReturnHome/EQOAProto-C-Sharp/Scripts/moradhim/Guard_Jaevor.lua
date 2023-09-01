function  event_say(choice)
diagOptions = {}
    npcDialogue = "Don't stand too close to the sparring ring, these tend to be dangerous, especially for bystanders."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end