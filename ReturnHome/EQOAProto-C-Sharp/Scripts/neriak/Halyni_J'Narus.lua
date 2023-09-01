function  event_say(choice)
diagOptions = {}
    npcDialogue = "I would sooner be stripped of my house and title than every be seen speaking with you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end