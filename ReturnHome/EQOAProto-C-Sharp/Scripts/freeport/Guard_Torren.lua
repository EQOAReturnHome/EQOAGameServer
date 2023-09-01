function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm on duty. Shove off."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end