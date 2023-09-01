function  event_say(choice)
diagOptions = {}
    npcDialogue = "We've had to actually crack down on fish thieves, if you can believe it. What a waste of my skills."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end