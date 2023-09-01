function  event_say(choice)
diagOptions = {}
    npcDialogue = "Look, I'm busy here. Doing what? None of your business, stiff."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end