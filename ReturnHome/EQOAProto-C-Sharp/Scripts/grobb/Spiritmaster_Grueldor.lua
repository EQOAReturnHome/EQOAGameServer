function  event_say(choice)
diagOptions = {}
    npcDialogue = "Greetings young greenskin. What brings you to Grueldor?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end